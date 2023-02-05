using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptingEngine.API
{
    public delegate Jint.Native.JsValue JSNativeFunction(Jint.Native.JsValue value, Jint.Native.JsValue[] values);

    public abstract class JintAPI
    {
        public string ModuleName { get; protected set; }

        public abstract string __HELP__();

        public object ExposedAPI { get; protected set; }
    }

    public class JintCommand<T> : JintAPI
    {
        public Action<T> commandAction;
        private string help;

        public JintCommand(string ModuleName, string help, Action<T> command)
        {
            this.ModuleName = ModuleName;
            this.help = help;
            commandAction = command;
            ExposedAPI = commandAction;
        }

        public override string __HELP__()
        {
            return this.help;
        }
    }

    public class JintCommand : JintAPI
    {
        public Action commandAction;
        private string help;

        public JintCommand(string ModuleName, string help, Action command)
        {
            this.ModuleName = ModuleName;
            this.help = help;
            commandAction = command;
            ExposedAPI = commandAction;
        }

        public override string __HELP__()
        {
            return this.help;
        }
    }
}
