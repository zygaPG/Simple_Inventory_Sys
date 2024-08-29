using System.Collections.Generic;
using UnityEngine;

public class ItemsDetector : MonoBehaviour
{
    // List to keep track of detected items
    [SerializeField] private List<Collider> itemsInRange = new List<Collider>();

    [SerializeField] bool autoOutlineSelection = true;

    Item clossestItem;

    [SerializeField] Color OutlineColor;

    void Update()
    {
        if (autoOutlineSelection)
        {
            if (clossestItem != null)
                clossestItem.SetOutline(false, OutlineColor);

            if (GetClosestItem() is Item item)
            {
                clossestItem = item;
                item.SetOutline(true, OutlineColor);
            }
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            if (!itemsInRange.Contains(other))
            {
                itemsInRange.Add(other);
            }
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            itemsInRange.Remove(other);
        }
    }

    public Item GetClosestItem()
    {
        CleanList();
        if (itemsInRange.Count == 0)
        {
            return null; // No items in range :/
        }

        Collider closestItem = null;
        float closestDistance = float.MaxValue;

        foreach (Collider item in itemsInRange)
        {
            float distance = Vector3.Distance(transform.position, item.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestItem = item;
            }
        }

        return closestItem.GetComponent<Item>();
    }

    void CleanList()
    {
        if (itemsInRange.Count == 0)
            return;

        List<Collider> itms = new List<Collider>();


        foreach (Collider cc in itemsInRange)
        {
            if (cc != null && cc.gameObject.activeInHierarchy)
                itms.Add(cc);
        }
        itemsInRange = itms;
    }
}
