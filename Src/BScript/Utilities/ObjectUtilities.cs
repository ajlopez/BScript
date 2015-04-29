namespace BScript.Utilities
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    // Based on AjSharp AjLanguage.ObjectUtilities, taken from PythonSharp
    public class ObjectUtilities
    {
        public static void SetValue(object obj, string name, object value)
        {
            Type type = obj.GetType();

            type.InvokeMember(name, System.Reflection.BindingFlags.SetProperty | System.Reflection.BindingFlags.SetField | System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance, null, obj, new object[] { value });
        }

        public static object GetValue(object obj, string name)
        {
            Type type = obj.GetType();

            return type.InvokeMember(name, System.Reflection.BindingFlags.GetProperty | System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | /* System.Reflection.BindingFlags.InvokeMethod | */ System.Reflection.BindingFlags.Instance, null, obj, null);
        }

        public static object GetValue(object obj, string name, IList<object> arguments)
        {
            return GetNativeValue(obj, name, arguments);
        }

        public static IList<string> GetNames(object obj)
        {
            return TypeUtilities.GetNames(obj.GetType());
        }

        public static object GetNativeValue(object obj, string name, IList<object> arguments)
        {
            Type type = obj.GetType();

            return type.InvokeMember(name, System.Reflection.BindingFlags.GetProperty | System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Instance, null, obj, arguments == null ? null : arguments.ToArray());
        }

        public static bool IsNumber(object obj)
        {
            return obj is int ||
                obj is short ||
                obj is long ||
                obj is decimal ||
                obj is double ||
                obj is float ||
                obj is byte;
        }
    }
}
