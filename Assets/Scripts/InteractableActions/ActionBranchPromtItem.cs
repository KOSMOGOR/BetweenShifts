using System;
using System.Collections;

[Serializable]
public class ActionBranchPromtItem : ActionBranchTrueFalse
{
    public InventoryItem targetItem;
    InventoryItem selectedItem = null;
    public bool removeRightItemAfterPromt = false;

    protected override IEnumerator BeforeBranch() {
        Inventory.I.DisplayInventory(Inventory.InventoryState.PromtToChoose);
        while (Inventory.I.PromtSelected == -1) yield return null;
        Inventory.I.HideInventory();
        selectedItem = Inventory.I.TryGetPromtSelectedItem();
    }

    protected override IEnumerator AfterBranch() {
        if (selectedItem == targetItem) {
            if (removeRightItemAfterPromt) Inventory.I.RemoveItem(targetItem);
            SoundManager.I.PlaySound(GameSound.GhostGiveItem);
        }
        yield return null;
    }

    protected override bool BranchCondition() {
        return selectedItem == targetItem;
    }
}
