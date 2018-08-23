using UnityEngine;

public class DefaultUnit : MonoBehaviour,ICommonUnitProps {

    private int _strenght;

    public string Name
    {
        get
        {
            return gameObject.name;
        }
    }

    public int Strenght
    {
        get { return _strenght; }
        set { _strenght = value; }
    }

    public void UpgradeUnit() // Polymorphismin kullanıldığı method
    {
        Strenght++;
        Debug.Log(Strenght);
    }

    private void OnEnable()
    {
        UpgradeManager.onUpgrade += UpgradeUnit;
    }

    private void OnDisable()
    {
        UpgradeManager.onUpgrade -= UpgradeUnit;
    }

}
