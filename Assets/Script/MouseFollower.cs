
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseFollower : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPoint(InputAction.CallbackContext context)
    {
        Vector2 mousePosition = context.ReadValue<Vector2>();
        Debug.Log(mousePosition);
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        worldMousePosition.z = 0f;

        transform.position = worldMousePosition;

    }
}
