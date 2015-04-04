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
            new string[] { "==", "<>", "<", ">", "<=", ">=" },
            new string[] { "+", "-" },
            new string[] { "*", "/" }
        };

        private Lexer lexer;

        public Parser(string text)
        {
            this.lexer = new Lexer(text);
        }

        public ICommand ParseCommands()
        {
            IList<ICommand> cmds = new List<ICommand>();

            for (ICommand cmd = this.ParseCommand(); cmd != null; cmd = this.ParseCommand())
                cmds.Add(cmd);

            if (cmds.Count == 1)
                return cmds[0];

            return new CompositeCommand(cmds);
        }

        public ICommand ParseCommand()
        {
            while (this.TryParseToken(TokenType.EndOfLine))
            {
            }

            var token = this.lexer.NextToken();

            if (token == null)
                return null;

            if (token.Type == TokenType.Name)
            {
                if (token.Value == "if") 
                    return this.ParseIfCommand();
                if (token.Value == "for")
                    return this.ParseForCommand();
                if (token.Value == "while")
                    return this.ParseWhileCommand();
                if (token.Value == "return")
                    return this.ParseReturnCommand();
                if (token.Value == "function")
                    return this.ParseFunctionCommand();
            }

            this.lexer.PushToken(token);

            var expr = this.ParseExpression();

            this.ParseEndOfCommand();

            return new ExpressionCommand(expr);
        }

        public IExpression ParseExpression()
        {
            IExpression expr = this.ParseOrLogicalExpression();

            if (expr is NameExpression && this.TryParseToken(TokenType.Operator, "="))
                return new AssignExpression(((NameExpression)expr).Name, this.ParseExpression());

            return expr;
        }

        private ICommand ParseFunctionCommand()
        {
            string name = this.ParseName();

            IList<string> argnames = new List<string>();

            this.ParseToken(TokenType.Delimiter, "(");

            while (!this.TryParseToken(TokenType.Delimiter, ")"))
            {
                if (argnames.Count > 0)
                    this.ParseToken(TokenType.Delimiter, ",");

                argnames.Add(this.ParseName());
            }

            this.ParseEndOfCommand();
            IList<ICommand> bodycmds = new List<ICommand>();

            while (!this.TryParseToken(TokenType.Name, "end"))
                bodycmds.Add(this.ParseCommand());

            this.ParseEndOfCommand();

            ICommand body;

            if (bodycmds.Count == 1)
                body = bodycmds[0];
            else
                body = new CompositeCommand(bodycmds);

            return new FunctionCommand(name, argnames, body);
        }

        private ICommand ParseForCommand()
        {
            string name = this.ParseName();

            this.ParseToken(TokenType.Operator, "=");

            IExpression fromexpr = this.ParseExpression();

            this.ParseToken(TokenType.Name, "to");

            IExpression toexpr = this.ParseExpression();

            IExpression stepexpr = null;

            if (this.TryParseToken(TokenType.Name, "step"))
                stepexpr = this.ParseExpression();

            this.ParseEndOfCommand();

            IList<ICommand> bodycmds = new List<ICommand>();

            while (!this.TryParseToken(TokenType.Name, "end"))
                bodycmds.Add(this.ParseCommand());

            this.ParseEndOfCommand();

            if (bodycmds.Count == 1)
                return new ForCommand(name, fromexpr, toexpr, stepexpr, bodycmds[0]);
            else
                return new ForCommand(name, fromexpr, toexpr, stepexpr, new CompositeCommand(bodycmds));
        }

        private ICommand ParseIfCommand()
        {
            IExpression cond = this.ParseExpression();
            this.ParseEndOfCommand();
            IList<ICommand> thencmds = new List<ICommand>();
            IList<ICommand> elsecmds = new List<ICommand>();
            bool inelse = false;

            while (!this.TryParseToken(TokenType.Name, "end"))
            {
                if (!inelse && this.TryParseToken(TokenType.Name, "else"))
                {
                    inelse = true;
                    continue;
                }

                if (inelse)
                    elsecmds.Add(this.ParseCommand());
                else
                    thencmds.Add(this.ParseCommand());
            }

            this.ParseEndOfCommand();

            ICommand thencmd = null;
            ICommand elsecmd = null;

            if (thencmds.Count == 1)
                thencmd = thencmds[0];
            else
                thencmd = new CompositeCommand(thencmds);

            if (elsecmds.Count == 1)
                elsecmd = elsecmds[0];
            else if (elsecmds.Count > 1)
                elsecmd = new CompositeCommand(elsecmds);

            return new IfCommand(cond, thencmd, elsecmd);
        }

        private ICommand ParseReturnCommand()
        {
            if (this.TryParseEndOfCommand())
                return new ReturnCommand(null);

            ICommand cmd = new ReturnCommand(this.ParseExpression());

            this.ParseEndOfCommand();

            return cmd;
        }

        private ICommand ParseWhileCommand()
        {
            IExpression cond = this.ParseExpression();
            this.ParseEndOfCommand();
            IList<ICommand> cmds = new List<ICommand>();

            while (!this.TryParseToken(TokenType.Name, "end"))
                cmds.Add(this.ParseCommand());

            this.ParseEndOfCommand();

            if (cmds.Count == 1)
                return new WhileCommand(cond, cmds[0]);

            return new WhileCommand(cond, new CompositeCommand(cmds));
        }

        private IExpression ParseOrLogicalExpression()
        {
            IExpression expr = this.ParseAndLogicalExpression();

            if (expr == null)
                return null;

            while (this.TryParseToken(TokenType.Name, "or"))
                expr = new OrExpression(expr, this.ParseAndLogicalExpression());

            return expr;
        }

        private IExpression ParseAndLogicalExpression()
        {
            IExpression expr = this.ParseNotLogicalExpression();

            if (expr == null)
                return null;

            while (this.TryParseToken(TokenType.Name, "and"))
                expr = new AndExpression(expr, this.ParseNotLogicalExpression());

            return expr;
        }

        private IExpression ParseNotLogicalExpression()
        {
            IExpression expr = null;

            while (this.TryParseToken(TokenType.Name, "not"))
                expr = new NotExpression(this.ParseNotLogicalExpression());

            if (expr == null)
                expr = this.ParseBinaryExpression(0);

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
                if (token.Value == "<")
                    expr = new BinaryOperatorExpression(BinaryOperator.Less, expr, this.ParseBinaryExpression(level + 1));
                if (token.Value == ">")
                    expr = new BinaryOperatorExpression(BinaryOperator.Greater, expr, this.ParseBinaryExpression(level + 1));
                if (token.Value == "<=")
                    expr = new BinaryOperatorExpression(BinaryOperator.LessEqual, expr, this.ParseBinaryExpression(level + 1));
                if (token.Value == ">=")
                    expr = new BinaryOperatorExpression(BinaryOperator.GreaterEqual, expr, this.ParseBinaryExpression(level + 1));
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

        private string ParseName()
        {
            Token token = this.lexer.NextToken();

            if (token != null && token.Type == TokenType.Name)
                return token.Value;

            throw new ParserException("Name expected");
        }

        private void ParseEndOfCommand()
        {
            Token token = this.lexer.NextToken();

            if (token == null || token.Type == TokenType.EndOfLine)
                return;

            throw new ParserException(string.Format("Unexpected '{0}'", token.Value));
        }

        private bool TryParseEndOfCommand()
        {
            Token token = this.lexer.NextToken();

            if (token == null || token.Type == TokenType.EndOfLine)
                return true;

            this.lexer.PushToken(token);

            return false;
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
            {
                if (token.Value == "new")
                {
                    var callexpr = (CallExpression)this.ParseSimpleExpression();
                    return new NewExpression(new NameExpression(callexpr.Name), callexpr.ArgumentExpressions);
                }

                if (!this.TryParseToken(TokenType.Delimiter, "("))
                {
                    IExpression expr = new NameExpression(token.Value);

                    while (this.TryParseToken(TokenType.Delimiter, "."))
                        expr = new DotExpression(expr, this.ParseName());

                    return expr;
                }

                IList<IExpression> argexprs = new List<IExpression>();

                while (!this.TryParseToken(TokenType.Delimiter, ")"))
                {
                    if (argexprs.Count > 0)
                        this.ParseToken(TokenType.Delimiter, ",");

                    argexprs.Add(this.ParseExpression());
                }

                return new CallExpression(token.Value, argexprs);
            }

            if (token.Type == TokenType.Delimiter && token.Value == "(")
            {
                var expr = this.ParseExpression();
                this.ParseToken(TokenType.Delimiter, ")");
                return expr;
            }

            throw new ParserException(string.Format("Unexpected '{0}'", token.Value));
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
