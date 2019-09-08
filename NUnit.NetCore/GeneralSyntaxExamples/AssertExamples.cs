using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using ProductionCode;

namespace NUnit.NetCore
{
    [TestFixture]
    public class AssertExamples
    {

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



            // Constraint-style asserts:
            Assert.That(valueToTest_bool, Is.EqualTo(true));
            Assert.That(valueToTest_string, Is.EqualTo("some result"));
            Assert.That(valueToTest_datetime, Is.EqualTo(new DateTime(2019, 01, 01)));
            Assert.That(valueToTest_obj, Is.EqualTo(expectedValue_obj_equal));

            Assert.That(valueToTest_bool, Is.Not.EqualTo(false));
            Assert.That(valueToTest_string, Is.Not.EqualTo("some other result"));
            Assert.That(valueToTest_datetime, Is.Not.EqualTo(new DateTime(2019, 12, 01)));
            Assert.That(valueToTest_obj, Is.Not.EqualTo(expectedValue_obj_notequal));


            // Classic-style asserts:  (important: expected value comes first!)
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
        [Test]
        public void SameObjectChecks()
        {
            var valueToTest = new {Foo = "bar", Baz = true};
            var expectedValue_same = valueToTest;
            var expectedValue_notsame = new { Foo = "bar", Baz = true }; ;


            // Constraint-style asserts:
            Assert.That(valueToTest, Is.SameAs(expectedValue_same));
            Assert.That(valueToTest, Is.Not.SameAs(expectedValue_notsame));


            // Classic-style asserts:  (important: expected value comes first!)
            Assert.AreSame(expectedValue_same, valueToTest);
            Assert.AreNotSame(expectedValue_notsame, valueToTest);

        }

        /// <summary>
        /// Assertions that test for null values
        /// </summary>
        [Test]
        public void NullChecks()
        {
            var valueToTest = new { Foo = (object) null, Baz = new object() };
            
            
            // Constraint-style asserts:
            Assert.That(valueToTest.Foo, Is.Null);
            Assert.That(valueToTest.Baz, Is.Not.Null);


            // Classic-style asserts:  
            Assert.Null(valueToTest.Foo);
            Assert.IsNull(valueToTest.Foo);
            Assert.NotNull(valueToTest.Baz);
            Assert.IsNotNull(valueToTest.Baz);
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

            double notANumber = double.NaN; // NaN == 0D / 0D
            double infinity = double.PositiveInfinity;
            
            bool trueValue = true;
            bool falseValue = false;

            DateTime jan1 = new DateTime(2019, 01, 01);

            // Constraint-style asserts:
            Assert.That(bigNumber, Is.GreaterThan(smallNumber));
            Assert.That(bigNumber, Is.GreaterThanOrEqualTo(smallNumber));

            Assert.That(smallNumber, Is.LessThan(bigNumber));
            Assert.That(smallNumber, Is.LessThanOrEqualTo(bigNumber));

            Assert.That(trueValue, Is.True);
            Assert.That(falseValue, Is.False);

            Assert.That(bigNumber, Is.Positive);
            Assert.That(bigNumber, Is.Not.Negative);
            Assert.That(smallNumber, Is.Negative);
            Assert.That(smallNumber, Is.Not.Positive);

            Assert.That(zero, Is.Zero);
            Assert.That(bigNumber, Is.Not.Zero);

            Assert.That(notANumber, Is.NaN);
            Assert.That(infinity, Is.Not.NaN);

            Assert.That(zero, Is.InRange(-100, 5));
            Assert.That(zero, Is.Not.InRange(1, 10));
            Assert.That(jan1, Is.InRange(new DateTime(2018, 01, 01), new DateTime(2019, 12, 31)));
           
            Assert.That(zero, Is.AnyOf(42, 0, 100));

            Assert.That(2.333333d, Is.EqualTo(2.3).Within(0.5));
            Assert.That(jan1, Is.EqualTo(new DateTime(2019, 01, 10)).Within(10).Days);
            Assert.That(95, Is.EqualTo(100).Within(8).Percent);

            // Classic-style asserts:  
            Assert.Greater(bigNumber, smallNumber);
            Assert.GreaterOrEqual(bigNumber, smallNumber);

            Assert.Less(smallNumber, bigNumber);
            Assert.LessOrEqual(smallNumber, bigNumber);

            Assert.True(trueValue);
            Assert.False(falseValue);

            Assert.Positive(bigNumber);
            Assert.Negative(smallNumber);

            Assert.Zero(zero);
            Assert.NotZero(bigNumber);

            Assert.IsNaN(notANumber);
            
        }

       

