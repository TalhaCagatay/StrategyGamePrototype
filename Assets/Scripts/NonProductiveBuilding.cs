using UnityEngine;

public class NonProductiveBuilding : AbstractInfoManager, ICommonBuilding { // Üretim yapmayan binaların sınıfı, sadece ortak özelliklerden miras alıyorlar.

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

    public NonProductiveBuilding(string name) : base(name) // Polymorphism'e örnek olması için yazıldı.
    {
        Debug.Log(base.name);

        name = "Üretim yapmıyorum";
        Debug.Log(name); // Değiştirip kullanılan name
    }

    private void Start()
    {
        var obj = new NonProductiveBuilding("Üretim yapmıyorum");
    }

}
