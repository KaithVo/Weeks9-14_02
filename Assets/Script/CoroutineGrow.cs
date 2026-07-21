using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineGrow : MonoBehaviour
{
    public List<Transform> appleTransforms;


    public GameObject applePrefab;
    public float treeGrowDuration;
    public float appleGrowDuration;


    private Coroutine treeGrowCoroutine;
    private Coroutine appleGrowCoroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        treeGrowCoroutine = StartCoroutine(TreeGrow());



        //We need to store the spawned apple before we can destroy it
        //GameObject spawnedApple = Instantiate(applePrefab);
        //Destroy(spawnedApple);


    }

    private IEnumerator TreeGrow()
    {
        float t = 0;

        while (t < treeGrowDuration)
        {
            t += Time.deltaTime;
            transform.localScale = Vector3.one * t / treeGrowDuration;

            //Tell Unity that you can run other stuff (rather than freezing everything)
            yield return null;
        }

        //WAIT AFTER THE TREE HAS GROWN FOR A SECOND BEFORE GROWING THE APPLES
        yield return new WaitForSeconds(1);

        //IF WE WANT TO GROW ALL OF THEM AT ONCE:
        //t = 0;
        //while (t < 1)
        //{
        //    t += Time.deltaTime;

        //    for (int i = 0; i < appleTransforms.Count; i++)
        //    {
        //        appleTransforms[i].localScale = Vector3.one * t;
        //    }

        //    yield return null;
        //}

        //GROW THEM ONE AT A TIME:
        for (int i = 0; i < appleTransforms.Count; i++)
        {
            Debug.Log("Starting to grow apple: " + i);
            //LET THE APPLE COROUTINE FINISH EXECUTING BEFORE WE COME BACK TO THIS COROUTINE
            yield return appleGrowCoroutine = StartCoroutine(AppleGrow(appleTransforms[i]));
        }

        //LET EVERYTHING ELSE RUN FOR ONE FRAME
        yield return null;

        //LET EVERYTHING ELSE RUN FOR HALF A SECOND
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator AppleGrow(Transform appleTransform)
    {
        float t = 0;
        while (t < appleGrowDuration)
        {
            t += Time.deltaTime;
            appleTransform.localScale = Vector3.one * t / appleGrowDuration;

            yield return null;
        }

        //Wait half a second before growing the next apple
        yield return new WaitForSeconds(0.5f);
    }


    public void StopGrowing()
    {
        //ANY TIME YOU STOP A COROUTINE, YOU NEED TO CHECK IF IT IS NULL:
        if (treeGrowCoroutine != null)
        {
            StopCoroutine(treeGrowCoroutine);
        }

        if (appleGrowCoroutine != null)
        {
            StopCoroutine(appleGrowCoroutine);
        }

    }

}
