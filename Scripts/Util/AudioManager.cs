using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource gameMusic, playerHit, playerShoot, enemyHit, win, lose;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayGameMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGameMusic()
    {
        PlayClip(gameMusic);
    }
    public void PlayWinSound()
    {

    }
    public void PlayLoseSound()
    {

    }
    public void PlayPlayerHit()
    {

    }
    public void PlayEnemyHit()
    {

    }
    public void PlayPlayerShoot()
    {

    }

    private void PlayClip(AudioSource source)
    {
          source.Play();
        print(source.isPlaying);
    }
}
