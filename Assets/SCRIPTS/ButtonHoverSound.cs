using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverSound : MonoBehaviour, IPointerEnterHandler
{
    public MenuAudio menuAudio;

    public void OnPointerEnter(PointerEventData eventData)
    {
        menuAudio.PlayHover();
    }
}