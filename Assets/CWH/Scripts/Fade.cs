using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace CWH
{

    public class Fade : MonoBehaviour
    {
        public bool IsFadeIn =false;
        public Image image;
        public Image background;
        public Sprite[] sprites; // 다음 이미지 목록
        private int currentIndex = 0;

        private void Start()
        {
            if (IsFadeIn)
            {
                image.gameObject.SetActive(true);
                StartCoroutine(CoFadeIn());
            }
            else
            {
                image.gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            if (Keyboard.current.kKey.wasPressedThisFrame)
            {
                if (IsFadeIn)
                    FadeOut();
                else
                    FadeIn();
            }
        }

        public void FadeOut()
        {
            image.gameObject.SetActive(true);
            Debug.Log("FadeCanvasController_ Fade Out 시작");
            StartCoroutine(CoFadeOut());
            IsFadeIn = false;
        }

        public void FadeIn()
        {
            image.gameObject.SetActive(true);
            Debug.Log("FadeCanvasController_ Fade In 시작");
            StartCoroutine(CoFadeIn());
            IsFadeIn = true;
        }

        IEnumerator CoFadeIn()
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
            Debug.Log("Fade In 끝");

            // 페이드인 완료 후 다음 이미지로 변경
            currentIndex = (currentIndex + 1) % sprites.Length;
            background.sprite = sprites[currentIndex];

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
            Debug.Log("Fade Out 끝");
            image.gameObject.SetActive(false);
            yield break;
        }
    }

}