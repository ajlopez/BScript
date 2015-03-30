namespace BScript
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Context
    {
        private Context parent;
        private IDictionary<string, object> values = new Dictionary<string, object>();

        public Context()
            : this(null)
        {
        }

        public Context(Context parent)
        {
            this.parent = parent;
        }

        public Context Parent { get { return this.parent; } }

        public bool HasReturn { get; set; }

        public object ReturnValue { get; set; }

        public object GetValue(string name)
        {
            if (this.values.ContainsKey(name))
                return this.values[name];

            if (this.parent != null)
                return this.parent.GetValue(name);

            return null;
        }

        public void SetValue(string name, object value)
        {
            this.values[name] = value;
        }
    }
}
