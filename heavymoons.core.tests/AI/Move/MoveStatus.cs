namespace heavymoons.core.tests.AI.Move
{
    public class MoveStatus
    {
        public bool IsStop => Dx == 0;
        public bool IsMove => !IsStop;
        public bool IsGoal => X >= 100;
        public int X = 0;
        public int Dx = 0;
    }
}