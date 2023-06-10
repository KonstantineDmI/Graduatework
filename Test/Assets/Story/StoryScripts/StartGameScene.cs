using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameScene : MonoBehaviour
{
    public void StartGame()
    {
        StartCoroutine(LoadSceneAsyncCoroutine("GamePlayScene"));
    }

    private IEnumerator LoadSceneAsyncCoroutine(string sceneName)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            // Check if the scene has finished loading.
            if (asyncOperation.progress >= 0.9f)
            {
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }

}
