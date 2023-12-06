using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobber : MonoBehaviour
{
    public static Bobber instance;
    public bool inWater = false;
    private float easing = .5f;
    Rigidbody rb;
    public bool inFishRange;
    public GameObject fishOnHook;

    public Vector3 p01, p12, p012;
    public AudioSource audioSource;

    float timeStart;
    float timeDuration = .75f;
    float u;
    bool moving;

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

        rb = GetComponent<Rigidbody>();
        audioSource = GetComponentInChildren<AudioSource>();
        moving = true;
        timeStart = Time.time;

    }
    private void Update()
    {
        if (moving)
        {
            u = (Time.time - timeStart) / timeDuration;
            if (u >= 1)
            {
                u = 1;
                moving = false;
            }

            p01 = (1 - u) * Player.instance.bobberStartPosObj.transform.position + u * Player.instance.bobberMidPos.transform.position;
            p12 = (1 - u) * Player.instance.bobberMidPos.transform.position + u * Player.instance.bobberEndPosObj.transform.position;
            p012 = (1 - u) * p01 + u * p12;
            /*
            p01 = Vector3.Lerp(Player.instance.bobberStartPosObj.transform.position, Player.instance.bobberMidPos.transform.position, easing);
            p12 = Vector3.Lerp(Player.instance.bobberMidPos.transform.position, Player.instance.bobberEndPosObj.transform.position, easing);
            p012 = Vector3.Lerp(p01, p12, easing);
            */

            transform.position = p012;
        }

        

        //transform.position = p12;
        
        //transform.position = Vector3.Lerp(transform.position, Player.instance.bobberEndPos, u * Time.deltaTime);


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "WaterSolid")
        {
            inWater = true;
            print("hit water");
        }
        if (other.gameObject.tag == "Fish")
        {
            inFishRange = true;
            
            print("in fish range");
        }
    }
}
