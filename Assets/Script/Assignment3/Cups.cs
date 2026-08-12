
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Cups : MonoBehaviour
{
    // https://www.reddit.com/r/Unity3D/comments/gjvcse/textmeshpro_prefab_instantiates_but_not_visible/?logging_in=true
    [Header("Cup")]
    public float hitRadius = 0.5f;

    [Header("Animation")]
    public AnimationCurve curve;

    public float liftHeight = 1f;
    public float liftDuration = 0.5f;

    [Header("Game")]
    public GameManger gameManager;

    private Vector3 originalPosition;

    void Start()
    {

        originalPosition = transform.position;
    }

    public void RingLanded()
    {
        StartCoroutine(LiftCup());
    }

    //bool condition if the ring is land on the cup or not
    public bool IsRingInside(Vector3 ringPosition)
    {
        float distance = Vector3.Distance(ringPosition,transform.position);

        return distance <= hitRadius;
    }
    private IEnumerator LiftCup()
    {
        //reference gamemanger cuphit
        // Replace this line in LiftCup():
        // gameManager.cups();

        gameManager.CupHit(transform.position);

        // Lift cup
        Vector3 startPosition = transform.position;

        Vector3 endPosition = startPosition + Vector3.up * liftHeight;

        float t = 0f;

        while (t < 1f)
        {
            t +=Time.deltaTime /liftDuration;

            float value = curve.Evaluate(t);

            transform.position =Vector3.Lerp(startPosition,endPosition,value);

            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        // Bring cup back down
        t = 0f;

        while (t < 1f)
        {
            t +=Time.deltaTime /liftDuration;

            float value =curve.Evaluate(t);

            transform.position =Vector3.Lerp(endPosition,originalPosition,value);

            yield return null;
        }

        transform.position = originalPosition;
    }
}
