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

    [SerializeField] ItemAmount[] myItems = new ItemAmount[6];

    private int currentSelectedSlot = 0;


    [SerializeField] ItemsDetector itemsDetector;

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
        if (itemsDetector.GetClosestItem() is Item itm)
        {
            TakeItem(itm, currentSelectedSlot);
        }
    }

    void TakeItem(Item item, int slotId)
    {
        if (item == null)
            return;

        if (slotId >= myItems.Length)
            return;


        if (myItems[slotId].key == null)
        {
            myItems[slotId].key = item.SO;
            myItems[slotId].amount = 1;

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


    public void DropCurrentItem()
    {
        DropSingle(currentSelectedSlot);
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
