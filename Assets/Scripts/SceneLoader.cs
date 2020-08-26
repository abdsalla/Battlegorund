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
    public Scene lastScene;
    public Scene startScene;
    public Scene worldScene;
    public Scene gameOverScene;
    public Scene optionsScene;

    public bool multScene;

    private IEnumerator result;
    [SerializeField] private GameManager instance;


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

    public void Return()
    {
        SceneTriggers backButton;
        GameObject rtnBtn = GameObject.FindWithTag("ReturnButton");

        if (rtnBtn)
        {
            Debug.Log("Button Exists");
            backButton = rtnBtn.GetComponent<SceneTriggers>();
            backButton.sceneLoader = this;
        }
    }

    // The below functions Load our scenes setup in build settings through Index number

    public void RunStart()
    {
        lastScene = currentScene;
        SceneManager.LoadScene(START_INDEX);
        instance.LoadSubManagers();
    }

    public void RunWorld()
    {
        lastScene = currentScene;
        SceneManager.LoadScene(WORLD_INDEX);
        instance.LoadSubManagers();
    }

    public void RunGameOver()
    {
        lastScene = currentScene;
        SceneManager.LoadScene(GAME_OVER_INDEX);
        instance.LoadSubManagers();
    }

    public void RunOptions()
    {
        SceneLoader tempSC = instance.sceneLoader;
        SoundManager tempSM = instance.mixer;
        UIManager tempUM = instance.healthTracker;

        lastScene = currentScene;
        SceneManager.LoadSceneAsync(OPTIONS_INDEX);
        ResetRef();
        Return();
    }

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

    void ResetRef()
    {
        instance.sceneLoader = this;
        Debug.Log("Reference set");
    }
}