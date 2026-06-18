using UnityEngine;

public class OpenInventory : MonoBehaviour
{
    public GameObject inventoryPanel;

    public void ToggleInventory()
    {
        inventoryPanel.SetActive(
            !inventoryPanel.activeSelf
        );
    }
}