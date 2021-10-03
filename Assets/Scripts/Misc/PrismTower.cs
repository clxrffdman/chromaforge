using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrismTower : Interactable
{
    public int mirrorOrientation;
    public GameObject reflectMirror;
    public float rotateTime;
    public List<LaserProfile> laserProfiles;
    public GameObject personalLaser;
    public Animator anim;

    public override void Start()
    {
        base.Start();
        PrismManager.Instance.allPrismTowers.Add(this);
        if(GetComponent<Animator>() != null)
        {
            anim = GetComponent<Animator>();
        }
        
        if(anim != null)
        {
            switch (mirrorOrientation)
            {
                case 0:
                    reflectMirror.transform.localEulerAngles = new Vector3(0, 0, 0);
                    anim.Play("prism3-4");
                    break;
                case 1:
                    reflectMirror.transform.localEulerAngles = new Vector3(0, 0, 90);
                    anim.Play("prism0-1");
                    break;
                case 2:
                    reflectMirror.transform.localEulerAngles = new Vector3(0, 0, 180);
                    anim.Play("prism1-2");
                    break;
                case 3:
                    anim.Play("prism2-3");
                    reflectMirror.transform.localEulerAngles = new Vector3(0, 0, -90);
                    break;



            }
        }
        


    }
    public override IEnumerator InteractRoutine(int orientation)
    {
        isInteractable = false;
        mirrorOrientation += 1;
        if (mirrorOrientation > 3)
        {
            mirrorOrientation = 0;
        }

        switch (mirrorOrientation)
        {
            case 0:
                LeanTween.value(gameObject, -90, 0, rotateTime).setOnUpdate((float val) => {
                    reflectMirror.transform.localEulerAngles = new Vector3(0, 0, val);
                });
                anim.Play("prism3-4");
                break;
            case 1:
                LeanTween.value(gameObject, 0, 90, rotateTime).setOnUpdate((float val) => {
                    reflectMirror.transform.localEulerAngles = new Vector3(0, 0, val);
                });
                anim.Play("prism0-1");
                break;
            case 2:
                LeanTween.value(gameObject, 90, 180, rotateTime).setOnUpdate((float val) => {
                    reflectMirror.transform.localEulerAngles = new Vector3(0, 0, val);
                });
                anim.Play("prism1-2");
                break;
            case 3:
                LeanTween.value(gameObject, 180, 270, rotateTime).setOnUpdate((float val) => {
                    reflectMirror.transform.localEulerAngles = new Vector3(0, 0, val);
                });
                anim.Play("prism2-3");
                yield return new WaitForSeconds(rotateTime);
                reflectMirror.transform.localEulerAngles = new Vector3(0, 0, -90);
                break;



        }
        yield return new WaitForSeconds(interactCooldown);
        isInteractable = true;
    }


}
