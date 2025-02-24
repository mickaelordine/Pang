using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private List<IBubble> bubbles = new List<IBubble>();
    private static int _levelindex = 0;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject); // Assicura che esista un solo GameManager SINGLETON
    }

    public void AddBubble(IBubble bubble)
    {
        bubbles.Add(bubble);
    }

    private IEnumerator checkListnew()
    {
        yield return new WaitForSeconds(2f);
        if (bubbles.Count == 0)
        {
            // Chiama la funzione per terminare il gioco
            if(gameObject)
                EndGameGood();
        }
    }

    public void RemoveBubble(IBubble bubble)
    {
        bubbles.Remove(bubble);
        checkList();
    }

    public void EndGameBad()
    {
        //open the retry quit menu
        SceneManager.LoadScene(0); //go to main menu
    }

    private void checkList()
    {
        if(this != null)
            StartCoroutine(checkListnew());
    }
    
    private void EndGameGood()
    {
        SelectNextLevel();
    }
    
    public void SelectNextLevel()
    {
        _levelindex++;
        PlayerPrefs.SetInt("Level", _levelindex);
        SceneManager.LoadScene(_levelindex); // Load by build index
    }

    public void ContinueLevel()
    {
        _levelindex = PlayerPrefs.GetInt("Level");
        SceneManager.LoadScene(_levelindex);
    }
    
    public void SelectLevel(int selectedLevel)
    {
        _levelindex = selectedLevel;
        PlayerPrefs.SetInt("Level", _levelindex);
        SceneManager.LoadScene(_levelindex); // Load by build index
    }
    
    
}
