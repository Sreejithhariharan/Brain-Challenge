using UnityEngine;

public class Option : MonoBehaviour
{
    [SerializeField] GameObject settingsPanal;

    public void OpenSettings()
    {
        settingsPanal.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanal.SetActive(false);
    }
}
