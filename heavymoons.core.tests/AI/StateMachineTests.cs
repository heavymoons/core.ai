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
            machine.Next();
            Assert.True(machine.IsState(typeof(SwitchOff)));
            machine.BlackBoard.SetValue(SwitchMachine.Switch, true);
            machine.Next();
            Assert.True(machine.IsState(typeof(SwitchOn)));
            machine.BlackBoard.SetValue(SwitchMachine.Switch, false);
            machine.Next();
            Assert.True(machine.IsState(typeof(SwitchOff)));
            machine.BlackBoard.SetValue(SwitchMachine.Switch, true);
            machine.Next();
            machine.Next();
            Assert.True(machine.IsState(typeof(SwitchOn)));
            machine.BlackBoard.SetValue(SwitchMachine.Switch, false);
            machine.Next();
            machine.Next();
            Assert.True(machine.IsState(typeof(SwitchOff)));
        }

        [Test]
        public void SwitchNoClassTest()
        {
            const string Switch = "switch";
            const string On = "on";
            const string Off = "off";

            var machine = new StateMachine();

            machine.BlackBoard.Register(Switch, false);

            var off = new State();
            off.CanChangeCallback += (m) =>
            {
                if (m.BlackBoard.GetValue<bool>(Switch))
                {
                    return m.GetState(On);
                }
                return null;
            };
            machine.RegisterState(off, Off);

            var on = new State();
            on.CanChangeCallback += (m) =>
            {
                if (!m.BlackBoard.GetValue<bool>(SwitchMachine.Switch))
                {
                    return m.GetState(Off);
                }
                return null;
            };
            machine.RegisterState(on, On);

            Assert.True(machine.IsState(Off));
            machine.Next();
            Assert.True(machine.IsState(Off));
            machine.BlackBoard.SetValue(Switch, true);
            machine.Next();
            Assert.True(machine.IsState(On));
            machine.BlackBoard.SetValue(Switch, false);
            machine.Next();
            Assert.True(machine.IsState(Off));
            machine.BlackBoard.SetValue(Switch, true);
            machine.Next();
            machine.Next();
            Assert.True(machine.IsState(On));
            machine.BlackBoard.SetValue(Switch, false);
            machine.Next();
            machine.Next();
            Assert.True(machine.IsState(Off));
        }

        [Test]
        public void MoveTest()
        {
            var machine = new MoveMachine();
            var status = machine.BlackBoard.GetValue<MoveStatus>(MoveMachine.Status);
            machine.Next();
            Assert.True(machine.IsState(typeof(Stop)));
            Assert.False(status.IsGoal);
            Assert.True(status.IsStop);
            Assert.False(status.IsMove);
            Assert.AreEqual(0, status.X);
            Assert.AreEqual(0, status.Dx);
            status.Dx = 1;
            machine.Next();
            Assert.True(machine.IsState(typeof(Move.Move)));
            Assert.False(status.IsGoal);
            Assert.False(status.IsStop);
            Assert.True(status.IsMove);
            Assert.AreEqual(1, status.X);
            Assert.AreEqual(1, status.Dx);
            machine.Next();
            Assert.True(machine.IsState(typeof(Move.Move)));
            Assert.False(status.IsGoal);
            Assert.False(status.IsStop);
            Assert.True(status.IsMove);
            Assert.AreEqual(2, status.X);
            Assert.AreEqual(1, status.Dx);
            status.Dx = 0;
            machine.Next();
            Assert.True(machine.IsState(typeof(Stop)));
            Assert.False(status.IsGoal);
            Assert.True(status.IsStop);
            Assert.False(status.IsMove);
            Assert.AreEqual(2, status.X);
            Assert.AreEqual(0, status.Dx);
            status.Dx = 10;
            machine.Next();
            Assert.True(machine.IsState(typeof(Move.Move)));
            Assert.False(status.IsGoal);
            Assert.False(status.IsStop);
            Assert.True(status.IsMove);
            Assert.AreEqual(12, status.X);
            Assert.AreEqual(10, status.Dx);
            machine.Next();
            machine.Next();
            machine.Next();
            machine.Next();
            machine.Next();
            machine.Next();
            machine.Next();
            machine.Next();
            Assert.True(machine.IsState(typeof(Move.Move)));
            Assert.False(status.IsGoal);
            Assert.False(status.IsStop);
            Assert.True(status.IsMove);
            Assert.AreEqual(92, status.X);
            Assert.AreEqual(10, status.Dx);
            machine.Next();
            Assert.True(machine.IsState(typeof(Move.Move)));
            Assert.True(status.IsGoal);
            Assert.False(status.IsStop);
            Assert.True(status.IsMove);
            Assert.AreEqual(102, status.X);
            Assert.AreEqual(10, status.Dx);
            machine.Next();
            Assert.True(machine.IsState(typeof(Goal)));
            Assert.True(status.IsGoal);
            Assert.False(status.IsStop);
            Assert.True(status.IsMove);
            Assert.AreEqual(111, status.X);
            Assert.AreEqual(9, status.Dx);
            machine.Next();
            Assert.AreEqual(8, status.Dx);
            machine.Next();
            machine.Next();
            machine.Next();
            machine.Next();
            machine.Next();
            machine.Next();
            Assert.AreEqual(2, status.Dx);
            machine.Next();
            Assert.AreEqual(1, status.Dx);
            machine.Next();
            Assert.AreEqual(0, status.Dx);
            machine.Next();
            Assert.True(machine.IsState(typeof(Goal)));
            Assert.True(status.IsGoal);
            Assert.True(status.IsStop);
            Assert.False(status.IsMove);
            Assert.AreEqual(147, status.X);
            Assert.AreEqual(0, status.Dx);
            machine.Next();
            Assert.True(machine.IsState(typeof(Goal)));
            Assert.True(status.IsGoal);
            Assert.True(status.IsStop);
            Assert.False(status.IsMove);
            Assert.AreEqual(147, status.X);
            Assert.AreEqual(0, status.Dx);
        }

    }
}