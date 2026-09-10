using System.Xml.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject howToPlayPanel;

    void Start()
    {
        if (BGMManager.instance != null)
            BGMManager.instance.PlayMenuMusic();
    }

    // =========================================
    // PLAY
    // =========================================

    public void Play()
    {
        SoundManager.instance.PlayClick();

        Invoke(nameof(LoadGame), 0.15f);
    }

    void LoadGame()
    {
        SceneManager.LoadScene("Level 1");
    }


    // =========================================
    // HOW TO PLAY
    // =========================================

    public void HowToPlay()
    {
        SoundManager.instance.PlayClick();

        howToPlayPanel.SetActive(true);
    }


    // =========================================
    // OPTIONS / SETTINGS
    // =========================================

    public void Options()
    {
        if (SoundManager.instance != null)
        {
            SoundManager.instance.PlayClick();
        }

        Invoke(nameof(LoadOptions), 0.15f);
    }


    // =========================================
    // QUIT
    // =========================================

    public void Quit()
    {
        SoundManager.instance.PlayClick();

        Invoke(nameof(QuitGame), 0.15f);
    }

    void QuitGame()
    {
        Application.Quit();
    }


    // =========================================
    // CLOSE HOW TO PLAY
    // =========================================

    public void CloseHowToPlay()
    {
        SoundManager.instance.PlayClick();

        howToPlayPanel.SetActive(false);
    }
}