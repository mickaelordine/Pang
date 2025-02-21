using UnityEngine;
using UnityEngine.UI;

public class QuitListener : MonoBehaviour
{
    [SerializeField]
    private Button _quitButton;
    
    private void OnEnable()
    {
        if (_quitButton != null)
        {
            _quitButton.onClick.AddListener(quit); //aggiungo la funzione A ai listener del click
        }
    }

    private void OnDisable()
    {
        if (_quitButton != null)
        {
            _quitButton.onClick.RemoveListener(quit); //aggiungo la funzione A ai listener del click
        }
    }

    private void quit()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
