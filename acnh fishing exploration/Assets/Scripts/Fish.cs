using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public GameObject bobber;
    public bool inBobberRange = false;
    public bool isMoving;
    public float speed = 1f;
    public int bites;
    public AudioSource audioSource;
    public GameObject[] waterBlocks;

    public bool isBiting = false;
    public bool canCatch = false;
    public bool canMove = true;

    public float reelWindow;

    public Vector3 bobberPos;

    public int destBlock;
    public Vector3 dest;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponentInChildren<AudioSource>();
        waterBlocks = GameObject.FindGameObjectsWithTag("Water");
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
                print("fish on!");
                StartCoroutine(BiteDelay());
                
            }
            
        }
    }

    public void Move()
    {
        if (isMoving == false)
        {
            
            StartCoroutine(MoveDelay());
            
        }
    }

    public IEnumerator BiteDelay()
    {
        inBobberRange = true;
        isMoving = true;
        canMove = false;
        //print("in range abt to bite");
        isBiting = true;
        while (Vector3.Distance(new Vector3(bobberPos.x, transform.position.y, bobberPos.z), transform.position) > .1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, bobberPos, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        

        bites = Random.Range(0, 4); // the number of bites a fish will fake before actually biting

        for (int i = 0; i < bites; i++)
        {
            if (Player.instance.reeled == true)
            {
                i = bites;
                print("fish lost, reeled too early");
                FishSpawner.instance.fish--;
                Destroy(gameObject);
            }
            if (Bobber.instance == null)
            {
                i = bites;
                print("fish lost, reeled too early");
                FishSpawner.instance.fish--;
                Destroy(gameObject);
            }
            print("bites:" + (bites - i));
            audioSource.Play(0);
            //print("played audio");
            yield return new WaitForSeconds(2f);

        }
        StartCoroutine(ReelWindow());
        
        
    }

    public IEnumerator ReelWindow()
    {
        canCatch = true;
        bobberPos.y -= .5f;
        Bobber.instance.gameObject.transform.position = bobberPos;
        //print("play splash audio");
        Bobber.instance.audioSource.Play(0);
        
        print("click now");
        yield return new WaitForSeconds(reelWindow);
        if (Player.instance.reeled == true && Bobber.instance.fishOnHook == null)
        {
            Bobber.instance.fishOnHook = gameObject;
            print("fish catched");
            FishSpawner.instance.fish--;
            Player.instance.Reset();
            Destroy(gameObject);
        }
        else if (Player.instance.reeled == true && Bobber.instance.fishOnHook != null)
        {
            if (Bobber.instance.fishOnHook != gameObject)
            {
                print("another fish on hook");
                FishSpawner.instance.fish--;
                Destroy(gameObject);
            }
            if (Bobber.instance.fishOnHook == gameObject)
            {
                print("fish caught");
                FishSpawner.instance.fish--;
                Player.instance.Reset();
                Destroy(gameObject);
            }
        }
        else
        {
            print("fish got away");
            FishSpawner.instance.fish--;
            canCatch = false;
            //Player.instance.ReelIn();
            Bobber.instance.gameObject.transform.position = new Vector3(bobberPos.x, bobberPos.y + .5f, bobberPos.z);
            Destroy(gameObject);
            
        }
        canCatch = false;


    }

    public IEnumerator MoveDelay()
    {
        //print("move");
        isMoving = true;
        destBlock = Random.Range(1, waterBlocks.Length - 1);

        dest = waterBlocks[destBlock].transform.position;
        dest.y += 1.1f;
        //print(Vector3.Distance(transform.position, dest));
        while (Vector3.Distance(dest, transform.position) > .1f && canMove == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, dest, speed * Time.deltaTime);
            //print("moving");
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(2f);
        //yield return new WaitForSeconds(2f);
        isMoving = false;
    }
}
