using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;


public class RingController : MonoBehaviour
{

    [Header("Movement")]

    public float moveSpeed = 5f;
    public float minX = -4f;
    public float maxX = -4f;

    //throwing value
    [Header("Throw")]
    public AnimationCurve curve;
    public float minDistance = 3f;
    public float maxDistance = 8f;
    public float throwDistance = 2f;
    public float throwDuration = 1f;

    [Header("Ring")]
    public Transform ring;

    [Header("Power")]
    public Slider powerSlider;
    public float chargeSpeed = 1f;


    //private 
    private Vector2 moveInput;
    private bool charging;
    private bool throwing;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    //https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/api/UnityEngine.InputSystem.InputAction.CallbackContext.html
    //https://docs.unity3d.com/Packages/com.unity.inputsystem@1.4/manual/Interactions.html
    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x += moveInput.x * moveSpeed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, -limitX, limitX); // limit them inside the screen
        transform.position = pos;

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnThrow(InputAction.CallbackContext context)
    {

        IEnumerator ThrowRing(Vector3 goal)
        {
            throwing = true;

            Vector3 start = ring.position;

            float t = 0f;

            while (t < 1f)
            {
                t += Time.deltaTime / throwDuration;

                float value = curve.Evaluate(t);

                Vector3 pos = Vector3.Lerp(start, goal, value);
                pos.y += Mathf.Sin(value * Mathf.PI) * throwDistance;
                ring.position = pos;

                yield return null;
            }

            // Return the ring to the player's hand
            ring.position = transform.position + Vector3.up;

            throwing = false;
        }

    }
}
