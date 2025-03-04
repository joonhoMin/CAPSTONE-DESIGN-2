using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{
    public static string nextScene;

    [SerializeField]
    Image progressBar;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadScene());
    }

    public static void LoadScene(string sceneName, int mapIndex)
    {
        nextScene = sceneName;
        string loadSceneName = mapIndex == 0 ? "LoadingScene_Horror_Mansion" : "LoadingScene_Laboratory";
        SceneManager.LoadScene(loadSceneName);
    }

    IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float timer = 0.0f;
        while (!op.isDone)
        {
            yield return null;

            timer += Time.deltaTime;
            if (op.progress < 0.9f)
            { 
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);
                if (progressBar.fillAmount >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);
                if (progressBar.fillAmount == 1.0f)
                {
                    op.allowSceneActivation = true; yield break;
                }
            }
        }
    }
   
}
