using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour, IAttackable
{
    private Vector3 _moveDirection = Vector3.down;
    private float _moveSpeed = 10f;
    private int _attack = 1;
    public int Attack => _attack;
    private const float LifeMax = 2f;
    private float _lifeCount = LifeMax;

    public void Initialize(Vector3 position, Vector3 direction)
    {
        _lifeCount = LifeMax;
        transform.position = position;
        //_moveDirection = direction;

        transform.rotation = Quaternion.FromToRotation(Vector3.down, direction);
    }

    void Update()
    {
        transform.Translate(_moveDirection * _moveSpeed * Time.deltaTime);
        _lifeCount -= Time.deltaTime;

        if(_lifeCount <= 0f)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //敵との衝突時
        if(other.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
