
using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class Typing : MonoBehaviour
{
    public string[] lines;

    public TMP_Text uiText;

    public float typingSpeed = 0.05f;

    public float lineDelay = 1f;
    
    private int lineIndex = 0;
    private bool isTyping = false;

    private void Start()
    {
        if (uiText != null && lines.Length > 0)
        {
            StartCoroutine(PlayTypingEffect());
        }
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && !isTyping)
        {
            StartCoroutine(PlayTypingEffect());
        }
    }

    public void StartTyping()
    {
        StartCoroutine(PlayTypingEffect());
    }
    
    private IEnumerator PlayTypingEffect()
    {
        isTyping = true;
        uiText.text = "";
        yield return StartCoroutine(TypeLine(lines[lineIndex]));
        yield return new WaitForSeconds(lineDelay);
        lineIndex++;
        isTyping = false;
    }

    private IEnumerator TypeLine(string line)
    {
        uiText.text = "";
        foreach (char c in line)
        {
            uiText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}


