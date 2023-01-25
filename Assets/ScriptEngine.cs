using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jint;

public class ScriptEngine : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var engine = new Engine();
        Debug.Log(engine.Execute("3 + 5 + 4 * 5").GetCompletionValue());
    }

    // Update is called once per frame
    void Update()
    {
        Card myCard = new Card();
        myCard.Name = "Test";
    }
}
