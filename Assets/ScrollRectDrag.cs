using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollRectDrag : MonoBehaviour, IBeginDragHandler {

    #region Variables
    [SerializeField]
    private GameObject buildAreaPrefab;
    [SerializeField]
    private GameObject parentContent;

    private List<GameObject> poolList = new List<GameObject>();

    private ScrollRect scrollRect;

    private float lastValue = 0;
    #endregion

    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    private void Start()
    {
        for (int i = 0; i < 143; i++)
        {
            GameObject newObj = Instantiate(buildAreaPrefab, parentContent.transform);
            poolList.Add(newObj);
        }
    }

    public void OnBeginDrag(PointerEventData data)
    {
        Debug.Log("Sürükleniyorummm");
        float mouseStartPos = Input.mousePosition.y;
    }

    void OnEnable()
    {
        scrollRect.onValueChanged.AddListener(scrollRectCallBack);
        //lastValue = scrollRect.velocity;
    }

    //Will be called when ScrollRect changes
    void scrollRectCallBack(Vector2 value)
    {
        Debug.Log("ScrollRect Changed: " + value);
    }

    void OnDisable()
    {
        //Un-Subscribe To ScrollRect Event
        scrollRect.onValueChanged.RemoveListener(scrollRectCallBack);
    }
}
