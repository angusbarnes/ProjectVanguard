using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Highlight;
using Highlight.Engines;
using TMPro;
using UnityEngine.UI;
using Highlight.Patterns;

public class TextEditor : MonoBehaviour
{
    public TMP_InputField input;

    public class UnityHighlighter : IEngine
    {
        public string Highlight(Definition definition, string input)
        {
            Debug.Log(input);
            return "NO";
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        var highlighter = new Highlighter(new UnityHighlighter());
        var highlightedCode = highlighter.Highlight("JavaScript", "3 + 4");
        input.text = highlightedCode;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
