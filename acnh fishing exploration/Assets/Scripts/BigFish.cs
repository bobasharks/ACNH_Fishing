using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigFish : Fish
{
    // Start is called before the first frame update
    void Start()
    {
        isMoving = false;
        audioSource = GetComponentInChildren<AudioSource>();
        waterBlocks = GameObject.FindGameObjectsWithTag("Water");
        reelWindow = .75f;
    }

    // Update is called once per frame
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
