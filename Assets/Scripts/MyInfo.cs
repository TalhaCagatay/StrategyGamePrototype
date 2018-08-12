using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyInfo : MonoBehaviour {

    [HideInInspector]
    public string myInfo = "";
    [HideInInspector]
    public GameObject myProduction;
    [HideInInspector]
    public Transform mySpawnPoint;

	void Awake ()
    {
        StartCoroutine(DelayedInfo());
	}

    // -- Gecikmeli Olarak myInfo Stringine Bulunduğu Objenin Adını Atıyoruz. -- //
    IEnumerator DelayedInfo()
    {
        yield return new WaitForSeconds(.1f);
        myInfo = gameObject.name;
    }

}
