using heavymoons.core.AI.BlackBoard;

namespace heavymoons.core.AI.BehaviourTree
{
    public interface INode
    {
        DataStorage DataStorage { get; }
        bool Execute(BehaviourMachine machine, INode parentNode);
        void OnExecute(BehaviourMachine machine, INode parentNode);
    }
}