using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitSelectionManager : MonoBehaviour, ISelectHandler, IPointerClickHandler, IDeselectHandler {

    public static HashSet<UnitSelectionManager> allMySelectables = new HashSet<UnitSelectionManager>();
    public static HashSet<UnitSelectionManager> currentlySelected = new HashSet<UnitSelectionManager>();

    private SpriteRenderer mySprite;

    private void Awake()
    {
        allMySelectables.Add(this);
        mySprite = GetComponent<SpriteRenderer>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.RightControl))
        {
            DeselectAll(eventData);
        }
        OnSelect(eventData);
    }

    public void OnSelect(BaseEventData eventData)
    {
        
        currentlySelected.Add(this);
        mySprite.color = new Color32(255, 0, 0, 255);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        mySprite.color = new Color32(255, 255, 255, 255);
    }

    public static void DeselectAll(BaseEventData eventData)
    {
        foreach (UnitSelectionManager selectable in currentlySelected)
        {
            selectable.OnDeselect(eventData);
        }
        currentlySelected.Clear();
    }

}
