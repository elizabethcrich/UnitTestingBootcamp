using System;
using System.Collections.Generic;
using System.Linq;
using Shouldly;
using Xunit;

namespace xUnit.FullFramework
{
    public class ShouldlyAssertExamples
    {

        /// <summary>
        /// Assertions that test for equality using object.Equals() to compare the actual value
        /// to the expected value
        /// </summary>
        [Fact]
        public void EqualityChecks()
        {
          
            bool valueToTest_bool = true;
            string valueToTest_string = "some result";
            DateTime valueToTest_datetime = new DateTime(2019, 01, 01);

            var valueToTest_obj = new { Foo = "bar", Baz = true };
            var expectedValue_obj_equal = new { Foo = "bar", Baz = true };
            var expectedValue_obj_notequal = new { Foo = "zoom", Baz = false }; ;


            valueToTest_bool.ShouldBe(true);
            valueToTest_string.ShouldBe("some result");
            valueToTest_datetime.ShouldBe(new DateTime(2019, 01, 01));
            valueToTest_obj.ShouldBe(expectedValue_obj_equal);

            valueToTest_bool.ShouldNotBe(false);
            valueToTest_string.ShouldNotBe("some other result");
            valueToTest_datetime.ShouldNotBe(new DateTime(2019, 12, 01));
            valueToTest_obj.ShouldNotBe(expectedValue_obj_notequal);

        }


        /// <summary>
        /// Assertions that test for equality using object.ReferenceEquals() to determine if both values point to
        /// the exact same object.
        /// </summary>
        [Fact]
        public void SameObjectChecks()
        {
            var valueToTest = new {Foo = "bar", Baz = true};
            var expectedValue_same = valueToTest;
            var expectedValue_notsame = new { Foo = "bar", Baz = true }; ;


            valueToTest.ShouldBeSameAs(expectedValue_same);
            valueToTest.ShouldNotBeSameAs(expectedValue_notsame);

        }

        /// <summary>
        /// Assertions that test for null values
        /// </summary>
        [Fact]
        public void NullChecks()
        {
            var valueToTest = new { Foo = (object) null, Baz = new object() };
            
            valueToTest.Foo.ShouldBeNull();
            valueToTest.Baz.ShouldNotBeNull();
            
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
 
            bool trueValue = true;
            bool falseValue = false;

            DateTime jan1 = new DateTime(2019, 01, 01);

            bigNumber.ShouldBeGreaterThan(smallNumber);
            bigNumber.ShouldBeGreaterThanOrEqualTo(smallNumber);

            smallNumber.ShouldBeLessThan(bigNumber);
            smallNumber.ShouldBeLessThanOrEqualTo(bigNumber);

            trueValue.ShouldBeTrue();
            falseValue.ShouldBeFalse();

            bigNumber.ShouldBePositive();
            smallNumber.ShouldBeNegative();

            zero.ShouldBeInRange(-100, 5);
            zero.ShouldNotBeInRange(1, 10);
            jan1.ShouldBeInRange(new DateTime(2018, 01, 01), new DateTime(2019, 12, 31));

            zero.ShouldBeOneOf(42, 0, 100);

            2.333333d.ShouldBe(2.3, 0.5);
            jan1.ShouldBe(new DateTime(2019, 01, 10), TimeSpan.FromDays(10));
            
        }



        /// <summary>
        /// String-specific checks
        /// </summary>
        [Fact]
        public void StringChecks()
        {
            var valueToTest = "Foo Bar Baz Bin";

            "".ShouldBeEmpty();
            valueToTest.ShouldNotBeEmpty();
            valueToTest.ShouldContain("Bar");
            valueToTest.ShouldNotContain("Bang");
            valueToTest.ShouldStartWith("Foo");
            valueToTest.ShouldNotStartWith("Bar");
            valueToTest.ShouldEndWith("Bin");
            valueToTest.ShouldNotEndWith("Baz");
            valueToTest.ShouldMatch("^Foo.*Bin$"); // param is a regex pattern
            valueToTest.ShouldNotMatch("^Foo.*Bar$"); // param is a regex pattern

        }

