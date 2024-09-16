
using System.Collections.Generic;
using UnityEngine;

public class InGameRepository
{
    //プレイヤーのデータ
    private PlayerData _playerData;
    public PlayerData PlayerData => _playerData;
    //プレイヤーが所持しているスキルデータ
    private List<SkillData> _skillDatas;
    public List<SkillData> SkillDatas => _skillDatas;
    //スキルゲット画面に出すスキルデータ
    public List<SkillData> SkillViewData;
    

    //マスターデータ
    private IReadOnlyList<MstSkillData> _mstSkillDatas;
    public IReadOnlyList<MstSkillData> MstSkillDatas => _mstSkillDatas;

    //リソースデータ
    private Sprite[] _iconSprites;
    public IReadOnlyList<Sprite> IconSprites => _iconSprites;

    public void Initialize()
    {
        //マスターデータの準備、ロード。
        //_iconSprites = Resources.LoadAll<Sprite>("SkillIcon");

        //リソースデータの準備、ロード。
        _mstSkillDatas = Resources.Load<MstSkillDataScriptableObject>("ScriptableObject/MstSkillDataScriptableObject").Data;

        //データの準備。
        _playerData = new PlayerData(1,0,3,0f,100,100,InGameConst.State.Play);
        _skillDatas = new List<SkillData>(InGameConst.SkillInventoryCount);
        SkillViewData = new List<SkillData>(InGameConst.SkillSelectCount);
        //デバグ用。
        //_skillDatas.Add(new SkillData(0,0,null,"ブリンク","短い距離を素早く移動する。",1,3,2f));
        //_skillDatas.Add(new SkillData(1,1,null,"フォールキャロット","巨大なニンジンを召喚して攻撃する。",1,1,5f));
        //_skillDatas.Add(new SkillData(MstSkillDatas[0]));
        //_skillDatas.Add(new SkillData(MstSkillDatas[1]));
    }
}