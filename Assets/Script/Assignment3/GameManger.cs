using UnityEngine;
using UnityEngine.Events;
using TMPro;


public class GameManger : MonoBehaviour
{

    [Header("Score")]
    public int score = 0;
    public int winningScore = 5;
    public TextMeshProUGUI scoreText;

    [Header("Player")]
    public PlayerController player;
    public bool gameFinished = false;

    //UnityEvent
    public UnityEvent onScoreUpdated;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0;
        gameFinished = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AddScore()
    {

        score++;
        UpdateScoreText();
        onScoreUpdated.Invoke();

        if (score >= winningScore)
        {
            WinGame();
        }
    }
    private void WinGame()
    {
        gameFinished = true;
        congratulationsScreen.SetActive(true);
    }
}