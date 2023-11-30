using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    private bool canCast = true;
    private bool stillCast = false;
    public GameObject bobberPrefab;
    public GameObject bobber;
    public GameObject bobberStartPosObj;
    public GameObject bobberEndPosObj;

    public Animator anim;
    PlayerController playerController;

    //public Vector3 bobberStartPos;
    //public Vector3 bobberEndPos;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        playerController = GetComponent<PlayerController>();
    }
    private void Start()
    {
        
        
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canCast == true)
        {
            Cast();
        }
        else if (Input.GetMouseButtonDown(0) && stillCast == true)
        {
            print("reeling");
            ReelIn();

        }
        
    }
    public void Cast()
    {
        print("casting");
        StartCoroutine("CastDelay");

        playerController.enabled = false;
        //if bobber lands in water 
        if (Bobber.instance.inWater == true)
        {
            //canCast = false
            canCast = false;
            stillCast = true;
        }
        //if bobber lands on the ground
        else
        {
            ReelIn();
        }
        //reverse cast animation and remove bobber
        //cancast = true
    }

    public void ReelIn()
    {
        stillCast = false;
        playerController.enabled = true;
        anim.SetBool("IdleCast", false);
        anim.SetBool("Casting", false);
        StartCoroutine("ReelDelay");
    }

    private IEnumerator CastDelay()
    {
        anim.SetBool("Casting", true);
        print("casting anim played");
        yield return new WaitForSeconds(1f);
        //instantiate bobber at bobberstartpos
        bobber = Instantiate(bobberPrefab, bobberStartPosObj.transform);
        print("starting idle");
        anim.SetBool("IdleCast", true);
    }

    private IEnumerator ReelDelay()
    {
        anim.SetBool("Reel", true);
        Destroy(bobber);
        yield return new WaitForSeconds(1.2f);
        anim.SetBool("Reel", false);
        canCast = true;
    }
}
