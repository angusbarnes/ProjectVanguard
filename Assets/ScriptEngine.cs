using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jint;
using System;

public class ScriptEngine : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var engine = new Engine();
        engine.SetValue("print", new Action<object>(Debug.Log));
        engine.SetValue("printError", new Action<object>(Debug.LogError));
        engine.SetValue("printWarning", new Action<object>(Debug.LogWarning));

        engine.Execute(@"
                        class EventSystem { 
                            static events = [];
                            static OnInit(func) { 
                                this.events.push(func)
                            }
                            static _init() {
                                for (var event of this.events) {
                                    event.Invoke()
                                }
                            }
                        }
                      ");

        engine.Invoke("EventSystem.Init()");
        Debug.Log(engine.Execute("print('test')"));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
