using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayGroundMaker : MonoBehaviour {

    #region Variables
    [SerializeField]
    private GameObject buildAreaPrefab;
    [SerializeField]
    private GameObject parentContent;

    [SerializeField]
    private int firstSize = 150;

    public List<GameObject> poolList = new List<GameObject>();

    private float lastValue = 0;
    #endregion

    private void Start()
    {
        for (int i = 0; i < firstSize; i++)
        {
            GameObject newObj = Instantiate(buildAreaPrefab, parentContent.transform);
            newObj.name = newObj.name.Replace("(Clone)", i.ToString());
            poolList.Add(newObj);
        }
    }
}
