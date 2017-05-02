using System;
using System.Diagnostics;
using heavymoons.core.AI;
using heavymoons.core.AI.FiniteStateMachine;
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
            Assert.True(machine.IsCurrentState("SwitchOff"));
            machine.Execute();
            Assert.True(machine.IsCurrentState("SwitchOff"));
            machine.DataStore.SetValue(SwitchMachine.Switch, true);
            machine.Execute();
            Assert.True(machine.IsCurrentState("SwitchOn"));
            machine.DataStore.SetValue(SwitchMachine.Switch, false);
            machine.Execute();
            Assert.True(machine.IsCurrentState("SwitchOff"));
            machine.DataStore.SetValue(SwitchMachine.Switch, true);
            machine.Execute();
            machine.Execute();
            Assert.True(machine.IsCurrentState("SwitchOn"));
            machine.DataStore.SetValue(SwitchMachine.Switch, false);
            machine.Execute();
            machine.Execute();
            Assert.True(machine.IsCurrentState("SwitchOff"));
        }

        [Test]
        public void SwitchNoClassTest()
        {
            const string onOffSwitch = "switch";
            const string on = "on";
            const string off = "off";

            var machine = new StateMachine();
            machine.OnExecuteEvent += (m) => { Debug.WriteLine($"Counter: {m.Counter}"); };

            machine.DataStore.Register(onOffSwitch, false);

            var offState = new State();
            offState.OnExecuteEvent += (m, s) =>
            {
                if (m.DataStore.GetValue<bool>(onOffSwitch))
                {
                    m.NextState = on;
                }
            };
            machine.RegisterState(off, offState);

            var onState = new State();
            onState.OnExecuteEvent += (m, s) =>
            {
                if (!m.DataStore.GetValue<bool>(onOffSwitch))
                {
                    m.NextState = off;
                }
            };
            machine.RegisterState(on, onState);

            Assert.True(machine.IsCurrentState(off));
            machine.Execute();
            Assert.True(machine.IsCurrentState(off));
            machine.DataStore.SetValue(onOffSwitch, true);
            machine.Execute();
            Assert.True(machine.IsCurrentState(on));
            machine.DataStore.SetValue(onOffSwitch, false);
            machine.Execute();
            Assert.True(machine.IsCurrentState(off));
            machine.DataStore.SetValue(onOffSwitch, true);
            machine.Execute();
            machine.Execute();
            Assert.True(machine.IsCurrentState(on));
            machine.DataStore.SetValue(onOffSwitch, false);
            machine.Execute();
            machine.Execute();
            Assert.True(machine.IsCurrentState(off));
        }

        [Test]
        public void MoveTest()
        {
            var machine = new MoveMachine();
            var status = machine.DataStore.GetValue<MoveStatus>();
            machine.Execute();
            Assert.True(machine.IsCurrentState("Stop"));
            Assert.False(status.IsGoal);
            Assert.True(status.IsStop);
            Assert.False(status.IsMove);
            Assert.AreEqual(0, status.X);
            Assert.AreEqual(0, status.Dx);
            status.Dx = 1;
            machine.Execute();
            Assert.True(machine.IsCurrentState("Move"));
            Assert.False(status.IsGoal);
            Assert.False(status.IsStop);
            Assert.True(status.IsMove);
            Assert.AreEqual(0, status.X);
            Assert.AreEqual(1, status.Dx);
            machine.Execute();
            Assert.True(machine.IsCurrentState("Move"));
            Assert.False(status.IsGoal);
            Assert.False(status.IsStop);
            Assert.True(status.IsMove);
            Assert.AreEqual(1, status.X);
            Assert.AreEqual(1, status.Dx);
            status.Dx = 0;
            machine.Execute();
            Assert.True(machine.IsCurrentState("Stop"));
            Assert.False(status.IsGoal);
            Assert.True(status.IsStop);
            Assert.False(status.IsMove);
            Assert.AreEqual(1, status.X);
            Assert.AreEqual(0, status.Dx);
            status.Dx = 10;
            machine.Execute();
            Assert.True(machine.IsCurrentState("Move"));
            Assert.False(status.IsGoal);
            Assert.False(status.IsStop);
            Assert.True(status.IsMove);
            Assert.AreEqual(1, status.X);
            Assert.AreEqual(10, status.Dx);
            machine.Execute();
            machine.Execute();
            machine.Execute();
            machine.Execute();
            machine.Execute();
            machine.Execute();
            machine.Execute();
            machine.Execute();
            machine.Execute();
            Assert.True(machine.IsCurrentState("Move"));
            Assert.False(status.IsGoal);
            Assert.False(status.IsStop);
            Assert.True(status.IsMove);
            Assert.AreEqual(91, status.X);
            Assert.AreEqual(10, status.Dx);
            machine.Execute();
            Assert.True(machine.IsCurrentState("Goal"));
            Assert.True(status.IsGoal);
            Assert.False(status.IsStop);
            Assert.True(status.IsMove);
            Assert.AreEqual(101, status.X);
            Assert.AreEqual(10, status.Dx);
            machine.Execute();
            Assert.True(machine.IsCurrentState("Goal"));
            Assert.True(status.IsGoal);
            Assert.False(status.IsStop);
            Assert.True(status.IsMove);
            Assert.AreEqual(110, status.X);
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
            Assert.True(machine.IsCurrentState("Goal"));
            Assert.True(status.IsGoal);
            Assert.True(status.IsStop);
            Assert.False(status.IsMove);
            Assert.AreEqual(146, status.X);
            Assert.AreEqual(0, status.Dx);
            machine.Execute();
            Assert.True(machine.IsCurrentState("Goal"));
            Assert.True(status.IsGoal);
            Assert.True(status.IsStop);
            Assert.False(status.IsMove);
            Assert.AreEqual(146, status.X);
            Assert.AreEqual(0, status.Dx);
        }
    }
}