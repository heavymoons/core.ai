using System;
using heavymoons.core.AI;
using heavymoons.core.tests.AI.Move;
using heavymoons.core.tests.AI.Switch;
using NUnit.Framework;

namespace heavymoons.core.tests.AI
{
    [TestFixture]
    public class StateMachineTests
    {
        [Test]
        public void SwitchTest()
        {
            var machine = new SwitchMachine();
            Assert.True(machine.IsState(typeof(SwitchOff)));
            machine.Execute();
            Assert.True(machine.IsState(typeof(SwitchOff)));
            machine.BlackBoard.SetValue(SwitchMachine.Switch, true);
            machine.Execute();
            Assert.True(machine.IsState(typeof(SwitchOn)));
            machine.BlackBoard.SetValue(SwitchMachine.Switch, false);
            machine.Execute();
            Assert.True(machine.IsState(typeof(SwitchOff)));
            machine.BlackBoard.SetValue(SwitchMachine.Switch, true);
            machine.Execute();
            machine.Execute();
            Assert.True(machine.IsState(typeof(SwitchOn)));
            machine.BlackBoard.SetValue(SwitchMachine.Switch, false);
            machine.Execute();
            machine.Execute();
            Assert.True(machine.IsState(typeof(SwitchOff)));
        }

        [Test]
        public void SwitchNoClassTest()
        {
            const string onOffSwitch = "switch";
            const string on = "on";
            const string off = "off";

            var machine = new StateMachine();
            machine.OnExecuteEvent += (m, s) =>
            {
                Console.WriteLine($"Counter: {m.Counter}");
            };

            machine.BlackBoard.Register(onOffSwitch, false);

            var offState = new State();
            offState.CanChangeCallback += (m) =>
            {
                if (m.BlackBoard.GetValue<bool>(onOffSwitch))
                {
                    return m.GetState(on);
                }
                return null;
            };
            machine.RegisterState(offState, off);

            var onState = new State();
            onState.CanChangeCallback += (m) =>
            {
                if (!m.BlackBoard.GetValue<bool>(SwitchMachine.Switch))
                {
                    return m.GetState(off);
                }
                return null;
            };
            machine.RegisterState(onState, on);

            Assert.True(machine.IsState(off));
            machine.Execute();
            Assert.True(machine.IsState(off));
            machine.BlackBoard.SetValue(onOffSwitch, true);
            machine.Execute();
            Assert.True(machine.IsState(on));
            machine.BlackBoard.SetValue(onOffSwitch, false);
            machine.Execute();
            Assert.True(machine.IsState(off));
            machine.BlackBoard.SetValue(onOffSwitch, true);
            machine.Execute();
            machine.Execute();
            Assert.True(machine.IsState(on));
            machine.BlackBoard.SetValue(onOffSwitch, false);
            machine.Execute();
            machine.Execute();
            Assert.True(machine.IsState(off));
        }

        [Test]
        public void MoveTest()
        {
            var machine = new MoveMachine();
            var status = machine.BlackBoard.GetValue<MoveStatus>(MoveMachine.Status);
            machine.Execute();
            Assert.True(machine.IsState(typeof(Stop)));
            Assert.False(status.IsGoal);
            Assert.True(status.IsStop);
            Assert.False(status.IsMove);
            Assert.AreEqual(0, status.X);
            Assert.AreEqual(0, status.Dx);
            status.Dx = 1;
            machine.Execute();
            Assert.True(machine.IsState(typeof(Move.Move)));
            Assert.False(status.IsGoal);
            Assert.False(status.IsStop);
            Assert.True(status.IsMove);
            Assert.AreEqual(1, status.X);
            Assert.AreEqual(1, status.Dx);
            machine.Execute();
            Assert.True(machine.IsState(typeof(Move.Move)));
            Assert.False(status.IsGoal);
            Assert.False(status.IsStop);
            Assert.True(status.IsMove);
            Assert.AreEqual(2, status.X);
            Assert.AreEqual(1, status.Dx);
            status.Dx = 0;
            machine.Execute();
            Assert.True(machine.IsState(typeof(Stop)));
            Assert.False(status.IsGoal);
            Assert.True(status.IsStop);
            Assert.False(status.IsMove);
            Assert.AreEqual(2, status.X);
            Assert.AreEqual(0, status.Dx);
            status.Dx = 10;
            machine.Execute();
            Assert.True(machine.IsState(typeof(Move.Move)));
            Assert.False(status.IsGoal);
            Assert.False(status.IsStop);
            Assert.True(status.IsMove);
            Assert.AreEqual(12, status.X);
            Assert.AreEqual(10, status.Dx);
            machine.Execute();
            machine.Execute();
            machine.Execute();
            machine.Execute();
            machine.Execute();
            machine.Execute();
            machine.Execute();
            machine.Execute();
            Assert.True(machine.IsState(typeof(Move.Move)));
            Assert.False(status.IsGoal);
            Assert.False(status.IsStop);
            Assert.True(status.IsMove);
            Assert.AreEqual(92, status.X);
            Assert.AreEqual(10, status.Dx);
            machine.Execute();
            Assert.True(machine.IsState(typeof(Move.Move)));
            Assert.True(status.IsGoal);
            Assert.False(status.IsStop);
            Assert.True(status.IsMove);
            Assert.AreEqual(102, status.X);
            Assert.AreEqual(10, status.Dx);
            machine.Execute();
            Assert.True(machine.IsState(typeof(Goal)));
            Assert.True(status.IsGoal);
            Assert.False(status.IsStop);
            Assert.True(status.IsMove);
            Assert.AreEqual(111, status.X);
            Assert.AreEqual(9, status.Dx);
            machine.Execute();
            Assert.AreEqual(8, status.Dx);
            machine.Execute();
            machine.Execute();
            machine.Execute();
            machine.Execute();
            machine.Execute();
            machine.Execute();
            Assert.AreEqual(2, status.Dx);
            machine.Execute();
            Assert.AreEqual(1, status.Dx);
            machine.Execute();
            Assert.AreEqual(0, status.Dx);
            machine.Execute();
            Assert.True(machine.IsState(typeof(Goal)));
            Assert.True(status.IsGoal);
            Assert.True(status.IsStop);
            Assert.False(status.IsMove);
            Assert.AreEqual(147, status.X);
            Assert.AreEqual(0, status.Dx);
            machine.Execute();
            Assert.True(machine.IsState(typeof(Goal)));
            Assert.True(status.IsGoal);
            Assert.True(status.IsStop);
            Assert.False(status.IsMove);
            Assert.AreEqual(147, status.X);
            Assert.AreEqual(0, status.Dx);
        }

    }
}