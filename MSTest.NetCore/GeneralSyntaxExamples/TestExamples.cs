using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductionCode;

namespace MSTest.NetCore
{
    [TestClass]
    public class TestExamples
    {

        [TestMethod]
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
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestValidateWithAlphaCharsReturnsFalse()
        {
            // Arrange
            var inputString = "asdf";
            var expectedResult = false;

            // Act
            var zipCodeValidator = new SimpleZipCodeValidator();
            var actualResult = zipCodeValidator.Validate(inputString);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }


        [TestMethod]
        public void TestValidateWithSixCharStringReturnsFalse()
        {
            // Arrange
            var inputString = "123456";
            var expectedResult = false;

            // Act
            var zipCodeValidator = new SimpleZipCodeValidator();
            var actualResult = zipCodeValidator.Validate(inputString);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }


        [TestMethod]
        public void TestValidateWithNegativeNumberReturnsFalse()
        {
            // Arrange
            var inputString = "-12345";
            var expectedResult = false;

            // Act
            var zipCodeValidator = new SimpleZipCodeValidator();
            var actualResult = zipCodeValidator.Validate(inputString);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }


        [DataTestMethod]
        [DataRow("-12345", false)]
        [DataRow("123456", false)]
        [DataRow("asdf", false)]
        [DataRow("12345", true)]
        public void TestValidateUsingTestCases(string inputString, bool expectedResult)
        {
            // Act
            var zipCodeValidator = new SimpleZipCodeValidator();
            var actualResult = zipCodeValidator.Validate(inputString);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [DataTestMethod]
        [DataRow("2018/01/01 12:32:00")]
        [DataRow("2019/11/11 1:32:00")]
        public void TestCaseWithDateTime(string inputDateTimeString)
        {
            DateTime actualDateTime = DateTime.Parse(inputDateTimeString);

            MethodTakingADateTime(actualDateTime);

            void MethodTakingADateTime(DateTime input)
            { }


        }

        
        [TestMethod]
        [DynamicData(nameof(InputStringTestCases), DynamicDataSourceType.Method)]
        public void TestValidate(string inputString, bool expectedResult)
        {
            // Act
            var zipCodeValidator = new SimpleZipCodeValidator();
            var actualResult = zipCodeValidator.Validate(inputString);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        public static IEnumerable<object[]> InputStringTestCases()
        {
            yield return new object[] { "-12345", false };
            yield return new object[] { "123456", false };
            yield return new object[] { "asdf", false };
            yield return new object[] { "12345", true };
        }



    }
}
