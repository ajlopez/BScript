namespace BScript.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Print : IFunction
    {
        private Machine machine;

        public Print(Machine machine)
        {
            this.machine = machine;
        }

        public object Evaluate(Context parent, IList<object> values)
        {
            foreach (var value in values)
                this.machine.Out.Write(value.ToString());

            return null;
        }
    }
}
