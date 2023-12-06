using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumFish : Fish
{
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponentInChildren<AudioSource>();
        waterBlocks = GameObject.FindGameObjectsWithTag("Water");
        reelWindow = 1f;
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
