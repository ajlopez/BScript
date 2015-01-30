namespace BScript.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BScript.Commands;
    using BScript.Expressions;

    public class Parser
    {
        private static string[][] operators = new string[][]
        {
            new string[] { "==", "<>" },
            new string[] { "+", "-" },
            new string[] { "*", "/" }
        };

        private Lexer lexer;

        public Parser(string text)
        {
            this.lexer = new Lexer(text);
        }

        public ICommand ParseCommand()
        {
            while (this.TryParseToken(TokenType.EndOfLine))
            {
            }

            var expr = this.ParseExpression();

            if (expr == null)
                return null;

            this.ParseEndOfCommand();

            return new ExpressionCommand(expr);
        }

        public IExpression ParseExpression()
        {
            IExpression expr = this.ParseBinaryExpression(0);

            if (expr is NameExpression && this.TryParseToken(TokenType.Operator, "="))
                return new AssignExpression(((NameExpression)expr).Name, this.ParseExpression());

            return expr;
        }

        private IExpression ParseBinaryExpression(int level)
        {
            if (level >= operators.Length)
                return this.ParseSimpleExpression();

            IExpression expr = this.ParseBinaryExpression(level + 1);

            if (expr == null)
                return null;

            Token token = this.lexer.NextToken();

            while (token != null && token.Type == TokenType.Operator && operators[level].Contains(token.Value))
            {
                if (token.Value == "==")
                    expr = new BinaryOperatorExpression(BinaryOperator.Equal, expr, this.ParseBinaryExpression(level + 1));
                if (token.Value == "<>")
                    expr = new BinaryOperatorExpression(BinaryOperator.NotEqual, expr, this.ParseBinaryExpression(level + 1));
                if (token.Value == "+")
                    expr = new BinaryOperatorExpression(BinaryOperator.Add, expr, this.ParseBinaryExpression(level + 1));
                if (token.Value == "-")
                    expr = new BinaryOperatorExpression(BinaryOperator.Subtract, expr, this.ParseBinaryExpression(level + 1));
                if (token.Value == "*")
                    expr = new BinaryOperatorExpression(BinaryOperator.Multiply, expr, this.ParseBinaryExpression(level + 1));
                if (token.Value == "/")
                    expr = new BinaryOperatorExpression(BinaryOperator.Divide, expr, this.ParseBinaryExpression(level + 1));

                token = this.lexer.NextToken();
            }

            if (token != null)
                this.lexer.PushToken(token);

            return expr;
        }

        private void ParseEndOfCommand()
        {
            Token token = this.lexer.NextToken();

            if (token == null || token.Type == TokenType.EndOfLine)
                return;

            throw new ParserException(string.Format("Unexpected '{0}'", token.Value));
        }

        private IExpression ParseSimpleExpression()
        {
            var token = this.lexer.NextToken();

            if (token == null)
                return null;

            if (token.Type == TokenType.String)
                return new ConstantExpression(token.Value);

            if (token.Type == TokenType.Integer)
                return new ConstantExpression(int.Parse(token.Value));

            if (token.Type == TokenType.Name)
                return new NameExpression(token.Value);

            if (token.Type == TokenType.Delimiter && token.Value == "(")
            {
                var expr = this.ParseExpression();
                this.ParseToken(TokenType.Delimiter, ")");
                return expr;
            }

            this.lexer.PushToken(token);

            return null;
        }

        private bool TryParseToken(TokenType type)
        {
            var token = this.lexer.NextToken();

            if (token == null)
                return false;

            if (token.Type == type)
                return true;

            this.lexer.PushToken(token);

            return false;
        }

        private void ParseToken(TokenType type, string value)
        {
            var token = this.lexer.NextToken();

            if (token == null || token.Type != type || token.Value != value)
                throw new ParserException(string.Format("Expected '{0}'", value));
        }

        private bool TryParseToken(TokenType type, string value)
        {
            var token = this.lexer.NextToken();

            if (token == null)
                return false;

            if (token.Type == type && token.Value == value)
                return true;

            this.lexer.PushToken(token);

            return false;
        }
    }
}
