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
    private float speed = 4f;
    private bool moving = false;
    private Vector2 left, right, up, down;

    private void Awake()
    {
        playerMovement = new PlayerMovement();

        playerMovement.Enable();

        /*
        up = new Vector2(0, 1);
        down = new Vector2(0, -1);
        left = new Vector2(-1, 0);
        right = new Vector2(1, 0);
        */
    }

    private void Update()
    {
        //gets the vector2 data from the move action composite
        Vector2 moveVec = playerMovement.Controls.Walking.ReadValue<Vector2>();
        transform.Translate(new Vector3(moveVec.x, 0, moveVec.y) * speed * Time.deltaTime);

        //sets the forward variable that controls the animation states
        forward = moveVec.y;
        anim.SetFloat("Forward", forward);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        
        Vector2 moveVec = context.ReadValue<Vector2>();
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        
        
    }
}
