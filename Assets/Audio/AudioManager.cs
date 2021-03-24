using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip GameLoop,BuffMusic;
    public AudioClip CollectSound,DeadSound,SlimeDead,Jump,Land;

    AudioSource musicSource,effectSource;

    private void Start() {
        musicSource = GetComponent<AudioSource>();
        musicSource.clip = GameLoop;
        musicSource.Play();
        effectSource = gameObject.AddComponent<AudioSource>();
    }

    private void OnEnable() {
    EventManager.AddHandler(GameEvent.OnInvulnerable,OnInvulnerable);
    EventManager.AddHandler(GameEvent.OnVulnerable,OnVulnerable);
    EventManager.AddHandler(GameEvent.OnJump,OnJump);
    EventManager.AddHandler(GameEvent.OnPlayerLanded,OnLanded);
    EventManager.AddHandler(GameEvent.OnIncreaseScore,OnScore);
    }
    private void OnDisable() {
    EventManager.RemoveHandler(GameEvent.OnInvulnerable,OnInvulnerable);
    EventManager.RemoveHandler(GameEvent.OnInvulnerable,OnVulnerable);
    EventManager.RemoveHandler(GameEvent.OnJump,OnJump);
    EventManager.RemoveHandler(GameEvent.OnPlayerLanded,OnLanded);
    EventManager.RemoveHandler(GameEvent.OnIncreaseScore,OnScore);
    }

    void OnInvulnerable(){
        musicSource.clip = BuffMusic;
        musicSource.Play();
    }
    void OnVulnerable(){
        musicSource.clip = GameLoop;
        musicSource.Play();
    }

    void OnJump(){
        effectSource.PlayOneShot(Jump);
    }
    void OnLanded(){
        effectSource.PlayOneShot(Land);
    }
    void OnScore(){
        effectSource.PlayOneShot(CollectSound);
    }

}
