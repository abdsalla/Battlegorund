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


    void OnEnable()
    {
        instance = GameManager.Instance;
        if (sceneLoader == null) sceneLoader = instance.sceneLoader;
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

    IEnumerator Press()
    {
        yield return new WaitForSeconds(click.length);
    }
}