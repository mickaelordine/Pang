using UnityEngine;
using UnityEngine.UI;

public class InGameUIScript : MonoBehaviour
{
    [SerializeField]
    private Button _pauseButton;
    [SerializeField]
    private Button _continueButton;
    [SerializeField]
    private Button _mainMenuButton;
    [SerializeField]
    private GameObject _pausePanel;
    [SerializeField]
    private GameObject _inGamePanel;
    
    private void OnEnable()
    {
        if (_pauseButton != null)
        {
            _pauseButton.onClick.AddListener(OpenPause); //aggiungo la funzione A ai listener del click
        }
        if (_mainMenuButton != null)
        {
            _mainMenuButton.onClick.AddListener(BackToMenu); //aggiungo la funzione A ai listener del click
        }
        if (_continueButton != null)
        {
            _continueButton.onClick.AddListener(ContinueLevel); //aggiungo la funzione A ai listener del click
        }
    }

    private void OnDisable()
    {
        if (_pauseButton != null)
        {
            _pauseButton.onClick.RemoveListener(OpenPause); //rimuovo la funzione A ai listener del click
        }
        if (_mainMenuButton != null)
        {
            _mainMenuButton.onClick.RemoveListener(BackToMenu); //rimuovo la funzione A ai listener del click
        }
        if (_continueButton != null)
        {
            _continueButton.onClick.RemoveListener(ContinueLevel); //rimuovo la funzione A ai listener del click
        }
    }

    private void OpenPause()
    {
        Time.timeScale = 0;
        _inGamePanel.SetActive(false);
        _pausePanel.SetActive(true);
    }

    private void BackToMenu()
    {
        Time.timeScale = 1;
        GameManager.Instance.SelectLevel(0); //back to MainMenu
    }

    private void ContinueLevel()
    {
        Time.timeScale = 1;
        _inGamePanel.SetActive(true);
        _pausePanel.SetActive(false);
    }
}
