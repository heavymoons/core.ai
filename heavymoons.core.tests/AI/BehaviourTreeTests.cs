using System;
using heavymoons.core.AI;
using NUnit.Framework;
using Action = heavymoons.core.AI.Action;

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

            var selector = new Selector();
            machine.RegisterState(selector);

            var decoratorHp = new Decorator();
            selector.Actions.Add(decoratorHp);

            decoratorHp.ConditionCallback = (m, s) => m.BlackBoard.GetValue<int>(hitpoint) > 5;
            var attackNearEnemy = new Action();
            attackNearEnemy.ActionCallback = (m, s) =>
            {
                Console.WriteLine("近くの敵を攻撃");
                var hp = m.BlackBoard.GetValue<int>(hitpoint);
                hp -= 5;
                m.BlackBoard.SetValue(hitpoint, hp);
                return true;
            };
            decoratorHp.Action = attackNearEnemy;

            var sequencer = new Sequencer();
            selector.Actions.Add(sequencer);

            var moveToNearTower = new Action();
            sequencer.Actions.Add(moveToNearTower);

            moveToNearTower.ActionCallback = (m, s) =>
            {
                var distance = m.BlackBoard.GetValue<int>(distanceToTower);
                if (distance <= 0) return true;

                Console.WriteLine("近くのタワーに移動");
                distance--;
                m.BlackBoard.SetValue(distanceToTower, distance);
                return true;
            };

            var wait = new Action();
            sequencer.Actions.Add(wait);

            wait.ActionCallback = (m, s) =>
            {
                Console.WriteLine("待機");
                return true;
            };

            for (var i = 0; i < 100; i++)
            {
                machine.Execute();
            }

        }

    }
}