using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//敵の生成器
public class EnemyGenerator : MonoBehaviour
{
    [SerializeField]
    private Transform _player = default;
    [SerializeField]
    private GameObject _enemyPrefab = default;


    private const float EnemyCooltime = 2f;
    //private float _enemyTimeCount = EnemyCooltime;
    private float _latestGenerateTime = 0f;

    void Start()
    {
        InGameModel.Instance.OnTimeChanged += onTimeChanged;
    }
    private void onTimeChanged(float value)
    {
        if(value - _latestGenerateTime >= EnemyCooltime)
        {
            _latestGenerateTime += EnemyCooltime;
            Instantiate(_enemyPrefab).GetComponent<EnemyController>().Initialize(Vector3.zero, _player);
        }
    }
    //デバグ用生成処理
    //void Update()
    //{
    //    if(_enemyTimeCount > 0f)
    //    {
    //        _enemyTimeCount -= Time.deltaTime;
    //        if(_enemyTimeCount <= 0f)
    //        {
    //            _enemyTimeCount = EnemyCooltime;
    //            Instantiate(_enemyPrefab).GetComponent<EnemyController>().Initialize(Vector3.zero, _player);
    //        }
    //    }
    //}
}
