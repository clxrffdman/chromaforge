using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransTower : PrismTower
{
    public List<GameObject> personalLasers;
    public int throughput;
    public int[] tp;
    public List<Throughput> throughputList;

    public void Update()
    {
        for (int l = 0; l < 4; l++)
        {
            if (tp[l] == 0)
            {
                personalLasers[l].GetComponent<Laser>().laserEnabled = false;

            }
        }
    }
    public override void Start()
    {
        base.Start();
        tp = new int[4];
        
        PrismManager.Instance.allTransTowers.Add(this);
        if (GetComponent<Animator>() != null)
        {
            anim = GetComponent<Animator>();
        }

        if (anim != null)
        {
            switch (mirrorOrientation)
            {

                case 0:

                    reflectMirror.transform.localEulerAngles = new Vector3(0, 0, -45);
                    anim.Play("trans0-1");

                    break;
                case 1:
                    reflectMirror.transform.localEulerAngles = new Vector3(0, 0, -135);

                    anim.Play("trans1-0");

                    break;


            }
        }



    }



    public override IEnumerator InteractRoutine(int orientation)
    {
        isInteractable = false;
        mirrorOrientation += 1;
        if (mirrorOrientation > 1)
        {
            mirrorOrientation = 0;
        }

        switch (mirrorOrientation)
        {
            case 0:
                LeanTween.value(gameObject, -135, -45, rotateTime).setOnUpdate((float val) => {
                    reflectMirror.transform.localEulerAngles = new Vector3(0, 0, val);
                });
                reflectMirror.transform.localEulerAngles = new Vector3(0, 0, -45);
                anim.Play("trans0-1");

                break;
            case 1:

                LeanTween.value(gameObject, -45, -135, rotateTime).setOnUpdate((float val) => {
                    reflectMirror.transform.localEulerAngles = new Vector3(0, 0, val);
                });


                

                anim.Play("trans1-0");

                break;
           



        }
        yield return new WaitForSeconds(interactCooldown);
        isInteractable = true;
    }

}
