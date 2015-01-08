namespace BScript.Expressions
{
    using System;

    public interface IExpression
    {
        object Evaluate(BScript.Context context);
    }
}
