using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathsVisualisationTool;
using DataDomain;
using System.Collections.Generic;

namespace UnitAndIntegrationTests
{
    [TestClass]
    public class ParserTest
    {
        Lexer target = new Lexer();
        Parser target2 = new Parser();

        [TestMethod]
        public void TestMethod1()
        {
            var output = target.TokeniseInput("x=5");

            var res = target2.AnalyseTokens(output);

            Assert.AreEqual(res, double.NaN);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var output = target.TokeniseInput("3+2*5+6*7");

            var res = target2.AnalyseTokens(output);

            Assert.AreEqual(res, 55);
        }

        [TestMethod]
        public void TestMethod3()
        {
            try
            {
                var output = target.TokeniseInput("yes=67");

                target2.AnalyseTokens(output);

                Assert.Fail();
            } catch(Exception e)
            {

            }
            
        }

        [TestMethod]
        public void TestMethod4()
        {
            var output = target.TokeniseInput("2^2^3");

            var res = target2.AnalyseTokens(output);

            Assert.AreEqual(res, 256);
        }

        [TestMethod]
        public void TestMethod5()
        {
            var output = target.TokeniseInput("2+x");

            var vars = new System.Collections.Hashtable();

            vars.Add("x", "23");

            target2 = new Parser(vars);
            target2.WRITE_TO_FILE = false;

            target2.AnalyseTokens(output);
        }
    }
}
