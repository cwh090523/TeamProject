
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

    private void Start()
    {
        if (uiText != null && lines.Length > 0)
        {
            StartCoroutine(PlayTypingEffect());
        }
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            
        }
    }

    public void StartTyping()
    {
        StopAllCoroutines();
        StartCoroutine(PlayTypingEffect());
    }

    private IEnumerator PlayTypingEffect()
    {
        uiText.text = "";

        for (int i = 0; i < lines.Length; i++)
        {
                yield return StartCoroutine(TypeLine(lines[i]));
                yield return new WaitForSeconds(lineDelay);
        }
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


