
using UnityEngine;

public class Pulse : MonoBehaviour
{
    public float speed = 20f;

    public AnimationCurve pulseCurve;

    TrailRenderer TrailRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        TrailRenderer = GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        //https://discussions.unity.com/t/trail-renderer-emit-switch-off/735579/8   
        //using emtting to turn on and off the trail renderer, so it doesn't leave a trail when it wraps around the screen
        //instead of toggle enabled which would turn off the trail renderer completely and not show any trail at all

        //miving right
        screenPos.x += speed * Time.deltaTime;
       
        if (screenPos.x > Screen.width)
        {
           TrailRenderer.emitting = false;
           screenPos.x = 0;
        }
        // Use AnimationCurve for Y
        float t = screenPos.x / Screen.width;
        screenPos.y = Screen.height / 2 + pulseCurve.Evaluate(t) * 100f;

        // Convert back to world position
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        transform.position = worldPos;

        if (screenPos.x <= 1f)
        {
            TrailRenderer.Clear();      // removes any leftover trail
            TrailRenderer.emitting = true;
        }
    }
}
