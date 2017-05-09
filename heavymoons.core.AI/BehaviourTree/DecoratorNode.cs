namespace heavymoons.core.AI.BehaviourTree
{
    public class DecoratorNode : BaseNode
    {
        /// <summary>
        /// 条件を満たすかどうか判断するコールバック
        /// </summary>
        public NodeCallback ConditionCallback { get; set; }

        /// <summary>
        /// 条件を満たしたときに実行するノード
        /// </summary>
        public INode ChildNode { get; set; }

        public override bool Execute(BehaviourMachine machine, INode parentNode)
        {
            base.Execute(machine, parentNode);

            var result = ConditionCallback?.Invoke(machine, this) ?? false;
            if (result) return ChildNode.Execute(machine, this);

            return false;
        }
    }
}