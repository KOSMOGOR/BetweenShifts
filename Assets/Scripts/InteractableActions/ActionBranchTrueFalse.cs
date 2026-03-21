using System;

[Serializable]
abstract public class ActionBranchTrueFalse : ActionBranchBase
{
    public string trueLabel;
    public string falseLabel;

    abstract protected bool BranchCondition();

    protected override string SelectNextBranch() {
        return BranchCondition() ? trueLabel : falseLabel;
    }
}
