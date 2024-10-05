using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverInterfaceController : MonoBehaviour
{
    private GameObject _wrapper = default;
    [SerializeField]
    private GameObject _newRecordObject = default;
    [SerializeField]
    private TextMeshProUGUI _killCountText = default;
    [SerializeField]
    private Transform _skillAreaParent = default;
    [SerializeField]
    private GameObject _skillAreaItemPrefab = default;

    // Start is called before the first frame update
    void Start()
    {
        _wrapper = transform.GetChild(0).gameObject;

        //データ更新時の処理を登録
        InGameModel.Instance.OnStateChanged += OnStateChanged;
        
    }
    private void OnStateChanged(InGameConst.State oldState, InGameConst.State newState)
    {
        if(newState == InGameConst.State.GameOver)
        {
            var playerData = InGameModel.Instance.GetPlayerData();
            _killCountText.SetText("{0}体", playerData.KillCount);
            var skillData = InGameModel.Instance.GetSkillDatas();
            var position = new Vector2(InGameConst.GameOverSkillLeftX, 0f);
            foreach(var skill in skillData)
            {
                var component = Instantiate(_skillAreaItemPrefab, _skillAreaParent).GetComponent<GameOverSkillAreaItemComponent>();
                component.Initialize(position, skill.Icon, skill.Level);
                position += new Vector2(InGameConst.GameOverSkillOffsetX, 0f);
            }
            _wrapper.SetActive(true);
        }
        if(oldState == InGameConst.State.GameOver)
        {
            _wrapper.SetActive(false);
        }
    }
    //デバグ用の表示操作。
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            InGameModel.Instance.Damage(99999);
        }
    }
}
