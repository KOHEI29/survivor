using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class GameOverSkillAreaItemComponent : MonoBehaviour
{
    [SerializeField]
    private RectTransform _rectTransform = default;
    [SerializeField]
    private Image _iconImage = default;
    [SerializeField]
    private TextMeshProUGUI _levelText = default;
    
    public void Initialize(Vector2 position, Sprite iconSprite, int level)
    {
        _rectTransform.anchoredPosition = position;
        if(iconSprite != null)
            _iconImage.sprite = iconSprite;
        _levelText.SetText("Lv.{0}", level);
    }
}
