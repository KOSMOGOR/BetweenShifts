using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItem", menuName = "Scriptable Objects/InventoryItem")]
public class InventoryItem : ScriptableObject
{
    public string itemName = "ItemName";
    public string itemDescription = "ItemDescription";
    public List<InventoryItem> combineRecipe = new();
}
