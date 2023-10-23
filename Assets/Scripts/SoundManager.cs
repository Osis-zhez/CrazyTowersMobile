using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] AudioSource levelMusic;
    [SerializeField] AudioSource[] sfx;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else
        {
            Destroy(gameObject);
        }
    }

    public void PlayLevelMusic()
    {
        if(!levelMusic.isPlaying)
            levelMusic.Play();
    }

    public void PlaySFX(int sfxIndex)
    {
        sfx[sfxIndex].Stop();
        sfx[sfxIndex].Play();
    }

    public void PlaySfxAdjusted(int sfxToAdjusted)
    {
        sfx[sfxToAdjusted].pitch = Random.Range(0.85f, 1.2f);
        PlaySFX(sfxToAdjusted);
    }
}
