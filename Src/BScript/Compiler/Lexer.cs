namespace BScript.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Lexer
    {
        private string text;
        private int position;
        private int length;

        public Lexer(string text)
        {
            this.text = text;
            this.length = text == null ? 0 : text.Length;
            this.position = 0;
        }

        public Token NextToken()
        {
            char ch = ' ';

            while (this.position < this.length) {
                ch = this.text[this.position];

                if (ch == '\r' || ch == '\n' || !char.IsWhiteSpace(ch))
                    break;

                this.position++;
            }

            if (this.position >= this.length)
                return null;

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

            return NextName();
        }

        private Token NextName()
        {
            string value = string.Empty;

            while (this.position < this.length && !char.IsWhiteSpace(this.text[this.position]))
                value += this.text[this.position++];

            return new Token(TokenType.Name, value);
        }

        private Token NextInteger()
        {
            string value = string.Empty;

            while (this.position < this.length && char.IsDigit(this.text[this.position]))
                value += this.text[this.position++];

            return new Token(TokenType.Integer, value);
        }

        private Token NextString()
        {
            string value = string.Empty;

            this.position++;

            while (this.position < this.length && this.text[this.position] != '"')
                value += this.text[position++];

            if (this.position < this.length)
                this.position++;

            return new Token(TokenType.String, value);
        }
    }
}
