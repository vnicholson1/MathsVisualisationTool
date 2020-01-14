using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathsVisualisationTool;
using DataDomain;
using System.Collections.Generic;

namespace UnitAndIntegrationTests
{
    [TestClass]
    public class SyntaxAnalyserTest
    {
        Lexer target = new Lexer();
        Parser target2 = new Parser();

        [TestMethod]
        public void TestMethod1()
        {
            try
            {
                var output = target.TokeniseInput("2+*3");

                target2.AnalyseTokens(output);

                Assert.Fail();
            } catch(Exception e)
            {

            }
            
        }

        [TestMethod]
        public void TestMethod2()
        {

            try
            {
                var output = target.TokeniseInput("()");

                target2.AnalyseTokens(output);

                Assert.Fail();
            }
            catch (Exception e)
            {

            }
            
        }

        [TestMethod]
        public void TestMethod3()
        {
            try
            {
                var output = target.TokeniseInput("x=()");

                target2.AnalyseTokens(output);

                Assert.Fail();
            }
            catch (Exception e)
            {

            }
            
        }

        [TestMethod]
        public void TestMethod4()
        {
            try
            {
                var output = target.TokeniseInput("plot(y=x,1<x<10)+3");

                target2.AnalyseTokens(output);

                Assert.Fail();
            }
            catch (Exception e)
            {

            }
            
        }

        [TestMethod]
        public void TestMethod5()
        {
            try
            {
                var output = target.TokeniseInput("~3+2");

                target2.AnalyseTokens(output);

                Assert.Fail();
            }
            catch (Exception e)
            {

            }
            
        }

        [TestMethod]
        public void TestMethod6()
        {
            var output = target.TokeniseInput("2*+3");

            target2.AnalyseTokens(output);
        }

        [TestMethod]
        public void TestMethod7()
        {
            try
            {
                var output = target.TokeniseInput(".");

                target2.AnalyseTokens(output);

                Assert.Fail();
            }
            catch (Exception e)
            {

            }
            
        }

        [TestMethod]
        public void TestMethod8()
        {
            try
            {
                var output = target.TokeniseInput("2..3");

                target2.AnalyseTokens(output);

                Assert.Fail();
            }
            catch (Exception e)
            {

            }
            
        }

        [TestMethod]
        public void TestMethod9()
        {
            try
            {
                var output = target.TokeniseInput("x==3");

                target2.AnalyseTokens(output);

                Assert.Fail();
            }
            catch (Exception e)
            {

            }
            
        }

        [TestMethod]
        public void TestMethod10()
        {
            try
            {
                var output = target.TokeniseInput("3<54");

                target2.AnalyseTokens(output);

                Assert.Fail();
            }
            catch (Exception e)
            {

            }
            
        }

        [TestMethod]
        public void TestMethod11()
        {
            try
            {
                var output = target.TokeniseInput("2x");

                target2.AnalyseTokens(output);

                Assert.Fail();
            }
            catch (Exception e)
            {

            }
            
        }

        [TestMethod]
        public void TestMethod12()
        {
            try
            {
                var output = target.TokeniseInput("2+3)");

                target2.AnalyseTokens(output);

                Assert.Fail();
            }
            catch (Exception e)
            {

            }
            
        }
    }

}
