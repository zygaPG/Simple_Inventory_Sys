using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Serializable]
    public struct ItemAmount
    {
        public ItemData key;
        public int amount;
    }

    private ItemAmount[] myItems = new ItemAmount[6];

    public ItemData TakeItemFromSlot(int id) => myItems[id].key;
    public int TakeAmountFromSlot(int id) => myItems[id].amount;

    public int currentSelectedSlot = 0;


    [SerializeField] ItemsDetector itemsDetector;


    /// <summary>
    /// inventory is lock for inputs interactions
    /// </summary>
    public bool isLocked = false;

    public int FirstEmptySlotId
    {
        get
        {
            for (int i = 0; i < myItems.Length; i++)
            {
                if (myItems[i].key == null)
                    return i;
            }

            return int.MaxValue;
        }
    }

    public int FirstSlotWithItem(ItemData item)
    {
        for (int i = 0; i < myItems.Length; i++)
        {
            if (myItems[i].key == item)
                return i;
        }

        return int.MaxValue;
    }

    public void Lock(bool isTrue) => isLocked = isTrue;


    public void SelectNextSlot()
    {
        currentSelectedSlot++;
        if (currentSelectedSlot >= myItems.Length)
            currentSelectedSlot = 0;

        InventoryUI.Instance.SelectSlot(currentSelectedSlot);
    }

    public void SelectPreviousSlot()
    {
        currentSelectedSlot--;
        if (currentSelectedSlot < 0)
            currentSelectedSlot = myItems.Length - 1;

        InventoryUI.Instance.SelectSlot(currentSelectedSlot);
    }

    public void TryTakeClosestItem()
    {
        if (isLocked)
            return;

        if (itemsDetector.GetClosestItem() is Item itm)
        {
            TakeItem(itm, currentSelectedSlot);
        }
    }

    public void TakeItem(Item item, int slotId)
    {
        if (item == null)
            return;

        if (slotId >= myItems.Length)
            return;


        if (myItems[slotId].key == null)
        {
            myItems[slotId].key = item.SO;
            myItems[slotId].amount = 1;

            if (item)
                item.Take(transform.position);
            InventoryUI.Instance.UpdateItemSlot(slotId, myItems[slotId].key, myItems[slotId].amount);
            return;
        }


        if (myItems[slotId].key == item.SO)
        {
            myItems[slotId].amount++;
            item.Take(transform.position);

            InventoryUI.Instance.UpdateItemSlot(slotId, myItems[slotId].key, myItems[slotId].amount);
            return;
        }

        if (myItems[slotId].key != null)
        {
            DropAll(slotId);

            myItems[slotId].key = item.SO;
            myItems[slotId].amount = 1;
            item.Take(transform.position);
            InventoryUI.Instance.UpdateItemSlot(slotId, myItems[slotId].key, myItems[slotId].amount);
        }
    }

    public void TakeItem(ItemData item, int slotId)
    {
        if (item == null)
            return;

        if (slotId >= myItems.Length)
            return;


        if (myItems[slotId].key == null)
        {
            myItems[slotId].key = item;
            myItems[slotId].amount = 1;


            InventoryUI.Instance.UpdateItemSlot(slotId, myItems[slotId].key, myItems[slotId].amount);
            return;
        }


        if (myItems[slotId].key == item)
        {
            myItems[slotId].amount++;

            InventoryUI.Instance.UpdateItemSlot(slotId, myItems[slotId].key, myItems[slotId].amount);
            return;
        }

        if (myItems[slotId].key != null)
        {
            DropAll(slotId);

            myItems[slotId].key = item;
            myItems[slotId].amount = 1;
            InventoryUI.Instance.UpdateItemSlot(slotId, myItems[slotId].key, myItems[slotId].amount);
        }
    }


    public void DropCurrentItem()
    {
        if (isLocked)
            return;

        DropSingle(currentSelectedSlot);
    }

    public void UseItemToCrafting(int slotId)
    {
        myItems[slotId].amount--;
        if (myItems[slotId].amount <= 0)
            myItems[slotId].key = null;

        InventoryUI.Instance.UpdateItemSlot(slotId, myItems[slotId].key, myItems[slotId].amount);
    }

  

    /// <summary>
    /// remove item completely form list
    /// </summary>
    void DropAll(int slotId)
    {
        for (int i = 0; i < myItems[slotId].amount; i++)
        {
            ItemSpawnManager.Instance.DropAnItem(myItems[slotId].key, transform.position);
        }

        myItems[slotId].key = null;
        myItems[slotId].amount = 0;
    }

    void DropSingle(int slotId)
    {
        if (myItems[slotId].key == null || myItems[slotId].amount == 0)
            return;

        ItemSpawnManager.Instance.DropAnItem(myItems[slotId].key, transform.position);

        myItems[slotId].amount--;

        if (myItems[slotId].amount == 0)
        {
            myItems[slotId].key = null;
        }

        InventoryUI.Instance.UpdateItemSlot(slotId, myItems[slotId].key, myItems[slotId].amount);
    }

}
