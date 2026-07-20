
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    public Transform gunBarrel;

    private Vector2 look;
    private Vector2 movementDirection = Vector2.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //If we have a Vector2 and we want to add it to the a Vector3
        //We can convert between them using (Vector3) in front of the Vector2
        transform.position += (Vector3)movementDirection * speed * Time.deltaTime;

        if (look != Vector2.zero)
        {
            gunBarrel.right = look;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementDirection = context.ReadValue<Vector2>();
        //Debug.Log(movementDirection);
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        Debug.Log("Attack!");
    }
    public void OnLook(InputAction.CallbackContext context)
    {
        look = context.action.ReadValue<Vector2>();
    }
}