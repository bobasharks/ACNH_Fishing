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
    private float speed = 6f;
    public bool moving;
    private GameObject playerModel;
    //private Vector2 left, right, up, down;

    private void Awake()
    {
        //moving = false;
        playerMovement = new PlayerMovement();

        playerMovement.Enable();
        playerModel = transform.GetChild(0).gameObject;
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

        if (moveVec.x <= -1)
        {
            forward = moveVec.x *= -1;
            print("going left");
            playerModel.transform.rotation = Quaternion.Euler(0, 270, 0);
            
        }
        if (moveVec.x >= 1)
        {
            forward = moveVec.x;
            print("going right");
            playerModel.transform.rotation = Quaternion.Euler(0, 90, 0);
            //moving = true;
        }
        if (moveVec.y >= 1)
        {
            forward = moveVec.y;
            //moving = true;
            print("going up");
            playerModel.transform.rotation = Quaternion.Euler(0, 0, 0);
            //moving = true;
        }
        if (moveVec.y <= -1)
        {
            forward = moveVec.y *= -1;
            print("going down");
            playerModel.transform.rotation = Quaternion.Euler(0, 180, 0);
            //moving = true;
        }
        

        //sets the forward variable that controls the animation states
        //forward = moveVec.y;
        anim.SetFloat("Forward", forward);
        //anim.SetBool("Walking", moving);

    }
    public void OnMove(InputAction.CallbackContext context)
    {
        
        Vector2 moveVec = context.ReadValue<Vector2>();
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        
        
    }
}
