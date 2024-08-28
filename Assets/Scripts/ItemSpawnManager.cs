using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{
    public static ItemSpawnManager Instance;
    
    void Awake()
    {
        if(ItemSpawnManager.Instance != null)
            Debug.LogError("ItemSpawnManager conflict");
            
        Instance = this;
    }
    public void DropAnItem(ItemData data, Vector3 dropPosition)
    {
        dropPosition.y += 1;
        Item newItem = Instantiate(data.prefab, dropPosition, Quaternion.identity);
        newItem.Drop();
    }





}
