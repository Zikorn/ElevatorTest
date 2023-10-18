using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ChangeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button button;
    private Text buttonText;

    public Color normalColor;
    public Color highlightedColor;

    private void Start()
    {
        button = GetComponent<Button>();
        buttonText = button.GetComponentInChildren<Text>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        button.image.color = highlightedColor;
        buttonText.color = normalColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        button.image.color = normalColor;
        buttonText.color = highlightedColor;
    }
}
