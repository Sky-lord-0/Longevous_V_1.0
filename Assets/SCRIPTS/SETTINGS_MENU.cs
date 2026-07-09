using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public GameObject volumePanel;
    public GameObject controlsPanel;

    void Start()
    {
        volumePanel.SetActive(false);
        controlsPanel.SetActive(false);
    }

    public void OpenVolume()
    {
        volumePanel.SetActive(true);
        controlsPanel.SetActive(false);
    }

    public void OpenControls()
    {
        volumePanel.SetActive(false);
        controlsPanel.SetActive(true);
    }

    public void CloseAll()
    {
        volumePanel.SetActive(false);
        controlsPanel.SetActive(false);
    }
}