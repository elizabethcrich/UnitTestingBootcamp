using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Extensions;
using NUnit.Framework;
using Shouldly;

namespace NUnit.FullFramework
{
    [TestFixture]
    public class FluentAssertionExamples
    {

        /*
         * There 's SOOOO much more than this!  See the official documentation at
         * https://fluentassertions.com/introduction 
         */


        /// <summary>
        /// Assertions that test for equality using object.Equals() to compare the actual value
        /// to the expected value
        /// </summary>
        [Test]
        public void EqualityChecks()
        {
          
            bool valueToTest_bool = true;
            string valueToTest_string = "some result";
            DateTime valueToTest_datetime = new DateTime(2019, 01, 01);

            var valueToTest_obj = new { Foo = "bar", Baz = true };
            var expectedValue_obj_equal = new { Foo = "bar", Baz = true };
            var expectedValue_obj_notequal = new { Foo = "zoom", Baz = false }; ;


            valueToTest_bool.Should().Be(true);
            valueToTest_string.Should().Be("some result");
            valueToTest_datetime.Should().Be(new DateTime(2019, 01, 01));
            valueToTest_obj.Should().Be(expectedValue_obj_equal);

            valueToTest_string.Should().NotBe("some other result");
            valueToTest_datetime.Should().NotBe(new DateTime(2019, 12, 01));
            valueToTest_obj.Should().NotBe(expectedValue_obj_notequal);

        }


        /// <summary>
        /// Assertions that test for equality using object.ReferenceEquals() to determine if both values point to
        /// the exact same object.
        /// </summary>
        [Test]
        public void SameObjectChecks()
        {
            var valueToTest = new {Foo = "bar", Baz = true};
            var expectedValue_same = valueToTest;
            var expectedValue_notsame = new { Foo = "bar", Baz = true }; ;

            valueToTest.Should().BeSameAs(expectedValue_same);
            valueToTest.Should().NotBeSameAs(expectedValue_notsame);

        }

        /// <summary>
        /// Assertions that test for null values
        /// </summary>
        [Test]
        public void NullChecks()
        {
            var valueToTest = new { Foo = (object) null, Baz = new object() };

            valueToTest.Foo.Should().BeNull();
            valueToTest.Baz.Should().NotBeNull();
        }

        /// <summary>
        /// Assertions that compare the value to a set of constraints
        /// </summary>
        [Test]
        public void ComparisonChecks()
        {
            int bigNumber = int.MaxValue;
            int smallNumber = int.MinValue;
            int zero = 0;

            bool trueValue = true;
            bool falseValue = false;

            DateTime jan1 = new DateTime(2019, 01, 01);

           
            bigNumber.Should().BeGreaterThan(smallNumber);
            bigNumber.Should().BeGreaterOrEqualTo(smallNumber);

            smallNumber.Should().BeLessThan(bigNumber);
            smallNumber.Should().BeLessOrEqualTo(bigNumber);

            trueValue.Should().BeTrue();
            falseValue.Should().BeFalse();

            bigNumber.Should().BePositive();
            smallNumber.Should().BeNegative();

            zero.Should().BeInRange(-100, 5);
            zero.Should().NotBeInRange(1, 10);
            jan1.Should().BeAfter(new DateTime(2018, 01, 01)).And.BeBefore(new DateTime(2019, 12, 31));

            zero.Should().BeOneOf(42, 0, 100);

            2.333333d.Should().BeApproximately(2.3, 0.5);
            jan1.Should().BeCloseTo(new DateTime(2019, 01, 10), 10.Days());
           
        }

       

        /// <summary>
        /// String-specific checks
        /// </summary>
        [Test]
        public void StringChecks()
        {
            var valueToTest = "Foo Bar Baz Bin";
          
            
            "".Should().BeEmpty();
            valueToTest.Should().NotBeEmpty();
            valueToTest.Should().Contain("Bar");
            valueToTest.Should().NotContain("Bang");
            valueToTest.Should().StartWith("Foo");
            valueToTest.Should().NotStartWith("Bar");
            valueToTest.Should().EndWith("Bin");
            valueToTest.Should().NotEndWith("Baz");
            valueToTest.Should().BeEquivalentTo("foo bar baz bin");
            valueToTest.Should().NotBeEquivalentTo("something else");
            valueToTest.Should().MatchRegex("^Foo.*Bin$"); // param is a regex pattern
            valueToTest.Should().NotMatchRegex("^Foo.*Bar$"); // param is a regex pattern
            valueToTest.Should().Match("Foo*Bin"); // param is a wildcard pattern
            valueToTest.Should().NotMatch("Foo*Bar"); // param is a wildcard pattern


        }

