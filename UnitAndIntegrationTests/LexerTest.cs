using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathsVisualisationTool;
using DataDomain;
using System.Collections.Generic;

namespace UnitAndIntegrationTests
{
    [TestClass]
    public class LexerTest
    {

        Lexer target = new Lexer();

        /// <summary>
        /// Method to test the input "2+2"
        /// </summary>
        [TestMethod]
        public void TestMethod1()
        {
            var expected = new List<Token>() { new Token(Globals.SUPPORTED_TOKENS.CONSTANT, "2"), new Token(Globals.SUPPORTED_TOKENS.PLUS, "+"), new Token(Globals.SUPPORTED_TOKENS.CONSTANT, "2") };

            var actual = target.TokeniseInput("2+2");

            bool res = testIfListsAreEqual(expected, actual);

            Assert.IsTrue(res);
        }

        /// <summary>
        /// Method to test the input 3+4*5/6-7^8
        /// </summary>
        [TestMethod]
        public void TestMethod2()
        {
            var expected = new List<Token>() { new Token(Globals.SUPPORTED_TOKENS.CONSTANT, "3"),
                                               new Token(Globals.SUPPORTED_TOKENS.PLUS, "+"),
                                               new Token(Globals.SUPPORTED_TOKENS.CONSTANT, "4"),
                                               new Token(Globals.SUPPORTED_TOKENS.MULTIPLICATION, "*"),
                                               new Token(Globals.SUPPORTED_TOKENS.CONSTANT, "5"),
                                               new Token(Globals.SUPPORTED_TOKENS.DIVISION, "/"),
                                               new Token(Globals.SUPPORTED_TOKENS.CONSTANT, "6"),
                                               new Token(Globals.SUPPORTED_TOKENS.MINUS, "-"),
                                               new Token(Globals.SUPPORTED_TOKENS.CONSTANT, "7"),
                                               new Token(Globals.SUPPORTED_TOKENS.INDICIES, "^"),
                                               new Token(Globals.SUPPORTED_TOKENS.CONSTANT, "8")
                                              };

            var actual = target.TokeniseInput("3+4*5/6-7^8");

            bool res = testIfListsAreEqual(expected, actual);

            Assert.IsTrue(res);
        }

        /// <summary>
        /// Method to test the input sin+cos*tan
        /// </summary>
        [TestMethod]
        public void TestMethod4()
        {
            var expected = new List<Token>() { new Token(Globals.SUPPORTED_TOKENS.SIN, "sin"),
                                               new Token(Globals.SUPPORTED_TOKENS.PLUS, "+"),
                                               new Token(Globals.SUPPORTED_TOKENS.COS, "cos"),
                                               new Token(Globals.SUPPORTED_TOKENS.MULTIPLICATION, "*"),
                                               new Token(Globals.SUPPORTED_TOKENS.TAN, "tan")
                                              };

            var actual = target.TokeniseInput("sin+cos*tan");

            bool res = testIfListsAreEqual(expected, actual);

            Assert.IsTrue(res);

        }

        /// <summary>
        /// Method to test the input log2*plot3
        /// </summary>
        [TestMethod]
        public void TestMethod5()
        {
            var expected = new List<Token>() { new Token(Globals.SUPPORTED_TOKENS.VARIABLE_NAME, "log2"),
                                               new Token(Globals.SUPPORTED_TOKENS.MULTIPLICATION, "*"),
                                               new Token(Globals.SUPPORTED_TOKENS.VARIABLE_NAME, "plot3")
                                              };

            var actual = target.TokeniseInput("log2*plot3");

            bool res = testIfListsAreEqual(expected, actual);

            Assert.IsTrue(res);
        }

        /// <summary>
        /// Method to test the input 3headphones + 2mouse
        /// </summary>
        [TestMethod]
        public void TestMethod6()
        {
            var expected = new List<Token>() { new Token(Globals.SUPPORTED_TOKENS.CONSTANT, "3"),
                                               new Token(Globals.SUPPORTED_TOKENS.VARIABLE_NAME, "headphones"),
                                               new Token(Globals.SUPPORTED_TOKENS.PLUS, "+"),
                                               new Token(Globals.SUPPORTED_TOKENS.CONSTANT, "2"),
                                               new Token(Globals.SUPPORTED_TOKENS.VARIABLE_NAME, "mouse")
                                              };

            var actual = target.TokeniseInput("3headphones + 2mouse");

            bool res = testIfListsAreEqual(expected, actual);

            Assert.IsTrue(res);
        }

