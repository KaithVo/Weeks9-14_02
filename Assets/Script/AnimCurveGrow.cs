using UnityEngine;

public class AnimCurveGrow : MonoBehaviour
{
    public AnimationCurve growingCurve;
    public float growDuration;

    private float treeGrowProgress = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        treeGrowProgress += Time.deltaTime;
        transform.localScale = growingCurve.Evaluate(treeGrowProgress / growDuration) * Vector3.one;
        Debug.Log(treeGrowProgress);

        if (treeGrowProgress > growDuration)
        {
            //We are done growing the tree
            treeGrowProgress = 0f;
        }
    }
}
