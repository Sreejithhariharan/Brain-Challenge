using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using PrimeTween;

public class UIButtonHover : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerDownHandler,
    IPointerUpHandler
{
    [Header("Button Text")]
    [SerializeField] private TextMeshProUGUI buttonText;

    [Header("Scale")]
    [SerializeField] private float hoverScale = 1.08f;
    [SerializeField] private float clickScale = 0.92f;

    [Header("Animation")]
    [SerializeField] private float duration = 0.15f;

    [Header("Colors")]
    [SerializeField] private Color normalColor = Color.white;

    [SerializeField]
    private Color hoverColor =
        new Color(0.2f, 0.9f, 1f); // Cyan

    [SerializeField]
    private Color clickColor =
        new Color(1f, 0.25f, 0.9f); // Pink


    private bool isHovering;


    // =========================================
    // START
    // =========================================

    void Start()
    {
        if (buttonText == null)
            buttonText = GetComponentInChildren<TextMeshProUGUI>();

        transform.localScale = Vector3.one;

        if (buttonText != null)
            buttonText.color = normalColor;
    }


    // =========================================
    // HOVER ENTER
    // =========================================

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;

        AnimateScale(hoverScale);
        AnimateColor(hoverColor);
    }


    // =========================================
    // HOVER EXIT
    // =========================================

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;

        AnimateScale(1f);
        AnimateColor(normalColor);
    }


    // =========================================
    // CLICK / PRESS
    // =========================================

    public void OnPointerDown(PointerEventData eventData)
    {
        AnimateScale(clickScale);
        AnimateColor(clickColor);
    }


    // =========================================
    // RELEASE
    // =========================================

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isHovering)
        {
            AnimateScale(hoverScale);
            AnimateColor(hoverColor);
        }
        else
        {
            AnimateScale(1f);
            AnimateColor(normalColor);
        }
    }


    // =========================================
    // SCALE
    // =========================================

    void AnimateScale(float targetScale)
    {
        Tween.StopAll(onTarget: transform);

        Tween.Scale(
            transform,
            Vector3.one * targetScale,
            duration,
            Ease.OutBack
        );
    }


    // =========================================
    // TEXT COLOR
    // =========================================

    void AnimateColor(Color targetColor)
    {
        if (buttonText == null)
            return;

        Tween.StopAll(onTarget: buttonText);

        Tween.Color(
            buttonText,
            targetColor,
            duration,
            Ease.OutQuad
        );
    }
}