using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace NUnit.FullFramework
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void TestExample()
        {
            var expectedValue = new List<int> { 1, 3, 5, 7 };
            var actualValue = new int[] { 9, 7, 3, 1 };

            Assert.That(actualValue, Is.Not.EquivalentTo(expectedValue));
        }

        [Test]
        public void TestException()
        {
            var ex = Assert.Throws<NotImplementedException>(code: () => ThrowsException());
        }

        public void ThrowsException()
        {
            throw new NotImplementedException(message: "this broke");
        }
    }
}
