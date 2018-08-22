using UnityEngine;

public class BuildingManager : MonoBehaviour {

    #region Variables
    [SerializeField]
    private Grid grid;
    [SerializeField]
    private MousePositionChecker mousePositionChecker;
    [SerializeField]
    private Transform parentTransform;

    private BuildingPlacement buildingPlacement;

    private Transform currentBuilding;
    private bool hasPlaced;
    #endregion

    // Update is called once per frame
    void LateUpdate() {

        // -- currentBuilding Değişkenimizin Null Olmadığını Yani Bir Binanın Instantiate Edildiğini Ve Fareyi Takip Edip Etmeyeceğini Anlamak İçinde hasPlaced Değişkenimizin Durumunu Kontrol Ediyoruz. -- //
        if (currentBuilding != null && !hasPlaced)
        {
            BuildingPlacer();
        }
    }

    private void BuildingPlacer()
    {
        if (mousePositionChecker.mousePos)
            currentBuilding.position = mousePositionChecker.mousePos.position;

        // -- Eğer Sol Tıkı Bırakırsak Ve Binamız Yerleştirilmeye Uygunsa hasPlaced Değişkenimizi true Yapıp Fareyi Takip Etmesini Durduruyoruz ve tagını Placed Yapıyoruz. -- //
        if (Input.GetMouseButtonUp(0) && buildingPlacement.canPlace)
        {
            hasPlaced = true;
            currentBuilding.tag = "Placed";
            currentBuilding.parent = mousePositionChecker.mousePos;
            //Her binayı yerleştirdikten sonra gridimizi yeniden oluşturuyoruz. *Optimizasyon yapılabilir burada*.
            grid.CreateGrid();

        }
        else if (Input.GetMouseButtonUp(0))
            Destroy(currentBuilding.gameObject);
    }

    // -- Butonlara Atanan Metot. Tıklanan Butona Ait Prefabı Instantiate Yapıyor -- //
    public void SetItem(GameObject b)
    {
        hasPlaced = false;
        currentBuilding = ((GameObject)Instantiate(b, parentTransform.GetChild(0))).transform;
        currentBuilding.name = currentBuilding.name.Replace("(Clone)", "");
        buildingPlacement = currentBuilding.GetComponent<BuildingPlacement>();
    }

}
