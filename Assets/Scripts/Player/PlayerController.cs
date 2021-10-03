using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public bool canMove;
    public bool isMove;
    public bool canInteract;
    public Rigidbody2D rb;
    public Vector2 playerVelocity;
    public float playerSpeed;
    public int pushOrientation;
    public int interactOrientation;

    public bool isLeft;

    public GameObject playerSpriteObject;
    public Animator anim;
    public Animator mainAnim;
    public float footstepRate;
    public float footstepTimer;


    public List<Interactable> allInteractables;

    public Interactable highlightedTower;
    public Material highlightedOgmat;
    public bool isHighlightingTower;

    // Start is called before the first frame update
    void Start()
    {
        //canMove = true;
        mainAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        canInteract = true;
        playerSpriteObject = transform.GetChild(0).gameObject;
        anim = transform.GetChild(0).GetComponent<Animator>();
        footstepTimer = footstepRate;

    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            MovementCheck();
            MoveCheck();
            //InteractCheck();


        }

        if (isHighlightingTower)
        {
            HighlightState();
        }

    }

    private void FixedUpdate()
    {
        if (isMove)
        {
            footstepTimer -= Time.deltaTime;
            if(footstepTimer <= 0)
            {
                footstepTimer = footstepRate;

                var soundOneShot = Instantiate(GameManager.Instance.audioOneshotPrefab, transform.position, Quaternion.identity);
                soundOneShot.transform.parent = GameManager.Instance.gameObject.transform;
                soundOneShot.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Audio/Footstep_0" + Random.Range(1, 5));
                soundOneShot.GetComponent<AudioSource>().volume = 0.1f;
            }
        }
    }

    void InteractCheck()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.R))
        {
            if (Input.GetKey(KeyCode.W))
            {
                interactOrientation = 0;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                interactOrientation = 2;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                interactOrientation = 3;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                interactOrientation = 1;
            }
            else
            {
                interactOrientation = -1;
            }

            if (interactOrientation != -1)
            {
                foreach (Interactable i in allInteractables)
                {
                    if (i.isInteractable && i.isPushable)
                    {
                        var soundOneShot = Instantiate(GameManager.Instance.audioOneshotPrefab, transform.position, Quaternion.identity);
                        soundOneShot.transform.parent = GameManager.Instance.gameObject.transform;
                        soundOneShot.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Audio/Action_Tower_Rotate_0" + Random.Range(1, 4));

                        i.StartCoroutine(i.InteractRoutine(interactOrientation));
                    }
                }
            }
        }
    }

    public void HighlightState()
    {

        pushOrientation = -2;

        if (Input.GetKey(KeyCode.W))
        {
            pushOrientation = 0;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            pushOrientation = 2;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            pushOrientation = 3;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            pushOrientation = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            pushOrientation = -1;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            pushOrientation = -2;
            var soundOneShot = Instantiate(GameManager.Instance.audioOneshotPrefab, transform.position, Quaternion.identity);
            soundOneShot.transform.parent = GameManager.Instance.gameObject.transform;
            soundOneShot.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Audio/Action_Tower_Rotate_0" + Random.Range(1, 4));

            highlightedTower.GetComponent<Interactable>().StartCoroutine(highlightedTower.GetComponent<Interactable>().InteractRoutine(interactOrientation));
        }

        if (pushOrientation == 0 || pushOrientation == 1 || pushOrientation == 2 || pushOrientation == 3)
        {
            if (highlightedTower.isPushable && !highlightedTower.isMoving)
            {

                highlightedTower.StartCoroutine(highlightedTower.PushRoutine(pushOrientation));

                /*
                canMove = true;
                highlightedTower.transform.GetComponent<SpriteRenderer>().material = highlightedOgmat;
                highlightedTower = null;
                isHighlightingTower = false;
                */
                
            }

        }
        else if (pushOrientation == -1 && !highlightedTower.isMoving)
        {
            canMove = true;
            highlightedTower.transform.GetComponent<SpriteRenderer>().material = highlightedOgmat;
            highlightedTower = null;
            isHighlightingTower = false;
            
        }
        else
        {
            isHighlightingTower = true;
        }

    }

    void MoveCheck()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.Space))
        {

            foreach (Interactable i in allInteractables)
            {
                if (!isHighlightingTower && i.isPushable && !i.isMoving)
                {

                    print("HIGHLIGHTING TOWER");
                    highlightedTower = i;

                    var soundOneShot = Instantiate(GameManager.Instance.audioOneshotPrefab, transform.position, Quaternion.identity);
                    soundOneShot.transform.parent = GameManager.Instance.gameObject.transform;
                    soundOneShot.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Audio/Action_Tower_Select_0" + Random.Range(1, 4));

                    canMove = false;
                    rb.velocity = new Vector2(0, 0);
                    anim.SetBool("isWalking", false);
                    highlightedOgmat = i.GetComponent<SpriteRenderer>().material;
                    i.GetComponent<SpriteRenderer>().material = Resources.Load("SpriteGlowOutline", typeof(Material)) as Material;
                    Invoke("SetIsHighlightingTower", 0.15f);


                }
            }





        }
    }

    public void SetIsHighlightingTower()
    {
        isHighlightingTower = true;
    }

    void MovementCheck()
    {
        if (Input.GetKey(KeyCode.A))
        {
            playerVelocity.x = -playerSpeed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            playerVelocity.x = playerSpeed;
        }

        if ((Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) || (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)))
        {
            playerVelocity.x = 0;
        }

        if (Input.GetKey(KeyCode.W))
        {
            playerVelocity.y = playerSpeed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            playerVelocity.y = -playerSpeed;
        }

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S) || (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S)))
        {
            playerVelocity.y = 0;
        }

        if (playerVelocity == Vector2.zero)
        {
            isMove = false;
        }
        else
        {
            isMove = true;
        }

        if (isMove)
        {
            anim.SetBool("isWalking", true);
            if (playerVelocity.x > 0)
            {
                isLeft = false;
            }
            else
            {
                isLeft = true;
            }

            if (isLeft)
            {
                playerSpriteObject.transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                playerSpriteObject.transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }
        else
        {
            anim.SetBool("isWalking", false);
        }


        rb.velocity = playerVelocity;
    }


}
