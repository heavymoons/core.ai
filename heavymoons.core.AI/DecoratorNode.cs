﻿using heavymoons.core.AI.Interfaces;

namespace heavymoons.core.AI
{
    public class DecoratorNode : BaseNode
    {
        public ActionCallback ConditionCallback;
        public INode Action;

        public override bool Execute(IMachine machine, IState state)
        {
            OnExecute(machine, state, this);

            var result = ConditionCallback?.Invoke(machine, state, this) ?? false;
            if (result) return Action.Execute(machine, state);
            return false;
        }
    }
}