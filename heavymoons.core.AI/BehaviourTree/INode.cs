using heavymoons.core.AI.BlackBoard;

namespace heavymoons.core.AI.BehaviourTree
{
    public interface INode
    {
        DataStorage DataStorage { get; }
        string Name { get; }
        bool Execute(BehaviourMachine machine);
        void OnExecute(BehaviourMachine machine);
    }
}