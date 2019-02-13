using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace NUnit.FullFramework
{
    [TestFixture]
    public class SyntaxExamples
    {

        /*
         * The "OneTimeSetUp" method is called once per TestFixture
         * before any tests or SetUp methods are run.
         *
         * Use this method to set up any state you want to have in place
         * for all tests in this class.
         */
        [OneTimeSetUp]
        public void PerFixtureSetup()
        {

        }

        /*
         * The "SetUp" method is called before each Test.
         *
         * Use this method to set up any state you want to have in place
         * for each tests in this class, or to reset class-level state
         * to ensure it isn't impacted by other tests.
         */
        [SetUp]
        public void PerTestSetup()
        {

        }


        /*
         * The "Test" is the actual test logic.
         * You can have as many of these in a class as you want.
         */
        [Test]
        public void TestMethod()
        {
        }


        /*
         * The "TestCase" is the actual test logic, allowing parameters
         * to be passed in. The parameters of the TestCase attribute must
         * match the type and order of the method's parameters.
         * You can have as many of these in a class as you want.
         */
        [TestCase(1, "a")]
        [TestCase(2, "b")]
        [TestCase(-1, "")]
        public void TestMethodWithInputs(int a, string b)
        {

        }


        /*
         * The "TearDown" method is called after each Test.
         *
         * Use this method to clean up any state you have in place
         * to ensure it doesn't impacted other tests.
         */
        [TearDown]
        public void PerTestCleanup()
        {

        }


        /*
         * The "OneTimeTearDown" method is called once per TestClass
         * after all tests and TestCleanup methods are run.
         *
         * Use this method to clean up any state have in place
         * or dispose of any shared resources.
         */
        [OneTimeTearDown]
        public void PerFixtureCleanup()
        {

        }
    }
}
