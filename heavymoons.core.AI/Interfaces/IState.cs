namespace heavymoons.core.AI.Interfaces
{
    public interface IState
    {
        IState CanChange(IMachine machine);
        string Name { get; }
        void Next(IMachine machine);
        void OnRegister(IMachine machine);
        void OnExit(IMachine machine, IState state);
        void OnChange(IMachine machine, IState state);
    }
}