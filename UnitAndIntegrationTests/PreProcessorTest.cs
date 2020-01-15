using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathsVisualisationTool;
using DataDomain;
using System.Collections.Generic;

namespace UnitAndIntegrationTests
{
    [TestClass]
    public class PreProcessorTest
    {
        Lexer target = new Lexer();
        Preprocessor target2;

        //new Token(Globals.SUPPORTED_TOKENS.OPEN_BRACKET,"(")
        //new Token(Globals.SUPPORTED_TOKENS.CLOSE_BRACKET,")")

        /*var expected = new List<Token>() { new Token(Globals.SUPPORTED_TOKENS.CONSTANT, "3"),
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
                                          };*/
        [TestMethod]
        public void TestMethod1()
        {
            var tokens = target.TokeniseInput("2+3*5");

            target2 = new Preprocessor(tokens);

            var actual = target2.processTokens();

            var expected = new List<Token>() { new Token(Globals.SUPPORTED_TOKENS.OPEN_BRACKET,"("),
                                               new Token(Globals.SUPPORTED_TOKENS.CONSTANT, "2"),
                                               new Token(Globals.SUPPORTED_TOKENS.PLUS, "+"),
                                               new Token(Globals.SUPPORTED_TOKENS.OPEN_BRACKET,"("),
                                               new Token(Globals.SUPPORTED_TOKENS.CONSTANT, "3"),
                                               new Token(Globals.SUPPORTED_TOKENS.MULTIPLICATION, "*"),
                                               new Token(Globals.SUPPORTED_TOKENS.CONSTANT, "5"),
                                               new Token(Globals.SUPPORTED_TOKENS.CLOSE_BRACKET,")"),
                                               new Token(Globals.SUPPORTED_TOKENS.CLOSE_BRACKET,")")
                                              };

            var res = testIfListsAreEqual(expected, actual);

            Assert.IsTrue(res);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var tokens = target.TokeniseInput("2-3/5-6/8");

            target2 = new Preprocessor(tokens);

            var actual = target2.processTokens();

            var expected = new List<Token>() { new Token(Globals.SUPPORTED_TOKENS.OPEN_BRACKET,"("),
                                               new Token(Globals.SUPPORTED_TOKENS.OPEN_BRACKET,"("),
                                               new Token(Globals.SUPPORTED_TOKENS.CONSTANT, "2"),
                                               new Token(Globals.SUPPORTED_TOKENS.MINUS, "-"),
                                               new Token(Globals.SUPPORTED_TOKENS.OPEN_BRACKET,"("),
                                               new Token(Globals.SUPPORTED_TOKENS.CONSTANT, "3"),
                                               new Token(Globals.SUPPORTED_TOKENS.DIVISION, "/"),
                                               new Token(Globals.SUPPORTED_TOKENS.CONSTANT, "5"),
                                               new Token(Globals.SUPPORTED_TOKENS.CLOSE_BRACKET,")"),
                                               new Token(Globals.SUPPORTED_TOKENS.CLOSE_BRACKET,")"),
                                               new Token(Globals.SUPPORTED_TOKENS.MINUS,"-"),
                                               new Token(Globals.SUPPORTED_TOKENS.OPEN_BRACKET,"("),
                                               new Token(Globals.SUPPORTED_TOKENS.CONSTANT, "6"),
                                               new Token(Globals.SUPPORTED_TOKENS.DIVISION, "/"),
                                               new Token(Globals.SUPPORTED_TOKENS.CONSTANT, "8"),
                                               new Token(Globals.SUPPORTED_TOKENS.CLOSE_BRACKET,")"),
                                               new Token(Globals.SUPPORTED_TOKENS.CLOSE_BRACKET,")"),
                                              };

            var res = testIfListsAreEqual(expected, actual);

            Assert.IsTrue(res);
        }

        [TestMethod]
        public void TestMethod3()
        {
            var tokens = target.TokeniseInput("x=76-65");

            target2 = new Preprocessor(tokens);

            var actual = target2.processTokens();

            var expected = new List<Token>() { new Token(Globals.SUPPORTED_TOKENS.VARIABLE_NAME,"x"),
                                               new Token(Globals.SUPPORTED_TOKENS.ASSIGNMENT, "="),
                                               new Token(Globals.SUPPORTED_TOKENS.OPEN_BRACKET,"("),
                                               new Token(Globals.SUPPORTED_TOKENS.CONSTANT, "76"),
                                               new Token(Globals.SUPPORTED_TOKENS.MINUS, "-"),
                                               new Token(Globals.SUPPORTED_TOKENS.CONSTANT, "65"),
                                               new Token(Globals.SUPPORTED_TOKENS.CLOSE_BRACKET,")")
                                              };

            var res = testIfListsAreEqual(expected, actual);

            Assert.IsTrue(res);
        }

