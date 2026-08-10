using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Cups : MonoBehaviour
{
    public TextMeshProUGUI successText;
    public AnimationCurve curve;
    public UnityEvent onScoreUpdated;

    private SpriteRenderer sr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (sr.bounds.Contains(mousePos))
            {
                ShowResultText();
            }
        }
    }

    private void ShowResultText()
    {
        DisplayAnimatedText(successText);
    }
    private void DisplayAnimatedText(TextMeshProUGUI text)
    { 
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
    }
}
