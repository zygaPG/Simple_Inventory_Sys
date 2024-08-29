using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingUI : MonoBehaviour
{
    public static CraftingUI Instance;

    [SerializeField] GameObject craftingPanel;
    [SerializeField] ItemSlotUI slotA;
    [SerializeField] ItemSlotUI slotB;
    [SerializeField] ItemSlotUI slotResult;

    [SerializeField] TextMeshProUGUI selectInfo;
    [SerializeField] TextMeshProUGUI acceptInfo;

    [SerializeField] TextMeshProUGUI removeItemInfo;

    public bool IsOpen => craftingPanel.activeInHierarchy;

    void Awake()
    {
        if (CraftingUI.Instance != null)
            Debug.LogError("CraftingUI conflict");
        
        Instance = this;
    }


    void OnEnable()
    {
        Clean();
    }

    void Clean()
    {
        slotA.SetItem(null, 0);
        slotB.SetItem(null, 0);
        slotResult.SetItem(null, 0);
    }

    public void Open()
    {
        Clean();
        craftingPanel.SetActive(true);
    }

    public void Close()
    {
        craftingPanel.SetActive(false);
    }


    public void UpdateSelectedItems(ItemData itm_A, ItemData itm_B, ItemData itm_R)
    {
        slotA.SetItem(itm_A, 1);
        slotB.SetItem(itm_B, 1);
        slotResult.SetItem(itm_R, 1);
    }

    public void ShowSelectInfoText()
    {
        selectInfo.gameObject.SetActive(true);
        acceptInfo.gameObject.SetActive(false);
    }

    public void ShowAcceptInfoText()
    {
        selectInfo.gameObject.SetActive(false);
        acceptInfo.gameObject.SetActive(true);
    }

    public void ShowRemoveItemInfo(bool isVisable)
    {
        removeItemInfo.gameObject.SetActive(isVisable);
    }
}
