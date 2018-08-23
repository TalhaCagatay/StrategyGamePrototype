using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommonUnitProps // Polymorphism'e örnek interface, bu interface'den kalıtım alan 2 sınıf (specialunits ve defaultunits), UpgradeUnit() metodunu farklı kullanıyorlar.
{
    string Name { get; }
    int Strenght { get; set; }

    void UpgradeUnit();
}
