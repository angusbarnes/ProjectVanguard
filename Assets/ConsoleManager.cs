using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Text;
using ScriptingEngine;
using ScriptingEngine.API;

public interface IConsole
{
    public void StandardOuput(string log);
}

public class VirtualConsole
{
    public static VirtualConsole instance;
    private static IConsole consoleClient;

    public VirtualConsole(IConsole ConsoleClient)
    {
        if (instance != null)
            throw new System.Exception("The runtime already contains an instance of VirtualConsole");

        instance = this;

        Application.logMessageReceived += Application_logMessageReceived;

        consoleClient = ConsoleClient;
    }

    private void Application_logMessageReceived(string logString, string stackTrace, LogType type)
    {
        switch (type)
        {
            case LogType.Warning:
                    LogWarning(logString);
                    break;
           
            case LogType.Error:
                    LogError(logString);
                    break;
            
            default: 
                Log(logString); 
                break;
        }
    }

    public static void Log(string logString)
    {
        instance.GenericLog(logString, "white");
    }

    public static void LogError(string logString)
    {
        instance.GenericLog(logString, "#FF0000");
    }

    public static void LogWarning(string logString)
    {
        instance.GenericLog(logString, "#EE4400");
    }

    public static void LogInfo(string logString)
    {
        instance.GenericLog(logString, "#3344FF");
    }

    protected void GenericLog(string logString, string colorCode)
    {
        StringBuilder log = new StringBuilder("<color=" + colorCode + ">");
        log.Append(logString);
        log.Append("</color>");

        consoleClient.StandardOuput(log.ToString());
    }
}
