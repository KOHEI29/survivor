using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using DG.Tweening;

public class BlinkSkillExecuter : BaseSkillExecuter
{
    PlayerController _player;
    public BlinkSkillExecuter(PlayerController player)
        : base(InGameConst.SkillType.BLINK, 0f)
    {
        _player = player;
    }
    //実行可能かどうか
    public override bool CanDoSkill()
    {
        return _player.MoveDirection != Vector3.zero;
    }
    //実行
    public override async UniTask DoSkillAsync(CancellationToken token, UniTask task)
    {
        await base.DoSkillAsync(token, task);

        var target = _player.transform.position + _player.MoveDirection * 5f;

        await _player.transform.DOMove(target, 0.2f)
            .ToUniTask(TweenCancelBehaviour.Kill, token);
    }
}
