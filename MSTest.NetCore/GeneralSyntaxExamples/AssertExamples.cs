using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSTest.NetCore
{
    [TestClass]
    public class AssertExamples
    {
        /// <summary>
        /// Assertions that test for equality using object.Equals() to compare the actual value
        /// to the expected value
        /// </summary>
        [TestMethod]
        public void EqualityChecks()
        {

            bool valueToTest_bool = true;
            string valueToTest_string = "some result";
            DateTime valueToTest_datetime = new DateTime(2019, 01, 01);

            var valueToTest_obj = new { Foo = "bar", Baz = true };
            var expectedValue_obj_equal = new { Foo = "bar", Baz = true };
            var expectedValue_obj_notequal = new { Foo = "zoom", Baz = false }; ;


            //    (important: expected value comes first!)
            Assert.AreEqual(true, valueToTest_bool);
            Assert.AreEqual("some result", valueToTest_string);
            Assert.AreEqual(new DateTime(2019, 01, 01), valueToTest_datetime);
            Assert.AreEqual(expectedValue_obj_equal, valueToTest_obj);

            Assert.AreNotEqual(false, valueToTest_bool);
            Assert.AreNotEqual("some other result", valueToTest_string);
            Assert.AreNotEqual(new DateTime(2019, 12, 01), valueToTest_datetime);
            Assert.AreNotEqual(expectedValue_obj_notequal, valueToTest_obj);
        }


        /// <summary>
        /// Assertions that test for equality using object.ReferenceEquals() to determine if both values point to
        /// the exact same object.
        /// </summary>
        [TestMethod]
        public void SameObjectChecks()
        {
            var valueToTest = new { Foo = "bar", Baz = true };
            var expectedValue_same = valueToTest;
            var expectedValue_notsame = new { Foo = "bar", Baz = true }; ;


            // (important: expected value comes first!)
            Assert.AreSame(expectedValue_same, valueToTest);
            Assert.AreNotSame(expectedValue_notsame, valueToTest);

        }

        /// <summary>
        /// Assertions that test for null values
        /// </summary>
        [TestMethod]
        public void NullChecks()
        {
            var valueToTest = new {Foo = (object) null, Baz = new object()};

            Assert.IsNull(valueToTest.Foo);
            Assert.IsNotNull(valueToTest.Baz);
        }

        /// <summary>
        /// Assertions that compare the value to a set of constraints
        /// </summary>
        [TestMethod]
        public void ComparisonChecks()
        {
            int bigNumber = int.MaxValue;
            int smallNumber = int.MinValue;

            bool trueValue = true;
            bool falseValue = false;

            Assert.IsTrue(bigNumber > smallNumber);
            Assert.IsTrue(bigNumber >= smallNumber);

            Assert.IsTrue(smallNumber < bigNumber);
            Assert.IsTrue(smallNumber <= bigNumber);

            Assert.IsTrue(trueValue);
            Assert.IsFalse(falseValue);

        }

        /// <summary>
        /// String-specific checks
        /// </summary>
        [TestMethod]
        public void StringChecks()
        {
            var valueToTest = "Foo Bar Baz Bin";

            StringAssert.Contains(valueToTest, "Bar");
            StringAssert.StartsWith(valueToTest, "Foo");
            StringAssert.EndsWith(valueToTest, "Bin");
            StringAssert.Matches(valueToTest, new Regex( "^Foo.*Bin$"));
            StringAssert.DoesNotMatch(valueToTest, new Regex( "^Foo.*Bain$")); 

        }

        /// <summary>
        /// Tests related to object types and inheritance
        /// </summary>
        [TestMethod]
        public void TypeChecks()
        {
            IList<string> stringList = new List<string>();
            IEnumerable<int> intEnumerable = new int[] { };
            
            Assert.IsInstanceOfType(stringList, typeof(List<string>));
            Assert.IsNotInstanceOfType(intEnumerable, typeof(List<int>));
          
        }

        /// <summary>
        /// Checks specific to collections
        /// </summary>
        [TestMethod]
        public void CollectionChecks()
        {
            var objArr = new object[] { new object(), 42, "my string" };
            var stringArr = new object[] { "foo", "bar", "baz", "bin", "" };
            var intList = Enumerable.Range(0, 100).ToList();

            CollectionAssert.AllItemsAreInstancesOfType(stringArr, typeof(string));
            CollectionAssert.AllItemsAreNotNull(objArr);

            //CollectionAssert.AllItemsAreUnique(intList);

            CollectionAssert.AreEqual(Enumerable.Range(0, 100).ToList(), intList);
            CollectionAssert.AreNotEqual(Enumerable.Range(1, 5).ToList(), intList);

            CollectionAssert.AreEquivalent(new string[] { "bar", "baz", "", "bin", "foo" }, stringArr);
            CollectionAssert.AreNotEquivalent(new string[] { "bar", "baz" }, stringArr);

            CollectionAssert.Contains(stringArr, "foo");
            CollectionAssert.DoesNotContain(stringArr, "zoom");

            CollectionAssert.IsSubsetOf(Enumerable.Range(5, 20).ToList(), intList);
            CollectionAssert.IsNotSubsetOf(Enumerable.Range(-1, 1).ToList(), intList);

        }


        /// <summary>
        /// Exception-specific checks
        /// </summary>
        [TestMethod]
        public void ExceptionChecks()
        {
            void MethodThatThrows() { throw new ArgumentException(); }

          
            Assert.ThrowsException<ArgumentException>(() => MethodThatThrows());
            Assert.ThrowsException<ArgumentException>(() => throw new ArgumentException());

            Exception ex = Assert.ThrowsException<Exception>(() => throw new Exception("message"));
            Assert.AreEqual("message", ex.Message);
            
        }

        /// <summary>
        /// Assert calls that dynamically change the test results
        /// </summary>
        [TestMethod]
        public void ForcedResults()
        {
            Assert.Inconclusive("indicates that the test could not be completed with the data available");
            Assert.Fail("immediately end the test with a failure result");
            
        }


    }
}
