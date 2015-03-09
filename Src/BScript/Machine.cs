namespace BScript
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Machine
    {
        private Context context;

        public Machine()
        {
            this.context = new Context();
        }

        public Context RootContext { get { return this.context; } }
    }
}
