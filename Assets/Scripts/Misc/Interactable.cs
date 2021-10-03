using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public PlayerController playerController;
    public bool isPushable;
    public bool isInteractable;
    public bool isInRange;
    public float pushDistance;
    public bool isMoving;
    public float checkDistance;
    public bool[] validPushDirections;
    public LayerMask detectionLayer;
    public float moveTime;
    public float interactCooldown;
    public int numCollidersToIgnore;

    // Start is called before the first frame update
    public virtual void Start()
    {
        isInteractable = true;
        validPushDirections = new bool[4];
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        playerController.allInteractables.Add(GetComponent<Interactable>());
    }

    public virtual IEnumerator InteractRoutine(int orientation)
    {
        yield return new WaitForSeconds(0.5f);
    }

    public IEnumerator PushRoutine(int orientation)
    {
        if (!isMoving)
        {
            //yield return new WaitForSeconds(0.25f);

            RaycastHit2D[] lineCheck = Physics2D.RaycastAll(transform.position - new Vector3(0, 0.5f, 0), transform.up, checkDistance, detectionLayer);
            if ((lineCheck.Length >= numCollidersToIgnore))
                validPushDirections[0] = false;
            else
                validPushDirections[0] = true;
            lineCheck = Physics2D.RaycastAll(transform.position - new Vector3(0, 0.5f, 0), transform.right, checkDistance, detectionLayer);
            if ((lineCheck.Length >= numCollidersToIgnore))
                validPushDirections[1] = false;
            else
                validPushDirections[1] = true;
            lineCheck = Physics2D.RaycastAll(transform.position - new Vector3(0, 0.5f, 0), -transform.up, checkDistance, detectionLayer);
            if ((lineCheck.Length >= numCollidersToIgnore))
                validPushDirections[2] = false;
            else
                validPushDirections[2] = true;
            lineCheck = Physics2D.RaycastAll(transform.position - new Vector3(0, 0.5f, 0), -transform.right, checkDistance, detectionLayer);
            if ((lineCheck.Length >= numCollidersToIgnore))
                validPushDirections[3] = false;
            else
                validPushDirections[3] = true;

            if (validPushDirections[orientation])
            {
                Vector2 ogPosition = transform.position;
                Vector2 ogPlayerPosition = playerController.transform.position;
                isMoving = true;
                switch (orientation)
                {
                    case 0:
                        LeanTween.value(gameObject, ogPosition.y, ogPosition.y + pushDistance, moveTime).setOnUpdate((float val) => {
                            transform.position = new Vector2(transform.position.x, val);
                        });

                        playerController.mainAnim.Play("playerSmallHop");
                        LeanTween.value(gameObject, ogPlayerPosition.y, ogPlayerPosition.y + pushDistance, moveTime).setOnUpdate((float val) => {
                            playerController.transform.position = new Vector2(playerController.transform.position.x, val);
                        });

                        break;
                    case 1:
                        LeanTween.value(gameObject, ogPosition.x, ogPosition.x + pushDistance, moveTime).setOnUpdate((float val) => {
                            transform.position = new Vector2(val, transform.position.y);
                        });
                        playerController.mainAnim.Play("playerSmallHop");
                        LeanTween.value(gameObject, ogPlayerPosition.x, ogPlayerPosition.x + pushDistance, moveTime).setOnUpdate((float val) => {
                            playerController.transform.position = new Vector2(val, playerController.transform.position.y);
                        });
                        break;
                    case 2:
                        LeanTween.value(gameObject, ogPosition.y, ogPosition.y - pushDistance, moveTime).setOnUpdate((float val) => {
                            transform.position = new Vector2(transform.position.x, val);
                        });
                        playerController.mainAnim.Play("playerSmallHop");
                        LeanTween.value(gameObject, ogPlayerPosition.y, ogPlayerPosition.y - pushDistance, moveTime).setOnUpdate((float val) => {
                            playerController.transform.position = new Vector2(playerController.transform.position.x, val);
                        });
                        break;
                    case 3:
                        LeanTween.value(gameObject, ogPosition.x, ogPosition.x - pushDistance, moveTime).setOnUpdate((float val) => {
                            transform.position = new Vector2(val, transform.position.y);
                        });
                        playerController.mainAnim.Play("playerSmallHop");
                        LeanTween.value(gameObject, ogPlayerPosition.x, ogPlayerPosition.x - pushDistance, moveTime).setOnUpdate((float val) => {
                            playerController.transform.position = new Vector2(val, playerController.transform.position.y);
                        });
                        break;
                }

                var soundOneShot = Instantiate(GameManager.Instance.audioOneshotPrefab, transform.position, Quaternion.identity);
                soundOneShot.transform.parent = GameManager.Instance.gameObject.transform;
                soundOneShot.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Audio/Action_Push_0" + Random.Range(1, 4));

                yield return new WaitForSeconds(moveTime + 0.05f);
                isMoving = false;
            }
            else
            {
                print("Invalid Push");
            }
        }
        

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isPushable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isPushable = false;
        }
    }
}
