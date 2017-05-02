using System;
using System.Diagnostics;
using System.Drawing;
using heavymoons.core.AI;
using heavymoons.core.AI.BehaviourTree;
using heavymoons.core.AI.FiniteStateMachine;

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
            DataStore.Register(PlayerPosition, new Point(10, 10));
            DataStore.Register(GoalPosition, new Point(0, 0));
            DataStore.Register(Switch, false);

            var switchOffState = new State();
            RegisterState(SwitchOff, switchOffState);
            switchOffState.OnExecuteEvent = (machine, state) =>
            {
                Debug.WriteLine($"SwitchOff");
                if (machine.DataStore.GetValue<bool>(Switch))
                {
                    machine.NextState = SwitchOn;
                }
            };

            var switchOnState = new State();
            RegisterState(SwitchOn, switchOnState);

            var behaviourMachine = new BehaviourMachine();
            behaviourMachine.DataStore.Override(DataStore, PlayerPosition);
            behaviourMachine.DataStore.Override(DataStore, GoalPosition);

            switchOnState.OnExecuteEvent = (machine, state) =>
            {
                Debug.WriteLine($"SwitchOn");
                behaviourMachine.Execute();
            };

            var moveOrTeleportSelector = new SelectorNode();
            behaviourMachine.Node = moveOrTeleportSelector;

            var moveDecorator = new DecoratorNode();
            moveOrTeleportSelector.Nodes.Add(moveDecorator);
            moveDecorator.ConditionCallback = (machine, node) =>
            {
                Debug.WriteLine($"move condition check");
                var playerPosition = machine.DataStore.GetValue<Point>(PlayerPosition);
                var goalPosition = machine.DataStore.GetValue<Point>(GoalPosition);
                Debug.WriteLine($"P:{playerPosition} G:{goalPosition}");
                return !playerPosition.Equals(goalPosition);
            };
            var moveAction = new ActionNode();
            moveDecorator.Node = moveAction;
            moveAction.ActionCallback = (machine, node) =>
            {
                Debug.WriteLine($"Move");
                var playerPosition = machine.DataStore.GetValue<Point>(PlayerPosition);
                var goalPosition = machine.DataStore.GetValue<Point>(GoalPosition);
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
                machine.DataStore.SetValue(PlayerPosition, playerPosition);
                return true;
            };

            var teleportAction = new ActionNode();
            teleportAction.DataStore.Register(TeleportCounter, 10);
            moveOrTeleportSelector.Nodes.Add(teleportAction);
            teleportAction.ActionCallback = (machine, node) =>
            {
                Debug.WriteLine($"teleport");
                var teleportCounter = node.DataStore.GetValue<int>(TeleportCounter);
                teleportCounter--;
                node.DataStore.SetValue(TeleportCounter, teleportCounter);
                Debug.WriteLine($"teleport counter: {teleportCounter}");
                if (teleportCounter > 0)
                {
                    return false;
                }

                var playerPosition = machine.DataStore.GetValue<Point>(PlayerPosition);
                playerPosition.X = new Random().Next(-10, 10);
                playerPosition.Y = new Random().Next(-10, 10);
                machine.DataStore.SetValue(PlayerPosition, playerPosition);

                node.DataStore.SetValue(TeleportCounter, 10);
                Debug.WriteLine($"teleport to {playerPosition}");
                return true;
            };
        }

    }
}