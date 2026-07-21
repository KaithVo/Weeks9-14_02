using UnityEngine;

public class IfGrowExample : MonoBehaviour
{
    private float t = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (t < 1)
        {
            t += Time.deltaTime;
        }
        transform.localScale = Vector3.one * t;
    }
}
