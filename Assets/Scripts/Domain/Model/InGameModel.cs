using System.Collections.Generic;
using UnityEngine.Events;

//処理（データ周りのビジネスロジック）〜データの操作とデータ公開をするクラス。
public class InGameModel
{
    private static InGameModel _instance = default;
    public static InGameModel Instance
    {
        get
        {
            _instance ??= new InGameModel();
            return _instance;
        }
    }
    private InGameRepository _repository = new InGameRepository();

    public event UnityAction<int> OnLevelChanged = default;
    public event UnityAction<int, int> OnExpChanged = default;
    public event UnityAction<float> OnTimeChanged = default;
    public event UnityAction<int, int> OnHpChanged = default;
    public event UnityAction<InGameConst.State, InGameConst.State> OnStateChanged = default;
    public event UnityAction<int, ISkillData> OnSkillAdded = default;
    public event UnityAction<int, float, float> OnSkillCoolTimeChanged = default;
    public event UnityAction<int, int> OnSkillStackChanged = default;

    public InGameModel()
    {
        _repository.Initialize();
    }

    public IPlayerData GetPlayerData()
    {
        return _repository.PlayerData;
    }
    public IReadOnlyList<ISkillData> GetSkillDatas()
    {
        return _repository.SkillDatas;
    }
    //マスターIDでインデックスを返す。無いなら-1を返す。
    public int GetSkillIndexByMstId(int id)
    {
        for(int i = 0, c = _repository.SkillDatas.Count; i < c; i++)
            if(_repository.SkillDatas[i].MstId == id) return i;        
        return -1;
    }
    public IReadOnlyList<ISkillData> GetSkillViewData()
    {
        return _repository.SkillViewData;
    }
    public bool CanUseSkill(int index)
    {
        if(_repository.SkillDatas.Count <= index) return false;
        return _repository.SkillDatas[index].StackCurrent > 0;
    }

    //Stateをセット。
    public void SetState(InGameConst.State state)
    {
        var old = _repository.PlayerData.CurrentState;
        _repository.PlayerData.CurrentState = state;
        OnStateChanged?.Invoke(old, state);
    }
    //経験値を増やす。
    public void AddExp(int value)
    {
        _repository.PlayerData.Exp += value;
        if(_repository.PlayerData.Exp >= _repository.PlayerData.ReqExp)
        {
            while(_repository.PlayerData.Exp >= _repository.PlayerData.ReqExp)
            {
                _repository.PlayerData.Level++;
                _repository.PlayerData.Exp -= _repository.PlayerData.ReqExp;
            }
            OnLevelChanged?.Invoke(_repository.PlayerData.Level);
        }
        OnExpChanged?.Invoke(_repository.PlayerData.Exp, _repository.PlayerData.ReqExp);
    }
    //経過時間を増やす。
    public void AddTime(float value)
    {
        _repository.PlayerData.Time += value;
        OnTimeChanged?.Invoke(_repository.PlayerData.Time);
        for(int i = 0, c = _repository.SkillDatas.Count; i < c; i++)
        {
            var skill = _repository.SkillDatas[i];
            if(skill.CoolTimeCurrent != 0f)
            {
                skill.CoolTimeCurrent -= value;
                if(skill.CoolTimeCurrent <= 0f)
                {
                    //スタックを増やす
                    OnSkillStackChanged?.Invoke(i, ++skill.StackCurrent);
                    if(skill.StackCurrent < skill.StackMax)
                    {
                        skill.CoolTimeCurrent += skill.CoolTimeMax;
                    }
                    else
                        skill.CoolTimeCurrent = 0f;
                }
                OnSkillCoolTimeChanged?.Invoke(i, skill.CoolTimeCurrent, skill.CoolTimeMax);
            }
        }
    }
    //プレイヤーにダメージを与える。
    public void Damage(int value)
    {
        _repository.PlayerData.HpCurrent -= value;
        if(_repository.PlayerData.HpCurrent <= 0)
        {
            _repository.PlayerData.HpCurrent = 0;
            //死んだ
            SetState(InGameConst.State.GameOver);
        }
        OnHpChanged?.Invoke(_repository.PlayerData.HpCurrent, _repository.PlayerData.HpMax);
    }
    //敵を撃破
    public void DealEnemy()
    {
        _repository.PlayerData.KillCount++;
    }
    //デバグ用スキルセット。
    //private void SetSkill(int skillId, int value)
    //{
    //    for(int i = 0, c = _repository.SkillDatas.Count; i < c; i++)
    //    {
    //        if(_repository.SkillDatas[i].MstId == skillId)
    //        {
    //            _repository.SkillDatas[i].Level = value;
    //            return;
    //        }
    //    }
    //    for(int i = 0, c = _repository.MstSkillDatas.Count; i < c; i++)
    //    {
    //        if(_repository.MstSkillDatas[i].Id == skillId)
    //        {
    //            var mst = _repository.MstSkillDatas[i];
    //            _repository.SkillDatas.Add(new SkillData(mst.Id,mst.ExecuterId,mst.Icon,mst.Name,mst.Description,value,mst.StackMax,mst.CoolTime));
    //            OnSkillAdded?.Invoke(i, _repository.SkillDatas[^1]);
    //            return;
    //        }
    //    }
    //}
    //スキルのスタックを減らす。
    public int ReduceSkillStack(int index)
    {
        var skill = _repository.SkillDatas[index];
        if(skill.CoolTimeCurrent == 0f)
            skill.CoolTimeCurrent = skill.CoolTimeMax;
        OnSkillStackChanged?.Invoke(index, --skill.StackCurrent);
        return skill.StackCurrent;
    }
    //スキル入手画面を表示する。
    public void DisplaySelectSkillView()
    {
        //抽選
        LotterySkillSelect();
        //Stateを遷移する。
        SetState(InGameConst.State.Skill);
    }
    //スキル入手画面でスキルが選ばれた。
    public void SelectedSkill(int index)
    {
        var id = GetSkillIndexByMstId(_repository.SkillViewData[index].MstId);
        //持っているならレベルを上げ、持っていないなら追加
        if(id == -1)
        {
            _repository.SkillDatas.Add(_repository.SkillViewData[index]);
            OnSkillAdded?.Invoke(_repository.SkillDatas.Count-1, _repository.SkillDatas[^1]);
        }else
            _repository.SkillDatas[id].Level = _repository.SkillViewData[index].Level;

        //Stateを遷移する。
        SetState(InGameConst.State.Play);
    }
    //スキル入手画面で出るスキルの抽選をする。
    private void LotterySkillSelect()
    {
        _repository.SkillViewData.Clear();
        //デバグ用にブリンクとフォールキャロットの次のレベルを出す。
        var idArray = new int[]{0, 1};
        for(int i = 0; i < idArray.Length; i++)
        {
            var index = GetSkillIndexByMstId(idArray[i]);
            //未所持ならLv1で作成。
            if(index == -1)
                _repository.SkillViewData.Add(new SkillData(_repository.MstSkillDatas[idArray[i]], 1));
            else
                _repository.SkillViewData.Add(new SkillData(_repository.MstSkillDatas[idArray[i]], _repository.SkillDatas[index].Level + 1));
        }
    }

}

