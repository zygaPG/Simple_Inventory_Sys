using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Crafting : MonoBehaviour
{
    public Inventory inventory;
    public bool isOpen;

    [System.Serializable]
    public class SimpleEvent : UnityEvent<bool> { }

    public SimpleEvent onCraftingOpen;

    int selectedItem_A = int.MaxValue;
    int selectedItem_B = int.MaxValue;

    ItemData item_R = null;

    public void TogleOpenClose()
    {
        if (isOpen)
        {
            CraftingUI.Instance.Close();
        }
        else
        {
            CraftingUI.Instance.Open();
        }
        Clean();

        isOpen = !isOpen;
        onCraftingOpen.Invoke(isOpen);
    }

    void Clean()
    {
        selectedItem_A = int.MaxValue;
        selectedItem_B = int.MaxValue;

        CraftingUI.Instance.UpdateSelectedItems(null, null, null);
    }

    public void SelectItem()
    {
        if (!isOpen)
            return;

        if (selectedItem_A == int.MaxValue)
        {
            selectedItem_A = inventory.currentSelectedSlot;
            UpdateItems();
            return;
        }

        if (selectedItem_B == int.MaxValue)
        {
            if (selectedItem_A == inventory.currentSelectedSlot)
            {
                if (inventory.TakeAmountFromSlot(inventory.currentSelectedSlot) <= 1)
                    return;
            }

            selectedItem_B = inventory.currentSelectedSlot;
            UpdateItems();
            return;
        }

        if (item_R != null)
        {
            AcceptCrafting();
        }
    }

    public void UnselectItem()
    {
        if (!isOpen)
            return;

        if (selectedItem_B != int.MaxValue)
        {
            selectedItem_B = int.MaxValue;
            UpdateItems();
            return;
        }

        if (selectedItem_A != int.MaxValue)
        {
            selectedItem_A = int.MaxValue;
            UpdateItems();
            return;
        }


    }


    public void AcceptCrafting()
    {
        inventory.UseItemToCrafting(selectedItem_A);
        inventory.UseItemToCrafting(selectedItem_B);

        int slotForNewItem = inventory.FirstSlotWithItem(item_R);

        if (slotForNewItem == int.MaxValue)
        {
            slotForNewItem = inventory.FirstEmptySlotId;
            if (slotForNewItem == int.MaxValue)
                slotForNewItem = inventory.currentSelectedSlot;

        }


        inventory.TakeItem(item_R, slotForNewItem);
        //UpdateSelected();
        //UpdateItems();  
        Clean();
    }


    public void UpdateItems()
    {
        ItemData item_A = selectedItem_A != int.MaxValue ? inventory.TakeItemFromSlot(selectedItem_A) : null;
        ItemData item_B = selectedItem_B != int.MaxValue ? inventory.TakeItemFromSlot(selectedItem_B) : null;

        item_R = null;

        if (item_A != null && item_B != null)
        {
            List<ItemData> fitItms = new List<ItemData>();

            foreach (ItemData it in ItemSpawnManager.Instance.allItems)
            {
                if (it.craftingMaterial_A == item_A || it.craftingMaterial_A == item_B || it.craftingMaterial_B == item_A || it.craftingMaterial_B == item_B)
                {
                    fitItms.Add(it);
                }
            }

            foreach (ItemData it in fitItms)
            {
                if ((it.craftingMaterial_A == item_A && it.craftingMaterial_B == item_B)
                || (it.craftingMaterial_A == item_B && it.craftingMaterial_B == item_A))
                {
                    item_R = it;
                    break;
                }

            }
        }

        CraftingUI.Instance.UpdateSelectedItems(item_A, item_B, item_R);
    }


    //clean selectedSlot when items was removed
    public void UpdateSelected()
    {
        ItemData item_A = selectedItem_A != int.MaxValue ? inventory.TakeItemFromSlot(selectedItem_A) : null;
        ItemData item_B = selectedItem_B != int.MaxValue ? inventory.TakeItemFromSlot(selectedItem_B) : null;

        if (item_A == null)
            selectedItem_A = int.MaxValue;

        if (item_B == null)
            selectedItem_B = int.MaxValue;
    }
}