        /// <summary>
        /// String-specific checks
        /// </summary>
        [Test]
        public void StringChecks()
        {
            var valueToTest = "Foo Bar Baz Bin";
          
            // Constraint-style asserts:
            Assert.That("", Is.Empty);
            Assert.That(valueToTest, Is.Not.Empty);
            Assert.That(valueToTest, Does.Contain("Bar"));
            Assert.That(valueToTest, Does.Not.Contain("Bang"));
            Assert.That(valueToTest, Does.StartWith("Foo"));
            Assert.That(valueToTest, Does.Not.StartWith("Bar"));
            Assert.That(valueToTest, Does.EndWith("Bin"));
            Assert.That(valueToTest, Does.Not.EndWith("Baz"));
            Assert.That(valueToTest, Is.EqualTo("foo bar baz bin").IgnoreCase);
            Assert.That(valueToTest, Is.Not.EqualTo("something else").IgnoreCase);
            Assert.That(valueToTest, Does.Match("^Foo.*Bin$")); // param is a regex pattern
            Assert.That(valueToTest, Does.Not.Match("^Foo.*Bar$")); // param is a regex pattern


            // Classic-style asserts:
            StringAssert.Contains("Bar", valueToTest);
            StringAssert.DoesNotContain("Bang", valueToTest);
            StringAssert.StartsWith("Foo", valueToTest);
            StringAssert.DoesNotStartWith("Bar", valueToTest);
            StringAssert.EndsWith("Bin", valueToTest);
            StringAssert.DoesNotEndWith("Baz", valueToTest);
            StringAssert.AreEqualIgnoringCase("foo bar baz bin", valueToTest);
            StringAssert.AreNotEqualIgnoringCase("something else", valueToTest);
            StringAssert.IsMatch("^Foo.*Bin$", valueToTest); //first param is a regex pattern
            StringAssert.DoesNotMatch("^Foo.*Bar$", valueToTest); //first param is a regex pattern

        }

