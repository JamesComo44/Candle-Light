using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jutebox : MonoBehaviour
{
    [System.Serializable]
    public class LevelMusic
    {
        public AudioClip audioClip;
        public float volume = 1.0f;
    }

    [Header("                                Configure JuteBox")]
    [SerializeField] public float RandomInterval;
    [SerializeField] public AudioClip[] randomClips;
    [Header("Music")]

    [SerializeField] public AudioClip MainMenu;
    [SerializeField] public AudioClip Floor1;
    [SerializeField] public AudioClip Floor2;
    [SerializeField] public AudioClip Floor3;
    [SerializeField] public AudioClip Floor4;
    [SerializeField] public AudioClip Ending;
    [SerializeField] public RemoteSource[] remoteSources;

    [SerializeField] public LevelMusic MainMenu;
    [SerializeField] public LevelMusic Floor1;
    [SerializeField] public LevelMusic Floor2;
    [SerializeField] public LevelMusic Floor3;
    [SerializeField] public LevelMusic Floor4;
    [SerializeField] public LevelMusic Ending;


    private GameObject juteBox;
    private AudioSource[] audioSource;
    private bool isPlaying = false;
    private float timer;
    private AudioClip[] audioClips;


    Dictionary<string, AudioClip> clips = new Dictionary<string, AudioClip>();

    [System.Serializable]
    public class RemoteSource
    {
        public GameObject gameObject;
        public AudioSource[] audioSource;
    }

    public class Play
    {
        /// <summary>
        /// Play oneshot of the audioclip corresponding to the string passed.
        /// </summary>
        /// <param name="soundClipName"></param>
        public void Sound(string soundClipName)
        {
            try
            {
                audioSource[1].PlayOneShot(clips[soundClipName]);
            }
            catch
            {
                print("AudioClip '" + soundClipName + "' not found.");
            }
        }


        /// <summary>
        /// Play a loop of the audioclip corresponding to the string passed.
        /// </summary>
        /// <param name="soundClipName"></param>
        public void Music(string soundClipName)
        {
            try
            {
                audioSource[0].clip = clips[soundClipName];
                audioSource[0].Play();
            }
            catch
            {
                print("AudioClip '" + soundClipName + "' not found.");
            }
        }


        /// <summary>
        /// Enable/Disable random play.
        /// </summary>
        /// <param name="val"></param>
        /// <param name="interval"></param>
        public void Random(bool val, float interval)
        {
            isPlaying = val;
            RandomInterval = interval;
        }

        /// <summary>
        /// Enable/Disable random play.
        /// </summary>
        /// <param name="val"></param>
        /// <param name="interval"></param>
        public void Random(bool val)
        {
            isPlaying = val;
        }
    }


    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("JuteBox");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    //Start is called before the first frame update
    void Start()
    {
        juteBox = GameObject.FindGameObjectWithTag("JuteBox");
        audioSource = juteBox.GetComponents<AudioSource>();
        LoadAudioClips();
        CreateDictionary();
        timer = RandomInterval;
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        PlayMusic(MainMenu.audioClip.name);
    }

    private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "Main Menu":
                {
                    PlayMusic(MainMenu.audioClip.name);
                    MusicVolume(MainMenu.volume);
                    PlayRandom(false);
                    break;
                }
            case "Level 1":
                {
                    PlayMusic(Floor1.audioClip.name);
                    MusicVolume(Floor1.volume);
                    PlayRandom(true);
                    break;
                }
            case "Level 2":
                {
                    PlayMusic(Floor2.audioClip.name);
                    MusicVolume(Floor2.volume);
                    PlayRandom(true);
                    break;
                }
            case "Level 3":
                {
                    PlayMusic(Floor3.audioClip.name);
                    MusicVolume(Floor3.volume);
                    PlayRandom(true);
                    break;
                }
            case "Level 4":
                {
                    PlayMusic(Floor4.audioClip.name);
                    MusicVolume(Floor4.volume);
                    PlayRandom(true);
                    break;
                }
            case "Final Scene":
                {
                    PlayMusic(Ending.audioClip.name);
                    MusicVolume(Ending.volume);
                    PlayRandom(false);
                    break;
                }
        }
    }

    // Update is called once per frame
    void Update()
    {
        RandomPlayInterval();
        ForceLevelChance();
    }

    void ForceLevelChance()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                SceneManager.LoadSceneAsync(0);
            }
            else
            {
                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }


    // loads all the audio files in the resource folder into an array
    private void LoadAudioClips()
    {
        audioClips = Resources.LoadAll<AudioClip>("Sound");
    }

    // Creates a dictionary that allows reference of array elements by audio file name
    void CreateDictionary()
    {
        foreach (AudioClip audio in audioClips)
            clips.Add(audio.name, audio);
    }

    // Plays the random audioclips at the determined interval.
    void RandomPlayInterval()
    {
        if (isPlaying)
        {
            timer -= Time.deltaTime;
            if (timer <= 0.0f)
            {
                audioSource[1].PlayOneShot(randomClips[UnityEngine.Random.Range(0, randomClips.Length)]);
                timer = RandomInterval;
            }
        }
    }



    /// <summary>
    /// Set the sound effect volume.
    /// </summary>
    /// <param name="volume"></param>
    public void SoundVolume(float volume)
    {
        audioSource[1].volume = volume;
    }


    /// <summary>
    /// Set the music volume.
    /// </summary>
    /// <param name="volume"></param>
    public void MusicVolume(float volume)
    {
        audioSource[0].volume = volume;
    }

    /// <summary>
    /// Pause the current looping audioclip.
    /// </summary>
    public void PauseMusic()
    {
        audioSource[0].Pause();
    }
    
}
