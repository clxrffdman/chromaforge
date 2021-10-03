using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreTower : PrismTower
{
    public float necessaryChargeTime;
    public float currentTime;
    public SpriteRenderer sr;
    public SpriteRenderer whiteGlowSprite;
    public bool completed;



    public override void Start()
    {
        base.Start();
        
    }

    public override IEnumerator InteractRoutine(int orientation)
    {
        yield return new WaitForSeconds(interactCooldown);
    }

    public void FixedUpdate()
    {
        if(laserProfiles.Count == 0)
        {
            personalLaser.GetComponent<LaserProfile>().color = -1;
        }

        if (personalLaser.GetComponent<LaserProfile>().color % 2 == 0)
        {
            sr.material.SetInt("_isRed", 1);
        }
        else
        {
            sr.material.SetInt("_isRed", 0);
        }

        if (personalLaser.GetComponent<LaserProfile>().color % 3 == 0)
        {
            sr.material.SetInt("_isGreen", 1);
        }
        else
        {
            sr.material.SetInt("_isGreen", 0);
        }

        if (personalLaser.GetComponent<LaserProfile>().color % 5 == 0)
        {
            sr.material.SetInt("_isBlue", 1);
        }
        else
        {
            sr.material.SetInt("_isBlue", 0);
        }

        if (personalLaser.GetComponent<LaserProfile>().color == 30)
        {
            currentTime += Time.deltaTime;



            if(currentTime >= necessaryChargeTime)
            {
                GameManager.Instance.Victory();
                completed = true;
            }
        }
        else
        {
            if(!completed == true && currentTime > 0)
            {
                currentTime -= Time.deltaTime;
            }
            
        }

        if(currentTime > 0 && !completed)
        {
            whiteGlowSprite.color = new Color(1,1,1,currentTime/necessaryChargeTime);
        }
    }
}
