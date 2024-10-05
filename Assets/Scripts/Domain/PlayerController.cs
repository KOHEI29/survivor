using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;

//プレイヤーコントローラ
public class PlayerController : MonoBehaviour
{
    private Animator _animator = default;
    private Vector3 _moveDirection = Vector3.zero;
    public Vector3 MoveDirection => _moveDirection;
    private Vector3 _headingDirection = Vector3.down;
    public Vector3 HeadingDirection => _headingDirection;
    private float _moveSpeed = 5f;

    //投射物
    [SerializeField]
    private GameObject _projectilePrefab = default;
    private ProjectileController _projectileController = default;
    private const float ProjectileCooltime = 1f;
    private float _projectileTimeCount = 0f;

    //スキル
    private List<ISkillExecuter> _skillExecuters = new List<ISkillExecuter>(4);
    //スキルTask。予約入力ができるようになる。
    private UniTask _skillTask;

    //付いてきて欲しいUI
    [SerializeField]
    private Transform _hpGuage = default;

    void Start()
    {
        _animator = GetComponent<Animator>();
        //データ更新時の処理を登録
        InGameModel.Instance.OnSkillAdded += CreateSkillExcecuter;

        //初期化
        var skills = InGameModel.Instance.GetSkillDatas();
        for(int i = 0, c = skills.Count; i < c; i++)
            CreateSkillExcecuter(i, skills[i]);
    }
    private void CreateSkillExcecuter(int index, ISkillData skill)
    {
        _skillExecuters.Add(skill.ExecuterId switch
        {
            0 => new BlinkSkillExecuter(this),
            1 => new FallCarrotSkillExecuter(this),
            _ => throw new System.ArgumentException()
        });
    }

    void Update()
    {
        float animatorSpeed = 0f;
        _moveDirection = Vector3.zero;

        //WASD
        if(Input.GetKey(KeyCode.W))
        {
            _moveDirection.y += 1f;
            animatorSpeed = 1f;
        }
        if(Input.GetKey(KeyCode.S))
        {
            _moveDirection.y -= 1f;
            animatorSpeed = 1f;
        }
        if(Input.GetKey(KeyCode.A))
        {
            _moveDirection.x -= 1f;
            animatorSpeed = 1f;
        }
        if(Input.GetKey(KeyCode.D))
        {
            _moveDirection.x += 1f;
            animatorSpeed = 1f;
        }
        transform.Translate(_moveDirection * _moveSpeed * Time.deltaTime);
        if(_moveDirection != Vector3.zero)
        {
            _headingDirection = _moveDirection.normalized;
            if(_moveDirection.x != 0)
            {
                transform.localScale = new Vector3(3 * _moveDirection.x, 3f, 1f);
            }
        }

        _hpGuage.position = transform.position;

        //MouseClick
        if(Input.GetMouseButton(0))
        {
            if(_projectileTimeCount <= 0f)
            {
                _projectileTimeCount = ProjectileCooltime;
                _projectileController = Instantiate(_projectilePrefab).GetComponent<ProjectileController>();

                var temp = Camera.main.ScreenToWorldPoint(
                            new Vector3(
                                    Input.mousePosition.x,
                                    Input.mousePosition.y,
                                    -Camera.main.transform.position.z));
                temp.z = 0f;

                var position = transform.position;
                
                _projectileController.Initialize(position, (temp - position).normalized);
            }
        }
        if(_projectileTimeCount > 0f)
        {
            _projectileTimeCount -= Time.deltaTime;
        }

        _animator.SetFloat("Speed", animatorSpeed);

        InGameModel.Instance.AddTime(Time.deltaTime);

        
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            //0番目のスキル発動
            TryUseSkill(0);
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            //1番目のスキル発動
            TryUseSkill(1);
        }
    }
    private bool TryUseSkill(int index)
    {
        if(InGameModel.Instance.CanUseSkill(index) && _skillExecuters[index].CanDoSkill())
        {
            _skillTask = _skillExecuters[index].DoSkillAsync(this.GetCancellationTokenOnDestroy(), _skillTask);
            InGameModel.Instance.ReduceSkillStack(index);
            return true;
        }
        return false;
    }
    private void OnTriggerEnter(Collider other)
    {
        //敵との衝突時
        if(other.tag == "Enemy")
        {
            Debug.Log("Damaged!!!");
        }
        else if(other.tag == "Exp")
        {
            InGameModel.Instance.AddExp(1);
            Destroy(other.gameObject);
        }
        else if(other.tag == "Chest")
        {
            other.GetComponent<ChestController>().Open();
        }
    }
}