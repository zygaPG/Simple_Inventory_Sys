using UnityEngine;

[CreateAssetMenu(menuName = "Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;

    public Item prefab;
}

