using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBounceFX : MonoBehaviour
{
    public Laser associatedLaser;
    bool isValid;

    public void FixedUpdate()
    {
        isValid = false;
        foreach(Vector3 v in associatedLaser.laserPoint)
        {
            if((Vector2)v == (Vector2)transform.position)
            {
                isValid = true;
            }
        }

        if (!isValid)
        {
            associatedLaser.bounceParticles.Remove(this.gameObject);
            Destroy(gameObject);
        }
    }
}