        /// <summary>
        /// Tests related to object types and inheritance
        /// </summary>
        [Fact]
        public void TypeChecks()
        {
            IList<string> stringList = new List<string>();
            IEnumerable<int> intEnumerable = new int[] { };

            stringList.ShouldBeAssignableTo(typeof(IEnumerable<string>));
            stringList.ShouldBeAssignableTo<IEnumerable<string>>();

            stringList.ShouldNotBeAssignableTo(typeof(string[]));
            stringList.ShouldNotBeAssignableTo<string[]>();


            intEnumerable.ShouldBeOfType(typeof(int[])); //must be exact type
            intEnumerable.ShouldBeOfType<int[]>(); //must be exact type


            stringList.ShouldNotBeOfType(typeof(IEnumerable<string>)); //must be exact type
            stringList.ShouldNotBeOfType<IEnumerable<string>>(); //must be exact type


        }

        /// <summary>
        /// Checks specific to collections
        /// </summary>
        [Fact]
        public void CollectionChecks()
        {
            var objArr = new object[] {new object(), 42, "my string"};
            var stringArr = new object[] {"foo", "bar", "baz", "bin", ""};
            var intList = Enumerable.Range(0, 100);

            stringArr.ShouldAllBe(x => x is string);
            intList.ShouldAllBe(x => x >= 0);
            objArr.ShouldAllBe(x => x != null);

            intList.ShouldBeUnique();


            intList.ShouldBe(Enumerable.Range(0, 100));
            intList.ShouldNotBe(Enumerable.Range(1, 5));

            stringArr.ShouldBe(new string[] { "bar", "baz", "", "bin", "foo" }, true);

            stringArr.ShouldContain("foo");
            stringArr.ShouldNotContain("zoom");

            Enumerable.Range(5, 20).ShouldBeSubsetOf(intList);

            new int[] { }.ShouldBeEmpty();
            intList.ShouldNotBeEmpty();

            new int[] { 1, 2, 3 }.ShouldBeInOrder();




            string[] sarray = new string[] { "a", "aa", "aaa" };
            sarray.ShouldBeInOrder(SortDirection.Ascending, new StringLengthComparer());

            intList.Count().ShouldBe(100);

            intList.ShouldAllBe(x => x >= 0);


        }

        private class StringLengthComparer: IComparer<string> {
            public int Compare(string x, string y)
            {

                if (x == null || y == null)
                {
                    if (x == y) return 0;

                    if (x == null) 
                        return -1;
                    else return 1;
                }

                return x.Length.CompareTo(y.Length);
            }
        }

        /// <summary>
        /// Exception-specific checks
        /// </summary>
        [Fact]
        public void ExceptionChecks()
        {

            void MethodThatThrows() { throw new ArgumentException(); }

            void MethodThatDoesNotThrow() { return;}


            Action actionThatThrows = MethodThatThrows;
            Action actionThatDoesNotThrow = MethodThatDoesNotThrow;

            actionThatThrows.ShouldThrow<ArgumentException>();
            actionThatThrows.ShouldThrow(typeof(ArgumentException));
            actionThatDoesNotThrow.ShouldNotThrow();

            var ex = ((Action)(() => throw new Exception("message")))
                .ShouldThrow<Exception>();
            ex.Message.ShouldBe("message");

        }

      

        /// <summary>
        /// Syntax for executing multiple assertions in the same test (ie: all asserts are run)
        /// </summary>
        [Fact]
        public void MultipleCriteriaChecks()
        {
            double aNumber = 5.0;

            aNumber.ShouldSatisfyAllConditions(
                () => aNumber.ShouldBeAssignableTo<double>(),
                () => aNumber.ShouldBeGreaterThanOrEqualTo(0.0),
                () => aNumber.ShouldBeLessThanOrEqualTo(10.0)
            );

        }

        

    }
}
