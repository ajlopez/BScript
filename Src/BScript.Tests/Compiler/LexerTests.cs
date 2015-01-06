namespace BScript.Tests.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BScript.Compiler;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class LexerTests
    {
        [TestMethod]
        public void ProcessNullText()
        {
            Lexer lexer = new Lexer(null);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void ProcessEmptyText()
        {
            Lexer lexer = new Lexer(string.Empty);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void ProcessWhiteSpaceText()
        {
            Lexer lexer = new Lexer("   ");

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetName()
        {
            Lexer lexer = new Lexer("foo");

            var token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.Type);
            Assert.AreEqual("foo", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetNameWithSpaces()
        {
            Lexer lexer = new Lexer("  foo   ");

            var token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.Type);
            Assert.AreEqual("foo", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetNames()
        {
            Lexer lexer = new Lexer("foo bar");

            var token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.Type);
            Assert.AreEqual("foo", token.Value);

            token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.Type);
            Assert.AreEqual("bar", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetInteger()
        {
            Lexer lexer = new Lexer("123");

            var token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Integer, token.Type);
            Assert.AreEqual("123", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetReal()
        {
            Lexer lexer = new Lexer("123.45");

            var token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Real, token.Type);
            Assert.AreEqual("123.45", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetString()
        {
            Lexer lexer = new Lexer("\"foo\"");

            var token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.String, token.Type);
            Assert.AreEqual("foo", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetEndOfLineFromNewLine()
        {
            Lexer lexer = new Lexer("\n");

            var token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.EndOfLine, token.Type);
            Assert.AreEqual("\n", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetEndOfLineFromCarriageReturn()
        {
            Lexer lexer = new Lexer("\r");

            var token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.EndOfLine, token.Type);
            Assert.AreEqual("\r", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetEndOfLineFromCarriageReturnLineFeed()
        {
            Lexer lexer = new Lexer("\r\n");

            var token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.EndOfLine, token.Type);
            Assert.AreEqual("\r\n", token.Value);

            Assert.IsNull(lexer.NextToken());
        }
    }
}
