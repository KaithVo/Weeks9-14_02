using System.Collections;
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
    public AnimationCurve curve;
    public TextMeshProUGUI successText;
    public TextMeshProUGUI failureText;

    [Header("List Of Cups")]
    public List<Cups> cups;

    //UnityEvent
    //public UnityEvent onScoreUpdated;
    //public UnityEvent onGameFinished;//fuck can't use when create loop and make game explode
    public UnityEvent onSecretFound;//particle effect

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

    public void CupHit(Vector3 cupPosition)
    {
        if (gameFinished)
            return; 


        // 10% chance of surprise
        bool secret = Random.Range(0, 100) < 10; 
       
        if (secret)
        {
            DisplayAnimatedText(successText);

            AddScore();
            onSecretFound.Invoke();
        } 
        else 
        {
            DisplayAnimatedText(failureText);

        }
    }


    //checking if the ring is overlapped on each one or not
    public void CheckRingLanding(Vector3 ringPosition)
    {
        
        bool ringHit = false;

        for (int i = 0; i < cups.Count; i++)
        {
            if (ringHit == false)
            {
                if (cups[i].IsRingInside(ringPosition))
                {
                    cups[i].RingLanded();

                    ringHit = true;
                }
            }
        }
    }

    public void AddScore()
    {

        score += 1;
        UpdateScoreText();
        //onScoreUpdated.Invoke();

        if (score >= winningScore)
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        gameFinished = true;
        congratulationsScreen.SetActive(true);
        //onGameFinished.Invoke();
    }

    //update score
    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    //instantite text
    //https://discussions.unity.com/t/instantiating-prefab-as-child-of-existing-gameobject-c/440787/10


    private void DisplayAnimatedText(TextMeshProUGUI text)
    {
        text.gameObject.SetActive(true);
        StartCoroutine(ShowText(text));
    }

    private IEnumerator ShowText(TextMeshProUGUI text)
    {
        float t = 0f;
        while (t < 0.4f)
        {
            t += Time.deltaTime;
            text.transform.localScale = Vector3.one * curve.Evaluate(t);
            yield return null;
        }

        t = 0f;
        while (t < 0.4f)
        {
            t += Time.deltaTime;
            text.transform.localScale = Vector3.one * curve.Evaluate(1 - t);
            yield return null;
        }

        text.gameObject.SetActive(false);
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