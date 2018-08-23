using UnityEngine;

public class UpgradeManager : MonoBehaviour {

    public delegate void UpgradeUnitStrengh();
    public static event UpgradeUnitStrengh onUpgrade;
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            if (onUpgrade != null)
            {
                onUpgrade();
            }
        }
    }
}
