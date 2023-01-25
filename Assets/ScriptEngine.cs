using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Jint;
using Jint.Runtime.Interop;
using System.IO;

public class ScriptEngine : MonoBehaviour
{
    public class JintLoggerInterop
    {
        public static void log(string logMessage) {
            Debug.Log(logMessage);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        var engine = new Engine(cfg => cfg.AllowClr());
        engine.SetValue("print", new Action<object>(Debug.Log));
        engine.SetValue("DebugLogger", TypeReference.CreateTypeReference(engine, typeof(JintLoggerInterop)));
        string path = Application.streamingAssetsPath + "/Scripts/lib/EventSystem.js";
        string scriptText = File.ReadAllText(path);
        engine.Execute(scriptText);
        engine.Invoke("Events.RaiseEvent", "init");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
