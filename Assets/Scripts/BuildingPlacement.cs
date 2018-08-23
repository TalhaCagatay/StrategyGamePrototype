using UnityEngine;

public class BuildingPlacement : MonoBehaviour {

    public SpriteRenderer myBG;

	public bool canPlace=true;

    // -- Seçili Bina Daha Önceden Yerleştirilmiş Bir Binanın Üzerine Geldiğinde Arkaplan Rengini Kırmızı Yapıyor -- //
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Placed") || (collision.CompareTag("Soldier") && gameObject.tag == "Building"))
        {
            canPlace = false;
            myBG.color = new Color32(255, 0, 0, 255);
        }
        if (collision.CompareTag("BuildArea") && gameObject.tag == "Placed")
        {
            collision.tag = "UnUsableArea";
        }
    }
    // -- Seçili Bina Daha Önceden Yerleştirilmiş Bir Binanın Üzerinden Çıktığında Arkaplan Rengini Tekrardan Yeşil Yapıyor -- //
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Placed") || collision.CompareTag("Soldier"))
        {
            canPlace = true;
            myBG.color = new Color32(0, 255, 0, 255);
        }
    }
}
