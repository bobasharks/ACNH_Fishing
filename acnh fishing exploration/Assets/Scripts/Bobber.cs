using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobber : MonoBehaviour
{
    public static Bobber instance;
    public bool inWater = false;
    private float easing = .1f;
    Rigidbody rb;

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

    }
    // Start is called before the first frame update
    private void Update()
    {
        Vector3 bobberEndPos = Player.instance.bobberEndPosObj.transform.position;
        bobberEndPos = Vector3.Lerp(transform.position, bobberEndPos, easing);

        transform.position = bobberEndPos;
        
        //transform.position = Vector3.Lerp(transform.position, Player.instance.bobberEndPos, u * Time.deltaTime);


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            inWater = true;
        }
    }
}
