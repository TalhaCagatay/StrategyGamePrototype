using UnityEngine;

public interface IProductionInterface // Üretim yapan binalar için interface
{ 
    SpriteRenderer MyProductionImage { get; }
    Transform MySpawnPoint { get; set; }
    GameObject MyProductionObject { get; } // Üretilen askerler birden fazla olduğu durumda listeye çevirilebilir.
}
