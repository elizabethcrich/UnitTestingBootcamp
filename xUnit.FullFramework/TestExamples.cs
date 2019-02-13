using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionCode;
using Xunit;

namespace xUnit.FullFramework
{
   
    public class TestExamples
    {

        [Fact]
        public void TestMethod()
        {
            // Arrange
            var zipCodeValidator = new SimpleZipCodeValidator();
            var inputString = "12345";
            var expectedResult = true;

            // Act
            var actualResult = zipCodeValidator.Validate(inputString);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }


    }
}
