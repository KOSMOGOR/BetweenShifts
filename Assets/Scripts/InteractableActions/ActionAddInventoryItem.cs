using System;
using System.Collections;

[Serializable]
public class ActionAddInventoryItem : ActionBase
{
    public InventoryItem itemToAdd;

    public override IEnumerator DoAction() {
        Inventory.I.AddItem(itemToAdd);
        SoundManager.I.PlaySound(GameSound.PickupItem);
        DialogueRenderer.I.StartDialogue($"Вы получили {itemToAdd.itemName}");
        while (!DialogueRenderer.I.DialogueDone) yield return null;
        DialogueRenderer.I.HideForAction(next);
    }
}
