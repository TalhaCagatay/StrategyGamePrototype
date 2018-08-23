using System;
using UnityEngine;

public abstract class AbstractInfoManager : MonoBehaviour
{
    public string name = "BELİRSİZ";

    public AbstractInfoManager(string name) // Bu abstract sınıftan miras alan sınıflar kendi constructorlarını oluşturup farklı biçimde yada varsayılan şekliyle kullanabilirler.
                                            // Polymorphism'e örnek olması için yazıldı.
    {
        name = this.name; // Default
    }
}

