namespace heavymoons.core.AI.BehaviourTree
{
    public interface INode
    {
        DataStore DataStore { get; }
        string Name { get; }
        bool Execute(BehaviourMachine machine);
        void OnExecute(BehaviourMachine machine);
    }
}