using UnityEngine;

public class ProductiveBuilding : AbstractInfoManager,ICommonBuilding,IProductionInterface { // Üretim yapan binaların sınıfı, hem ortak interface'den hemde üretim interface'inden miras alıyor.

    [SerializeField]
    private GameObject myProduction;
    [SerializeField]
    private Transform mySpawnPoint;

    public string BuildingName
    {
        get
        {
            return gameObject.name;
        }
    }

    public SpriteRenderer BuildingImage
    {
        get { return GetComponentInChildren<SpriteRenderer>(); }
    }

    public SpriteRenderer MyProductionImage
    {
        get
        {
            return myProduction.GetComponent<SpriteRenderer>();
        }
    }

    public GameObject MyProductionObject
    {
        get { return myProduction; }
    }

    public Transform MySpawnPoint
    {
        get
        {
            return mySpawnPoint;
        }

        set
        {
            mySpawnPoint = value;
        }
    }

    public ProductiveBuilding(string name) : base(name) // Polymorphism'e örnek olması için yazıldı.
    {
        Debug.Log(name); // Base sınıfın name'i

        name = "Üretim Yapıyorum";
        Debug.Log(name); // Değiştirip kullanılan name
    }
}