        /// <summary>
        /// Tests related to object types and inheritance
        /// </summary>
        [Test]
        public void TypeChecks()
        {
            IList<string> stringList = new List<string>();
            IEnumerable<int> intEnumerable = new int[] { };

            Assert.That("foo", Is.AssignableFrom(typeof(string)));
            Assert.That("foo", Is.AssignableFrom<string>());

            Assert.That("foo", Is.Not.AssignableFrom(typeof(int)));
            Assert.That("foo", Is.Not.AssignableFrom<int>());


            Assert.That(stringList, Is.InstanceOf(typeof(List<string>)));
            Assert.That(stringList, Is.InstanceOf<List<string>>());

            Assert.That(intEnumerable, Is.Not.InstanceOf(typeof(List<int>)));
            Assert.That(intEnumerable, Is.Not.InstanceOf<List<int>>());

            Assert.That(stringList, Is.AssignableTo(typeof(IEnumerable<string>)));
            Assert.That(stringList, Is.AssignableTo<IEnumerable<string>>());

            Assert.That(stringList, Is.Not.AssignableTo(typeof(string[])));
            Assert.That(stringList, Is.Not.AssignableTo<string[]>());


            Assert.That(intEnumerable, Is.TypeOf(typeof(int[]))); //must be exact type
            Assert.That(intEnumerable, Is.TypeOf<int[]>()); //must be exact type


            Assert.That(stringList, Is.Not.TypeOf(typeof(IEnumerable<string>))); //must be exact type
            Assert.That(stringList, Is.Not.TypeOf<IEnumerable<string>>()); //must be exact type

            // Classic-style asserts:
            Assert.IsAssignableFrom(typeof(string), "foo");
            Assert.IsAssignableFrom<string>("foo");

            Assert.IsNotAssignableFrom(typeof(int), "foo");
            Assert.IsNotAssignableFrom<int>("foo");

            Assert.IsInstanceOf(typeof(List<string>), stringList);
            Assert.IsInstanceOf<List<string>>(stringList);

            Assert.IsNotInstanceOf(typeof(List<int>), intEnumerable);
            Assert.IsNotInstanceOf<List<int>>(intEnumerable);

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

            // Constraint-style asserts:
            Assert.That(stringArr, Is.All.TypeOf<string>());
            Assert.That(intList, Is.All.GreaterThanOrEqualTo(0));
            Assert.That(objArr, Is.All.Not.Null);

            Assert.That(intList, Is.Unique);


            Assert.That(intList, Is.EqualTo(Enumerable.Range(0, 100)));
            Assert.That(intList, Is.Not.EqualTo(Enumerable.Range(1, 5)));

            Assert.That(stringArr, Is.EquivalentTo(new string[] {"bar", "baz", "", "bin", "foo"}));
            Assert.That(stringArr, Is.Not.EquivalentTo(new string[] {"bar", "baz"}));


            Assert.That(stringArr, Has.Member("foo"));
            Assert.That(stringArr, Does.Contain("foo"));
            Assert.That(stringArr, Contains.Item("foo"));

            Assert.That(stringArr, Has.No.Member("zoom"));
            Assert.That(stringArr, Does.Not.Contain("zoom"));

            Assert.That(Enumerable.Range(5, 20), Is.SubsetOf(intList));
            Assert.That(Enumerable.Range(-1, 1), Is.Not.SubsetOf(intList));


            Assert.That(intList, Is.SupersetOf(Enumerable.Range(5, 20)));
            Assert.That(intList, Is.Not.SupersetOf(Enumerable.Range(-1, 1)));

            Assert.That(new int[] { }, Is.Empty);
            Assert.That(intList, Is.Not.Empty);

            Assert.That(new int[] {1, 2, 3}, Is.Ordered);
            Assert.That(new int[] {2, 1, 3}, Is.Not.Ordered);

            string[] sarray = new string[] {"a", "aa", "aaa"};
            Assert.That(sarray, Is.Ordered.By("Length"));
            Assert.That(sarray, Is.Ordered.Ascending.By("Length"));

            Assert.That(intList, Has.Exactly(100).Items);
            Assert.That(intList, Has.Exactly(50).Items.GreaterThanOrEqualTo(50));

            Assert.That(intList, Has.None.LessThan(0));
            Assert.That(objArr, Has.Some.TypeOf<string>());
            Assert.That(intList, Has.All.GreaterThanOrEqualTo(0));

            // Classic-style asserts:
            CollectionAssert.AllItemsAreInstancesOfType(stringArr, typeof(string));
            CollectionAssert.AllItemsAreNotNull(objArr);

            CollectionAssert.AllItemsAreUnique(intList);

            CollectionAssert.AreEqual(Enumerable.Range(0, 100), intList);
            CollectionAssert.AreNotEqual(Enumerable.Range(1, 5), intList);

            CollectionAssert.AreEquivalent(new string[] {"bar", "baz", "", "bin", "foo"}, stringArr);
            CollectionAssert.AreNotEquivalent(new string[] {"bar", "baz"}, stringArr);

            CollectionAssert.Contains(stringArr, "foo");
            CollectionAssert.DoesNotContain(stringArr, "zoom");

            CollectionAssert.IsSubsetOf(Enumerable.Range(5, 20), intList);
            CollectionAssert.IsNotSubsetOf(Enumerable.Range(-1, 1), intList);

            CollectionAssert.IsEmpty(new int[] { });
            CollectionAssert.IsNotEmpty(new int[] {1, 2});

            CollectionAssert.IsOrdered(new int[] {1, 2, 3});
                //  CollectionAssert.IsOrdered(new int[] {1, 2, 3}, new NUnitComparer());

        }

      

        /// <summary>
        /// Exception-specific checks
        /// </summary>
        [Test]
        public void ExceptionChecks()
        {

            void MethodThatThrows() { throw new ArgumentException(); }

            // Constraint-style asserts:
            Assert.That(() => { return; }, Throws.Nothing);


            Assert.That(MethodThatThrows, Throws.ArgumentException);
            Assert.That(MethodThatThrows, Throws.TypeOf<ArgumentException>());

            Assert.That(() => throw new Exception("message"),
                Throws.InstanceOf<Exception>()
                    .And.With.Property(nameof(Exception.Message)).EqualTo("message"));


            // Require an ApplicationException - derived types fail!
            Assert.That(() => throw new ApplicationException("message"),
                Throws.TypeOf<ApplicationException>());

            // Allow both ApplicationException and any derived type
            Assert.That(() => throw new ApplicationException("message"),
                Throws.InstanceOf<Exception>());



            // Classic-style asserts:
            Assert.DoesNotThrow(() => { return; });


            Assert.Throws<ArgumentException>(MethodThatThrows);
            Assert.Throws<ArgumentException>(() => throw new ArgumentException());

            Exception ex = Assert.Throws<Exception>(() => throw new Exception("message"));
            Assert.That(ex.Message, Is.EqualTo("message"));

            Assert.Throws(Is.TypeOf<Exception>().And.Message.EqualTo("message"),
                () => throw new Exception("message"));

            // Require an ApplicationException - derived types fail!
            Assert.Throws<ApplicationException>(() => throw new ApplicationException());

            // Allow both ApplicationException and any derived type
            Assert.Throws(Is.InstanceOf<Exception>(), () => throw new ApplicationException("message"));

            
        }

