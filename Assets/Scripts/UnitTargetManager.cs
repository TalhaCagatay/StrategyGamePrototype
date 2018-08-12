using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitTargetManager : MonoBehaviour {

    [SerializeField]
    private Image movementStoperImage;
    [SerializeField]
    private Grid grid;

    // Update is called once per frame
    void Update ()
    {
        //Sağ tık yapıldığında seçili bir asker var ise...
        if (Input.GetMouseButtonDown(1) && UnitSelectionManager.currentlySelected.Count > 0)
        {
            //Raycast yapıyoruz
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
            //Gerekli kontrolleri yapıp rayin buildarea ya çarptığına bakıyoruz
            if (hit != null && hit.collider != null && hit.transform.tag == "BuildArea")
            {
                //Gridi güncelliyoruz
                grid.CreateGrid();
                //Ekranı oynatmamızı engelliyoruz
                movementStoperImage.gameObject.SetActive(true);
                foreach (UnitSelectionManager selected in UnitSelectionManager.currentlySelected)
                {
                    // GetComponent optimize edilebilir. Seçili askerin myTarget değişkenine mouseun worldposition değerini atıyoruz.
                    selected.GetComponent<Unit>().myTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    // Seçili askerin GoToPosition metodunu çalıştırarak hedefe yolluyoruz.
                    selected.GetComponent<Unit>().GoToPosition();
                }
            } 
            else
                movementStoperImage.gameObject.SetActive(false);
        }	
	}
}
