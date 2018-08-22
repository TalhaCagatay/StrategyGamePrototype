using UnityEngine;

public interface ICommonBuilding // Tüm binaların ortak özellikleri için interface
{
    string BuildingName { get; }
    SpriteRenderer BuildingImage { get; }
}
