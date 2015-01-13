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
            var expr = this.ParseExpression();

            if (expr == null)
                return null;

            return new ExpressionCommand(expr);
        }

        public IExpression ParseExpression()
        {
            var expr = this.ParseSimpleExpression();

            if (expr == null)
                return null;

            if (expr is NameExpression && this.TryParseToken(TokenType.Operator, "="))
                return new AssignExpression(((NameExpression)expr).Name, this.ParseExpression());

            return expr;
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
