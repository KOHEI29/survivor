using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SelectSkillItemComponent : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _nameText = default;
    [SerializeField]
    private Image _iconImage = default;
    [SerializeField]
    private TextMeshProUGUI _descriptionText = default;
    [SerializeField]
    private TextMeshProUGUI _levelText = default;
    [SerializeField]
    private GameObject _newObject = default;
    [SerializeField]
    private Button _button = default;

    public void SetAction(UnityAction action)
    {
        _button.onClick.AddListener(action);
    }
    public void SetData(string name, Sprite icon, string description, int currentLv, int finallyLv, bool isNew)
    {
        _nameText.SetText(name);
        _iconImage.sprite = icon;
        _descriptionText.SetText(description);
        if(isNew)
        {
            _newObject.SetActive(true);
            _levelText.gameObject.SetActive(false);
        }
        else
        {
            _newObject.SetActive(false);
            _levelText.gameObject.SetActive(true);
            _levelText.SetText("Lv.{0}→Lv.{1}", currentLv, finallyLv);
        }
    }
    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}