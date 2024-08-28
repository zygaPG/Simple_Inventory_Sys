using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;

    [SerializeField] ItemSlotUI[] itemsRow = new ItemSlotUI[6];

    ItemSlotUI currentSelectedSlot;

    void Awake()
    {
        if(InventoryUI.Instance != null)
        Debug.LogError("InventoryUI conflict");

        Instance = this;
    }
    
    

    public void UpdateItemSlot(int slot, ItemData itm = null, int amount = 0) => itemsRow[slot].SetItem(itm, amount);

    public void SelectSlot(int id)
    {
        if(currentSelectedSlot != null)
            currentSelectedSlot.Unselect();

        currentSelectedSlot = itemsRow[id];

        itemsRow[id].Select();
    }
}
