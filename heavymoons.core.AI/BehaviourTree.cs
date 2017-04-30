using heavymoons.core.AI.Interfaces;

namespace heavymoons.core.AI
{
    public class BehaviourTree : INode
    {
        public BlackBoard BlackBoard { get; } = new BlackBoard();

        public void Next()
        {
        }
    }
}