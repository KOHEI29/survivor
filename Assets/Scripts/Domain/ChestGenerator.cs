using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//宝箱生成器。5レベル毎に1個出す。既に出てるなら取った次のレベルアップで出す。
public class ChestGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject _chestPrefab = default;

    private GameObject _createdChest = default;

    private static readonly Vector3 ChestPopPosition = new Vector3(0f,3.96f,0f);
    private const int ChectLevelOffset = 5;
    private int _latestGenerateLevel = 0;

    void Start()
    {
        GenerateChest();
        InGameModel.Instance.OnLevelChanged += onLevelChanged;
    }
    private void onLevelChanged(int value)
    {
        if(value - _latestGenerateLevel >= ChectLevelOffset)
        {
            if(_createdChest == null)
            {
                _latestGenerateLevel += ChectLevelOffset;
                GenerateChest();
            }
        }
    }
    private void GenerateChest()
    {
        _createdChest = Instantiate(_chestPrefab);
        _createdChest.GetComponent<ChestController>().Initialize(ChestPopPosition);
    }
}
