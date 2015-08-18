namespace BScript.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class Predicates
    {
        public static bool IsFalse(object obj)
        {
            return obj == null || obj.Equals(false);
        }

        public static bool IsTrue(object obj)
        {
            return !IsFalse(obj);
        }
    }
}
