using UnityEngine;
using System;
using System.Collections.Generic;

public class ChestController : MonoBehaviour
{
    private bool _isOpening = false;
    [SerializeField]
    private Animator _animator = default;
    public Action AnimationCompleteCallback = default;

    private static readonly string OpenTriggerString = "Open";
    private static readonly int CompleteHash = Animator.StringToHash("Complete");

    public void Initialize(Vector3 position)
    {
        transform.position = position;
    }

    // Start is called before the first frame update
    void Start()
    {
        
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
    //開く
    public void Open()
    {
        if(_isOpening) return;
        _isOpening = true;

        //アニメーションを再生
        _animator.SetTrigger(OpenTriggerString);
        //アニメーション終了時にする処理を登録
        AnimationCompleteCallback = () =>
        {
            InGameModel.Instance.DisplaySelectSkillView();
            Destroy(gameObject);
        };
    }
}