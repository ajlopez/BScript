﻿namespace BScript.Compiler
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

            var value = string.Empty;

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

            if (char.IsDigit(ch))
            {
                while (this.position < this.length && char.IsDigit(this.text[this.position]))
                    value += this.text[this.position++];

                return new Token(TokenType.Integer, value);
            }

            while (this.position < this.length && !char.IsWhiteSpace(this.text[this.position]))
                value += this.text[this.position++];

            return new Token(TokenType.Name, value);
        }
    }
}
