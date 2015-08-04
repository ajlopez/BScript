namespace BScript.Language
{
    using System;
    using System.Collections.Generic;

    public interface IFunction
    {
        object Evaluate(BScript.Context parent, IList<object> values);
    }
}
