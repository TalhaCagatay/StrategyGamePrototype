using UnityEngine;

public class SpawnUnit : MonoBehaviour
{

    #region Variables
    [SerializeField]
    private GameInfoManager gameInfoManager;

    [HideInInspector]
    public Transform unitSpawnPoint;
    #endregion

    void Update()
    {
        // -- Bir Bina Seçiliyken Oyun Alanında Bir Gride Sol Tık Yapılırsa Eğer O Binadan Asker Üretiliyorsa O Binanın Askerleri İçin Spawn Point Atıyoruz. -- //
        if (Input.GetMouseButtonDown(0))
        {
            SpawnPoint();
        }
        else // -- Belirtilen Spawn Pointe Asker Çıkartıyoruz. -- //
        if (Input.GetMouseButtonDown(1))
        {
            SpawnSoldier();
        }
    }

    private void SpawnPoint()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
        if (hit != null && hit.collider != null && hit.transform.tag == "BuildArea")
        {
            //Spawn Noktası Belirleyip Her Binanın MyInfo Scriptinin İçindeki mySpawnPoint Değişkenine Atıyoruz;

            if (gameInfoManager.currentlySelected && gameInfoManager.currentlySelected.GetComponent<IProductionInterface>() != null)
            {
                unitSpawnPoint = hit.transform;
                gameInfoManager.currentlySelected.GetComponent<IProductionInterface>().MySpawnPoint = unitSpawnPoint;
            }

        }
    }
    private void SpawnSoldier()
    {
        //Belirlenen Spawn Noktasında Asker Bas. GetComponent normalde performansa etki eder ancak sadece tıklama başına çalıştırdığımız için burada sıkıntı çıkarmaz.
        //Genede büyük projelerde her zaman GetComponent optimize edilmeli veya değiştirilmelidir.
        if (unitSpawnPoint && gameInfoManager.currentlySelected && gameInfoManager.currentlySelected.GetComponent<IProductionInterface>().MyProductionObject != null)
        {
            GameObject newUnit = Instantiate(gameInfoManager.currentlySelected.GetComponent<IProductionInterface>().MyProductionObject, gameInfoManager.currentlySelected.GetComponent<IProductionInterface>().MySpawnPoint, true);
            newUnit.transform.parent = gameInfoManager.currentlySelected.GetComponent<IProductionInterface>().MySpawnPoint;
            newUnit.transform.position = gameInfoManager.currentlySelected.GetComponent<IProductionInterface>().MySpawnPoint.position;
        }
    }
}
