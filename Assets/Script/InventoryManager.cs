using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject slotPrefab;
    public Transform contentPanel;
    public Button nextButton;
    public Button prevButton;
    
    private int itemsPerPage = 6;
    private int currentPage = 0;

    private void OnEnable()
    {
        RefreshUI();
    }

    public void RefreshUI()
    {
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }

        int totalItems = GameManager.Instance.inventory.Count;
        int startIndex = currentPage * itemsPerPage;
        int endIndex = Mathf.Min(startIndex + itemsPerPage, totalItems);

        for (int i = startIndex; i < endIndex; i++)
        {
            EvidenceData data = GameManager.Instance.inventory[i];
            GameObject newSlot = Instantiate(slotPrefab, contentPanel);
            
            Transform iconTransform = newSlot.transform.Find("Icon");
            if (iconTransform != null)
            {
                Image iconImage = iconTransform.GetComponent<Image>();
                if (iconImage != null && data.icon != null)
                {
                    iconImage.sprite = data.icon;
                }
            }
        }

        UpdateNavigationButtons(totalItems);
    }

    private void UpdateNavigationButtons(int totalItems)
    {
        if (nextButton != null)
        {
            nextButton.gameObject.SetActive(totalItems > itemsPerPage && (currentPage + 1) * itemsPerPage < totalItems);
        }

        if (prevButton != null)
        {
            prevButton.gameObject.SetActive(currentPage > 0);
        }
    }

    public void NextPage()
    {
        currentPage++;
        RefreshUI();
    }

    public void PreviousPage()
    {
        currentPage--;
        RefreshUI();
    }
}