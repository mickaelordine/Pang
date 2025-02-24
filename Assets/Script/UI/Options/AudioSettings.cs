using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;  // Reference to your AudioMixer
    [SerializeField]
    private Scrollbar masterScrollbar;
    [SerializeField]
    private Scrollbar musicScrollbar;
    [SerializeField]
    private Scrollbar sfxScrollbar;

    void Start()
    {
        // Initialize scrollbars from saved PlayerPrefs or default values
        masterScrollbar.value = PlayerPrefs.GetFloat("Master");
        musicScrollbar.value = PlayerPrefs.GetFloat("Music");
        sfxScrollbar.value = PlayerPrefs.GetFloat("SFX");

        // Attach listeners to handle value changes
        masterScrollbar.onValueChanged.AddListener(SetMasterVolume);
        musicScrollbar.onValueChanged.AddListener(SetMusicVolume);
        sfxScrollbar.onValueChanged.AddListener(SetSFXVolume);

        // Apply initial values
        SetMasterVolume(masterScrollbar.value);
        SetMusicVolume(musicScrollbar.value);
        SetSFXVolume(sfxScrollbar.value);
    }

    public void SetMasterVolume(float value)
    {
        audioMixer.SetFloat("Master", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20);
        PlayerPrefs.SetFloat("Master", value);
    }

    public void SetMusicVolume(float value)
    {
        audioMixer.SetFloat("Music", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20);
        PlayerPrefs.SetFloat("Music", value);
    }

    public void SetSFXVolume(float value)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20);
        PlayerPrefs.SetFloat("SFX", value);
    }

    private void OnDisable()
    {
        PlayerPrefs.Save(); // Ensure volume settings persist
    }
}
