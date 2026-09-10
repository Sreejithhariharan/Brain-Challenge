using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public static BGMManager instance;

    [Header("Audio Source")]
    [SerializeField] private AudioSource audioSource;

    [Header("Music")]
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip gameMusic;

    private void Awake()
    {
        // Destroy duplicate BGM managers
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        // Keep BGM alive between scenes
        DontDestroyOnLoad(gameObject);

        // Automatically find AudioSource
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (audioSource == null)
        {
            Debug.LogError("BGMManager needs an AudioSource component!");
        }
    }

    public void PlayMenuMusic()
    {
        if (audioSource == null || menuMusic == null)
            return;

        if (audioSource.clip == menuMusic && audioSource.isPlaying)
            return;

        audioSource.clip = menuMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayGameMusic()
    {
        if (audioSource == null || gameMusic == null)
            return;

        if (audioSource.clip == gameMusic && audioSource.isPlaying)
            return;

        audioSource.clip = gameMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void SetVolume(float volume)
    {
        if (audioSource == null)
            return;

        audioSource.volume = volume;
    }

    public void StopMusic()
    {
        if (audioSource == null)
            return;

        audioSource.Stop();
    }
}