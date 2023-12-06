using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallFish : Fish
{
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponentInChildren<AudioSource>();
        waterBlocks = GameObject.FindGameObjectsWithTag("Water");
        reelWindow = 1.5f;
    }

    // Update is called once per frame
    /*
    void Update()
    {
        
        if (Bobber.instance != null)
        {
            bobberPos = Bobber.instance.gameObject.transform.position;
            if (Vector3.Distance(bobberPos, transform.position) < 3f && isBiting == false)
            {
                inBobberRange = true;
                isMoving = true;
                print("in range abt to bite");
                isBiting = true;
                StopCoroutine(MoveDelay());
                dest = transform.position;
                transform.position = Vector3.MoveTowards(transform.position, bobberPos, speed * Time.deltaTime);

                bites = Random.Range(0, 4); // the number of bites a fish will fake before actually biting

                for (int i = 0; i < bites; i++)
                {
                    print("bites:" + bites);
                    StartCoroutine(BiteDelay());
                }
                StartCoroutine(ReelWindow());
            }
            Move();
        }
    }
    */
    void Update()
    {
        Move();
        if (Bobber.instance != null)
        {
            bobberPos = Bobber.instance.gameObject.transform.position;
            if (Vector3.Distance(bobberPos, transform.position) < 3f && isBiting == false)
            {
                StartCoroutine(BiteDelay());

            }

        }
        else
        {
            return;
        }

    }
}
