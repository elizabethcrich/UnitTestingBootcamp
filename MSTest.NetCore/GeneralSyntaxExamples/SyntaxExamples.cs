using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSTest.NetCore
{
    [TestClass]
    public class SyntaxExamples
    {
        /*
         * The "ClassInitialize" method is called once per TestClass
         * before any tests or TestInitialize methods are run. This
         * method must be static.
         *
         * Use this method to set up any state you want to have in place
         * for all tests in this class.
         */
        [ClassInitialize]
        public static void PerFixtureSetup(TestContext context)
        {
            
        }

        /*
         * The "TestInitialize" method is called before each TestMethod.
         *
         * Use this method to set up any state you want to have in place
         * for each tests in this class, or to reset class-level state
         * to ensure it isn't impacted by other tests.
         */
        [TestInitialize]
        public void PerTestSetup()
        {

        }

        /*
         * The "TestMethod" is the actual test logic. You can have as many
         * of these in a class as you want.
         */
        [TestMethod]
        public void TestMethod()
        {
        }


        /*
         * You can use "DataRow" to pass parameters to the TestMethod test logic.
         * The parameters of the DataRow attribute must
         * match the type and order of the method's parameters.
         * You can have as many of these in a class as you want.
         */
        [DataTestMethod]
        [DataRow(1, "a")]
        [DataRow(2, "b")]
        [DataRow(-1, "")]
        public void TestMethodWithInputs(int a, string b)
        {

        }

        /*
        * The "TestCleanup" method is called after each TestMethod.
        *
        * Use this method to clean up any state you have in place
        * to ensure it doesn't impacted other tests.
        */
        [TestCleanup]
        public void PerTestCleanup()
        {

        }

        /*
         * The "ClassCleanup" method is called once per TestClass
         * after all tests and TestCleanup methods are run. This
         * method must be static.
         *
         * Use this method to clean up any state have in place
         * or dispose of any shared resources.
         */
        [ClassCleanup]
        public static void PerFixtureCleanup()
        {

        }
    }
}
