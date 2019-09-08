using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xunit;

namespace xUnit.FullFramework
{

    public class AssertExamples
    {
        /// <summary>
        /// Assertions that test for equality using object.Equals() to compare the actual value
        /// to the expected value
        /// </summary>
        [Fact]
        public void EqualityChecks()
        {
            string valueToTest_string = "some result";
            DateTime valueToTest_datetime = new DateTime(2019, 01, 01);

            var valueToTest_obj = new { Foo = "bar", Baz = true };
            var expectedValue_obj_equal = new { Foo = "bar", Baz = true };
            var expectedValue_obj_notequal = new { Foo = "zoom", Baz = false }; ;

            
            // (important: expected value comes first!)
            Assert.Equal("some result", valueToTest_string);
            Assert.Equal(new DateTime(2019, 01, 01), valueToTest_datetime);
            Assert.Equal(expectedValue_obj_equal, valueToTest_obj);

            Assert.NotEqual("some other result", valueToTest_string);
            Assert.NotEqual(new DateTime(2019, 12, 01), valueToTest_datetime);
            Assert.NotEqual(expectedValue_obj_notequal, valueToTest_obj);
        }


        /// <summary>
        /// Assertions that test for equality using object.ReferenceEquals() to determine if both values point to
        /// the exact same object.
        /// </summary>
        [Fact]
        public void SameObjectChecks()
        {
            var valueToTest = new { Foo = "bar", Baz = true };
            var expectedValue_same = valueToTest;
            var expectedValue_notsame = new { Foo = "bar", Baz = true }; ;
            

            // (important: expected value comes first!)
            Assert.Same(expectedValue_same, valueToTest);
            Assert.NotSame(expectedValue_notsame, valueToTest);

        }

        /// <summary>
        /// Assertions that test for null values
        /// </summary>
        [Fact]
        public void NullChecks()
        {
            var valueToTest = new { Foo = (object)null, Baz = new object() };

            Assert.Null(valueToTest.Foo);
            Assert.NotNull(valueToTest.Baz);
            
        }

        /// <summary>
        /// Assertions that compare the value to a set of constraints
        /// </summary>
        [Fact]
        public void ComparisonChecks()
        {
            int bigNumber = int.MaxValue;
            int smallNumber = int.MinValue;
            int zero = 0;

            double notANumber = double.NaN; // NaN == 0D / 0D
            double infinity = double.PositiveInfinity;


            bool trueValue = true;
            bool falseValue = false;

            DateTime jan1 = new DateTime(2019, 01, 01);

            // Constraint-style asserts:
            Assert.True(bigNumber  > smallNumber);
            Assert.True(bigNumber >= smallNumber);

            Assert.True(smallNumber < bigNumber);
            Assert.True(smallNumber <= bigNumber);

            Assert.True(trueValue);
            Assert.False(falseValue);
            
            Assert.InRange(zero, -100, 5);
            Assert.NotInRange(zero, 1, 10);
            Assert.InRange(jan1, new DateTime(2018, 01, 01), new DateTime(2019, 12, 31));
           
        }

        /// <summary>
        /// String-specific checks
        /// </summary>
        [Fact]
        public void StringChecks()
        {
            var valueToTest = "Foo Bar Baz Bin";

           
            Assert.Contains("Bar", valueToTest);
            Assert.DoesNotContain("Bang", valueToTest);
            Assert.StartsWith("Foo", valueToTest);
            Assert.EndsWith("Bin", valueToTest);
            Assert.Equal("foo bar baz bin", valueToTest, ignoreCase: true);
            Assert.NotEqual("something else", valueToTest, StringComparer.InvariantCultureIgnoreCase);
            Assert.Matches("^Foo.*Bin$", valueToTest); //first param is a regex pattern
            Assert.Matches(new Regex("^Foo.*Bin$"), valueToTest); 
            Assert.DoesNotMatch("^Foo.*Bar$", valueToTest); //first param is a regex pattern
            Assert.DoesNotMatch(new Regex("^Foo.*Bar$"), valueToTest); 
        }

        /// <summary>
        /// Tests related to object types and inheritance
        /// </summary>
        [Fact]
        public void TypeChecks()
        {
            IList<string> stringList = new List<string>();
            IEnumerable<int> intEnumerable = new int[] { };

            Assert.IsAssignableFrom<string>("foo");
            Assert.IsType<List<string>>(stringList);
            Assert.IsNotType<List<int>>(intEnumerable);
            
        }

        /// <summary>
        /// Checks specific to collections
        /// </summary>
        [Fact]
        public void CollectionChecks()
        {
            var objArr = new object[] { new object(), 42, "my string" };
            var stringArr = new string[] { "foo", "bar", "baz", "bin", "" };
            var intList = Enumerable.Range(0, 100);

          
            Assert.All(stringArr, s => Assert.IsType<string>(s));
            Assert.All(objArr, Assert.NotNull );

            Assert.Equal(Enumerable.Range(0, 100), intList);
            Assert.NotEqual(Enumerable.Range(1, 5), intList);

           
            Assert.Contains("foo", stringArr);
            Assert.DoesNotContain("zoom", stringArr);

            Assert.Subset(intList.ToHashSet(), Enumerable.Range(5, 20).ToHashSet());
            Assert.Superset(Enumerable.Range(5, 20).ToHashSet(), intList.ToHashSet());

            Assert.Empty(new int[] { });
            Assert.NotEmpty(new int[] { 1, 2 });


        }


        /// <summary>
        /// Exception-specific checks
        /// </summary>
        [Fact]
        public void ExceptionChecks()
        {
            void MethodThatThrows() { throw new ArgumentException(); }

           
            Assert.Throws<ArgumentException>(() => MethodThatThrows());
           
            Exception ex = Assert.Throws<Exception>((Action)(() => throw new Exception("message")));
            Assert.Equal("message", ex.Message);
        }

        /// <summary>
        /// Assert calls that dynamically change the test results
        /// </summary>
        [Fact]
        public void ForcedResults()
        {
            //There is no Assert.Fail option, but you can just thrown an exception.
            //Tip: Create your own Assert.Fail method to wrap throwing an exception
            throw new Exception("immediately end the test with a failure result");

            //There's not a built-in Assert.Skip (aka Ignore), but there is a somewhat complicated
            //way to do it. Example code here: https://github.com/xunit/samples.xunit/tree/master/DynamicSkipExample

        }

    }

//#if NET46 
    public static class EnumerableExtension
    {
        public static HashSet<TSource> ToHashSet<TSource>(this IEnumerable<TSource> source)
        {
            return new HashSet<TSource>(source);
        }
    }
//#endif
}
