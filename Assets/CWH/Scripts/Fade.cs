using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace CWH
{

    public class Fade : MonoBehaviour
    {
        public static Fade instance;
        public Typing typing;
        public bool IsFadeIn =false;
        public Image image;
        private int currentIndex = 0;
        public Action OnFadeIn;
        public GameObject texts;

        private void Start()
        {
            instance = this;
            if (IsFadeIn)
            {
                image.gameObject.SetActive(true);
                StartCoroutine(CoFadeIn());
            }
            else
            {
                image.gameObject.SetActive(false);
            }
            FadeOut();
            OnFadeIn += FadeIn;
        }

        private void OnDestroy()
        {
            OnFadeIn -= FadeIn;
        }

        public void FadeOut()
        {
            image.gameObject.SetActive(true);
            StartCoroutine(CoFadeOut());
            IsFadeIn = false;
        }

        public void FadeIn()
        {
            image.gameObject.SetActive(true);
            StartCoroutine(CoFadeIn());
            IsFadeIn = true;
        }

        public IEnumerator CoFadeIn()
        {
            float elapsedTime = 0f;
            float fadedTime = 1f;

            image.canvasRenderer.SetAlpha(0f);

            while (elapsedTime <= fadedTime)
            {
                image.canvasRenderer.SetAlpha(Mathf.Lerp(0f, 1f, elapsedTime / fadedTime));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            image.canvasRenderer.SetAlpha(1f);
            if(typing.NeedChangeScene)
                typing.OnChangeScene?.Invoke();
            FadeOut();
            texts.SetActive(false);

            yield break;
        }

        IEnumerator CoFadeOut()
        {
            float elapsedTime = 0f;
            float fadedTime = 1f;

            image.canvasRenderer.SetAlpha(1f);

            while (elapsedTime <= fadedTime)
            {
                image.canvasRenderer.SetAlpha(Mathf.Lerp(1f, 0f, elapsedTime / fadedTime));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            image.canvasRenderer.SetAlpha(0f);
            image.gameObject.SetActive(false);
            if(typing.NeedChangeScene)
                StartCoroutine(typing.PlayTypingEffect());
            yield break;
        }
    }

}