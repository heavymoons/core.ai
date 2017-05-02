namespace heavymoons.core.AI.Interfaces
{
    public interface INode
    {
        BlackBoard BlackBoard { get; }
        string Name { get; }
        bool Execute(IMachine machine, IState state);
        void OnExecute(IMachine machine, IState state, INode node);
        void OnRegister(IMachine machine, IState state, INode node);
        void OnExit(IMachine machine, IState state, INode node);
        void OnEnter(IMachine machine, IState state, INode node);
    }
}