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
            var expr = this.ParseSimpleExpression();

            if (expr == null)
                return null;

            if (this.TryParseToken(TokenType.Operator, "+"))
                expr = new BinaryOperatorExpression(BinaryOperator.Add, expr, this.ParseSimpleExpression());
            if (this.TryParseToken(TokenType.Operator, "-"))
                expr = new BinaryOperatorExpression(BinaryOperator.Subtract, expr, this.ParseSimpleExpression());
            if (this.TryParseToken(TokenType.Operator, "*"))
                expr = new BinaryOperatorExpression(BinaryOperator.Multiply, expr, this.ParseSimpleExpression());
            if (this.TryParseToken(TokenType.Operator, "/"))
                expr = new BinaryOperatorExpression(BinaryOperator.Divide, expr, this.ParseSimpleExpression());

            if (expr is NameExpression && this.TryParseToken(TokenType.Operator, "="))
                return new AssignExpression(((NameExpression)expr).Name, this.ParseExpression());

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
