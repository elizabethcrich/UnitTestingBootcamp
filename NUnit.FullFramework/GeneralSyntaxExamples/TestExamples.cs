using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ProductionCode;

namespace NUnit.FullFramework
{

    [TestFixture]
    public class TestExamples
    {

        [Test]
        public void TestValidateWithValidCodeReturnsTrue()
        {
            //
            // Arrange
            //
            var inputString = "12345";
            var expectedResult = true;

            //
            // Act
            //
            var zipCodeValidator = new SimpleZipCodeValidator();
            var actualResult = zipCodeValidator.Validate(inputString);

            //
            // Assert
            //
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public void TestValidateWithAlphaCharsReturnsFalse()
        {
            // Arrange
            var inputString = "asdf";
            var expectedResult = false;

            // Act
            var zipCodeValidator = new SimpleZipCodeValidator();
            var actualResult = zipCodeValidator.Validate(inputString);

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }


        [Test]
        public void TestValidateWithSixCharStringReturnsFalse()
        {
            // Arrange
            var inputString = "123456";
            var expectedResult = false;

            // Act
            var zipCodeValidator = new SimpleZipCodeValidator();
            var actualResult = zipCodeValidator.Validate(inputString);

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }


        [Test]
        public void TestValidateWithNegativeNumberReturnsFalse()
        {
            // Arrange
            var inputString = "-12345";
            var expectedResult = false;

            // Act
            var zipCodeValidator = new SimpleZipCodeValidator();
            var actualResult = zipCodeValidator.Validate(inputString);

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }


        [Test]
        [TestCase("-12345", false)]
        [TestCase("123456", false)]
        [TestCase("asdf", false)]
        [TestCase("12345", true)]        
        public void TestValidateUsingTestCases(string inputString, bool expectedResult)
        {
            // Act
            var zipCodeValidator = new SimpleZipCodeValidator();
            var actualResult = zipCodeValidator.Validate(inputString);

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }
        
        [Test]
        [TestCase("2018/01/01 12:32:00")]
        [TestCase("2019/11/11 1:32:00")]
        public void TestCaseWithDateTime(string inputDateTimeString)
        {
            DateTime actualDateTime = DateTime.Parse(inputDateTimeString);

            MethodTakingADateTime(actualDateTime);

            void MethodTakingADateTime(DateTime input)
            {}


        }


        [Test]
        [TestCaseSource(nameof(InputStringTestCases))]
        public void TestValidate(string inputString, bool expectedResult)
        {
            // Act
            var zipCodeValidator = new SimpleZipCodeValidator();
            var actualResult = zipCodeValidator.Validate(inputString);

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        private static IEnumerable<object[]> InputStringTestCases()
        {
            yield return new object[] {"-12345", false};
            yield return new object[] {"123456", false};
            yield return new object[] {"asdf", false};
            yield return new object[] {"12345", true};
        }

    }

}
