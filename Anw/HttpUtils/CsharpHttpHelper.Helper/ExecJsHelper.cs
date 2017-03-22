using System;
using System.Reflection;

namespace Anw.HttpUtils.CsharpHttpHelper.Helper
{
    internal class ExecJsHelper
    {
        private static readonly Type Type = Type.GetTypeFromProgID("ScriptControl");

        internal static string JavaScriptEval(string strJs, string main)
        {
            var scriptControl = GetScriptControl();
            SetScriptControlType(strJs, scriptControl);
            return Type.InvokeMember("Eval", BindingFlags.InvokeMethod, null, scriptControl, new object[]
            {
                main
            }).ToString();
        }

        private static Type SetScriptControlType(string strJs, object obj)
        {
            Type.InvokeMember("Language", BindingFlags.SetProperty, null, obj, new object[]
            {
                "JScript"
            });
            Type.InvokeMember("AddCode", BindingFlags.InvokeMethod, null, obj, new object[]
            {
                strJs
            });
            return Type;
        }

        private static object GetScriptControl()
        {
            return Activator.CreateInstance(Type);
        }
    }
}