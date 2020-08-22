using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader sceneManager { get; private set; }

    public const int START_INDEX = 0;
    public const int WORLD_INDEX = 1;
    public const int GAME_OVER_INDEX = 2;
    public const int OPTIONS_INDEX = 3;

    [Header("Scene List")]
    public Scene currentScene;
    public Scene startScene;
    public Scene worldScene;
    public Scene gameOverScene;
    public Scene optionsScene;

    public bool multScene;

    private IEnumerator result;
    private GameManager instance;

    void Awake()
    {
        if (sceneManager != null && sceneManager != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            sceneManager = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void Start()
    {
        instance = GameManager.Instance;  
    }

    void ScenePrep()
    {
        currentScene = SceneManager.GetActiveScene();
        startScene = SceneManager.GetSceneByBuildIndex(0);
        worldScene = SceneManager.GetSceneByBuildIndex(1);
        gameOverScene = SceneManager.GetSceneByBuildIndex(2);
        optionsScene = SceneManager.GetSceneByBuildIndex(3);
    }

    // The below functions Load our scenes setup in build settings through Index number

    public void RunStart()
    {
        SceneManager.LoadScene(START_INDEX);
    }

    public void RunWorld()
    {
        SceneManager.LoadScene(WORLD_INDEX);
    }

    public void RunGameOver()
    {
        SceneManager.LoadScene(GAME_OVER_INDEX);
    }

    public void RunOptions() { SceneManager.LoadSceneAsync(OPTIONS_INDEX); }

    public void Quitter() { StartCoroutine(Quit()); }

    public IEnumerator Quit()
    {
        if (Application.isEditor)
        {
            Application.Quit(); // Build Quit
            yield return result;
        }

        UnityEditor.EditorApplication.isPlaying = false; // Editor Quit
        yield return result;
    }
}