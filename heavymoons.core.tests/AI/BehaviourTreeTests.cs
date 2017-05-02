using System;
using heavymoons.core.AI;
using NUnit.Framework;

namespace heavymoons.core.tests.AI
{
    [TestFixture]
    public class BehaviourTreeTests
    {
        [Test]
        public void SimpleSearcherTest()
        {
            const string hitpoint = "hp";
            const string distanceToTower = "distance";

            var machine = new StateMachine();
            machine.BlackBoard.Register(hitpoint, 100);
            machine.BlackBoard.Register(distanceToTower, 10);

            machine.OnExecuteEvent += (m, s) =>
            {
                var hp = m.BlackBoard.GetValue<int>(hitpoint);
                var distance = m.BlackBoard.GetValue<int>(distanceToTower);
                Console.WriteLine($"HP: {hp}");
                Console.WriteLine($"Distance: {distance}");
            };

            var behaviour = new BehaviourState();
            machine.RegisterState(behaviour);

            var selector = new SelectorNode();
            behaviour.Node = selector;

            var decoratorHp = new DecoratorNode();
            selector.Actions.Add(decoratorHp);

            decoratorHp.ConditionCallback = (m, s, n) => m.BlackBoard.GetValue<int>(hitpoint) > 5;
            var attackNearEnemy = new ActionNode();
            attackNearEnemy.ActionCallback = (m, s, n) =>
            {
                Console.WriteLine("近くの敵を攻撃");

                var distance = m.BlackBoard.GetValue<int>(distanceToTower);
                distance += 1;
                m.BlackBoard.SetValue(distanceToTower, distance);

                var hp = m.BlackBoard.GetValue<int>(hitpoint);
                hp -= 5;
                m.BlackBoard.SetValue(hitpoint, hp);
                return true;
            };
            decoratorHp.Action = attackNearEnemy;

            var sequencer = new SequencerNode();
            selector.Actions.Add(sequencer);

            var moveToNearTower = new ActionNode();
            sequencer.Actions.Add(moveToNearTower);

            moveToNearTower.ActionCallback = (m, s, n) =>
            {
                var distance = m.BlackBoard.GetValue<int>(distanceToTower);
                if (distance <= 0) return true;

                Console.WriteLine("近くのタワーに移動");
                distance--;
                m.BlackBoard.SetValue(distanceToTower, distance);
                return true;
            };

            var wait = new ActionNode();
            sequencer.Actions.Add(wait);

            wait.ActionCallback = (m, s, n) =>
            {
                Console.WriteLine("待機");

                var distance = m.BlackBoard.GetValue<int>(distanceToTower);
                if (distance < 5)
                {
                    var hp = m.BlackBoard.GetValue<int>(hitpoint);
                    hp += 20;
                    m.BlackBoard.SetValue(hitpoint, hp);
                }

                return true;
            };

            for (var i = 0; i < 100; i++)
            {
                machine.Execute();
            }
        }
    }
}