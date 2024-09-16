using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSkillInterfaceController : MonoBehaviour
{
    //[SerializeField]
    private GameObject _wrapper = default;
    [SerializeField]
    private SelectSkillItemComponent[] _item = default;

    // Start is called before the first frame update
    void Start()
    {
        _wrapper = transform.GetChild(0).gameObject;

        //データ更新時の処理を登録
        InGameModel.Instance.OnStateChanged += OnStateChanged;

        //ボタン押下時の処理を登録
        for(int i = 0; i < _item.Length; i++)
        {
            int ii = i;
            _item[i].SetAction(() => InGameModel.Instance.SelectedSkill(ii));
        }
    }
    private void OnStateChanged(InGameConst.State oldState, InGameConst.State newState)
    {
        if(newState == InGameConst.State.Skill)
        {
            var data = InGameModel.Instance.GetSkillViewData();
            for(int i = 0, c = _item.Length; i < c; i++)
            {
                if(data[i] == null)
                    _item[i].SetActive(false);
                else
                {
                    var index = InGameModel.Instance.GetSkillIndexByMstId(data[i].MstId);
                    if(index == -1)
                        _item[i].SetData(data[i].Name, data[i].Icon, data[i].Description, 0, data[i].Level, true);
                    else
                        _item[i].SetData(data[i].Name, data[i].Icon, data[i].Description, InGameModel.Instance.GetSkillDatas()[index].Level, data[i].Level, false);
                    _item[i].SetActive(true);
                }
            }
            _wrapper.SetActive(true);
        }
        if(oldState == InGameConst.State.Skill)
        {
            _wrapper.SetActive(false);
        }
    }
    //デバグ用の表示操作。
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            InGameModel.Instance.DisplaySelectSkillView();
        }
    }
}
