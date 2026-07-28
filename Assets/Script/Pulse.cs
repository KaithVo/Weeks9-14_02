using System.Security.Cryptography;
using UnityEngine;

public class Pulse : MonoBehaviour
{
    public float speed = 20f;

    public AnimationCurve pulseCurve;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        //miving right
        screenPos.x += speed * Time.deltaTime;
        if (screenPos.x > Screen.width)
        {
            screenPos.x = 0;
        }
        // Use AnimationCurve for Y
        float t = screenPos.x / Screen.width;
        screenPos.y = Screen.height / 2 + pulseCurve.Evaluate(t) * 100f;

        // Convert back to world position
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        transform.position = worldPos;

    }
}
