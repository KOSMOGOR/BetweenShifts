using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : SingletonMonoBehaviour<Inventory>
{
    public List<InventoryItem> items = new();
    public GameObject inventoryItemUIPrefab;
    public Transform inventoryRenderRoot;
    public Transform inventoryItemList;
    public TMP_Text itemNameText;
    public TMP_Text itemDescriptionText;

    bool isDisplayed = false;
    int currentlySelected = -1;
    
    void OnEnable() { InputManager.I.Subscribe(gameObject); }
    void OnDisable() { InputManager.I.Unsubscribe(gameObject); }

    protected override void AwakeNew() {
        inventoryRenderRoot.gameObject.SetActive(false);
    }

#pragma warning disable IDE0051, IDE0060
    void OnMove(InputValue input) {
        if (Player.I.playerState != PlayerState.Interacting) return;
        Vector2 moveInput = input.Get<Vector2>();
        if (moveInput.y < 0) currentlySelected += 1;
        if (moveInput.y > 0) currentlySelected -= 1;
        UpdateSelected();
    }

    void OnInteract(InputValue input) {
        if (Player.I.playerState != PlayerState.Interacting) return;
    }
#pragma warning restore IDE0051, IDE0060

    void UpdateSelected() {
        if (items.Count == 0) currentlySelected = -1;
        else if (currentlySelected != -1) {
            currentlySelected %= items.Count;
            InventoryItem item = items[currentlySelected];
            itemNameText.text = item.itemName;
            itemDescriptionText.text = item.itemDescription;
        }
    }

    public void AddItem(InventoryItem item) {
        if (item != null && !items.Contains(item)) items.Add(item);
        UpdateSelected();
    }

    public void RemoveItem(InventoryItem item) {
        items.Remove(item);
        UpdateSelected();
    }

    GameObject CreateInventoryItemUI(InventoryItem item) {
        GameObject inventoryItem = Instantiate(inventoryItemUIPrefab, inventoryItemList);
        inventoryItem.GetComponentInChildren<TMP_Text>().text = item.name;
        return inventoryItem;
    }

    public void DisplayInventory() {
        if (Player.I.playerState != PlayerState.Free) return;
        Player.I.playerState = PlayerState.Interacting;
        isDisplayed = true;
        foreach (Transform child in inventoryItemList) Destroy(child.gameObject);
        foreach (InventoryItem item in items) CreateInventoryItemUI(item);
        inventoryRenderRoot.gameObject.SetActive(true);
        currentlySelected = 0;
        UpdateSelected();
    }

    public void HideInventory() {
        Player.I.playerState = PlayerState.Free;
        isDisplayed = false;
        inventoryRenderRoot.gameObject.SetActive(false);
    }

    public void ChangeInventoryVisibility() {
        if (!isDisplayed) DisplayInventory();
        else HideInventory();
    }
}
