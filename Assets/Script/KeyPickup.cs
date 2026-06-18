using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public void CollectKey()
    {
        GameManager.Instance.hasKey = true;

        gameObject.SetActive(false);

        Debug.Log("Kunci berhasil diambil");
    }
}