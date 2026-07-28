using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class LineMaker : MonoBehaviour
{
    public float growDuration;
    public Vector3 startPosition;
    public Vector3 endPosition;

    LineRenderer lineRenderer;
    Coroutine growCoroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        if (lineRenderer == null)
        {
            Debug.Log("LineMaker does not have a LineRenderer component.");
        }

    }

    // Update is called once per frame
    void Update()
    {



    }

    public void OnJump(InputAction.CallbackContext context)
    {
        //Start growing the line from left to right over an amount of time
        if (context.performed)
        {
            //This needs to be here,
            //otherwise releasing the key will also cancel the coroutine
            if (growCoroutine != null)
            {
                StopCoroutine(growCoroutine);
            }

            growCoroutine = StartCoroutine(GrowUpdate());
        }
    }

    IEnumerator GrowUpdate()
    {
        float t = 0;
        lineRenderer.positionCount = 2;

        //Line renderer positions start at index 0 (like with lists)
        lineRenderer.SetPosition(0, startPosition);
        lineRenderer.SetPosition(1, startPosition);


        while (t < growDuration)
        {
            //Grow the line using a Lerp
            Vector2 currentSecondPosition = Vector2.Lerp(startPosition, endPosition, t / growDuration);
            lineRenderer.SetPosition(1, currentSecondPosition);

            t += Time.deltaTime;
            yield return null;
        }
    }

}