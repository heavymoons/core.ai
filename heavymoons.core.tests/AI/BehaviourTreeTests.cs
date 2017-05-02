using System;
using System.Diagnostics;
using heavymoons.core.AI;
using heavymoons.core.AI.BehaviourTree;
using heavymoons.core.AI.FiniteStateMachine;
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

            var machine = new BehaviourMachine();
            machine.DataStore.Register(hitpoint, 100);
            machine.DataStore.Register(distanceToTower, 10);

            machine.OnExecuteEvent += (m) =>
            {
                var hp = m.DataStore.GetValue<int>(hitpoint);
                var distance = m.DataStore.GetValue<int>(distanceToTower);
                Debug.WriteLine($"HP: {hp}");
                Debug.WriteLine($"Distance: {distance}");
            };

            var selector = new SelectorNode();
            machine.Node = selector;

            var decoratorHp = new DecoratorNode();
            selector.Nodes.Add(decoratorHp);

            decoratorHp.ConditionCallback = (m, n) => m.DataStore.GetValue<int>(hitpoint) > 5;
            var attackNearEnemy = new ActionNode();
            attackNearEnemy.ActionCallback = (m, n) =>
            {
                Debug.WriteLine("近くの敵を攻撃");

                var distance = m.DataStore.GetValue<int>(distanceToTower);
                distance += 1;
                m.DataStore.SetValue(distanceToTower, distance);

                var hp = m.DataStore.GetValue<int>(hitpoint);
                hp -= 5;
                m.DataStore.SetValue(hitpoint, hp);
                return true;
            };
            decoratorHp.Node = attackNearEnemy;

            var sequencer = new SequencerNode();
            selector.Nodes.Add(sequencer);

            var moveToNearTower = new ActionNode();
            sequencer.Nodes.Add(moveToNearTower);

            moveToNearTower.ActionCallback = (m, n) =>
            {
                var distance = m.DataStore.GetValue<int>(distanceToTower);
                if (distance <= 0) return true;

                Debug.WriteLine("近くのタワーに移動");
                distance--;
                m.DataStore.SetValue(distanceToTower, distance);
                return true;
            };

            var wait = new ActionNode();
            sequencer.Nodes.Add(wait);

            wait.ActionCallback = (m, n) =>
            {
                Debug.WriteLine("待機");

                var distance = m.DataStore.GetValue<int>(distanceToTower);
                if (distance < 5)
                {
                    var hp = m.DataStore.GetValue<int>(hitpoint);
                    hp += 20;
                    m.DataStore.SetValue(hitpoint, hp);
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