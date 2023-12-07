using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public static Player instance;
    private bool canCast = true;
    private bool stillCast = false;
    public bool reeled = false;
    public GameObject bobberPrefab;
    public GameObject bobber;
    public GameObject cameraPivot;

    public GameObject speechBubble;
    public TextMeshProUGUI fishCountText;
    public int fishCount = 0;

    [Header("bobber interpolation")]
    public Transform bobberStartPosObj;
    public Transform bobberEndPosObj;
    public Transform bobberMidPos;

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
    public void Reset()
    {
        canCast = false;
        stillCast = false;
        StartCoroutine(ShowOff());
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
        else if (Input.GetMouseButtonDown(1) && speechBubble.activeSelf)
        {
            StopCoroutine(ShowOff());
            speechBubble.SetActive(false);
            cameraPivot.transform.rotation = Quaternion.Euler(36.17f,0, 0);
            //speechBubble.SetActive(false);
            //cameraPivot.transform.rotation = Quaternion.Euler(36.17f, 0, 0);
            canCast = true;
            stillCast = false;
            playerController.enabled = true;
        }

        fishCountText.text = "fish caught: " + fishCount;
        
    }
    public void Cast()
    {
        //print("casting");
        StartCoroutine("CastDelay");

        playerController.enabled = false;
    }

    public void ReelIn()
    {
        reeled = true;
        stillCast = false;
        playerController.enabled = true;
        anim.SetBool("IdleCast", false);
        anim.SetBool("Casting", false);
        StartCoroutine("ReelDelay");
    }

    public IEnumerator ShowOff()
    {
        playerController.enabled = false;
        speechBubble.SetActive(true);
        Quaternion endRot = Quaternion.Euler(0f,0f,0f);
        fishCount++;
        while (cameraPivot.transform.rotation != endRot && playerController.playerModel.transform.rotation != Quaternion.Euler(0,180,0))
        {
            cameraPivot.transform.rotation = Quaternion.Slerp(cameraPivot.transform.rotation, endRot, 2f * Time.deltaTime);
            playerController.playerModel.transform.rotation = Quaternion.Slerp(playerController.playerModel.transform.rotation, Quaternion.Euler(0, 180, 0), 4f * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        //playerController.enabled = true;
        //playerController.playerModel.transform.rotation = Quaternion.Euler(0, 180, 0);
        //cameraPivot.transform.rotation = Quaternion.Euler(0, 0, 0);
        print("speech bubble activate");
        
        
        //yield return new WaitForSeconds(1.2f);
        canCast = false;
        stillCast = false;

    }

    private IEnumerator CastDelay()
    {
        anim.SetBool("Casting", true);
        //print("casting anim played");
        yield return new WaitForSeconds(1.25f);
        //instantiate bobber at bobberstartpos
        bobber = Instantiate(bobberPrefab, bobberStartPosObj.transform);
        yield return new WaitForSeconds(2f);
        if (Bobber.instance.inWater == true)
        {
            //canCast = false
            canCast = false;
            stillCast = true;
            //print("starting idle");
            anim.SetBool("IdleCast", true);
            //print("cast successful");
        }
        else
        {
            print("cast failed");
            ReelIn();
        }

    }

    private IEnumerator ReelDelay()
    {
        print("Reeling");
        anim.SetBool("Reel", true);
        Destroy(bobber);
        yield return new WaitForSeconds(1.2f);
        reeled = false;
        anim.SetBool("Reel", false);
        canCast = true;
        if (GameObject.FindWithTag("bobber"))
        {
            Destroy(GameObject.FindWithTag("bobber"));
            print("destroyed extra bobber");
        }
    }
}
