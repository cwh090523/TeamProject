
using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.UI;
using CWH;
using UnityEngine.SceneManagement;

public class Typing : MonoBehaviour
{
    public string[] lines;
    public Action OnChangeScene;
    public TMP_Text uiText;

    public float typingSpeed = 0.05f;
    public GameObject NextTextImage;
    public Image BGImage;
    public string currentText = "";

    private int lineIndex = 0;
    public bool NeedChangeScene = false;
    private bool isTyping = false;
    public List<Sprite> images = new List<Sprite>();
    public List<GameObject> Characters = new List<GameObject>();

    private void Start()
    {
        if (uiText != null && lines.Length > 0)
        {
            StartCoroutine(PlayTypingEffect());
        }
        OnChangeScene += SceneChange;
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Write();
        }
    }
    public void Write()
    {
        if (!isTyping)
        {
            NextTextImage.SetActive(false);
            StartCoroutine(PlayTypingEffect());
        }
    }

    public IEnumerator PlayTypingEffect()
    {
        isTyping = true;
        uiText.text = "";
        if (lineIndex >= lines.Length)
        {
            Fade.instance.image.gameObject.SetActive(true);
            Fade.instance.IsFadeIn = true;
            yield return StartCoroutine(Fade.instance.CoFadeIn());
            SceneManager.LoadScene(currentText);
            yield break;
        }
        StartCoroutine(TypeLine(lines[lineIndex]));
        yield return new WaitForSeconds(typingSpeed * lines[lineIndex].Length + 1f);
        lineIndex++;
        isTyping = false;
    }
    private bool a = false;
    private IEnumerator TypeLine(string line)
    {
        uiText.text = "";
        if (line == "ChangeScene")
        {
            Debug.Log(lineIndex);
            a = true;
            NeedChangeScene = true;
            Fade.instance.OnFadeIn?.Invoke();
        }
        else if (line == "AddCharacter")
        {
            a = true;
            for (int i = 0; i < Characters.Count; i++)
            {
                Characters[i].SetActive(false);
            }
            NeedChangeScene = false;
            Characters[int.Parse(lines[lineIndex + 1])].SetActive(true);
            lineIndex++;
        }
        if(line == "Remove")
        {
            for (int i = 0; i < Characters.Count; i++)
            {
                Characters[i].SetActive(false);
            }
            a = true;
            NeedChangeScene = false;
        }
        if (a == false)
        {
            NeedChangeScene = false;
            foreach (char c in line)
            {
                uiText.text += c;
                yield return new WaitForSeconds(typingSpeed);
            }
        }
        else
        {
            a = false;
        }
        NextTextImage.SetActive(true);
    }

    private void SceneChange()
    {
        NeedChangeScene = false;
        lineIndex++;
        BGImage.sprite = images[int.Parse(lines[lineIndex])];
        lineIndex++;
        StartCoroutine(PlayTypingEffect());
    }
}