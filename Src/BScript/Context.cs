namespace BScript
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Context
    {
        private IDictionary<string, object> values = new Dictionary<string, object>();

        public Context()
        {
        }

        public object GetValue(string name)
        {
            if (this.values.ContainsKey(name))
                return this.values[name];

            return null;
        }

        public void SetValue(string name, object value)
        {
            this.values[name] = value;
        }
    }
}
