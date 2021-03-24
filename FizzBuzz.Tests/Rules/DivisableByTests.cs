using System;
using FizzBuzz.Rules;
using NUnit.Framework;

namespace FizzBuzz.tests.Rules
{
    [TestFixture]
    public class DivisableByTests
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void WhenInvalidOutputIsPasedToConstructor_ShouldThrowArgumentNullException(string output)
        {
            // arrange
            // act
            TestDelegate func = () => new DivisableBy(output, 111);

            // assert
            Assert.Throws<ArgumentNullException>(func);
        }

        [TestCase(0)]
        public void WhenInvalidDivisionByIsPasedToConstructor_ShouldThrowArgumenException(int divisionBy)
        {
            // arrange
            // act
            TestDelegate func = () => new DivisableBy("fizz", divisionBy);

            // assert
            Assert.Throws<ArgumentException>(func);
        }

        [TestCase(3, true)]
        [TestCase(5, false)]
        public void WhenValuePassed_ShouldCanHandleReturn(int value, bool expectedResult)
        {
            // arrange
            var rule = new DivisableBy("fizz", 3);

            // act
            var result = rule.CanHandle(value);

            // assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase(int.MinValue)]
        [TestCase(int.MaxValue)]
        [TestCase(123)]
        [TestCase(456)]
        public void WhenAnyValidValuePassed_ShoulHandleReturnFizz(int value)
        {
            // arrange
            var rule = new DivisableBy("fizz", 3);

            // act
            var result = rule.Handle(value);

            // assert
            Assert.That(result, Is.EqualTo("fizz"));
        }
    }
}
