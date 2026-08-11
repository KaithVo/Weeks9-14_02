using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

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
    public GameObject successPopup; 
    public GameObject failurePopup;

    [Header("List Of Cups")]
    public List<Cups> cups;

    //UnityEvent
    public UnityEvent onScoreUpdated;
    public UnityEvent onGameFinished;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0;
        gameFinished = false;
        congratulationsScreen.SetActive(false);

        UpdateScoreText();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CupHit(Vector3 cupPosition)
    {
        if (gameFinished)
            return; 

        AddScore(); // 10% chance of surprise

        bool secret = Random.Range(0, 100) < 10; 
       
        if (secret) 
        { 
            ShowPopup( successPopup, cupPosition ); 
        } 
        else 
        {
            ShowPopup( failurePopup,cupPosition ); 
        } 
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
        onGameFinished.Invoke();
    }

    //update score
    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    //instantite text
    //https://discussions.unity.com/t/instantiating-prefab-as-child-of-existing-gameobject-c/440787/10
    private void ShowPopup(GameObject popupPrefab, Vector3 position) 
    { 

        GameObject popup = Instantiate(popupPrefab, position, Quaternion.identity); 
        TextMeshPro text = popup.GetComponent<TextMeshPro>();

        Destroy(popup, 1f);

    }

    /// <summary>
    /// RESET SECTION
    /// </summary>

    public void ResetGame()
    {
        score = 0;
        gameFinished = false;
        congratulationsScreen.SetActive(false);
        UpdateScoreText();
        player.ResetPlayer();
    }
}