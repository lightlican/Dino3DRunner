using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource musicSource;
    public AudioSource soundSource;

    public AudioClip backgroundMusic; 
    public AudioClip menuMusic;       
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public AudioClip scoreSound;

    private float musicVolume = 0.7f;
    private float soundVolume = 0.8f;
    private bool isInMenu = true;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
            LoadAudioSettings();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PlayMenuMusic();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        if (scene.name == "MainMenu" || scene.name == "Menu")
        {
            isInMenu = true;
            PlayMenuMusic();
        }
        else
        {
            isInMenu = false;
            PlayGameMusic();
        }
    }

    void LoadAudioSettings()
    {
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.7f);
        soundVolume = PlayerPrefs.GetFloat("SoundVolume", 0.8f);
        ApplyVolumeSettings();
    }

    void ApplyVolumeSettings()
    {
        if (musicSource != null)
            musicSource.volume = musicVolume;

        if (soundSource != null)
            soundSource.volume = soundVolume;
    }

    public void UpdateMusicVolume(float volume)
    {
        musicVolume = volume;
        ApplyVolumeSettings();
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.Save();
    }

    public void UpdateSoundVolume(float volume)
    {
        soundVolume = volume;
        ApplyVolumeSettings();
        PlayerPrefs.SetFloat("SoundVolume", soundVolume);
        PlayerPrefs.Save();
    }

    public void PlayMenuMusic()
    {
        if (musicSource != null && menuMusic != null)
        {
            musicSource.Stop();
            musicSource.clip = menuMusic;
            musicSource.loop = true;
            musicSource.Play();
            Debug.Log("Музыка меню запущена");
        }
    }

    public void PlayGameMusic()
    {
        if (musicSource != null && backgroundMusic != null)
        {
            musicSource.Stop();
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
            Debug.Log("Игровая музыка запущена");
        }
    }

    public void StopMusic()
    {
        if (musicSource != null)
        {
            musicSource.Stop();
            Debug.Log("Музыка остановлена");
        }
    }

    public void PlayJumpSound()
    {
        PlaySound(jumpSound);
    }

    public void PlayCrashSound()
    {
        PlaySound(crashSound);
    }

    public void PlayScoreSound()
    {
        PlaySound(scoreSound);
    }

    private void PlaySound(AudioClip clip)
    {
        if (soundSource != null && clip != null)
        {
            soundSource.PlayOneShot(clip);
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}