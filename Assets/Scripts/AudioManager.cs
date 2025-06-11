using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [Header ("Audio clip")]
    public AudioClip backgorund;
    public AudioClip missleLaunch;
    public AudioClip laserShoot;
    public AudioClip rolll;
    public AudioClip blip;
    public AudioClip missileDropDowm;
    public AudioClip enemySpawn;

    
    public AudioClip[] explosions;

    public static AudioManager Instance { get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        musicSource.clip = backgorund;
        musicSource.loop = true;
        musicSource.Play();
    }


    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
        Debug.Log("Shot Played!");
    }
    public void PlayMusic()
    {
        musicSource.clip = backgorund;
        musicSource.loop = true; 
        musicSource.Play();
    }


    public void PlayExplosions()
    {
        if (explosions.Length > 0)
        {
            SFXSource.PlayOneShot(explosions[Random.Range(0, explosions.Length)]);
        }
    }





}
