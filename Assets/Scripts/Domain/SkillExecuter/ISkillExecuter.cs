using Cysharp.Threading.Tasks;
using System.Threading;

//スキル実行に必要なインターフェース
public interface ISkillExecuter
{
    //スキルタイプ
    InGameConst.SkillType SkillType {get;}
    //攻撃倍率
    float SkillValue {get;}
    //実行可能かどうか
    bool CanDoSkill();
    //実行
    UniTask DoSkillAsync(CancellationToken token, UniTask task);
}