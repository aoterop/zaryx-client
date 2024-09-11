using UnityEngine;
using UnityEngine.EventSystems;

public class DragPanel : MonoBehaviour, IDragHandler
{
    public Canvas canvas;

    private RectTransform rectTransform;
    private Vector2 minPosition;
    private Vector2 maxPosition;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        minPosition = canvas.GetComponent<RectTransform>().rect.min - rectTransform.rect.min;
        maxPosition = canvas.GetComponent<RectTransform>().rect.max - rectTransform.rect.max;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Vector2 currentPosition = rectTransform.anchoredPosition + eventData.delta / canvas.scaleFactor;
        currentPosition.x = Mathf.Clamp(currentPosition.x, minPosition.x, maxPosition.x);
        currentPosition.y = Mathf.Clamp(currentPosition.y, minPosition.y, maxPosition.y);
        rectTransform.anchoredPosition = currentPosition;
    }

    public void CerrarPanel()
    {
        if (canvas != null)
        {
            Destroy(gameObject);
        }
    }

    public void OcultarPanel()
    {
        if (canvas != null)
        {
            gameObject.SetActive(false);
        }
    }
}