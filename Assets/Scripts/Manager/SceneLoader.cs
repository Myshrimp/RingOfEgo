using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : PersistentSingleton<SceneLoader>
{
    const string MAIN = "MainMenu";
    const string GAMEPLAY = "RingScene";
    const string GAMEEND = "GameEnd";

    [SerializeField] private Image transitionImage;//Background Image with Black
    [SerializeField] private float fadeTime = 1f;// Fade time When Scene Changed

    private Color color;
    void Load(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator LoadCoroutine(string sceneName) 
    {
        // load new scene in background 
        var loadingOperation =  SceneManager.LoadSceneAsync(sceneName);
        
        if (loadingOperation == null) yield break;
        
        //set this scene inactive
        loadingOperation.allowSceneActivation = false;

        //fade out
        transitionImage.gameObject.SetActive(true);

        while (color.a < 1f)
        {
            color.a = Mathf.Clamp01(color.a + Time.unscaledDeltaTime / fadeTime);
            transitionImage.color = color;

            yield return null;
        }
        
        loadingOperation.allowSceneActivation = true;

        //fade in
        while (color.a > 0f)
        {
            color.a = Mathf.Clamp01(color.a - Time.unscaledDeltaTime / fadeTime);
            transitionImage.color = color;

            yield return null;
        }

        transitionImage.gameObject.SetActive(false);
    }

    //Call these function when need Scene Change
    public void LoadMainScene()
    {
        StartCoroutine(LoadCoroutine(MAIN));
    }

    public void LoadGameplayScene()
    {
        StartCoroutine(LoadCoroutine(GAMEPLAY));
    }

    public void LoadGameEndScene()
    {
        StartCoroutine (LoadCoroutine(GAMEEND));
    }
}
