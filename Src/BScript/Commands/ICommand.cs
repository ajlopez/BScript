namespace BScript.Commands
{
    using System;

    public interface ICommand
    {
        void Execute(BScript.Context context);
    }
}
