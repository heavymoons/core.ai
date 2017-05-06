namespace heavymoons.core.AI.BehaviourTree
{
    public delegate void BehaviourMachineEvent(BehaviourMachine machine);

    public delegate void NodeEvent(BehaviourMachine machine, INode node, INode parentNode = null);

    public delegate bool NodeCallback(BehaviourMachine machine, INode node, INode parentNode = null);
}