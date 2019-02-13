using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductionCode;

namespace MSTest.FullFramework
{
    [TestClass]
    public class TestExamples
    {

        [TestMethod]
        public void TestMethod()
        {
            // Arrange
            var zipCodeValidator = new SimpleZipCodeValidator();
            var inputString = "12345";
            var expectedResult = true;

            // Act
            var actualResult = zipCodeValidator.Validate(inputString);

            // Assert
            Assert.AreEqual(actualResult, expectedResult);
        }


    }
}
