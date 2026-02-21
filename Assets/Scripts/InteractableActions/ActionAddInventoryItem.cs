using System;
using System.Collections;

[Serializable]
public class ActionAddInventoryItem : InteractableAction
{
    public InventoryItem itemToAdd;

    public override IEnumerator DoAction() {
        Inventory.I.AddItem(itemToAdd);
        DialogueRenderer.I.StartDialogue($"You got {itemToAdd.itemName}");
        while (!DialogueRenderer.I.DialogueDone) yield return null;
        DialogueRenderer.I.HideForAction(next);
    }
}
