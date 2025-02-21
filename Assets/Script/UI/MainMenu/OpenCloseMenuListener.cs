using UnityEngine;
using UnityEngine.UI;

public class OpenCloseMenuListener : MonoBehaviour
{
    [SerializeField]
    private Button _optionsButton;
    [SerializeField]
    private Button _continueButton;
    [SerializeField]
    private Button _mainMenuButton;
    [SerializeField]
    private GameObject _optionsPanel;
    [SerializeField]
    private GameObject _mainMenuPanel;
    
    private void OnEnable()
    {
        if (_optionsButton != null)
        {
            _optionsButton.onClick.AddListener(OpenOptions); //aggiungo la funzione A ai listener del click
        }
        if (_mainMenuButton != null)
        {
            _mainMenuButton.onClick.AddListener(CloseOptions); //aggiungo la funzione A ai listener del click
        }
        if (_continueButton != null)
        {
            _continueButton.onClick.AddListener(ContinueLevel); //aggiungo la funzione A ai listener del click
        }
    }

    private void OnDisable()
    {
        if (_optionsButton != null)
        {
            _optionsButton.onClick.RemoveListener(OpenOptions); //rimuovo la funzione A ai listener del click
        }
        if (_mainMenuButton != null)
        {
            _mainMenuButton.onClick.RemoveListener(CloseOptions); //rimuovo la funzione A ai listener del click
        }
        if (_continueButton != null)
        {
            _continueButton.onClick.RemoveListener(ContinueLevel); //rimuovo la funzione A ai listener del click
        }
    }

    private void OpenOptions()
    {
        _mainMenuPanel.SetActive(false);
        _optionsPanel.SetActive(true);
    }

    private void CloseOptions()
    {
        _mainMenuPanel.SetActive(true);
        _optionsPanel.SetActive(false);
    }

    private void ContinueLevel()
    {
        GameManager.Instance.ContinueLevel();
    }
}
