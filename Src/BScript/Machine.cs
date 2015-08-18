﻿namespace BScript
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using BScript.Commands;
    using BScript.Compiler;
    using BScript.Language;

    public class Machine
    {
        private Context context;
        private TextWriter outWriter;

        public Machine()
        {
            this.outWriter = System.Console.Out;
            this.context = new Context();
            this.context.SetValue("machine", this);
            this.context.SetValue("print", new Print(this));
        }

        public Context RootContext { get { return this.context; } }

        public TextWriter Out 
        { 
            get 
            { 
                return this.outWriter; 
            }

            set 
            {
                this.outWriter = value;
            }
        }

        public static void Execute(string code, Context context)
        {
            Parser parser = new Parser(code);
            ICommand cmd = parser.ParseCommands();
            cmd.Execute(context);
        }

        public static void ExecuteFile(string filename, Context context)
        {
            var code = File.ReadAllText(filename);
            Execute(code, context);
        }

        public void Execute(string code)
        {
            Execute(code, this.context);
        }

        public void ExecuteFile(string filename)
        {
            ExecuteFile(filename, this.context);
        }
    }
}