        /// <summary>
        /// Tests related to object types and inheritance
        /// </summary>
        [Test]
        public void TypeChecks()
        {
            IList<string> stringList = new List<string>();
            IEnumerable<int> intEnumerable = new int[] { };


            stringList.Should().BeOfType(typeof(List<string>));
            stringList.Should().BeOfType<List<string>>();

            intEnumerable.Should().NotBeOfType(typeof(List<int>));
            intEnumerable.Should().NotBeOfType< List<int>>();

            stringList.Should().BeAssignableTo(typeof(IEnumerable<string>));
            stringList.Should().BeAssignableTo<IEnumerable<string>>();

            stringList.Should().NotBeAssignableTo(typeof(string[]));
            stringList.Should().NotBeAssignableTo<string[]>();


        }

        /// <summary>
        /// Checks specific to collections
        /// </summary>
        [Test]
        public void CollectionChecks()
        {
            var objArr = new object[] {new object(), 42, "my string"};
            var stringArr = new object[] {"foo", "bar", "baz", "bin", ""};
            var intList = Enumerable.Range(0, 100);


            stringArr.Should().ContainItemsAssignableTo<string>();
            intList.Should().Contain(x => x >= 0);
            objArr.Should().NotContainNulls();

            intList.Should().OnlyHaveUniqueItems();


            intList.Should().Equal(Enumerable.Range(0, 100));
            intList.Should().NotEqual(Enumerable.Range(1, 5));

            stringArr.Should().BeEquivalentTo(new string[] { "bar", "baz", "", "bin", "foo" });
            stringArr.Should().NotBeEquivalentTo(new string[] { "bar", "baz" });

            stringArr.Should().Contain("foo");
            stringArr.Should().NotContain("zoom");

            Enumerable.Range(5, 20).Should().BeSubsetOf(intList);
            Enumerable.Range(-1, 1).Should().NotBeSubsetOf(intList);

            new int[] { }.Should().BeEmpty();
            intList.Should().NotBeEmpty();

            new int[] { 1, 2, 3 }.Should().BeInAscendingOrder();
            new int[] { 2, 1, 3 }.Should().NotBeInDescendingOrder();

            string[] sarray = new string[] { "a", "aa", "aaa" };
            sarray.Should().BeInAscendingOrder(new StringLengthComparer());

            intList.Should().HaveCount(100);

            intList.Should().OnlyContain(x => x >= 0);
           
        }


        private class StringLengthComparer : IComparer<object>
        {
            public int Compare(object x, object y)
            {
                if (x == null || y == null)
                {
                    if (x == y) return 0;

                    if (x == null)
                        return -1;
                    else return 1;
                }

                if (x is string xs && y is string ys)
                {
                    return xs.Length.CompareTo(ys.Length);
                }
                else
                {
                    return -1;
                }
            }
        }

        /// <summary>
        /// Exception-specific checks
        /// </summary>
        [Test]
        public void ExceptionChecks()
        {

            void MethodThatThrows() { throw new ArgumentException(); }


            Action act = () => { return; };
            act.Should().NotThrow();

            act = () => MethodThatThrows();

            act.Should().Throw<ArgumentException>();

            act = () => throw new Exception("message");

            act.Should().Throw<Exception>().And.Message.Should().Be("message");

            // Require an ApplicationException - derived types fail!
            act = () => throw new ApplicationException("message");
            act.Should().ThrowExactly<ApplicationException>();

        }


        /// <summary>
        /// Syntax for executing multiple assertions in the same test (ie: all asserts are run)
        /// </summary>
        [Test]
        public void MultipleCriteriaChecks()
        {
            var aNumber = 5.0;

            aNumber.Should().BeGreaterOrEqualTo(0).And.BeLessOrEqualTo(10);

            using (new AssertionScope()) 
            {
                aNumber.Should().BeOfType(typeof(double));
                aNumber.Should().BeInRange(0.0, 10.0);
            }
            
        }



    }
}