        /// <summary>
        /// Method to test the input sin((2*xy)+yes)
        /// </summary>
        [TestMethod]
        public void TestMethod7()
        {
            var expected = new List<Token>() { new Token(Globals.SUPPORTED_TOKENS.SIN, "sin"),
                                               new Token(Globals.SUPPORTED_TOKENS.OPEN_BRACKET, "("),
                                               new Token(Globals.SUPPORTED_TOKENS.OPEN_BRACKET, "("),
                                               new Token(Globals.SUPPORTED_TOKENS.CONSTANT, "2"),
                                               new Token(Globals.SUPPORTED_TOKENS.MULTIPLICATION, "*"),
                                               new Token(Globals.SUPPORTED_TOKENS.VARIABLE_NAME, "xy"),
                                               new Token(Globals.SUPPORTED_TOKENS.CLOSE_BRACKET, ")"),
                                               new Token(Globals.SUPPORTED_TOKENS.PLUS, "+"),
                                               new Token(Globals.SUPPORTED_TOKENS.VARIABLE_NAME, "yes"),
                                               new Token(Globals.SUPPORTED_TOKENS.CLOSE_BRACKET,")")
                                              };

            var actual = target.TokeniseInput("sin((2*xy)+yes)");

            bool res = testIfListsAreEqual(expected, actual);

            Assert.IsTrue(res);
        }

        /// <summary>
        /// Method to test the input 2++3
        /// </summary>
        [TestMethod]
        public void TestMethod8()
        {
            var expected = new List<Token>() { new Token(Globals.SUPPORTED_TOKENS.CONSTANT, "2"),
                                               new Token(Globals.SUPPORTED_TOKENS.PLUS, "+"),
                                               new Token(Globals.SUPPORTED_TOKENS.PLUS, "+"),
                                               new Token(Globals.SUPPORTED_TOKENS.CONSTANT, "3")
                                              };

            var actual = target.TokeniseInput("2++3");

            bool res = testIfListsAreEqual(expected, actual);

            Assert.IsTrue(res);
        }

        /// <summary>
        /// Method to test the input 6**7
        /// </summary>
        [TestMethod]
        public void TestMethod9()
        {
            var expected = new List<Token>() { new Token(Globals.SUPPORTED_TOKENS.CONSTANT, "6"),
                                               new Token(Globals.SUPPORTED_TOKENS.MULTIPLICATION, "*"),
                                               new Token(Globals.SUPPORTED_TOKENS.MULTIPLICATION, "*"),
                                               new Token(Globals.SUPPORTED_TOKENS.CONSTANT, "7")
                                              };

            var actual = target.TokeniseInput("6**7");

            bool res = testIfListsAreEqual(expected, actual);

            Assert.IsTrue(res);
        }

        /// <summary>
        /// Method to test the input yes==6
        /// </summary>
        [TestMethod]
        public void TestMethod10()
        {
            var expected = new List<Token>() { new Token(Globals.SUPPORTED_TOKENS.VARIABLE_NAME, "yes"),
                                               new Token(Globals.SUPPORTED_TOKENS.ASSIGNMENT, "="),
                                               new Token(Globals.SUPPORTED_TOKENS.ASSIGNMENT, "="),
                                               new Token(Globals.SUPPORTED_TOKENS.CONSTANT, "6")
                                              };

            var actual = target.TokeniseInput("yes==6");

            bool res = testIfListsAreEqual(expected, actual);

            Assert.IsTrue(res);
        }

        /// <summary>
        /// Method to test the input yes63a = 7
        /// </summary>
        [TestMethod]
        public void TestMethod11()
        {
            var expected = new List<Token>() { new Token(Globals.SUPPORTED_TOKENS.VARIABLE_NAME, "yes63a"),
                                               new Token(Globals.SUPPORTED_TOKENS.ASSIGNMENT, "="),
                                               new Token(Globals.SUPPORTED_TOKENS.CONSTANT, "7")
                                              };

            var actual = target.TokeniseInput("yes63a = 7");

            bool res = testIfListsAreEqual(expected, actual);

            Assert.IsTrue(res);
        }

        /// <summary>
        /// Private method to test if two list of tokens are equal.
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns></returns>
        private bool testIfListsAreEqual(List<Token> l1,List<Token> l2)
        {
            if(l1.Count != l2.Count)
            {
                return false;
            }

            for(int i=0;i<l1.Count;i++)
            {
                bool sameType = l1[i].GetType() == l2[i].GetType();
                bool sameValue = l1[i].GetValue() == l2[i].GetValue();

                if(!sameType || !sameValue)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
