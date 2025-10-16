using UnityEngine;

public class START : MonoBehaviour
{
    public string nextSceneName;
    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
    }
}
