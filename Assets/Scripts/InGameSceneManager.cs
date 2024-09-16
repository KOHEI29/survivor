using UnityEngine;

public class InGameSceneManager : MonoBehaviour
{
    
    [SerializeField]
    private GameObject _gameover = default;
    // Start is called before the first frame update
    void Start()
    {
        _gameover.SetActive(false);

        InGameModel.Instance.OnStateChanged += UpdateState;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //状態が切り替わった時
    private void UpdateState(InGameConst.State oldState, InGameConst.State newState)
    {
        Time.timeScale = newState == InGameConst.State.Play ? 1f : 0f;

        if(newState == InGameConst.State.GameOver)
        {
            _gameover.SetActive(true);
        }
    }
}
