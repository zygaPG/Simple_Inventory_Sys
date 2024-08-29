using UnityEngine;

[CreateAssetMenu(menuName = "Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;

    public Item prefab;

    public ItemData craftingMaterial_A;
    public ItemData craftingMaterial_B;

    [Range(0,1)]public float chanceToCraft = 1;
}

