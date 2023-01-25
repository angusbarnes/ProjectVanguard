using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Jint;
using Jint.Runtime.Interop;
using System.IO;
using Jint.Native;
using System.Text;

public class ScriptEngine : MonoBehaviour
{
    public class JintLoggerInterop : JintAPI
    {
        public JintLoggerInterop()
        {
            ModuleName = "DebugLogger";
            ExposedAPI = this;
        }
        public static void log(string logMessage) {
            Debug.Log(logMessage);
        }

        public static void warn(string warning)
        {
            Debug.LogWarning(warning);
        }

        public static void error(string error)
        {
            Debug.LogError(error);
        }

        public override string __HELP__()
        {
            return "Available functions in this object are log(str), warn(str), error(str). There is also an alias for this object called logger and a shortcut called print()";
        }
    }

    public delegate Jint.Native.JsValue JSNativeFunction(Jint.Native.JsValue value, Jint.Native.JsValue[] values);

    public abstract class JintAPI
    {
        public string ModuleName { get; protected set; } 
       
        public abstract string __HELP__();

        public  object ExposedAPI { get; protected set; }
    }

    public class JintCommand<T> : JintAPI
    {
        public Action<T> commandAction;
        private string help;

        public JintCommand(string ModuleName, string help, Action<T> command) {
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

    public class JintEventSystem : JintAPI
    {
        // Pardon this absolute abomination of a variable declaration
        private Dictionary<string, List<JSNativeFunction>> events 
            = new Dictionary<string, List<JSNativeFunction>>();

        public JintEventSystem()
        {
            ModuleName = "Events";
            ExposedAPI = this;
        }

        /// <summary>
        /// This allows the Jint System to interop with the .NET Delegate system. The performance may be questionable
        /// but for simple usecases it will more than suffice. This function is not intended for use in the C# engine.
        /// </summary>
        /// <param name="callBackFunction"> This represents a JS function natively</param>
        public void Listen(string EventName, JSNativeFunction callBackFunction)
        {
            //if(configuartion.RegisteredEvent(EventName))

            if (!events.ContainsKey(EventName))
            {
                events.Add(EventName, new List<JSNativeFunction>() { callBackFunction });
                return;
            }

            events[EventName].Add(callBackFunction);

            callBackFunction.Invoke(JsValue.Undefined, new List<JsValue>() { 23d }.ToArray());
        }

        public void Invoke(string EventName) 
        {
            if (events.ContainsKey(EventName))
                events[EventName].ForEach((func) => { 
                    func.Invoke(JsValue.Undefined, new List<JsValue>() { 23d }.ToArray()); 
                });
        }

        public override string __HELP__()
        {
            return "This is the core event system. It should not be touched other than the Events.Listen(eventName, Callback) command";
        }
    }

    private Engine engine = new Engine();
    // Start is called before the first frame update
    void Start()
    {
        var Events = new JintEventSystem();
        // Dependecy Injection for the logging and callback events
        RegisterAPI(Events);
        RegisterAPI(new JintLoggerInterop());

        JintCommand<object> helpCommand = new JintCommand<object>(
            "help", 
            "Get help on other APIs", 
            new Action<object>(Help)
        );

        JintCommand listCommand = new JintCommand(
            "list", 
            "list all available function and APIs", 
            new Action(List)
        );

        RegisterAPI(helpCommand);
        RegisterAPI(listCommand);

        //engine.SetValue("help", new Action<object>(Help));
        engine.SetValue("list", new Action(List));
        string path = Application.streamingAssetsPath + "/Scripts/lib/main.js";
        string scriptText = File.ReadAllText(path);
        engine.Execute(scriptText);
        
        Events.Invoke("init");
    }

    List<JintAPI> registeredAPIs = new List<JintAPI>();
    public void List()
    {
        StringBuilder Output = new StringBuilder();
        Output.Append("The Following APIs and Modules are available: \n");
        foreach(JintAPI api in registeredAPIs)
        {
            Output.Append($"| {api.ModuleName}: {api.__HELP__()} \n");
        }

        Debug.Log(Output.ToString());
    }

    public void Help(object api)
    {
        var _api = api as JintAPI;
        
        if(_api == null) {
            Debug.Log("Cannot Provide Help on this object: " + api.ToString());
            return;
        }
        
        Debug.Log(_api.__HELP__());
    }

    void RegisterAPI(JintAPI api)
    {
        registeredAPIs.Add(api);
        engine.SetValue(api.ModuleName, api.ExposedAPI);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
