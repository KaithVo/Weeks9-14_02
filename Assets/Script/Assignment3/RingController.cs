using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;


public class RingController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float limitX = 4f;

    //throwing value
    public Transform ring;
    public AnimationCurve animationCurve;
    public float throwDistance = 2f;
    public float throwDuration = 1f;


    private Vector2 moveInput;
    private bool charging;
    private bool throwing; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

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

            // Return the ring to the player's hand
            ring.position = transform.position + Vector3.up;

            Vector3 start = ring.position;

            float t = 0f;

            t += Time.deltaTime / throwDuration;

            float value = curve.Evaluate(t);

            Vector3 pos = Vector3.Lerp(start, target, value);
            pos.y += Mathf.Clamp(throwDistance, 0f, 5f); 

            ring.position = pos;

            yield return null;
  
        }

}
