
using System.Collections;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RingController : MonoBehaviour
{

    [Header("Movement")]

    public float moveSpeed = 5f;
    public float minX = -4f;
    public float maxX = -4f;

    [Header("Ring")]
    public GameObject ringPrefab;
    public Transform ring;

    //throwing value
    [Header("Throw")]
    public AnimationCurve curve;
    public float throwDistance = 2f;
    public float throwDuration = 1f;
    public float minDistance = 3f;
    public float maxDistance = 8f;



    [Header("Power")]
    public Slider powerSlider;
    public float chargeSpeed = 1f;

    //Reference
    public GameManger gameManager;
    public Cups cup;

    private GameObject currentRing;

    private Vector2 moveInput;
    private float charge = 0f;
    private bool charging = false;
    private bool throwing = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    //https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/api/UnityEngine.InputSystem.InputAction.CallbackContext.html
    //https://docs.unity3d.com/Packages/com.unity.inputsystem@1.4/manual/Interactions.html
    // Update is called once per frame
    void Update()
    {


        //charge power
        if (charging)
        {
            charge += chargeSpeed * Time.deltaTime;
            charge = Mathf.Clamp01(charge);
            powerSlider.value = charge;
        }

    }

    void MovePlayer()
    {
        Vector3 pos = transform.position;
        pos.x += moveInput.x * moveSpeed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, minX, maxX); // limit them inside the screen
        transform.position = pos;
    }


    ///INPUT///
    ///
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnThrow(InputAction.CallbackContext context)
    {
        //https://docs.unity3d.com/ScriptReference/Vector3-normalized.html
        IEnumerator ThrowRing(float distance)
        {
            throwing = true;

            Vector3 startPosition = ring.transform.position;
            Vector3 cupPosition = cup.transform.position;
            Vector3 direction = (cupPosition - startPosition).normalized;
            Vector3 targetPosition = startPosition + direction * distance;

            float t = 0f;

            while (t < 1f)
            {
                t += Time.deltaTime / throwDuration;

                float value = curve.Evaluate(t);

                Vector3 pos = Vector3.Lerp(start, goal, value);

                //throwing arc
                pos.y += Mathf.Sin(value * Mathf.PI) * throwDistance;
                ring.transform.position = pos;

                yield return null;
            }
            ring.transform.position = targetPosition;

            throwing = false;
        }

    }
}
