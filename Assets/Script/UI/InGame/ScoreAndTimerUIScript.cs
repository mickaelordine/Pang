using System;
using TMPro; // IMPORTANTE per usare TextMeshProUGUI
using UnityEngine;

public class ScoreAndTimerUIScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timerText;

    private float timer;
    private int score;
    
    void Start()
    {
        GameManager.Instance.OnScoreChanged += UpdateScoreText;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        timerText.text = timer.ToString("0.00");
    }
    
    private void OnDestroy()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnScoreChanged -= UpdateScoreText;
    }
    
    private void UpdateScoreText(int newScore)
    {
        scoreText.text = newScore.ToString();
    }
    
}
