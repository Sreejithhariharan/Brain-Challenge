using UnityEngine;
using UnityEngine.UI;
public class MusicSlider : MonoBehaviour
{

    [SerializeField] Slider volumeSlider;

     void Start()
     {
        if(!PlayerPrefs.HasKey("Volume"))
        {
            PlayerPrefs.SetFloat("Volume", 1f);
            Load();
        }
        else
        {
            Load();
        }
     }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("Volume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
    }
}
