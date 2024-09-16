using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

//フォールキャロットコントローラ
public class FallCarrotController : MonoBehaviour, IAttackable
{
    private int _attack = 5;
    public int Attack => _attack;

    [SerializeField]
    private Animator _animator = default;
    public Action AnimationCompleteCallback = default;
    private static readonly int CompleteHash = Animator.StringToHash("Complete");
    public void Initialize(Vector3 position)
    {
        transform.position = position;
    }

    void Update()
    {
        if(AnimationCompleteCallback != null)
        {
            var info = _animator.GetCurrentAnimatorStateInfo(0);
            if(info.tagHash == CompleteHash)
                AnimationCompleteCallback.Invoke();
        }
    }
}
