using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;
    public GameObject skinSelectionPanel;

    public Slider musicSlider;
    public Slider soundSlider;
    public Text volumeText;

    void Start()
    {
        ShowMainMenu();
        LoadSettings();
    }

    
    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        settingsPanel.SetActive(false);
        skinSelectionPanel.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OpenSettings()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
        skinSelectionPanel.SetActive(false);
    }

    public void OpenSkinSelection()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        skinSelectionPanel.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    
    void LoadSettings()
    {
        
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.7f);
        float soundVolume = PlayerPrefs.GetFloat("SoundVolume", 0.8f);

        if (musicSlider != null) musicSlider.value = musicVolume;
        if (soundSlider != null) soundSlider.value = soundVolume;

        UpdateVolumeText();
    }

    
    public void OnMusicVolumeChanged()
    {
        float volume = musicSlider.value;
        AudioManager.Instance.UpdateMusicVolume(volume);
        UpdateVolumeText();
    }

    public void OnSoundVolumeChanged()
    {
        float volume = soundSlider.value;
        AudioManager.Instance.UpdateSoundVolume(volume);
        UpdateVolumeText();
    }

    void UpdateVolumeText()
    {
        if (volumeText != null)
        {
            int musicPercent = Mathf.RoundToInt(musicSlider.value * 100);
            int soundPercent = Mathf.RoundToInt(soundSlider.value * 100);
            volumeText.text = $"Музыка: {musicPercent}% | Звуки: {soundPercent}%";
        }
    }

    public void BackFromSettings()
    {
        ShowMainMenu();
    }

    
    public void SelectSkin(int skinIndex)
    {
        Debug.Log($"Выбираем скин: {skinIndex}");

        
        if (SkinSystem.Instance != null)
        {
            SkinSystem.Instance.ChangeSkin(skinIndex);
        }
        else
        {
            
            PlayerPrefs.SetInt("SelectedSkin", skinIndex);
            PlayerPrefs.Save();
        }

        
        if (skinSelectionPanel != null)
            skinSelectionPanel.SetActive(false);

        ShowMainMenu();
    }
}