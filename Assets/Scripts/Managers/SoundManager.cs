using System.Collections.Generic;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager masterMixer { get; private set; }

    private SceneLoader sceneLoader;

    [SerializeField] AudioMixer mixer;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip menuMusic;
    [SerializeField] AudioClip worldMusic;


    void Awake()
    {
        if (masterMixer != null && masterMixer != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            masterMixer = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void Start()
    {
        sceneLoader = SceneLoader.sceneManager;
        source.Play();
    }

    public void AdjustMasterVolume(float newVol) { mixer.SetFloat("MasterVolume", newVol); }

    public void AdjustSFXVolume(float newVol) { mixer.SetFloat("SFXVolume", newVol); }

    public void AdjustAmbientVolume(float newVol) { mixer.SetFloat("AmbientVolume", newVol); }

    public void SwitchBackgroundMusic()
    {
        if (sceneLoader.currentScene == sceneLoader.worldScene)
        {
            source.Stop();
            source.loop = true;
            source.clip = worldMusic;
            source.Play();
        }
        else if (sceneLoader.currentScene == sceneLoader.startScene)
        {
            source.Stop();
            source.clip = menuMusic;
            source.loop = true;
            source.Play();
        }
    }
}