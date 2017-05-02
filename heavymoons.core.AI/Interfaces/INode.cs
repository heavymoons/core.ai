namespace heavymoons.core.AI.Interfaces
{
    public interface INode
    {
        BlackBoard BlackBoard { get; }
        string Name { get; }
        bool Execute(IMachine machine, IState state);
        void OnExecute(IMachine machine, IState state, INode node);
    }
}