using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using UnityEditor;

public class FallCarrotSkillExecuter : BaseSkillExecuter
{
    PlayerController _player;
    GameObject _prefab;
    public FallCarrotSkillExecuter(PlayerController player)
        : base(InGameConst.SkillType.MELEE, 0f)
    {
        _player = player;
        //Prefabのロード。
        //NOTE:PrefabManagerなどを用意しておき、ゲーム開始前に必要分ロードしておくのが望ましい。
        _prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/FallCarrot.prefab");
    }
    //実行可能かどうか
    public override bool CanDoSkill()
    {
        return true;
    }
    //実行
    public override async UniTask DoSkillAsync(CancellationToken token, UniTask task)
    {
        var controller = MonoBehaviour.Instantiate(_prefab).GetComponent<FallCarrotController>();
        controller.Initialize(_player.transform.position + _player.HeadingDirection * 2f);

        controller.AnimationCompleteCallback = () => MonoBehaviour.Destroy(controller.gameObject);
        await UniTask.CompletedTask;
    }
}
