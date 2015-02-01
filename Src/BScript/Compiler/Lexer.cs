namespace BScript.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Lexer
    {
        private static char[] delimiters = new char[] { '(', ')' };
        private static char[] operators = new char[] { '=', '+', '-', '*', '/', '<', '>' };

        private string text;
        private int position;
        private int length;
        private Stack<Token> tokens = new Stack<Token>();

        public Lexer(string text)
        {
            this.text = text;
            this.length = text == null ? 0 : text.Length;
            this.position = 0;
        }

        public Token NextToken()
        {
            if (this.tokens.Count > 0)
                return this.tokens.Pop();

            char ch = ' ';

            while (this.position < this.length) 
            {
                ch = this.text[this.position];

                if (ch == '\r' || ch == '\n' || !char.IsWhiteSpace(ch))
                    break;

                this.position++;
            }

            if (this.position >= this.length)
                return null;

            if (ch == '=' && this.position < this.length - 1 && this.text[this.position + 1] == '=')
            {
                this.position += 2;
                return new Token(TokenType.Operator, "==");
            }

            if (ch == '<' && this.position < this.length - 1 && this.text[this.position + 1] == '>')
            {
                this.position += 2;
                return new Token(TokenType.Operator, "<>");
            }

            if (operators.Contains(ch))
            {
                this.position++;
                return new Token(TokenType.Operator, ch.ToString());
            }

            if (delimiters.Contains(ch))
            {
                this.position++;
                return new Token(TokenType.Delimiter, ch.ToString());
            }

            if (ch == '\n')
            {
                this.position++;
                return new Token(TokenType.EndOfLine, "\n");
            }

            if (ch == '\r')
            {
                this.position++;

                if (this.position < this.length && this.text[this.position] == '\n')
                {
                    this.position++;
                    return new Token(TokenType.EndOfLine, "\r\n");
                }

                return new Token(TokenType.EndOfLine, "\r");
            }

            if (ch == '"')
                return this.NextString();

            if (char.IsDigit(ch))
                return this.NextInteger();

            return this.NextName();
        }

        public void PushToken(Token token)
        {
            this.tokens.Push(token);
        }

        private Token NextName()
        {
            string value = string.Empty;

            while (this.position < this.length && char.IsLetter(this.text[this.position]))
                value += this.text[this.position++];

            return new Token(TokenType.Name, value);
        }

        private Token NextInteger()
        {
            string value = string.Empty;

            while (this.position < this.length && char.IsDigit(this.text[this.position]))
                value += this.text[this.position++];

            if (this.position < this.length - 1 && this.text[this.position] == '.' && char.IsDigit(this.text[this.position + 1])) 
            {
                this.position++;
                return this.NextReal(value);
            }

            return new Token(TokenType.Integer, value);
        }

        private Token NextReal(string integers)
        {
            string value = integers + ".";

            while (this.position < this.length && char.IsDigit(this.text[this.position]))
                value += this.text[this.position++];

            return new Token(TokenType.Real, value);
        }

        private Token NextString()
        {
            string value = string.Empty;

            this.position++;

            while (this.position < this.length && this.text[this.position] != '"')
                value += this.text[this.position++];

            if (this.position < this.length)
                this.position++;

            return new Token(TokenType.String, value);
        }
    }
}
