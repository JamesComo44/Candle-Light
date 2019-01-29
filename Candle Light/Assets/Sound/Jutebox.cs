using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jutebox : MonoBehaviour
{
    private GameObject juteBox;
    private AudioSource[] audioSource;
    public AudioClip[] audioClips;
    Dictionary<string, AudioClip> clips = new Dictionary<string, AudioClip>();
    float timer = 19.0f;

    //private void Awake()
    //{
    //    DontDestroyOnLoad(this.gameObject);
    //}
    // Start is called before the first frame update
    void Start()
    {
        juteBox = GameObject.Find("Jutebox");
        audioSource = juteBox.GetComponents<AudioSource>();
        CreateDictionary();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0.0f)
        {
            PlayRandom();
            timer = 19.0f;
        }
    }

    public void PlayRandom()
    {
        int clip;
        do
        {
            clip = Random.Range(4, 15);
        } while (clip == 13);

        audioSource[1].PlayOneShot(audioClips[clip]);
    }

    public void PlaySound(string soundClip)
    {
        audioSource[1].PlayOneShot(clips[soundClip]);
    }

    public void PlayMusic(string soundClip)
    {
        audioSource[0].clip = clips[soundClip];
        audioSource[0].Play();
    }

    public void StopMusic(string soundClip)
    {
        audioSource[0].clip = clips[soundClip];
        audioSource[0].Pause();
    }

    void CreateDictionary()
    {
        clips.Add("Door Opening", audioClips[0]);
        clips.Add("Fire", audioClips[1]);
        clips.Add("Flower Pickup", audioClips[2]);
        clips.Add("Footsteps up wooden stairs", audioClips[3]);
        clips.Add("Ghost Cough", audioClips[4]);
        clips.Add("Ghost Cry", audioClips[5]);
        clips.Add("Ghost Levitation", audioClips[6]);
        clips.Add("Ghost Sigh", audioClips[7]);
        clips.Add("Homemade Footsteps", audioClips[8]);
        clips.Add("Lisa Cough", audioClips[9]);
        clips.Add("Lisa gigglebot", audioClips[10]);
        clips.Add("Lisa Humming", audioClips[11]);
        clips.Add("Lisa Sigh", audioClips[12]);
        clips.Add("Rain", audioClips[13]);
        clips.Add("Squeaky Floor", audioClips[14]);

        clips.Add("Ending", audioClips[15]);
        clips.Add("Floor 1", audioClips[16]);
        clips.Add("Floor 2", audioClips[17]);
        clips.Add("Floor 3", audioClips[18]);
        clips.Add("Floor 4", audioClips[19]);
        clips.Add("Title Theme", audioClips[20]);
    }
}
