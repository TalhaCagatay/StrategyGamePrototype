using UnityEngine;

public class MousePositionChecker : MonoBehaviour {

    [HideInInspector]
    public Transform mousePos;
	
	// Update is called once per frame
	void Update () {

        // -- BuildingManager Scriptimizde Instantiate Edilen Binaların Fare Takibinin Gride Göre Olması İçin Burada Fareden Raycast Yapıp BuildArea Etiketli Yerlere Çarpıp Çarpmadığına Bakıyoruz -- //
        // -- Eğer Çarpıyorsa mousePos Değişkenimize Çarpan Yerin Transformunu Atıyoruz. -- //
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
        if (hit != null && hit.collider != null && hit.transform.tag == "BuildArea")
        {
            mousePos = hit.transform;
        }

    }
}
