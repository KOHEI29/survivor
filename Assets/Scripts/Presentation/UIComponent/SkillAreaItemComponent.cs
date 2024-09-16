using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class SkillAreaItemComponent : MonoBehaviour
{
    [SerializeField]
    private RectTransform _rectTransform = default;
    [SerializeField]
    private Image _icon = default;
    [SerializeField]
    private Image _fill = default;
    [SerializeField]
    private TextMeshProUGUI _stackCount = default;
    
    public void Initialize(Vector2 position, Sprite iconSprite, float fillAmount, int stackCount)
    {
        _rectTransform.anchoredPosition = position;
        if(iconSprite != null)
            _icon.sprite = iconSprite;
        _fill.fillAmount = fillAmount;
        _stackCount.SetText("{0}", stackCount);
    }
    public void SetFillAmount(float fillAmount)
    {
        _fill.fillAmount = fillAmount;
    }
    public void SetStackCount(int stackCount)
    {
        _stackCount.SetText("{0}", stackCount);
    }
}
