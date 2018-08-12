using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUnit : MonoBehaviour {

    #region Variables
    private MousePositionChecker mousePositionChecker;

    [SerializeField]
    private GameInfoManager gameInfoManager;

    [HideInInspector]
    public Transform unitSpawnPoint;
    #endregion

    void Update ()
    {
        // -- Bir Bina Seçiliyken Oyun Alanında Bir Gride Sol Tık Yapılırsa Eğer O Binadan Asker Üretiliyorsa O Binanın Askerleri İçin Spawn Point Atıyoruz. -- //
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
            if (hit != null && hit.collider != null && hit.transform.tag == "BuildArea")
            {
                //Spawn Noktası Belirleyip Her Binanın MyInfo Scriptinin İçindeki mySpawnPoint Değişkenine Atıyoruz;

                if (gameInfoManager.currentlySelected)
                {
                    unitSpawnPoint = hit.transform;
                    gameInfoManager.currentlySelected.GetComponent<MyInfo>().mySpawnPoint = unitSpawnPoint;
                }
                    
            }
        }
        // -- Belirtilen Spawn Pointe Asker Çıkartıyoruz. -- //
        else if (Input.GetMouseButtonDown(1))
        {
                //Belirlenen Spawn Noktasında Asker Bas. GetComponent normalde performansa etki eder ancak sadece tıklama başına çalıştırdığımız için burada sıkıntı çıkarmaz.
                //Genede büyük projelerde her zaman GetComponent optimize edilmeli veya değiştirilmelidir.
                if (unitSpawnPoint && gameInfoManager.currentlySelected && gameInfoManager.currentlySelected.GetComponent<MyInfo>().myProduction != null)
                {
                    GameObject newUnit = Instantiate(gameInfoManager.currentlySelected.GetComponent<MyInfo>().myProduction, gameInfoManager.currentlySelected.GetComponent<MyInfo>().mySpawnPoint, true);
                    newUnit.transform.parent = gameInfoManager.currentlySelected.GetComponent<MyInfo>().mySpawnPoint;
                    newUnit.transform.position = gameInfoManager.currentlySelected.GetComponent<MyInfo>().mySpawnPoint.position;
                }
        }
    }
}
