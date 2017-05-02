namespace heavymoons.core.AI.Interfaces
{
    public interface IState
    {
        BlackBoard BlackBoard { get; }
        string Name { get; }
        bool Execute(IMachine machine);
        void OnExecute(IMachine machine, IState state);
        void OnRegister(IMachine machine, IState state);
        void OnExit(IMachine machine, IState state);
        void OnEnter(IMachine machine, IState state);
        IState NextState { get; set; }
    }
}