using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;


public class RingController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float limitX = 4f;

    private Vector2 moveInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x += moveInput.x * moveSpeed * Time.deltaTime;
        transform.position = pos;

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}