        /// <summary>
        /// Assert calls that dynamically change the test results
        /// </summary>
        [Test]
        public void ForcedResults()
        {
            Assert.Pass("immediately end the test with a passing result");
            Assert.Fail("immediately end the test with a failure result");

            Assert.Ignore("dynamically cause a test or suite to be ignored at runtime");
            Assert.Inconclusive("indicates that the test could not be completed with the data available");
        }

        /// <summary>
        /// Syntax for executing multiple assertions in the same test (ie: all asserts are run)
        /// </summary>
        [Test]
        public void MultipleCriteriaChecks()
        {
            object aNumber = 5.0;

            Assert.That(aNumber, Is.AssignableTo<int>().Or.AssignableTo<double>());
            Assert.That(aNumber, Is.GreaterThanOrEqualTo(0).And.LessThanOrEqualTo(10));

            Assert.Multiple(() =>
                {
                    Assert.That(aNumber, Is.AssignableTo<double>());
                    Assert.That(aNumber, Is.InRange(0.0, 10.0));
                }
            );
        }

        /// <summary>
        /// File- and Directory-specific checks
        /// </summary>
        [Test]
        public void FileChecks()
        {
            var realFilePath = Assembly.GetExecutingAssembly().Location;
            var realFileInfo = new FileInfo(realFilePath);

            var realDirectoryPath = Path.GetDirectoryName(realFilePath);
            var realDirectoryInfo = new DirectoryInfo(realDirectoryPath);

            var nonexistantFilePath = "E:/fake.folder/this.is.fake";
            var nonexistantFileInfo = new FileInfo(nonexistantFilePath);

            var nonexistantDirectoryPath = Path.GetDirectoryName(nonexistantFilePath);
            var nonexistantDirectoryInfo = new DirectoryInfo(nonexistantDirectoryPath);


            // Constraint-style asserts:
            Assert.That(realFilePath, Does.Exist);
            Assert.That(realFileInfo, Does.Exist);

            Assert.That(nonexistantFilePath, Does.Not.Exist);
            Assert.That(nonexistantFileInfo, Does.Not.Exist);

            Assert.That(realDirectoryPath, Does.Exist);
            Assert.That(realDirectoryInfo, Does.Exist);

            Assert.That(nonexistantDirectoryPath, Does.Not.Exist);
            Assert.That(nonexistantDirectoryInfo, Does.Not.Exist);

            Assert.That(realDirectoryInfo, Is.Not.Empty);

            Assert.That("/folder1/./junk/../folder2", Is.SamePath("/folder1/folder2"));
            Assert.That("/folder1/./junk/../folder2/..", Is.Not.SamePath("/folder1/folder2"));

            Assert.That("/folder1/./junk/../folder2", Is.SamePath("/FOLDER1/folder2").IgnoreCase);
            Assert.That("/folder1/./junk/../folder2", Is.Not.SamePath("/FOLDER1/folder2").RespectCase);


            Assert.That("/folder1/./junk/../folder2/./foo", Is.SamePathOrUnder("/folder1/folder2"));
            Assert.That("/folder1/./junk/../folder2/./foo", Is.SubPathOf("/folder1"));




            // Classic-style asserts:
            // see: https://github.com/nunit/docs/wiki/File-Assert

            FileAssert.Exists(realFileInfo);
            FileAssert.Exists(realFilePath);

            FileAssert.DoesNotExist(nonexistantFileInfo);
            FileAssert.DoesNotExist(nonexistantFilePath);

            DirectoryAssert.Exists(realDirectoryPath);
            DirectoryAssert.Exists(realDirectoryInfo);

            DirectoryAssert.DoesNotExist(nonexistantDirectoryPath);
            DirectoryAssert.DoesNotExist(nonexistantDirectoryInfo);


        }

    }
}
