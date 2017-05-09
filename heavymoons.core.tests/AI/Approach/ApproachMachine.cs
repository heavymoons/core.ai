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
            DataStorage[PlayerPosition] = new Point(10, 10);
            DataStorage[GoalPosition] = new Point(0, 0);
            DataStorage[Switch] = false;

            var switchOffState = new State();
            this[SwitchOff] = switchOffState;
            switchOffState.OnExecuteEvent = (machine, state) =>
            {
                Debug.WriteLine($"SwitchOff");
                if ((bool)machine.DataStorage[Switch])
                {
                    machine.NextStateName = SwitchOn;
                }
            };

            var switchOnState = new State();
            this[SwitchOn] = switchOnState;

            var behaviourMachine = new BehaviourMachine();
            behaviourMachine.DataStorage.ReferTo(DataStorage, PlayerPosition);
            behaviourMachine.DataStorage.ReferTo(DataStorage, GoalPosition);

            switchOnState.OnExecuteEvent = (machine, state) =>
            {
                Debug.WriteLine($"SwitchOn");
                behaviourMachine.Execute();
            };

            var moveOrTeleportSelector = new SelectorNode();
            behaviourMachine.RegisterRootNode(moveOrTeleportSelector);

            var moveDecorator = new DecoratorNode();
            moveOrTeleportSelector.ChildNodes.Add(moveDecorator);
            moveDecorator.ConditionCallback = (machine, node) =>
            {
                Debug.WriteLine($"move condition check");
                var playerPosition = (Point)machine.DataStorage[PlayerPosition];
                var goalPosition = (Point)machine.DataStorage[GoalPosition];
                Debug.WriteLine($"P:{playerPosition} G:{goalPosition}");
                return !playerPosition.Equals(goalPosition);
            };
            var moveAction = new ActionNode();
            moveDecorator.ChildNode = moveAction;
            moveAction.ActionCallback = (machine, node) =>
            {
                Debug.WriteLine($"Move");
                var playerPosition = (Point)machine.DataStorage[PlayerPosition];
                var goalPosition = (Point)machine.DataStorage[GoalPosition];
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
                machine.DataStorage[PlayerPosition] = playerPosition;
                return true;
            };

            var teleportAction = new ActionNode();
            teleportAction.DataStorage[TeleportCounter] = 10;
            moveOrTeleportSelector.ChildNodes.Add(teleportAction);
            teleportAction.ActionCallback = (machine, node) =>
            {
                Debug.WriteLine($"teleport");
                var teleportCounter = (int)node.DataStorage[TeleportCounter];
                teleportCounter--;
                node.DataStorage[TeleportCounter] = teleportCounter;
                Debug.WriteLine($"teleport counter: {teleportCounter}");
                if (teleportCounter > 0)
                {
                    return false;
                }

                var playerPosition = (Point) machine.DataStorage[PlayerPosition];
                playerPosition.X = new Random().Next(-10, 10);
                playerPosition.Y = new Random().Next(-10, 10);
                machine.DataStorage[PlayerPosition] = playerPosition;

                node.DataStorage[TeleportCounter] = 10;
                Debug.WriteLine($"teleport to {playerPosition}");
                return true;
            };
        }

    }
}