namespace BScript
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BScript.Commands;
    using BScript.Compiler;

    public class Machine
    {
        private Context context;

        public Machine()
        {
            this.context = new Context();
        }

        public Context RootContext { get { return this.context; } }

        public void Execute(string code)
        {
            Parser parser = new Parser(code);
            ICommand cmd = parser.ParseCommands();
            cmd.Execute(this.context);
        }
    }
}
