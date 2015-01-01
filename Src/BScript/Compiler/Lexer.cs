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
            while (this.position < this.length && char.IsWhiteSpace(this.text[this.position]))
                this.position++;

            if (this.position >= this.length)
                return null;

            var value = string.Empty;

            while (this.position < this.length && !char.IsWhiteSpace(this.text[this.position]))
                value += this.text[this.position++];

            var token = new Token(TokenType.Name, value);

            return token;
        }
    }
}
