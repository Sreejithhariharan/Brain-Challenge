using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("Audio Source")]
    [SerializeField] private AudioSource audioSource;

    [Header("Sounds")]
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioClip wrongSound;
    [SerializeField] private AudioClip correctSound;

    private void Awake()
    {
        // Destroy duplicate SoundManager
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // This is the main SoundManager
        instance = this;

        // Keep it alive when changing scenes
        DontDestroyOnLoad(gameObject);

        // Automatically find AudioSource on this GameObject
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (audioSource == null)
        {
            Debug.LogError("SoundManager needs an AudioSource component!");
        }
    }

    public void PlayClick()
    {
        if (audioSource == null)
        {
            Debug.LogError("SoundManager AudioSource is missing!");
            return;
        }

        if (clickSound == null)
        {
            Debug.LogError("Click Sound is missing!");
            return;
        }

        audioSource.PlayOneShot(clickSound);
    }

    public void PlayWrong()
    {
        if (audioSource == null || wrongSound == null)
            return;

        audioSource.PlayOneShot(wrongSound);
    }

    public void PlayCorrect()
    {
        if (audioSource == null || correctSound == null)
            return;

        audioSource.PlayOneShot(correctSound);
    }

    public void SetVolume(float volume)
    {
        if (audioSource == null)
            return;

        audioSource.volume = volume;
    }
}