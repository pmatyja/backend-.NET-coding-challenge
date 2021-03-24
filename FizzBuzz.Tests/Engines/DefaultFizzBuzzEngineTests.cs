using System;
using FizzBuzz.Rules;
using FizzBuzz.Outputs;
using FizzBuzz.Engines;
using NUnit.Framework;
using Moq;

namespace FizzBuzz.tests.Rules
{
    [TestFixture]
    public class DefaultFizzBuzzEngineTests
    {
        [Test]
        public void WhenFirstArgumentIsNull_ShouldThrowArgumentNullException()
        {
            // arrange
            // act
            TestDelegate func = () => new DefaultFizzBuzzEngine(null, this.outputMock.Object);

            // assert
            Assert.Throws<ArgumentNullException>(func);
        }

        [Test]
        public void WhenSecondArgumentIsNull_ShouldThrowArgumentNullException()
        {
            // arrange
            // act
            TestDelegate func = () => new DefaultFizzBuzzEngine(new IRule[0] {}, null);

            // assert
            Assert.Throws<ArgumentNullException>(func);
        }

        [Test]
        public void WhenThereAreNoRules_ShouldWriteOnlyNumbers()
        {
            // arrange
            var times = 3;
            var engine = new DefaultFizzBuzzEngine(new IRule[0] {}, this.outputMock.Object);

            // act
            engine.Run(times);

            // assert
            this.VerifyThatOutputWritePassedOnlyNumbers(times);
        }

        [Test]
        public void WhenThereAreRules_ShouldCallEachRuleExactlyOnceForEachNumbers()
        {
            // arrange
            var times = 2;
            var engine = new DefaultFizzBuzzEngine(new IRule[] { matchingRule.Object, nonMatchingRule.Object }, this.outputMock.Object);

            // act
            engine.Run(times);

            // assert
            this.VerifyThatCanHandleWasCalledNTimes(matchingRule, 2);
            this.VerifyThatCanHandleWasCalledNTimes(nonMatchingRule, 2);
        }

        [Test]
        public void WhenThereAreRules_ShouldCallEachRuleExactlyOnceForEachNumbersUsingValue()
        {
            // arrange
            var times = 2;
            var engine = new DefaultFizzBuzzEngine(new IRule[] { matchingRule.Object, nonMatchingRule.Object }, this.outputMock.Object);

            // act
            engine.Run(times);

            // assert
            this.VerifyThatCanHandleWasCalledOnceWithValue(matchingRule, 1);
            this.VerifyThatCanHandleWasCalledOnceWithValue(nonMatchingRule, 1);
            this.VerifyThatCanHandleWasCalledOnceWithValue(matchingRule, 2);
            this.VerifyThatCanHandleWasCalledOnceWithValue(nonMatchingRule, 2);
        }

        [Test]
        public void WhenRuleCanHandleReturnsFalse_ShouldNotCallHandle()
        {
            // arrange
            var times = 1;
            var engine = new DefaultFizzBuzzEngine(new IRule[] { nonMatchingRule.Object }, this.outputMock.Object);

            // act
            engine.Run(times);

            // assert
            this.VerifyThatHandleWasNeverCalled(nonMatchingRule);
        }

        [Test]
        public void WhenRuleCanHandleReturnsTrue_ShouldCallHandle()
        {
            // arrange
            var times = 1;
            var engine = new DefaultFizzBuzzEngine(new IRule[] { matchingRule.Object }, this.outputMock.Object);

            // act
            engine.Run(times);

            // assert
            this.VerifyThatHandleWasCalled(matchingRule);
        }

        [Test]
        public void WhenRuleCanHandleReturnsTrue_ShouldCallWriteOuputWithRuleValue()
        {
            // arrange
            var times = 1;
            var engine = new DefaultFizzBuzzEngine(new IRule[] { matchingRule.Object }, this.outputMock.Object);

            // act
            engine.Run(times);

            // assert
            this.VerifyThatOutputWriteWasCalledWithValue(Times.Once(), this.MathchingOutput);
        }

        private Mock<IOutput> outputMock;
        private Mock<IRule> matchingRule;
        private Mock<IRule> nonMatchingRule;

        private readonly string MathchingOutput = "Fizz";

        [SetUp]
        public void Setup()
        {
            this.outputMock = new Mock<IOutput>();

            this.matchingRule = new Mock<IRule>();
            this.matchingRule.Setup(x => x.CanHandle(It.IsAny<int>())).Returns(true);
            this.matchingRule.Setup(x => x.Handle(It.IsAny<int>())).Returns(this.MathchingOutput);

            this.nonMatchingRule = new Mock<IRule>();
            this.nonMatchingRule.Setup(x => x.CanHandle(It.IsAny<int>())).Returns(false);
        }

        private void VerifyThatOutputWritePassedOnlyNumbers(int times)
        {
            int num;
            this.outputMock.Verify(x => x.Write(It.IsAny<int>(), It.Is<string>(p => int.TryParse(p, out num))), Times.Exactly(times));
        }

        private void VerifyThatOutputWriteWasCalledWithValue(Times times, string value)
        {
            this.outputMock.Verify(x => x.Write(It.IsAny<int>(), It.Is<string>(p => p == value)), times);
        }

        private void VerifyThatCanHandleWasCalledNTimes(Mock<IRule> rule, int times)
        {
            rule.Verify(x => x.CanHandle(It.IsAny<int>()), Times.Exactly(times));
        }

        private void VerifyThatCanHandleWasCalledOnceWithValue(Mock<IRule> rule, int value)
        {
            rule.Verify(x => x.CanHandle(It.Is<int>(p => p == value)), Times.Once());
        }

        private void VerifyThatHandleWasCalled(Mock<IRule> rule)
        {
            rule.Verify(x => x.Handle(It.IsAny<int>()), Times.Once());
        }

        private void VerifyThatHandleWasNeverCalled(Mock<IRule> rule)
        {
            rule.Verify(x => x.Handle(It.IsAny<int>()), Times.Never());
        }
    }
}
