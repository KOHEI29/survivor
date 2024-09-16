using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameInterfaceController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _level = default;
    [SerializeField]
    private TextMeshProUGUI _exp = default;
    [SerializeField]
    private TextMeshProUGUI _time = default;
    [SerializeField]
    private Image _expFill = default;
    [SerializeField]
    private Image _hpFill = default;
    [SerializeField]
    private Transform _skillArea = default;
    [SerializeField]
    private GameObject _skillAreaItemComponent = default;
    private SkillAreaItemComponent[] _skillAreaItemComponents = new SkillAreaItemComponent[InGameConst.SkillInventoryCount];

    void Start()
    {
        //データ更新時の処理を登録
        InGameModel.Instance.OnLevelChanged += DisplayLevel;
        InGameModel.Instance.OnExpChanged += DisplayExp;
        InGameModel.Instance.OnTimeChanged += DisplayTime;
        InGameModel.Instance.OnHpChanged += DisplayHp;
        InGameModel.Instance.OnSkillAdded += DisplaySkill;
        InGameModel.Instance.OnSkillCoolTimeChanged += DisplaySkillCoolTime;
        InGameModel.Instance.OnSkillStackChanged += DisplaySkillStack;

        //初期化
        var player = InGameModel.Instance.GetPlayerData();
        DisplayLevel(player.Level);
        DisplayExp(player.Exp, player.ReqExp);
        DisplayTime(player.Time);
        DisplayHp(player.HpCurrent, player.HpMax);
        var skills = InGameModel.Instance.GetSkillDatas();
        for(int i = 0, c = skills.Count; i < c; i++)
            DisplaySkill(i, skills[i]);

        Debug.Log(_hpFill.ToString() ?? "");
    }
    private void DisplayLevel(int value)
    {
        _level.SetText("Lv.{0}", value);
    }
    private void DisplayExp(int exp, int req)
    {
        _exp.SetText("{0}/{1}", exp, req);
        _expFill.fillAmount = (float)exp / (float) req;
    }
    private void DisplayTime(float value)
    {
        var minute = Mathf.FloorToInt(value / 60);
        var second = Mathf.FloorToInt(value % 60);
        _time.SetText("{0:00}:{1:00}", minute, second);
    }
    private void DisplayHp(int current, int max)
    {
        _hpFill.fillAmount = (float)current / (float) max;
    }
    private void DisplaySkill(int index, ISkillData skill)
    {
        var go = Instantiate(_skillAreaItemComponent);
        go.transform.SetParent(_skillArea, false);
        var component = go.GetComponent<SkillAreaItemComponent>();
        component.Initialize(new Vector2(InGameConst.SkillInventoryLeftX + InGameConst.SkillInventoryOffsetX * index, 0f), skill.Icon, 0f, skill.StackMax);
        _skillAreaItemComponents[index] = component;
    }
    private void DisplaySkillCoolTime(int index, float current, float max)
    {
        _skillAreaItemComponents[index].SetFillAmount(current / max);
    }
    private void DisplaySkillStack(int index, int stack)
    {
        _skillAreaItemComponents[index].SetStackCount(stack);
    }
}