using UnityEngine;

public class GrowTree : MonoBehaviour
{
    public float treeGrowDuration;

    private float treeGrowProgress = 0f;

    public Transform appleTransform;

    public float appleGrowDuration;
    private float appleGrowProgress = 0f;


    private bool onGrowPressed = false;
    private bool onTreeGrowDone = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (onGrowPressed)
        {
            treeGrowProgress += Time.deltaTime;
            transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, treeGrowProgress / treeGrowDuration);

            if (treeGrowProgress > treeGrowDuration)
            {
                onTreeGrowDone = true;
            }
        }

        if (onTreeGrowDone)
        {
            appleGrowProgress += Time.deltaTime;

            //grow the apple
            appleTransform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, appleGrowProgress / appleGrowDuration);
        }

    }

    public void OnGrow()
    {
        onGrowPressed = true;
    }
}
