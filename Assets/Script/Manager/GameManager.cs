using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private List<GameObject> bubbles = new List<GameObject>();
    private static int _levelindex = 0;
    private int points = 0;
    private bool isFreezingActive = false;
    private PowerUpManager powerUpManager;

    /*GETTERS AND SETTERS*/
    public PowerUpManager GetPowerUpManager()
    {
        return powerUpManager;
    }

    public List<GameObject> GetBubbles()
    {
        return bubbles;
    }

    public void SetIsFreezingActive(bool isActive)
    {
        this.isFreezingActive = isActive;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            powerUpManager = GetComponent<PowerUpManager>();
        }
        else
            Destroy(gameObject); // Assicura che esista un solo GameManager SINGLETON
    }
    
    

    /*BUBBLE CHECKER SECTION*/
    public void AddBubble(GameObject bubble)
    {
        bubbles.Add(bubble);
        if(isFreezingActive)
            bubble.GetComponent<ScriptedBubbleMovement>().shouldFreeze = true;
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

    public void RemoveBubble(GameObject bubble)
    {
        Random random = new Random();
        AddPoints(10);
        if(random.Next(0,2) % 2 == 0) //50% of spawn rate
            powerUpManager.SpawnPowerUp(bubble);
        bubbles.Remove(bubble);
        checkList();
    }
    
    private void checkList()
    {
        if(this != null)
            StartCoroutine(checkListnew());
    }
    
    public void AddPoints(int points)
    {
        this.points += points;
    }
    
    
    
    /*FINISH THE LEVEL SECTION*/
    private void EndGameGood()
    {
        SelectNextLevel();
    }
    
    public void EndGameBad()
    {
        //open the retry quit menu
        SceneManager.LoadScene(0); //go to main menu
    }
    
    
    
    
    /*LEVEL SELECTION SECTION*/
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
