using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;
    public AudioSource bgm; // (back ground music) (dragging music file from hierarchy)
    public AudioSource[] soundEffects;

    private void Awake()
    {
        instance = this;
    }


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StopBGM()
    {
        bgm.Stop();
    }

    public void PlaySXF(int sfxNumber)
    {
        soundEffects[sfxNumber].Stop(); // in case we picked up more than 1 at once
        soundEffects[sfxNumber].Play();
    }

    public void StopSFX(int sfxNumber)
    {
        soundEffects[sfxNumber].Stop();
    }
}
