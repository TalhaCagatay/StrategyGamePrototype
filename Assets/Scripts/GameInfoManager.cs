using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameInfoManager : MonoBehaviour
{

    #region Variables
    public GameObject currentlySelected;

    [SerializeField]
    private LayerMask myMask;

    [SerializeField]
    private TMP_Text buildingName;

    [SerializeField]
    private Image buildImage;

    [SerializeField]
    private Image productionImage;
    #endregion

    // Update is called once per frame
    void Update () {

        // -- Fareden Attığımız Raycast İle Çarptığı Placed Etiketli Binaları Kontrol Ederek Onların Bilgilerini Information Panelindeki Gerekli Yerlere Atıyoruz. -- //
        if (Input.GetMouseButtonDown(0))
        {
            GetInformation();
        }
    }

    private void GetInformation()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 100, myMask);
        if (hit != null && hit.collider != null && hit.transform.tag == "Placed")
        {
            //GetComponentlar optimize edilebilir.
            buildImage.sprite = hit.transform.GetComponent<ICommonBuilding>().BuildingImage.sprite;
            if (hit.transform.GetComponent<IProductionInterface>() != null)
            {
                //Seçili binanın bilgilerini alıp information panelinde gerekli yerlere atıyoruz.
                if (hit.transform.GetComponent<IProductionInterface>() != null)
                {
                    productionImage.sprite = hit.transform.GetComponent<IProductionInterface>().MyProductionImage.sprite;
                }
                buildingName.text = hit.transform.GetComponent<ICommonBuilding>().BuildingName;
                if (hit.transform.GetComponent<IProductionInterface>().MyProductionObject)
                    productionImage.sprite = hit.transform.GetComponent<IProductionInterface>().MyProductionImage.sprite;
                else
                    productionImage.sprite = null;
            }

            // -- Önceden Seçilmiş Binanın Arkaplan Rengine Erişip Rengini Tekrardan Yeşil Yapıyoruz. -- //
            if (currentlySelected)
                currentlySelected.GetComponent<BuildingPlacement>().myBG.color = new Color32(0, 255, 0, 255);

            // -- Seçilen Binayı currentlySelected Değişkenine Atıyoruz. -- //
            if(hit.transform.tag == "Placed")
                currentlySelected = hit.transform.gameObject;

            // -- Yeni Seçilen Binanın Arkaplan Rengine Erişip Rengini Mavi Yapıyoruz. -- //
            hit.transform.GetComponent<BuildingPlacement>().myBG.color = new Color32(0, 0, 255, 255);
        }

        else
        {
            //Seçili binayı seçmeyi bırakıyoruz.
            if (currentlySelected)
            {
                currentlySelected.GetComponent<BuildingPlacement>().myBG.color = new Color32(0, 255, 0, 255);
                buildingName.text = "Bina Adi";
                buildImage.sprite = null;
                productionImage.sprite = null;
                currentlySelected = null;
            }
        }
    }
}
