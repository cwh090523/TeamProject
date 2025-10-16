using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class Fade : MonoBehaviour
{
    public bool IsFadeIn;
    public Image image;

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
            {
                FadeIn();
            }
            else
            {
                FadeOut();
            }
        }
    }
    public void FadeOut()
    {
        image.gameObject.SetActive(true); // Panel 활성화
        Debug.Log("FadeCanvasController_ Fade Out 시작");
        StartCoroutine(CoFadeOut());
        Debug.Log("FadeCanvasController_ Fade Out 끝");
        IsFadeIn = true;
    }
    public void FadeIn()
    {
        image.gameObject.SetActive(true); // Panel 활성화
        Debug.Log("FadeCanvasController_ Fade Out 시작");
        StartCoroutine(CoFadeIn());
        Debug.Log("FadeCanvasController_ Fade Out 끝");
        IsFadeIn = false;
    }
    IEnumerator CoFadeIn()
    {
        float elapsedTime = 0f; // 누적 경과 시간
        float fadedTime = 1f; // 총 소요 시간

        while (elapsedTime <= fadedTime)
        {
            image.GetComponent<CanvasRenderer>().SetAlpha(Mathf.Lerp(1f, 0f, elapsedTime / fadedTime));

            elapsedTime += Time.deltaTime;
            Debug.Log("Fade In 중...");
            yield return null;
        }
        Debug.Log("Fade In 끝");
        image.gameObject.SetActive(false); // Panel을 비활성화
        yield break;
    }
    IEnumerator CoFadeOut()
    {
        float elapsedTime = 0f; // 누적 경과 시간
        float fadedTime = 1f; // 총 소요 시간

        while (elapsedTime <= fadedTime)
        {
            image.GetComponent<CanvasRenderer>().SetAlpha(Mathf.Lerp(0f, 1f, elapsedTime / fadedTime));

            elapsedTime += Time.deltaTime;
            Debug.Log("Fade Out 중...");
            yield return null;
        }

        Debug.Log("Fade Out 끝");
        image.gameObject.SetActive(false);
        yield break;
    }

}
