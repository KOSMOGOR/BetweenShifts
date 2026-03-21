using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : SingletonMonoBehaviour<Inventory>
{
    public List<InventoryItem> items = new();
    public Transform inventoryRenderRoot;
    [Header("Inventory Items")]
    public GameObject inventoryItemUIPrefab;
    public Transform inventoryItemList;
    public Transform firstItemPosition;
    public float spacing;
    [Header("Inventory Item Info")]
    public TMP_Text itemNameText;
    public TMP_Text itemDescriptionText;

    bool isDisplayed = false;
    int currentlySelected = -1;
    InventoryState inventoryState;
    
    public int PromtSelected { get; private set; } = -1;

    protected override void AwakeNew() {
        inventoryRenderRoot.gameObject.SetActive(false);
    }

    void Update() {
        if (!isDisplayed) return;
        Move();
        Interact();
    }

    void Move() {
        currentlySelected -= Keyboard.current.wKey.wasPressedThisFrame ? 1 : 0;
        currentlySelected += Keyboard.current.sKey.wasPressedThisFrame ? 1 : 0;
        UpdateSelected();
    }

    void Interact() {
        if (inventoryState == InventoryState.PromtToChoose && items.Count == 0 && PromtSelected == -1) PromtSelected = 0;
        if (InputManager.ConsumeInteract()) {
            if (inventoryState == InventoryState.PromtToChoose) PromtSelected = currentlySelected;
        }
    }

    void UpdateSelected() {
        if (items.Count == 0) {
            currentlySelected = -1;
            itemNameText.text = "";
            itemDescriptionText.text = "";
        } else {
            currentlySelected = (currentlySelected + items.Count) % items.Count;
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

    public InventoryItem TryGetItem(int ind) {
        if (ind < 0 || ind >= items.Count) return null;
        return items[ind];
    }

    public InventoryItem TryGetPromtSelectedItem() {
        return TryGetItem(PromtSelected);
    }

    void CreateItemsListUI() {
        for (int i = 0; i < items.Count; i++) {
            GameObject inventoryItemUI = CreateInventoryItemUI(items[i]);
            inventoryItemUI.transform.position = firstItemPosition.position + -transform.up * (i * spacing);
        }
    }

    GameObject CreateInventoryItemUI(InventoryItem item) {
        GameObject inventoryItem = Instantiate(inventoryItemUIPrefab, inventoryItemList);
        inventoryItem.GetComponentInChildren<TMP_Text>().text = item.name;
        return inventoryItem;
    }

    public void DisplayInventory(InventoryState state = InventoryState.Regular) {
        inventoryState = state;
        isDisplayed = true;
        PromtSelected = -1;
        foreach (Transform child in inventoryItemList) Destroy(child.gameObject);
        CreateItemsListUI();
        inventoryRenderRoot.gameObject.SetActive(true);
        currentlySelected = 0;
        UpdateSelected();
    }

    public void HideInventory() {
        isDisplayed = false;
        inventoryRenderRoot.gameObject.SetActive(false);
    }

    public void ChangeInventoryVisibility() {
        if (!isDisplayed && Player.I.playerState == PlayerState.Free) {
            Player.I.playerState = PlayerState.Interacting;
            DisplayInventory();
        } else if (isDisplayed) {
            Player.I.playerState = PlayerState.Free;
            HideInventory();
        }
    }

    public enum InventoryState {
        Regular,
        PromtToChoose,
        PromtToCombine
    }
}
