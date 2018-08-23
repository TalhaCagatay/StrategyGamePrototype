using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class InfiniteScroll : ScrollRect {

    [HideInInspector]
    public bool
        initOnAwake;

    protected RectTransform t
    {
        get
        {
            if (_t == null)
                _t = GetComponent<RectTransform>();
            return _t;
        }
    }

    private RectTransform _t;

    private RectTransform[] prefabItems;
    private int itemTypeStart = 0;
    private int itemTypeEnd = 0;

    private bool init;

    private Vector2 dragOffset = Vector2.zero;

    #region abstracts	
    protected abstract float GetSize(RectTransform item);

    protected abstract float GetDimension(Vector2 vector);

    protected abstract Vector2 GetVector(float value);

    protected abstract float GetPos(RectTransform item);

    protected abstract int OneOrMinusOne();
    #endregion

    #region core
    new void Awake()
    {
        if (!Application.isPlaying)
            return;

        if (initOnAwake)
            Init();
    }

    public void Init()
    {
        init = true;

        //Prefablardan array oluşturup disable yapıyoruz.

        var tempStack = new Stack<RectTransform>();
        foreach (RectTransform child in content)
        {
            if (!child.gameObject.activeSelf)
                continue;
            tempStack.Push(child);
            child.gameObject.SetActive(false);
        }
        prefabItems = tempStack.ToArray();

        float containerSize = 0;
        //Scrollview'ı başlangıç itemleriyle dolduruyoruz.
        while (containerSize < GetDimension(t.sizeDelta))
        {
            RectTransform nextItem = NewItemAtEnd();
            containerSize += GetSize(nextItem);
        }
    }
    private void Update()
    {
        if (!Application.isPlaying || !init)
            return;
        if (GetDimension(content.sizeDelta) - (GetDimension(content.localPosition) * OneOrMinusOne()) < GetDimension(t.sizeDelta))
        {
            NewItemAtEnd();
           //margin'i objeleri yok etmek için kullanıyoruz. marginin yarısında ekliyoruz.(eğer full marginde eklersek, objeleri aralıksız olarak ekleyip yok ederiz.)
        }
        else if (GetDimension(content.localPosition) * OneOrMinusOne() < (GetDimension(t.sizeDelta) * 0.5f))
        {
            NewItemAtStart();
            //Eşyalar eklendiğinde bazen UnityGUI'nin propertie leri sadece frame in en sonunda güncelleniyor.
            //Nesneleri sadece yeni birşey eklenmediğinde yok ediyoruz.(Aynı zamanda hızlı scroll yaptığımızda zamanlarda performansdan kar ediyoruz.)
        }
        else
        {
            //Tüm itemleri dönüyoruz.
            foreach (RectTransform child in content)
            {
                //Prefablarımız inactive
                if (!child.gameObject.activeSelf)
                    continue;
                //Eğer item çok uzaksa en sondan bir item yok ediyoruz.
                if (GetPos(child) > GetDimension(t.sizeDelta))
                {
                    Destroy(child.gameObject);
                    //Üstten bir prefab sildiğimiz için, container tüm içeriğini yukarıya çekiyor. Bizde container ın pozisyonunu güncelliyoruz.
                    content.localPosition -= (Vector3)GetVector(GetSize(child));
                    dragOffset -= GetVector(GetSize(child));
                    Add(ref itemTypeStart);
                }
                else if (GetPos(child) < -(GetDimension(t.sizeDelta) + GetSize(child)))
                {
                    Destroy(child.gameObject);
                    Subtract(ref itemTypeEnd);
                }
            }
        }
    }

    private RectTransform NewItemAtStart()
    {
        Subtract(ref itemTypeStart);
        RectTransform newItem = InstantiateNextItem(itemTypeStart);
        newItem.SetAsFirstSibling();

        content.localPosition += (Vector3)GetVector(GetSize(newItem));
        dragOffset += GetVector(GetSize(newItem));
        return newItem;
    }

    private RectTransform NewItemAtEnd()
    {
        RectTransform newItem = InstantiateNextItem(itemTypeEnd);
        Add(ref itemTypeEnd);
        return newItem;
    }

    private RectTransform InstantiateNextItem(int itemType)
    {
        RectTransform nextItem = Instantiate(prefabItems[itemType]) as RectTransform;
        nextItem.name = prefabItems[itemType].name;
        nextItem.transform.SetParent(content.transform, false);
        nextItem.gameObject.SetActive(true);
        return nextItem;
    }
    #endregion

    #region overrides
    public override void OnBeginDrag(UnityEngine.EventSystems.PointerEventData eventData)
    {
        dragOffset = Vector2.zero;
        base.OnBeginDrag(eventData);
    }

    public override void OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
    {
        //Daha iyi bir yol bulana kadar geçici bir çözüm bu !!!
        if (dragOffset != Vector2.zero)
        {
            OnEndDrag(eventData);
            OnBeginDrag(eventData);
            dragOffset = Vector2.zero;
        }

        base.OnDrag(eventData);
    }
    #endregion

    #region convenience


    private void Subtract(ref int i)
    {
        i--;
        if (i == -1)
        {
            i = prefabItems.Length - 1;
        }
    }

    private void Add(ref int i)
    {
        i++;
        if (i == prefabItems.Length)
        {
            i = 0;
        }
    }
    #endregion
}
