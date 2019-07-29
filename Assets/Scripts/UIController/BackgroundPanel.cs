using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// Задний фон для попапов
/// </summary>
public class BackgroundPanel : MonoBehaviour, IPointerClickHandler
{
    /// <summary>
    /// Событие нажатие на задний фон
    /// </summary>
    public UnityAction OnTap;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (OnTap != null)
        {
            OnTap.Invoke();
        }
    }
}
