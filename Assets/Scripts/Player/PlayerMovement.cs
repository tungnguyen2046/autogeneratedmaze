using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
    public float moveSmoothTime;
    public float walkspeed;

    private CharacterController controller;
    private Vector3 currentMoveVelocity;
    private Vector3 moveDampVelocity;

    private void Start() 
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector3 playerInput = new Vector3
        {
            x = Input.GetAxisRaw("Horizontal"),
            y = 0f,
            z = Input.GetAxisRaw("Vertical")
        };

        if(playerInput.magnitude > 1)
        {
            playerInput.Normalize();
        }

        Vector3 moveVector = transform.TransformDirection(playerInput);

        currentMoveVelocity = Vector3.SmoothDamp(currentMoveVelocity, moveVector * walkspeed, ref moveDampVelocity, moveSmoothTime);

        controller.Move(currentMoveVelocity * Time.deltaTime);
    }

}