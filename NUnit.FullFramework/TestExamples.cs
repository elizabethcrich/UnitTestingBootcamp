using System;
using System.Collections.Generic;
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
        public void TestMethod()
        {
            // Arrange
            var zipCodeValidator = new SimpleZipCodeValidator();
            var inputString = "12345";
            var expectedResult = true;

            // Act
            var actualResult = zipCodeValidator.Validate(inputString);

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }


    }
}
