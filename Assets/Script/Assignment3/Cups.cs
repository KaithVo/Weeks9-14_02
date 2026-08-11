
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Cups : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI successText;
    public TextMeshProUGUI failureText;

    [Header("Animation")]
    public AnimationCurve curve;

    public float liftHeight = 1f;
    public float liftDuration = 0.5f;

    [Header("Game")]
    public GameManger gameManager;

    private Vector3 originalPosition;

    void Start()
    {
        successText.gameObject.SetActive(false);
        failureText.gameObject.SetActive(false);

        originalPosition =
            transform.position;
    }

    public void RingLanded()
    {
        StartCoroutine(LiftCup());
    }

    private IEnumerator LiftCup()
    {
        bool secret = IsSecret();

        if (secret)
        {
            DisplayAnimatedText(
                successText
            );

            gameManager.AddScore();
        }
        else
        {
            DisplayAnimatedText(
                failureText
            );
        }

        // Lift cup
        Vector3 startPosition =
            transform.position;

        Vector3 endPosition =
            startPosition +
            Vector3.up * liftHeight;

        float t = 0f;

        while (t < 1f)
        {
            t +=
                Time.deltaTime /
                liftDuration;

            float value =
                curve.Evaluate(t);

            transform.position =
                Vector3.Lerp(
                    startPosition,
                    endPosition,
                    value
                );

            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        // Bring cup back down
        t = 0f;

        while (t < 1f)
        {
            t +=
                Time.deltaTime /
                liftDuration;

            float value =
                curve.Evaluate(t);

            transform.position =
                Vector3.Lerp(
                    endPosition,
                    originalPosition,
                    value
                );

            yield return null;
        }

        transform.position =
            originalPosition;
    }

    private bool IsSecret()
    {
        int result =
            Random.Range(0, 100);

        return result < 10;
    }

    private void DisplayAnimatedText(
        TextMeshProUGUI text
    )
    {
        text.gameObject.SetActive(true);

        StartCoroutine(
            ShowText(text)
        );
    }

    private IEnumerator ShowText(
        TextMeshProUGUI text
    )
    {
        float t = 0f;

        while (t < 0.4f)
        {
            t += Time.deltaTime;

            text.transform.localScale =
                Vector3.one *
                curve.Evaluate(t);

            yield return null;
        }

        t = 0f;

        while (t < 0.4f)
        {
            t += Time.deltaTime;

            text.transform.localScale =
                Vector3.one *
                curve.Evaluate(1f - t);

            yield return null;
        }

        text.gameObject.SetActive(false);
    }
}
