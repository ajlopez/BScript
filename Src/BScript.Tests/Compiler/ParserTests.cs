namespace BScript.Tests.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BScript.Commands;
    using BScript.Compiler;
    using BScript.Expressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void ParseNull()
        {
            Parser parser = new Parser(null);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseEmptyText()
        {
            Parser parser = new Parser(string.Empty);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseWhiteSpaceText()
        {
            Parser parser = new Parser("   ");

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseString()
        {
            Parser parser = new Parser("\"foo\"");

            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ConstantExpression));
            Assert.AreEqual("foo", ((ConstantExpression)result).Value);
        }

        [TestMethod]
        public void ParseName()
        {
            Parser parser = new Parser("foo");

            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NameExpression));
            Assert.AreEqual("foo", ((NameExpression)result).Name);
        }

        [TestMethod]
        public void ParseReturn()
        {
            Parser parser = new Parser("return\n");

            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ReturnCommand));

            var rtcmd = (ReturnCommand)result;

            Assert.IsNull(rtcmd.Expression);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseReturnWithoutNewLine()
        {
            Parser parser = new Parser("return");

            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ReturnCommand));

            var rtcmd = (ReturnCommand)result;

            Assert.IsNull(rtcmd.Expression);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseReturnWithExpression()
        {
            Parser parser = new Parser("return 42\n");

            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ReturnCommand));

            var rtcmd = (ReturnCommand)result;

            Assert.IsNotNull(rtcmd.Expression);
            Assert.IsInstanceOfType(rtcmd.Expression, typeof(ConstantExpression));
            Assert.AreEqual(42, ((ConstantExpression)rtcmd.Expression).Value);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseSimpleFor()
        {
            Parser parser = new Parser("for k = 1 to 4\n a = a + k\nend\n");

            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ForCommand));

            var forcmd = (ForCommand)result;

            Assert.AreEqual("k", forcmd.Name);

            Assert.IsNotNull(forcmd.FromExpression);
            Assert.IsInstanceOfType(forcmd.FromExpression, typeof(ConstantExpression));
            Assert.AreEqual(1, ((ConstantExpression)forcmd.FromExpression).Value);

            Assert.IsNotNull(forcmd.ToExpression);
            Assert.IsInstanceOfType(forcmd.ToExpression, typeof(ConstantExpression));
            Assert.AreEqual(4, ((ConstantExpression)forcmd.ToExpression).Value);

            Assert.IsNotNull(forcmd.Body);
            Assert.IsInstanceOfType(forcmd.Body, typeof(ExpressionCommand));

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseForWithCompositeCommand()
        {
            Parser parser = new Parser("for k = 1 to 4\n a = a + k\n b = 0\nend\n");

            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ForCommand));

            var forcmd = (ForCommand)result;

            Assert.AreEqual("k", forcmd.Name);

            Assert.IsNotNull(forcmd.FromExpression);
            Assert.IsInstanceOfType(forcmd.FromExpression, typeof(ConstantExpression));
            Assert.AreEqual(1, ((ConstantExpression)forcmd.FromExpression).Value);

            Assert.IsNotNull(forcmd.ToExpression);
            Assert.IsInstanceOfType(forcmd.ToExpression, typeof(ConstantExpression));
            Assert.AreEqual(4, ((ConstantExpression)forcmd.ToExpression).Value);

            Assert.IsNull(forcmd.StepExpression);

            Assert.IsNotNull(forcmd.Body);
            Assert.IsInstanceOfType(forcmd.Body, typeof(CompositeCommand));

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseForExpectName()
        {
            Parser parser = new Parser("for 1 = 1 to 4\n a = a + k\n b = 0\nend\n");

            try
            {
                var result = parser.ParseCommand();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ParserException));
                Assert.AreEqual("Name expected", ex.Message);
            }
        }

        [TestMethod]
        public void ParseForExpectNameNotEndOfCommand()
        {
            Parser parser = new Parser("for");

            try
            {
                var result = parser.ParseCommand();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ParserException));
                Assert.AreEqual("Name expected", ex.Message);
            }
        }

        [TestMethod]
        public void ParseForExpectEqual()
        {
            Parser parser = new Parser("for k 1 to 4\n a = a + k\n b = 0\nend\n");

            try
            {
                var result = parser.ParseCommand();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ParserException));
                Assert.AreEqual("Expected '='", ex.Message);
            }
        }

        [TestMethod]
        public void ParseForWithStepAndCompositeCommand()
        {
            Parser parser = new Parser("for k = 1 to 4 step 2\n a = a + k\n b = 0\nend\n");

            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ForCommand));

            var forcmd = (ForCommand)result;

            Assert.AreEqual("k", forcmd.Name);

            Assert.IsNotNull(forcmd.FromExpression);
            Assert.IsInstanceOfType(forcmd.FromExpression, typeof(ConstantExpression));
            Assert.AreEqual(1, ((ConstantExpression)forcmd.FromExpression).Value);

            Assert.IsNotNull(forcmd.ToExpression);
            Assert.IsInstanceOfType(forcmd.ToExpression, typeof(ConstantExpression));
            Assert.AreEqual(4, ((ConstantExpression)forcmd.ToExpression).Value);

            Assert.IsNotNull(forcmd.StepExpression);
            Assert.IsInstanceOfType(forcmd.StepExpression, typeof(ConstantExpression));
            Assert.AreEqual(2, ((ConstantExpression)forcmd.StepExpression).Value);

            Assert.IsNotNull(forcmd.Body);
            Assert.IsInstanceOfType(forcmd.Body, typeof(CompositeCommand));

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseSimpleIf()
        {
            Parser parser = new Parser("if a<10\n a = 20\nend\n");

            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IfCommand));

            var ifcmd = (IfCommand)result;

            Assert.IsNotNull(ifcmd.Condition);
            Assert.IsInstanceOfType(ifcmd.Condition, typeof(BinaryOperatorExpression));
            Assert.IsNotNull(ifcmd.ThenCommand);
            Assert.IsInstanceOfType(ifcmd.ThenCommand, typeof(ExpressionCommand));
            Assert.IsNull(ifcmd.ElseCommand);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseCompositeIf()
        {
            Parser parser = new Parser("if a<10\n a = 20\n b = 10\nend\n");

            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IfCommand));

            var ifcmd = (IfCommand)result;

            Assert.IsNotNull(ifcmd.Condition);
            Assert.IsInstanceOfType(ifcmd.Condition, typeof(BinaryOperatorExpression));
            Assert.IsNotNull(ifcmd.ThenCommand);
            Assert.IsInstanceOfType(ifcmd.ThenCommand, typeof(CompositeCommand));
            Assert.IsNull(ifcmd.ElseCommand);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseCompositeIfWithSimpleElse()
        {
            Parser parser = new Parser("if a<10\n a = 20\n b = 10\nelse\n b = 0\nend\n");

            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IfCommand));

            var ifcmd = (IfCommand)result;

            Assert.IsNotNull(ifcmd.Condition);
            Assert.IsInstanceOfType(ifcmd.Condition, typeof(BinaryOperatorExpression));
            Assert.IsNotNull(ifcmd.ThenCommand);
            Assert.IsInstanceOfType(ifcmd.ThenCommand, typeof(CompositeCommand));
            Assert.IsNotNull(ifcmd.ElseCommand);
            Assert.IsInstanceOfType(ifcmd.ElseCommand, typeof(ExpressionCommand));

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseCompositeIfWithCompositeElse()
        {
            Parser parser = new Parser("if a<10\n a = 20\n b = 10\nelse\n b = 0\n c = 1\nend\n");

            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IfCommand));

            var ifcmd = (IfCommand)result;

            Assert.IsNotNull(ifcmd.Condition);
            Assert.IsInstanceOfType(ifcmd.Condition, typeof(BinaryOperatorExpression));
            Assert.IsNotNull(ifcmd.ThenCommand);
            Assert.IsInstanceOfType(ifcmd.ThenCommand, typeof(CompositeCommand));
            Assert.IsNotNull(ifcmd.ElseCommand);
            Assert.IsInstanceOfType(ifcmd.ElseCommand, typeof(CompositeCommand));

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseSimpleWhile()
        {
            Parser parser = new Parser("while a<10\n a = 20\nend\n");

            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(WhileCommand));

            var whcmd = (WhileCommand)result;

            Assert.IsNotNull(whcmd.Condition);
            Assert.IsInstanceOfType(whcmd.Condition, typeof(BinaryOperatorExpression));
            Assert.IsNotNull(whcmd.Command);
            Assert.IsInstanceOfType(whcmd.Command, typeof(ExpressionCommand));

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseCompositeWhile()
        {
            Parser parser = new Parser("while a<10\n a = 20\n b = 10\nend\n");

            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(WhileCommand));

            var whcmd = (WhileCommand)result;

            Assert.IsNotNull(whcmd.Condition);
            Assert.IsInstanceOfType(whcmd.Condition, typeof(BinaryOperatorExpression));
            Assert.IsNotNull(whcmd.Command);
            Assert.IsInstanceOfType(whcmd.Command, typeof(CompositeCommand));

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseAssignment()
        {
            Parser parser = new Parser("foo=1");

            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(AssignExpression));

            var aexpr = (AssignExpression)result;

            Assert.IsNotNull(aexpr.Name);
            Assert.IsNotNull(aexpr.Expression);
            Assert.AreEqual("foo", aexpr.Name);
            Assert.IsInstanceOfType(aexpr.Expression, typeof(ConstantExpression));
            Assert.AreEqual(1, ((ConstantExpression)aexpr.Expression).Value);
        }

        [TestMethod]
        public void ParseEqualIntegers()
        {
            Parser parser = new Parser("1==2");

            var result = parser.ParseExpression();

            IsBinaryOperation(result, BinaryOperator.Equal, 1, 2);
        }

        [TestMethod]
        public void ParseNotEqualIntegers()
        {
            Parser parser = new Parser("1<>2");

            var result = parser.ParseExpression();

            IsBinaryOperation(result, BinaryOperator.NotEqual, 1, 2);
        }

        [TestMethod]
        public void ParseAddIntegers()
        {
            Parser parser = new Parser("1+2");

            var result = parser.ParseExpression();

            IsBinaryOperation(result, BinaryOperator.Add, 1, 2); 
        }

        [TestMethod]
        public void ParseAddThreeIntegers()
        {
            Parser parser = new Parser("1+2+3");

            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(BinaryOperatorExpression));

            var bexpr = (BinaryOperatorExpression)result;

            Assert.AreEqual(BinaryOperator.Add, bexpr.Operator);
            IsBinaryOperation(bexpr.LeftExpression, BinaryOperator.Add, 1, 2);
            Assert.IsInstanceOfType(bexpr.RightExpression, typeof(ConstantExpression));
            Assert.AreEqual(3, ((ConstantExpression)bexpr.RightExpression).Value);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseMixPrecedenceOperationWithThreeIntegers()
        {
            Parser parser = new Parser("1+2*3");

            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(BinaryOperatorExpression));

            var bexpr = (BinaryOperatorExpression)result;

            Assert.AreEqual(BinaryOperator.Add, bexpr.Operator);
            IsBinaryOperation(bexpr.RightExpression, BinaryOperator.Multiply, 2, 3);
            Assert.IsInstanceOfType(bexpr.LeftExpression, typeof(ConstantExpression));
            Assert.AreEqual(1, ((ConstantExpression)bexpr.LeftExpression).Value);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseMixPrecedenceOperationWithThreeIntegersAndParenthesis()
        {
            Parser parser = new Parser("(1+2)*3");

            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(BinaryOperatorExpression));

            var bexpr = (BinaryOperatorExpression)result;

            Assert.AreEqual(BinaryOperator.Multiply, bexpr.Operator);
            IsBinaryOperation(bexpr.LeftExpression, BinaryOperator.Add, 1, 2);
            Assert.IsInstanceOfType(bexpr.RightExpression, typeof(ConstantExpression));
            Assert.AreEqual(3, ((ConstantExpression)bexpr.RightExpression).Value);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseSubtractIntegers()
        {
            Parser parser = new Parser("1-2");

            var result = parser.ParseExpression();

            IsBinaryOperation(result, BinaryOperator.Subtract, 1, 2); 
        }

        [TestMethod]
        public void ParseMultiplyIntegers()
        {
            Parser parser = new Parser("2*3");

            var result = parser.ParseExpression();

            IsBinaryOperation(result, BinaryOperator.Multiply, 2, 3);
        }

        [TestMethod]
        public void ParseDivideIntegers()
        {
            Parser parser = new Parser("2/3");

            var result = parser.ParseExpression();

            IsBinaryOperation(result, BinaryOperator.Divide, 2, 3);
        }

        [TestMethod]
        public void ParseLessIntegers()
        {
            Parser parser = new Parser("2<3");

            var result = parser.ParseExpression();

            IsBinaryOperation(result, BinaryOperator.Less, 2, 3);
        }

        [TestMethod]
        public void ParseGreaterIntegers()
        {
            Parser parser = new Parser("2>3");

            var result = parser.ParseExpression();

            IsBinaryOperation(result, BinaryOperator.Greater, 2, 3);
        }

        [TestMethod]
        public void ParseLessEqualIntegers()
        {
            Parser parser = new Parser("2<=3");

            var result = parser.ParseExpression();

            IsBinaryOperation(result, BinaryOperator.LessEqual, 2, 3);
        }

        [TestMethod]
        public void ParseGreaterEqualIntegers()
        {
            Parser parser = new Parser("2>=3");

            var result = parser.ParseExpression();

            IsBinaryOperation(result, BinaryOperator.GreaterEqual, 2, 3);
        }

        [TestMethod]
        public void ParseExpressionCommand()
        {
            Parser parser = new Parser("foo=1");

            var cmd = parser.ParseCommand();

            Assert.IsNotNull(cmd);
            Assert.IsInstanceOfType(cmd, typeof(ExpressionCommand));

            var expr = ((ExpressionCommand)cmd).Expression;

            Assert.IsNotNull(expr);
            Assert.IsInstanceOfType(expr, typeof(AssignExpression));

            var aexpr = (AssignExpression)expr;

            Assert.IsNotNull(aexpr.Name);
            Assert.IsNotNull(aexpr.Expression);
            Assert.AreEqual("foo", aexpr.Name);
            Assert.IsInstanceOfType(aexpr.Expression, typeof(ConstantExpression));
            Assert.AreEqual(1, ((ConstantExpression)aexpr.Expression).Value);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseExpressionCommandSkippingNewLines()
        {
            Parser parser = new Parser("\n\r\n\rfoo=1");

            var cmd = parser.ParseCommand();

            Assert.IsNotNull(cmd);
            Assert.IsInstanceOfType(cmd, typeof(ExpressionCommand));

            var expr = ((ExpressionCommand)cmd).Expression;

            Assert.IsNotNull(expr);
            Assert.IsInstanceOfType(expr, typeof(AssignExpression));

            var aexpr = (AssignExpression)expr;

            Assert.IsNotNull(aexpr.Name);
            Assert.IsNotNull(aexpr.Expression);
            Assert.AreEqual("foo", aexpr.Name);
            Assert.IsInstanceOfType(aexpr.Expression, typeof(ConstantExpression));
            Assert.AreEqual(1, ((ConstantExpression)aexpr.Expression).Value);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseTwoExpressionCommands()
        {
            Parser parser = new Parser("foo=1\nbar=2");

            var cmd = parser.ParseCommand();

            Assert.IsNotNull(cmd);
            Assert.IsInstanceOfType(cmd, typeof(ExpressionCommand));

            var expr = ((ExpressionCommand)cmd).Expression;

            Assert.IsNotNull(expr);
            Assert.IsInstanceOfType(expr, typeof(AssignExpression));

            var aexpr = (AssignExpression)expr;

            Assert.IsNotNull(aexpr.Name);
            Assert.IsNotNull(aexpr.Expression);
            Assert.AreEqual("foo", aexpr.Name);
            Assert.IsInstanceOfType(aexpr.Expression, typeof(ConstantExpression));
            Assert.AreEqual(1, ((ConstantExpression)aexpr.Expression).Value);

            cmd = parser.ParseCommand();

            Assert.IsNotNull(cmd);
            Assert.IsInstanceOfType(cmd, typeof(ExpressionCommand));

            expr = ((ExpressionCommand)cmd).Expression;

            Assert.IsNotNull(expr);
            Assert.IsInstanceOfType(expr, typeof(AssignExpression));

            aexpr = (AssignExpression)expr;

            Assert.IsNotNull(aexpr.Name);
            Assert.IsNotNull(aexpr.Expression);
            Assert.AreEqual("bar", aexpr.Name);
            Assert.IsInstanceOfType(aexpr.Expression, typeof(ConstantExpression));
            Assert.AreEqual(2, ((ConstantExpression)aexpr.Expression).Value);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void RaiseWhenNoEndOfCommand()
        {
            Parser parser = new Parser("foo=1 a\nbar=2");

            try
            {
                parser.ParseCommand();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ParserException));
                Assert.AreEqual("Unexpected 'a'", ex.Message);
            }
        }

        [TestMethod]
        public void ParseFunctionCommand()
        {
            Parser parser = new Parser("function foo(a, b) \n return a+b\n end");

            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FunctionCommand));

            var fcmd = (FunctionCommand)result;

            Assert.AreEqual("foo", fcmd.Name);
            Assert.IsNotNull(fcmd.ArgumentNames);
            Assert.AreEqual(2, fcmd.ArgumentNames.Count);
            Assert.AreEqual("a", fcmd.ArgumentNames[0]);
            Assert.AreEqual("b", fcmd.ArgumentNames[1]);
            Assert.IsNotNull(fcmd.Body);
            Assert.IsInstanceOfType(fcmd.Body, typeof(ReturnCommand));

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseFunctionWithCompositeCommand()
        {
            Parser parser = new Parser("function foo(a, b) \n a = a+b \n return a\n end");

            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FunctionCommand));

            var fcmd = (FunctionCommand)result;

            Assert.AreEqual("foo", fcmd.Name);
            Assert.IsNotNull(fcmd.ArgumentNames);
            Assert.AreEqual(2, fcmd.ArgumentNames.Count);
            Assert.AreEqual("a", fcmd.ArgumentNames[0]);
            Assert.AreEqual("b", fcmd.ArgumentNames[1]);
            Assert.IsNotNull(fcmd.Body);
            Assert.IsInstanceOfType(fcmd.Body, typeof(CompositeCommand));

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseCommands()
        {
            Parser parser = new Parser("a=1\nb=2");

            var result = parser.ParseCommands();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CompositeCommand));

            var ccmd = (CompositeCommand)result;

            Assert.AreEqual(2, ccmd.Commands.Count);
        }

        [TestMethod]
        public void ParseCommandsOneCommand()
        {
            Parser parser = new Parser("a=1");

            var result = parser.ParseCommands();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ExpressionCommand));

            var excmd = (ExpressionCommand)result;

            Assert.IsInstanceOfType(excmd.Expression, typeof(AssignExpression));

            var aexpr = (AssignExpression)excmd.Expression;

            Assert.AreEqual("a", aexpr.Name);
            Assert.IsInstanceOfType(aexpr.Expression, typeof(ConstantExpression));
            Assert.AreEqual(1, ((ConstantExpression)aexpr.Expression).Value);
        }

        [TestMethod]
        public void ParseAndExpression()
        {
            Parser parser = new Parser("a and b");

            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(AndExpression));

            var andexpr = (AndExpression)result;

            Assert.IsNotNull(andexpr.LeftExpression);
            Assert.IsNotNull(andexpr.RightExpression);
            Assert.IsInstanceOfType(andexpr.LeftExpression, typeof(NameExpression));
            Assert.AreEqual("a", ((NameExpression)andexpr.LeftExpression).Name);
            Assert.IsInstanceOfType(andexpr.RightExpression, typeof(NameExpression));
            Assert.AreEqual("b", ((NameExpression)andexpr.RightExpression).Name);
        }

        [TestMethod]
        public void ParseOrExpression()
        {
            Parser parser = new Parser("a or b");

            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OrExpression));

            var orexpr = (OrExpression)result;

            Assert.IsNotNull(orexpr.LeftExpression);
            Assert.IsNotNull(orexpr.RightExpression);
            Assert.IsInstanceOfType(orexpr.LeftExpression, typeof(NameExpression));
            Assert.AreEqual("a", ((NameExpression)orexpr.LeftExpression).Name);
            Assert.IsInstanceOfType(orexpr.RightExpression, typeof(NameExpression));
            Assert.AreEqual("b", ((NameExpression)orexpr.RightExpression).Name);
        }

        [TestMethod]
        public void ParseNotExpression()
        {
            Parser parser = new Parser("not a");

            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotExpression));

            var notexpr = (NotExpression)result;

            Assert.IsNotNull(notexpr.Expression);
            Assert.IsInstanceOfType(notexpr.Expression, typeof(NameExpression));
            Assert.AreEqual("a", ((NameExpression)notexpr.Expression).Name);
        }

        [TestMethod]
        public void ParseNestedNotExpression()
        {
            Parser parser = new Parser("not not a");

            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotExpression));

            var notexpr = (NotExpression)result;

            Assert.IsNotNull(notexpr.Expression);
            Assert.IsInstanceOfType(notexpr.Expression, typeof(NotExpression));

            var not2expr = (NotExpression)notexpr.Expression;
            Assert.IsInstanceOfType(not2expr.Expression, typeof(NameExpression));
            Assert.AreEqual("a", ((NameExpression)not2expr.Expression).Name);
        }

        [TestMethod]
        public void ParseOrAndExpression()
        {
            Parser parser = new Parser("a or b and c");

            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OrExpression));

            var orexpr = (OrExpression)result;

            Assert.IsNotNull(orexpr.LeftExpression);
            Assert.IsNotNull(orexpr.RightExpression);
            Assert.IsInstanceOfType(orexpr.LeftExpression, typeof(NameExpression));
            Assert.AreEqual("a", ((NameExpression)orexpr.LeftExpression).Name);
            Assert.IsInstanceOfType(orexpr.RightExpression, typeof(AndExpression));

            var andexpr = (AndExpression)orexpr.RightExpression;

            Assert.IsNotNull(andexpr.LeftExpression);
            Assert.IsNotNull(andexpr.RightExpression);
            Assert.IsInstanceOfType(andexpr.LeftExpression, typeof(NameExpression));
            Assert.AreEqual("b", ((NameExpression)andexpr.LeftExpression).Name);
            Assert.IsInstanceOfType(andexpr.RightExpression, typeof(NameExpression));
            Assert.AreEqual("c", ((NameExpression)andexpr.RightExpression).Name);
        }

        [TestMethod]
        public void ParseCall()
        {
            Parser parser = new Parser("foo(1, 2)");

            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CallExpression));

            var cexpr = (CallExpression)result;

            Assert.AreEqual("foo", cexpr.Name);
            Assert.IsNotNull(cexpr.ArgumentExpressions);
            Assert.AreEqual(2, cexpr.ArgumentExpressions.Count);
        }

        [TestMethod]
        public void ParseExpressionUnexpectedToken()
        {
            Parser parser = new Parser("= 1");

            try
            {
                var result = parser.ParseExpression();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ParserException));
                Assert.AreEqual("Unexpected '='", ex.Message);
            }
        }

        [TestMethod]
        public void ParseNewExpression()
        {
            Parser parser = new Parser("new Context(parent)");

            var expr = parser.ParseExpression();

            Assert.IsNotNull(expr);
            Assert.IsInstanceOfType(expr, typeof(NewExpression));

            var newexpr = (NewExpression)expr;
            
            Assert.IsNotNull(newexpr.Expression);
            Assert.IsInstanceOfType(newexpr.Expression, typeof(NameExpression));
            Assert.AreEqual("Context", ((NameExpression)newexpr.Expression).Name);

            Assert.IsNotNull(newexpr.ArgumentExpressions);
            Assert.AreEqual(1, newexpr.ArgumentExpressions.Count);
            Assert.IsInstanceOfType(newexpr.ArgumentExpressions[0], typeof(NameExpression));
            Assert.AreEqual("parent", ((NameExpression)newexpr.ArgumentExpressions[0]).Name);

            Assert.IsNull(parser.ParseExpression());
        }

        private static void IsBinaryOperation(IExpression expr, BinaryOperator oper, int left, int right)
        {
            Assert.IsInstanceOfType(expr, typeof(BinaryOperatorExpression));
            var bexpr = (BinaryOperatorExpression)expr;

            Assert.AreEqual(oper, bexpr.Operator);
            Assert.IsInstanceOfType(bexpr.LeftExpression, typeof(ConstantExpression));
            Assert.AreEqual(left, ((ConstantExpression)bexpr.LeftExpression).Value);
            Assert.IsInstanceOfType(bexpr.RightExpression, typeof(ConstantExpression));
            Assert.AreEqual(right, ((ConstantExpression)bexpr.RightExpression).Value);
        }
    }
}
