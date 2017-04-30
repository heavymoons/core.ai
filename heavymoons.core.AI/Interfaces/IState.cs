namespace heavymoons.core.AI.Interfaces
{
    public interface IState
    {
        IState CanChange(IMachine machine);
        string Name { get; }
        void Next(IMachine machine);
        void OnNext(IMachine machine, IState state);
        void OnRegister(IMachine machine, IState state);
        void OnExit(IMachine machine, IState state);
        void OnChange(IMachine machine, IState state);
    }
}