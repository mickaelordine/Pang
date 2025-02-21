using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelListener : MonoBehaviour
{
    [SerializeField]
    private Button _levelButton;
    [SerializeField]
    private int _levelindex;
    
    private void OnEnable()
    {
        if (_levelButton != null)
        {
            _levelButton.onClick.AddListener(SelectLevel); //aggiungo la funzione A ai listener del click
        }
    }

    private void OnDisable()
    {
        if (_levelButton != null)
        {
            _levelButton.onClick.RemoveListener(SelectLevel); //aggiungo la funzione A ai listener del click
        }
    }

    private void SelectLevel()
    {
        GameManager.Instance.SelectLevel(_levelindex);
    }
}
