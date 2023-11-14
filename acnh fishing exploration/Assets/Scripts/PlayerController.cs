using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//Leon Guerrero, Veronica
//
// Controls the player's movement and animations
public class PlayerController : MonoBehaviour
{
    public Animator anim;
    PlayerMovement playerMovement;

    private float forward;

    private void Awake()
    {
        playerMovement = new PlayerMovement();

        playerMovement.Enable();
    }

    private void FixedUpdate()
    {
        //gets the vector2 data from the move action composite
        Vector2 moveVec = playerMovement.Controls.Walking.ReadValue<Vector2>();

        //apply the move vector to the player
        GetComponent<Rigidbody>().AddForce(new Vector3(moveVec.x, 0, moveVec.y) * 5f, ForceMode.Force);
        forward = moveVec.y;
        anim.SetFloat("Forward", forward);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 moveVec = context.ReadValue<Vector2>();
        //GetComponent<Rigidbody>().AddForce(new Vector3(moveVec.x, 0, forward) * 5f, ForceMode.Force);
    }
}
