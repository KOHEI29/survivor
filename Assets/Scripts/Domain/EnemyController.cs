using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform _target = default;
    [SerializeField]
    private GameObject _exp = default;

    private int _hp = 3;
    private int _attack = 1;
    private float _moveSpeed = 1f;

    public void Initialize(Vector3 position, Transform target)
    {
        transform.position = position;
        _target = target;
    }
    void Update()
    {
        var direction = (_target.position - transform.position).normalized;
        transform.Translate(_moveSpeed * direction * Time.deltaTime );
    }
    private void OnTriggerEnter(Collider other)
    {
        //投射物との衝突時
        if(other.tag == "Attackable")
        {
            _hp -= other.GetComponentInParent<IAttackable>().Attack;
            if(_hp <= 0)
            {
                Destroy(gameObject);
                Instantiate(_exp, transform.position, Quaternion.identity);
            }
        }
        if(other.tag == "Player")
        {
            InGameModel.Instance.Damage(_attack);
        }
    }
}
