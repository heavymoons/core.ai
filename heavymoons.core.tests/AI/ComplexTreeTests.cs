using heavymoons.core.tests.AI.Approach;
using NUnit.Framework;

namespace heavymoons.core.tests.AI
{
    [TestFixture]
    public class ComplexTreeTests
    {
        [Test]
        public void ApproachTest()
        {
            var approach = new ApproachMachine();

            Assert.True(approach.IsState("SwitchOff"));
            approach.Execute();
            Assert.True(approach.IsState("SwitchOff"));
            approach.BlackBoard.SetValue("Switch", true);
            approach.Execute();
            Assert.True(approach.IsState("SwitchOn"));
            approach.Execute();
            approach.Execute();
            approach.Execute();
            approach.Execute();
            approach.Execute();
            approach.Execute();
            approach.Execute();
            approach.Execute();
            approach.Execute();
            approach.Execute();
            approach.Execute();
            approach.Execute();
            approach.Execute();
            approach.Execute();
            approach.Execute();
            approach.Execute();
            approach.Execute();
            approach.Execute();
            approach.Execute();
            approach.Execute();
            approach.Execute();
            approach.Execute();
            approach.Execute();
            approach.Execute();
            approach.Execute();
            approach.Execute();
            approach.Execute();
            approach.Execute();
            approach.Execute();
            approach.Execute();
            approach.Execute();
            approach.Execute();
            approach.Execute();
            approach.Execute();
            approach.Execute();


        }

    }
}