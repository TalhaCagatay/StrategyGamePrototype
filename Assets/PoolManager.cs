using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoolManager : MonoBehaviour {

    #region Variables
    [SerializeField]
    private GameObject holderPrefab;
    [SerializeField]
    private GameObject buildAreaPrefab;
    [SerializeField]
    private GameObject parentContent;
    [SerializeField]
    private GridLayoutGroup myGridLayout;

    [SerializeField]
    private int size = 10;
    [SerializeField]
    private int firstSize = 150;

    public List<GameObject> poolList = new List<GameObject>();

    private ScrollRect scrollRect;

    private float lastValue = 0;
    #endregion

    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    private void Start()
    {
        for (int i = 0; i < firstSize; i++)
        {
            GameObject newObj = Instantiate(buildAreaPrefab, parentContent.transform);
            newObj.name = newObj.name.Replace("(Clone)", i.ToString());
            poolList.Add(newObj);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            //myGridLayout.startCorner = GridLayoutGroup.Corner.LowerLeft;
            for (int i = 0; i < size; i++)
            {
                //Instantiate(buildAreaPrefab, parentContent.transform);

                poolList[i].transform.SetAsLastSibling();
            }
            for (int i = 0; i < size; i++)
            {
                GameObject tempIndex2 = poolList[0];

                poolList.RemoveAt(0);
                poolList.Insert(poolList.Count, tempIndex2);

                //poolList[poolList.Count - (i + 1)] = poolList[i];
                //poolList[i] = tempIndex2;
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            //myGridLayout.startCorner = GridLayoutGroup.Corner.UpperLeft;
            for (int i = 1; i <= size; i++)
            {
                poolList[poolList.Count - i].transform.SetAsFirstSibling();

            }
        }
    }
}
