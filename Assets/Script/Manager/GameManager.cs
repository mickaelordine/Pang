using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private List<GameObject> bubbles = new List<GameObject>();
    private static int _levelindex = 0;
    private int points = 0;
    private bool isFreezingActive = false;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
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
        AddPoints(10);
        bubbles.Remove(bubble);
        checkList();
    }
    
    private void checkList()
    {
        if(this != null)
            StartCoroutine(checkListnew());
    }
    
    /*ACTIONS SECTION*/
    public void DestroyBubbles() //called by bomb powerUp
    {
        foreach (var elem in bubbles)
        {
            elem.GetComponent<DamageBubble>().DestroyBubble();
        }
    }
    
    public void AddPoints(int points)
    {
        this.points += points;
        Debug.Log(this.points);
    }

    IEnumerator RemoveFreezing()
    {
        yield return new WaitForSeconds(3f);
        foreach (var elem in bubbles)
        {
            elem.GetComponent<ScriptedBubbleMovement>().shouldFreeze = false;
        }
        isFreezingActive = false;
    }
    public void FreezeBubbles()
    {
        foreach (var elem in bubbles)
        {
            elem.GetComponent<ScriptedBubbleMovement>().shouldFreeze = true;
        }
        isFreezingActive = true;
        StartCoroutine(RemoveFreezing());
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
