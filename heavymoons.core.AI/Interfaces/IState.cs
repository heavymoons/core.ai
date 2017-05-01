namespace heavymoons.core.AI.Interfaces
{
    public interface IState : INode
    {
        IState NextState { get; set; }
    }
}