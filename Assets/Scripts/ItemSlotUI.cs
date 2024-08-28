using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ItemSlotUI : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI amountText;

    [SerializeField] Image selectionStroke;

    public void SetItem(ItemData itm, int amount)
    {
        if (itm == null)
        {
            icon.enabled = false;
            amountText.enabled = false;
        }
        else
        {
            icon.enabled = true;
            amountText.enabled = amount > 1 ? true : false;

            icon.sprite = itm.icon;
            amountText.text = amount.ToString();
        }


    }

    public void Select()
    {
        selectionStroke.enabled = true;
    }

    public void Unselect()
    {
        selectionStroke.enabled = false;
    }
}
