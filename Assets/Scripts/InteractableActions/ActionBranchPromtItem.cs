using System;
using System.Collections;

[Serializable]
public class ActionBranchPromtItem : ActionBranchTrueFalse
{
    public InventoryItem targetItem;
    InventoryItem selectedItem = null;

    protected override IEnumerator BeforeBranch() {
        Inventory.I.DisplayInventory(Inventory.InventoryState.PromtToChoose);
        while (Inventory.I.PromtSelected == -1) yield return null;
        Inventory.I.HideInventory();
        selectedItem = Inventory.I.TryGetPromtSelectedItem();
    }

    protected override bool BranchCondition() {
        return selectedItem == targetItem;
    }
}
