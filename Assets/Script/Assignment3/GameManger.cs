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
    public RingController player;
    public bool gameFinished = false;

    [Header("Congratulations")]// for end game only
    public GameObject congratulationsScreen;

    [Header("ResultText")]
    public TextMeshProUGUI successText;
    public TextMeshProUGUI failureText;


    //UnityEvent
    public UnityEvent onScoreUpdated;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0;
        gameFinished = false;
        congratulationsScreen.SetActive(false);

        successText.gameObject.SetActive(false);
        failureText.gameObject.SetActive(false);

        UpdateScoreText();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AddScore()
    {
        if (gameFinished)
            return;

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

    //update score
    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    public void ResetGame()
    {
        score = 0;
        gameFinished = false;
        congratulationsScreen.SetActive(false);
        UpdateScoreText();
        player.ResetPlayer();
    }
}