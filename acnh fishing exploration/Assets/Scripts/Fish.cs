using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public GameObject bobber;
    public float bobberDistX;
    public float bobberDistZ;
    public float speed = 1f;

    public Transform bobberPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        bobber = GameObject.FindWithTag("Bobber");
        bobberPos = bobber.transform;
        bobberDistX = gameObject.transform.position.x - bobber.transform.position.x;
        Mathf.Abs(bobberDistX);
        bobberDistZ = gameObject.transform.position.z - bobber.transform.position.z;
        Mathf.Abs(bobberDistZ);
        */

        Move();
    }

    public void Move()
    {
        /*
        if (bobberDistX < 4 || bobberDistZ < 4)
        {
            Vector3.MoveTowards(gameObject.transform.position, bobberPos.position, speed * Time.deltaTime);
            print("moving");
        }
        else if (bobberDistZ >=4 && bobberDistZ >= 4)
        {
            print("random swimming)");
        }
        else
        {
            print("something went wrong");
        }
        */
    }

}