        [TestMethod]
        public void TestMethod4()
        {
            var tokens = target.TokeniseInput("(2+3)*(3+4)*(4+5)");

            target2 = new Preprocessor(tokens);

            var actual = target2.processTokens();

            var expected = new List<Token>() { new Token(Globals.SUPPORTED_TOKENS.OPEN_BRACKET,"("),
                                               new Token(Globals.SUPPORTED_TOKENS.OPEN_BRACKET,"("),
                                               new Token(Globals.SUPPORTED_TOKENS.OPEN_BRACKET,"("),
                                               new Token(Globals.SUPPORTED_TOKENS.CONSTANT, "2"),
                                               new Token(Globals.SUPPORTED_TOKENS.PLUS, "+"),
                                               new Token(Globals.SUPPORTED_TOKENS.CONSTANT, "3"),
                                               new Token(Globals.SUPPORTED_TOKENS.CLOSE_BRACKET,")"),
                                               new Token(Globals.SUPPORTED_TOKENS.MULTIPLICATION, "*"),
                                               new Token(Globals.SUPPORTED_TOKENS.OPEN_BRACKET,"("),
                                               new Token(Globals.SUPPORTED_TOKENS.CONSTANT, "3"),
                                                new Token(Globals.SUPPORTED_TOKENS.PLUS, "+"),
                                               new Token(Globals.SUPPORTED_TOKENS.CONSTANT, "4"),
                                               new Token(Globals.SUPPORTED_TOKENS.CLOSE_BRACKET,")"),
                                               new Token(Globals.SUPPORTED_TOKENS.CLOSE_BRACKET,")"),
                                               new Token(Globals.SUPPORTED_TOKENS.MULTIPLICATION, "*"),
                                               new Token(Globals.SUPPORTED_TOKENS.OPEN_BRACKET,"("),
                                               new Token(Globals.SUPPORTED_TOKENS.CONSTANT, "4"),
                                                new Token(Globals.SUPPORTED_TOKENS.PLUS, "+"),
                                               new Token(Globals.SUPPORTED_TOKENS.CONSTANT, "5"),
                                               new Token(Globals.SUPPORTED_TOKENS.CLOSE_BRACKET,")"),
                                               new Token(Globals.SUPPORTED_TOKENS.CLOSE_BRACKET,")")
                                              };

            var res = testIfListsAreEqual(expected, actual);

            Assert.IsTrue(res);
        }

        [TestMethod]
        public void TestMethod5()
        {
            var tokens = target.TokeniseInput("2+((2+3)*(3+4))");

            target2 = new Preprocessor(tokens);

            var actual = target2.processTokens();

            var expected = new List<Token>() { new Token(Globals.SUPPORTED_TOKENS.OPEN_BRACKET,"("),
                                               new Token(Globals.SUPPORTED_TOKENS.CONSTANT, "2"),
                                               new Token(Globals.SUPPORTED_TOKENS.PLUS, "+"),
                                               new Token(Globals.SUPPORTED_TOKENS.OPEN_BRACKET,"("),
                                               new Token(Globals.SUPPORTED_TOKENS.OPEN_BRACKET,"("),
                                               new Token(Globals.SUPPORTED_TOKENS.CONSTANT, "2"),
                                               new Token(Globals.SUPPORTED_TOKENS.PLUS, "+"),
                                               new Token(Globals.SUPPORTED_TOKENS.CONSTANT, "3"),
                                               new Token(Globals.SUPPORTED_TOKENS.CLOSE_BRACKET,")"),
                                               new Token(Globals.SUPPORTED_TOKENS.MULTIPLICATION, "*"),
                                               new Token(Globals.SUPPORTED_TOKENS.OPEN_BRACKET,"("),
                                               new Token(Globals.SUPPORTED_TOKENS.CONSTANT, "3"),
                                               new Token(Globals.SUPPORTED_TOKENS.PLUS, "+"),
                                               new Token(Globals.SUPPORTED_TOKENS.CONSTANT, "4"),
                                               new Token(Globals.SUPPORTED_TOKENS.CLOSE_BRACKET,")"),
                                               new Token(Globals.SUPPORTED_TOKENS.CLOSE_BRACKET,")"),
                                               new Token(Globals.SUPPORTED_TOKENS.CLOSE_BRACKET,")")
                                              };

            var res = testIfListsAreEqual(expected, actual);

            Assert.IsTrue(res);
        }

        [TestMethod]
        public void TestMethod7()
        {
            var tokens = target.TokeniseInput("(2+3)*5");

            target2 = new Preprocessor(tokens);

            var actual = target2.processTokens();

            var expected = new List<Token>() { new Token(Globals.SUPPORTED_TOKENS.OPEN_BRACKET,"("),
                                               new Token(Globals.SUPPORTED_TOKENS.OPEN_BRACKET,"("),
                                               new Token(Globals.SUPPORTED_TOKENS.CONSTANT, "2"),
                                               new Token(Globals.SUPPORTED_TOKENS.PLUS, "+"),
                                               new Token(Globals.SUPPORTED_TOKENS.CONSTANT, "3"),
                                               new Token(Globals.SUPPORTED_TOKENS.CLOSE_BRACKET,")"),
                                               new Token(Globals.SUPPORTED_TOKENS.MULTIPLICATION, "*"),
                                               new Token(Globals.SUPPORTED_TOKENS.CONSTANT, "5"),
                                               new Token(Globals.SUPPORTED_TOKENS.CLOSE_BRACKET,")")
                                              };

            var res = testIfListsAreEqual(expected, actual);

            Assert.IsTrue(res);
        }

        /// <summary>
        /// Private method to test if two list of tokens are equal.
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns></returns>
        private bool testIfListsAreEqual(List<Token> l1, List<Token> l2)
        {
            if (l1.Count != l2.Count)
            {
                return false;
            }

            for (int i = 0; i < l1.Count; i++)
            {
                bool sameType = l1[i].GetType() == l2[i].GetType();
                bool sameValue = l1[i].GetValue() == l2[i].GetValue();

                if (!sameType || !sameValue)
                {
                    return false;
                }
            }

            return true;
        }
    }
}

