namespace heavymoons.core.AI.Interfaces
{
    public interface IState
    {
        BlackBoard BlackBoard { get; }
        IState CanChange(IMachine machine);
        string Name { get; }
        bool Execute(IMachine machine, IState state);
        void OnExecute(IMachine machine, IState state);
        void OnRegister(IMachine machine, IState state);
        void OnExit(IMachine machine, IState state);
        void OnChange(IMachine machine, IState state);
    }
}