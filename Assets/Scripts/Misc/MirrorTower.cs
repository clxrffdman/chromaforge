using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MirrorTower : Interactable
{
    public int mirrorOrientation;
    public GameObject reflectMirror;
    public float rotateTime;
    public Animator anim;


    public override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();


        switch (mirrorOrientation)
        {
            case 0:
                reflectMirror.transform.localEulerAngles = new Vector3(0, 0, -135);
                anim.Play("mirror2-3");

                break;
            case 1:
                reflectMirror.transform.localEulerAngles = new Vector3(0, 0, -225);

                anim.Play("mirror3-0");

                break;
            case 2:
                reflectMirror.transform.localEulerAngles = new Vector3(0, 0, -315);

                anim.Play("mirror0-1");

                break;
            case 3:


                anim.Play("mirror1-2");
                reflectMirror.transform.localEulerAngles = new Vector3(0, 0, -45);
                break;



        }




    }

    public override IEnumerator InteractRoutine(int orientation)
    {
        isInteractable = false;

        mirrorOrientation += 1;
        if(mirrorOrientation > 3)
        {
            mirrorOrientation = 0;
        }

        switch (mirrorOrientation) {
            case 0:
                LeanTween.value(gameObject, -45, -135, rotateTime).setOnUpdate((float val) => {
                    reflectMirror.transform.localEulerAngles = new Vector3(0, 0, val);
                });
                anim.Play("mirror2-3");
                
                break;
            case 1:
                LeanTween.value(gameObject, -135, -225, rotateTime).setOnUpdate((float val) => {
                    reflectMirror.transform.localEulerAngles = new Vector3(0, 0, val);
                });
               
                anim.Play("mirror3-0");

                break;
            case 2:
                LeanTween.value(gameObject,  -225, -315, rotateTime).setOnUpdate((float val) => {
                    reflectMirror.transform.localEulerAngles = new Vector3(0, 0, val);
                });
                
                anim.Play("mirror0-1");

                break;
            case 3:
                LeanTween.value(gameObject,  -315, -405, rotateTime).setOnUpdate((float val) => {
                    reflectMirror.transform.localEulerAngles = new Vector3(0, 0, val);
                });
                
                yield return new WaitForSeconds(rotateTime);

                anim.Play("mirror1-2");
                reflectMirror.transform.localEulerAngles = new Vector3(0, 0, -45);
                break;



        }
        yield return new WaitForSeconds(interactCooldown);
        isInteractable = true;
            
    }
}
