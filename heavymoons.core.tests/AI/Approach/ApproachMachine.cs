using System;
using System.Drawing;
using heavymoons.core.AI;

namespace heavymoons.core.tests.AI.Approach
{
    public class ApproachMachine : StateMachine
    {
        private const string PlayerPosition = "PlayerPosition";
        private const string GoalPosition = "GoalPosition";
        private const string Switch = "Switch";
        private const string TeleportCounter = "TeleportCounter";

        private const string SwitchOn = "SwitchOn";
        private const string SwitchOff = "SwitchOff";

        public ApproachMachine()
        {
            BlackBoard.Register(PlayerPosition, new Point(10, 10));
            BlackBoard.Register(GoalPosition, new Point(0, 0));
            BlackBoard.Register(Switch, false);

            var switchOffState = new State(SwitchOff);
            RegisterState(switchOffState);
            switchOffState.OnExecuteEvent = (machine, state) =>
            {
                Console.WriteLine($"SwitchOff");
                if (machine.BlackBoard.GetValue<bool>(Switch))
                {
                    state.NextState = machine.GetState(SwitchOn);
                }
            };

            var switchOnState = new BehaviourState(SwitchOn);
            RegisterState(switchOnState);
            switchOnState.OnExecuteEvent = (machine, state) =>
            {
                Console.WriteLine($"SwitchOn");
            };

            var moveOrTeleportSelector = new SelectorNode();
            switchOnState.Node = moveOrTeleportSelector;

            var moveDecorator = new DecoratorNode();
            moveOrTeleportSelector.Actions.Add(moveDecorator);
            moveDecorator.ConditionCallback = (machine, state, node) =>
            {
                Console.WriteLine($"move condition check");
                var playerPosition = machine.BlackBoard.GetValue<Point>(PlayerPosition);
                var goalPosition = machine.BlackBoard.GetValue<Point>(GoalPosition);
                Console.WriteLine($"P:{playerPosition} G:{goalPosition}");
                return !playerPosition.Equals(goalPosition);
            };
            var moveAction = new ActionNode();
            moveDecorator.Action = moveAction;
            moveAction.ActionCallback = (machine, state, node) =>
            {
                Console.WriteLine($"Move");
                var playerPosition = machine.BlackBoard.GetValue<Point>(PlayerPosition);
                var goalPosition = machine.BlackBoard.GetValue<Point>(GoalPosition);
                var diffX = Math.Abs(playerPosition.X - goalPosition.X);
                var diffY = Math.Abs(playerPosition.Y - goalPosition.Y);
                if (diffX >= diffY)
                {
                    playerPosition.X += playerPosition.X > goalPosition.X ? -1 : 1;
                }
                else
                {
                    playerPosition.Y += playerPosition.Y > goalPosition.Y ? -1 : 1;
                }
                machine.BlackBoard.SetValue(PlayerPosition, playerPosition);
                return true;
            };

            var teleportAction = new ActionNode();
            teleportAction.BlackBoard.Register(TeleportCounter, 10);
            moveOrTeleportSelector.Actions.Add(teleportAction);
            teleportAction.ActionCallback = (machine, state, node) =>
            {
                Console.WriteLine($"teleport");
                var teleportCounter = node.BlackBoard.GetValue<int>(TeleportCounter);
                teleportCounter--;
                node.BlackBoard.SetValue(TeleportCounter, teleportCounter);
                Console.WriteLine($"teleport counter: {teleportCounter}");
                if (teleportCounter > 0)
                {
                    return false;
                }

                var playerPosition = machine.BlackBoard.GetValue<Point>(PlayerPosition);
                playerPosition.X = new Random().Next(-10, 10);
                playerPosition.Y = new Random().Next(-10, 10);
                machine.BlackBoard.SetValue(PlayerPosition, playerPosition);

                node.BlackBoard.SetValue(TeleportCounter, 10);
                Console.WriteLine($"teleport to {playerPosition}");
                return true;
            };
        }

    }
}