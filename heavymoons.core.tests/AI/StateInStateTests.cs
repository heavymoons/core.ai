﻿using System;
using heavymoons.core.AI.FiniteStateMachine;
using NUnit.Framework;

namespace heavymoons.core.tests.AI
{
    [TestFixture]
    public class StateInStateTests
    {
        [Test]
        public void TreeOnOffTest()
        {
            var machine = new StateMachine();
            var on = new State()
            {
                OnExecuteEvent = (m, s) =>
                {
                    Console.WriteLine($"on");
                }
            };
            machine.RegisterState("on", on);
            var off = new State()
            {
                OnEnterEvent = (m, s) =>
                {
                    s.DataStorage["counter"] = 10;
                },
                OnExecuteEvent = (m, s) =>
                {
                    Console.WriteLine($"off");
                    var counter = (int) s.DataStorage["counter"];
                    counter--;
                    s.DataStorage["counter"] = counter;
                    Console.WriteLine($"counter = {counter}");
                    if (counter <= 0)
                    {
                        m.NextState = "on";
                    }
                }
            };
            off.DataStorage["counter"] = 10;
            machine.RegisterState("off", off);
            machine.ForceChangeState("off");

            var machine2 = new StateMachine();
            on.StateMachine = machine2;

            var on2 = new State()
            {
                OnEnterEvent = (m, s) =>
                {
                    s.DataStorage["counter"] = 10;
                },
                OnExecuteEvent = (m, s) =>
                {
                    Console.WriteLine($"on2");
                    var counter = (int) s.DataStorage["counter"];
                    counter--;
                    s.DataStorage["counter"] = counter;
                    Console.WriteLine($"counter = {counter}");
                    if (counter <= 0)
                    {
                        m.ParentStateMachine.NextState = "off";
                        m.NextState = "off";
                    }
                }
            };

            machine2.RegisterState("on", on2);
            var off2 = new State()            {
                OnEnterEvent = (m, s) =>
                {
                    s.DataStorage["counter"] = 10;
                },
                OnExecuteEvent = (m, s) =>
                {
                    Console.WriteLine($"off2");
                    var counter = (int) s.DataStorage["counter"];
                    counter--;
                    s.DataStorage["counter"] = counter;
                    Console.WriteLine($"counter = {counter}");
                    if (counter <= 0)
                    {
                        m.NextState = "on";
                    }
                }
            };
            off2.DataStorage["counter"] = 10;
            machine2.RegisterState("off", off2);
            machine2.ForceChangeState("off");

            Assert.AreEqual("off", machine.CurrentState);
            Assert.AreEqual("off", machine2.CurrentState);
            for (var i = 0; i < 10; i++)
            {
                machine.Execute();
            }
            Assert.AreEqual("on", machine.CurrentState);
            Assert.AreEqual("off", machine2.CurrentState);
            for (var i = 0; i < 10; i++)
            {
                machine.Execute();
            }
            Assert.AreEqual("on", machine.CurrentState);
            Assert.AreEqual("on", machine2.CurrentState);
            for (var i = 0; i < 10; i++)
            {
                machine.Execute();
            }
            Assert.AreEqual("off", machine.CurrentState);
            Assert.AreEqual("off", machine2.CurrentState);
            for (var i = 0; i < 10; i++)
            {
                machine.Execute();
            }
            Assert.AreEqual("on", machine.CurrentState);
            Assert.AreEqual("off", machine2.CurrentState);
        }
    }
}