using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class BaseSkillExecuter : ISkillExecuter
{
    //スキルタイプ
    private InGameConst.SkillType _skillType = InGameConst.SkillType.DEFAULT;
    public InGameConst.SkillType SkillType => _skillType;
    //攻撃倍率
    private float _skillValue;
    public float SkillValue => _skillValue;

    //コンストラクタ。
    private BaseSkillExecuter(){}
    protected BaseSkillExecuter(InGameConst.SkillType skillType, float skillValue)
    {
        _skillType = skillType;
        _skillValue = skillValue;
    }
    //実行可能かどうか
    public virtual bool CanDoSkill()
    {
        return true;
    }
    //実行
    public virtual async UniTask DoSkillAsync(CancellationToken token, UniTask task)
    {
        token.ThrowIfCancellationRequested();
        await task;
    }
}
