using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragSelectionHandler : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler,IPointerClickHandler {

    [SerializeField]
    private Image selectionBoxImage;

    private Vector2 startPos;

    private Rect selectionRect;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.RightControl))
        {
            UnitSelectionManager.DeselectAll(new BaseEventData(EventSystem.current));
        }
        selectionBoxImage.gameObject.SetActive(true);
        startPos = eventData.position;
        selectionRect=new Rect();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.position.x < startPos.x)
        {
            // eventData.positin = farenin pozisyonu
            selectionRect.xMin = eventData.position.x;
            selectionRect.xMax = startPos.x;
        }
        else
        {
            selectionRect.xMin = startPos.x;
            selectionRect.xMax = eventData.position.x;
        }

        if (eventData.position.y < startPos.y)
        {
            // eventData.positin = farenin pozisyonu
            selectionRect.yMin = eventData.position.y;
            selectionRect.yMax = startPos.y;
        }
        else
        {
            selectionRect.yMin = startPos.y;
            selectionRect.yMax = eventData.position.y;
        }

        selectionBoxImage.rectTransform.offsetMin = selectionRect.min;
        selectionBoxImage.rectTransform.offsetMax = selectionRect.max;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        selectionBoxImage.gameObject.SetActive(false);
        foreach (UnitSelectionManager selectable in UnitSelectionManager.allMySelectables)
        {
            if (selectionRect.Contains(Camera.main.WorldToScreenPoint(selectable.transform.position)))
            {
                selectable.OnSelect(eventData);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        float myDistance = 0;

        foreach (RaycastResult result in results)
        {
            if (result.gameObject == gameObject)
            {
                myDistance = result.distance;
                break;
            }
        }

        GameObject nextObject = null;
        float maxDistance = Mathf.Infinity;

        foreach (RaycastResult result in results)
        {
            if (result.distance > myDistance && result.distance < maxDistance)
            {
                nextObject = result.gameObject;
                maxDistance = result.distance;
            }
        }

        if (nextObject)
        {
            ExecuteEvents.Execute<IPointerClickHandler>(nextObject, eventData, (x, y) => { x.OnPointerClick((PointerEventData)y); });
        }
    }
}
