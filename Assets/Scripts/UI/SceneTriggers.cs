using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;

public class SceneTriggers : MonoBehaviour
{
    public AudioClip click;
    public SceneLoader sceneLoader;
    public enum Scenes { Start, World, GameOver, Options, Exit };
    public Scenes scene = Scenes.Start;

    private GameManager instance;


    void Start()
    {
        instance = GameManager.Instance;
    }

    void Update()
    {
        if (!sceneLoader)
        {
            instance = GameManager.Instance;
            instance.LoadSubManagers();
            instance.sceneLoader = sceneLoader;
        }
    }

    public void SceneSwitch()
    {
        switch (scene)
        {
            case Scenes.Start:
                sceneLoader.RunStart();
                break;
            case Scenes.World:
                sceneLoader.RunWorld();
                instance = GameManager.Instance;
                break;
            case Scenes.GameOver:
                sceneLoader.RunGameOver();
                break;
            case Scenes.Options:
                sceneLoader.RunOptions();
                break;
            case Scenes.Exit:
                sceneLoader.Quitter();
                break;
        }
    }

    public void Click()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.PlayOneShot(click);
        StartCoroutine(Press());
    }

    public void TwoPlayerMode(bool status)
    {
        sceneLoader.multScene = status;
        instance.isTwoPlayer = status;
    }

    IEnumerator Press()
    {
        yield return new WaitForSeconds(click.length);
    }

}