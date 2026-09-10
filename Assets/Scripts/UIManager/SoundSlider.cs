using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    private const string VolumeKey = "SoundVolume";

    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 1f);

        volumeSlider.value = savedVolume;

        ApplyVolume(savedVolume);
    }

    public void ChangeSoundVolume()
    {
        float volume = volumeSlider.value;

        ApplyVolume(volume);

        PlayerPrefs.SetFloat(VolumeKey, volume);
        PlayerPrefs.Save();
    }

    void ApplyVolume(float volume)
    {
        if (SoundManager.instance != null)
        {
            SoundManager.instance.SetVolume(volume);
        }
    }
}