using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class Laser : MonoBehaviour
{
    //Laser body
    public LineRenderer laser;
    public LaserProfile laserProfile;
    public bool hitwall;
    public bool hitPrism;
    //Store the list of paths that the Laser passes
    public List<Vector3> laserPoint = new List<Vector3>();
    public int layerMask;
    public bool laserEnabled;
    public float maxBounces;
    public List<GameObject> bounceParticles;
 
    

    public int laserOrientation;

    private void Start()
    {
        laser = transform.Find("Line").GetComponent<LineRenderer>();
        layerMask = (LayerMask.GetMask("Mirror", "Terrain", "LaserBlocker"));
        laserProfile = GetComponent<LaserProfile>();
    }
    private void Update()
    {
        if (laserEnabled)
        {
            laser.gameObject.SetActive(true);



            CasetLaser();
            laser.positionCount = laserPoint.Count;
            laser.SetPositions(laserPoint.ToArray());
        }
        else
        {
            laserPoint.Clear();
            laser.gameObject.SetActive(false);
        }
        
    }

    public void ClearProfiles()
    {

    }
    void CasetLaser()
    {
        //Empty the old LaserPoint
        laserPoint.Clear();
        //Start from the position of Laser Gun
        Vector3 startPoint = transform.position;
        // launch direction

        Vector3 direction = transform.right;

        if (laserOrientation == 0)
        {
            direction = transform.right;
        }

        if (laserOrientation == 1)
        {
            direction = -transform.up;
        }

        if (laserOrientation == 2)
        {
            direction = -transform.right;
        }

        if (laserOrientation == 3)
        {
            direction = transform.up;
        }


        //With the first starting point
        laserPoint.Add(startPoint);
        hitwall = false;
        hitPrism = false;
        int i = 0;
        do
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(startPoint, direction, 100, layerMask);

            //Add the ray hit point to the path
            if(hitInfo.collider.gameObject.tag == "Mirror")
            {
                laserPoint.Add(hitInfo.point);
                
            }
            else if (hitInfo.collider.gameObject.tag == "Prism")
            {
                
                hitPrism = true;
                hitwall = true;

                foreach (PrismTower p in PrismManager.Instance.allPrismTowers)
                {
                    if (p.laserProfiles.Contains(GetComponent<LaserProfile>()))
                    {
                        

                        p.laserProfiles.Remove(GetComponent<LaserProfile>());
                        if (!p.gameObject.transform.GetComponent<TransTower>())
                        {
                            p.personalLaser.GetComponent<Laser>().laserEnabled = false;
                        }


                        if (p.gameObject.transform.GetComponent<TransTower>())
                        {

                            for (int l = 0; l < 4; l++)
                            {
                                if (p.gameObject.transform.GetComponent<TransTower>().tp[l] == 0)
                                {
                                    p.gameObject.transform.GetComponent<TransTower>().personalLasers[l].GetComponent<Laser>().laserEnabled = false;

                                }
                            }







                            for (int k = 0; i < p.gameObject.transform.GetComponent<TransTower>().throughputList.Count; k++)
                            {
                                if (p.gameObject.transform.GetComponent<TransTower>().throughputList[k].host == GetComponent<LaserProfile>())
                                {
                                    p.gameObject.transform.GetComponent<TransTower>().personalLasers[p.gameObject.transform.GetComponent<TransTower>().throughputList[k].throughput].GetComponent<Laser>().laserEnabled = false;
                                }
                            }

                            
                        }

                        int n = 1;

                        

                        foreach (LaserProfile l in hitInfo.collider.gameObject.transform.parent.GetComponent<PrismTower>().laserProfiles)
                        {
                            n *= l.color;
                        }

                        p.personalLaser.GetComponent<LaserProfile>().GetComponent<LaserProfile>().color = n;
                        p.personalLaser.GetComponent<LaserProfile>().UpdateLaserColor();
                    }
                }

                if (!hitInfo.collider.gameObject.transform.parent.GetComponent<PrismTower>().laserProfiles.Contains(laserProfile))
                {
                    hitInfo.collider.gameObject.transform.parent.GetComponent<PrismTower>().laserProfiles.Add(laserProfile);
                    int n = 1;

                    


                    foreach (LaserProfile l in hitInfo.collider.gameObject.transform.parent.GetComponent<PrismTower>().laserProfiles)
                    {
                        n *= l.color;
                    }
                    hitInfo.collider.gameObject.transform.parent.GetComponent<PrismTower>().personalLaser.GetComponent<LaserProfile>().color = n;
                    hitInfo.collider.gameObject.transform.parent.GetComponent<PrismTower>().personalLaser.GetComponent<LaserProfile>().UpdateLaserColor();
                    hitInfo.collider.gameObject.transform.parent.GetComponent<PrismTower>().personalLaser.GetComponent<LaserProfile>().oldColor = hitInfo.collider.gameObject.transform.parent.GetComponent<PrismTower>().personalLaser.GetComponent<LaserProfile>().color;

                }
                hitInfo.collider.gameObject.transform.parent.GetComponent<PrismTower>().personalLaser.GetComponent<Laser>().laserEnabled = true;
                laserPoint.Add(hitInfo.point);

                

            }
            else if (hitInfo.collider.gameObject.tag == "Trans")
            {
                /*
                hitInfo.collider.gameObject.transform.parent.GetComponent<TransTower>().tp[0] = 0;
                hitInfo.collider.gameObject.transform.parent.GetComponent<TransTower>().tp[1] = 0;
                hitInfo.collider.gameObject.transform.parent.GetComponent<TransTower>().tp[2] = 0;
                hitInfo.collider.gameObject.transform.parent.GetComponent<TransTower>().tp[3] = 0;
                */

                hitPrism = true;
                //hitwall = true;

                int[] activeFromThis = { 0,0,0,0};
                bool containsThisLaser = false;

                foreach(Throughput t in hitInfo.collider.gameObject.transform.parent.GetComponent<TransTower>().throughputList)
                {
                    if(t.host == GetComponent<LaserProfile>())
                    {
                        containsThisLaser = true;
                    }
                }


                if((laserPoint[laserPoint.Count-1].x > hitInfo.point.x) && Mathf.Abs(laserPoint[laserPoint.Count - 1].x - hitInfo.point.x) > 0.2f)
                {
                    hitInfo.collider.gameObject.transform.parent.GetComponent<TransTower>().tp[3] = 1;
                    if (!containsThisLaser)
                    {
                        hitInfo.collider.gameObject.transform.parent.GetComponent<TransTower>().throughputList.Add(new Throughput(3, GetComponent<LaserProfile>()));
                    }
                    activeFromThis[3] = 1;
                    
                }

                if ((laserPoint[laserPoint.Count - 1].x < hitInfo.point.x) && Mathf.Abs(laserPoint[laserPoint.Count - 1].x - hitInfo.point.x) > 0.2f)
                {
                    hitInfo.collider.gameObject.transform.parent.GetComponent<TransTower>().tp[1] = 1;
                    if (!containsThisLaser)
                    {
                        hitInfo.collider.gameObject.transform.parent.GetComponent<TransTower>().throughputList.Add(new Throughput(1, GetComponent<LaserProfile>()));
                    }
                    activeFromThis[1] = 1;
                }

                if ((laserPoint[laserPoint.Count - 1].y < hitInfo.point.y) && Mathf.Abs(laserPoint[laserPoint.Count - 1].y - hitInfo.point.y) > 0.2f)
                {
                    hitInfo.collider.gameObject.transform.parent.GetComponent<TransTower>().tp[0] = 1;
                    if (!containsThisLaser)
                    {
                        hitInfo.collider.gameObject.transform.parent.GetComponent<TransTower>().throughputList.Add(new Throughput(0, GetComponent<LaserProfile>()));
                    }
                    activeFromThis[0] = 1;
                }

                if ((laserPoint[laserPoint.Count - 1].y > hitInfo.point.y) && Mathf.Abs(laserPoint[laserPoint.Count - 1].y - hitInfo.point.y) > 0.2f)
                {
                    hitInfo.collider.gameObject.transform.parent.GetComponent<TransTower>().tp[2] = 1;
                    if (!containsThisLaser)
                    {
                        hitInfo.collider.gameObject.transform.parent.GetComponent<TransTower>().throughputList.Add(new Throughput(2, GetComponent<LaserProfile>()));
                    }
                    activeFromThis[2] = 1;
                }

                //print(activeFromThis.ToString());

                laserPoint.Add(hitInfo.point);

                

                foreach (PrismTower p in PrismManager.Instance.allPrismTowers)
                {
                    if (p.laserProfiles.Contains(GetComponent<LaserProfile>()))
                    {
                        if (p.gameObject.transform.GetComponent<TransTower>())
                        {

                        }
                        else { 
                        
                        }

                        p.laserProfiles.Remove(GetComponent<LaserProfile>());
                        if (!p.gameObject.transform.GetComponent<TransTower>())
                        {
                            p.personalLaser.GetComponent<Laser>().laserEnabled = false;
                        }

                        if (p.gameObject.transform.GetComponent<TransTower>())
                        {

                            for(int l = 0; l < 4; l++)
                            {
                                if(p.gameObject.transform.GetComponent<TransTower>().tp[l] == 0)
                                {
                                    p.gameObject.transform.GetComponent<TransTower>().personalLasers[l].GetComponent<Laser>().laserEnabled = false;
            
                                }
                            }

                            
                        }
                        int n = 1;



                        foreach (LaserProfile l in hitInfo.collider.gameObject.transform.parent.GetComponent<PrismTower>().laserProfiles)
                        {
                            n *= l.color;
                        }

                        for (int l = 0; l < 4; l++)
                        {
                            if(activeFromThis[l] == 1)
                            {
                                if (p.gameObject.transform.GetComponent<TransTower>())
                                {
                                    p.gameObject.transform.GetComponent<TransTower>().personalLasers[l].GetComponent<LaserProfile>().color = n;
                                    p.gameObject.transform.GetComponent<TransTower>().personalLasers[l].GetComponent<LaserProfile>().UpdateLaserColor();
                                }
                               
                            }
                        }


                        
                        //p.personalLaser.GetComponent<LaserProfile>().GetComponent<LaserProfile>().color = n;
                        //p.personalLaser.GetComponent<LaserProfile>().UpdateLaserColor();
                    }
                }

                if (!hitInfo.collider.gameObject.transform.parent.GetComponent<TransTower>().laserProfiles.Contains(laserProfile))
                {
                    hitInfo.collider.gameObject.transform.parent.GetComponent<TransTower>().laserProfiles.Add(laserProfile);
                    

                    for(int k = 0; k < 4; k++)
                    {

                        int n = 1;
                        
                            foreach(Throughput t in hitInfo.collider.gameObject.transform.parent.GetComponent<TransTower>().throughputList)
                            {
                                if(t.throughput == k)
                                {
                                    n *= t.host.color;
                                }
                            }
                            
                        

                        if (activeFromThis[k] == 1)
                        {
                            hitInfo.collider.gameObject.transform.parent.GetComponent<TransTower>().personalLasers[k].GetComponent<LaserProfile>().color = n;
                            hitInfo.collider.gameObject.transform.parent.GetComponent<TransTower>().personalLasers[k].GetComponent<LaserProfile>().UpdateLaserColor();
                            hitInfo.collider.gameObject.transform.parent.GetComponent<TransTower>().personalLasers[k].GetComponent<LaserProfile>().oldColor = hitInfo.collider.gameObject.transform.parent.GetComponent<PrismTower>().personalLaser.GetComponent<LaserProfile>().color;
                        }
                    }

                    //hitInfo.collider.gameObject.transform.parent.GetComponent<TransTower>().personalLasers[hitInfo.collider.gameObject.transform.parent.GetComponent<TransTower>().throughput].GetComponent<LaserProfile>().color = n;
                    //hitInfo.collider.gameObject.transform.parent.GetComponent<TransTower>().personalLasers[hitInfo.collider.gameObject.transform.parent.GetComponent<TransTower>().throughput].GetComponent<LaserProfile>().UpdateLaserColor();
                    //hitInfo.collider.gameObject.transform.parent.GetComponent<TransTower>().personalLasers[hitInfo.collider.gameObject.transform.parent.GetComponent<TransTower>().throughput].GetComponent<LaserProfile>().oldColor = hitInfo.collider.gameObject.transform.parent.GetComponent<PrismTower>().personalLaser.GetComponent<LaserProfile>().color;

                }

                for(int j = 0; j < 4; j++ )
                {
                    if(hitInfo.collider.gameObject.transform.parent.GetComponent<TransTower>().tp[j] == 1)
                    {
                        hitInfo.collider.gameObject.transform.parent.GetComponent<TransTower>().personalLasers[j].GetComponent<Laser>().laserEnabled = true;
                    }
                }

                //hitInfo.collider.gameObject.transform.parent.GetComponent<TransTower>().personalLasers[hitInfo.collider.gameObject.transform.parent.GetComponent<TransTower>().throughput].GetComponent<Laser>().laserEnabled = true;
                
                
                

            }
            else if (hitInfo.collider.gameObject.tag == "Core")
            {

                hitPrism = true;
                hitwall = true;

                foreach (PrismTower p in PrismManager.Instance.allPrismTowers)
                {
                    if (p.laserProfiles.Contains(GetComponent<LaserProfile>()))
                    {
                        p.laserProfiles.Remove(GetComponent<LaserProfile>());
                        if (!p.gameObject.transform.GetComponent<TransTower>())
                        {
                            p.personalLaser.GetComponent<Laser>().laserEnabled = false;
                        }

                        int n = 1;
                        
                        foreach (LaserProfile l in hitInfo.collider.gameObject.transform.parent.GetComponent<PrismTower>().laserProfiles)
                        {
                            n *= l.color;
                        }
                        p.personalLaser.GetComponent<LaserProfile>().GetComponent<LaserProfile>().color = n;

                        p.personalLaser.GetComponent<LaserProfile>().UpdateLaserColor();



                    }
                }

                if (!hitInfo.collider.gameObject.transform.parent.GetComponent<PrismTower>().laserProfiles.Contains(laserProfile))
                {
                    hitInfo.collider.gameObject.transform.parent.GetComponent<PrismTower>().laserProfiles.Add(laserProfile);
                    int n = 1;
                    foreach (LaserProfile l in hitInfo.collider.gameObject.transform.parent.GetComponent<PrismTower>().laserProfiles)
                    {
                        n *= l.color;
                    }
                    hitInfo.collider.gameObject.transform.parent.GetComponent<PrismTower>().personalLaser.GetComponent<LaserProfile>().color = n;
                    hitInfo.collider.gameObject.transform.parent.GetComponent<PrismTower>().personalLaser.GetComponent<LaserProfile>().UpdateLaserColor();
                    

                }
                //hitInfo.collider.gameObject.transform.parent.GetComponent<PrismTower>().personalLaser.GetComponent<Laser>().laserEnabled = true;
                laserPoint.Add(hitInfo.point);

                
            }
            else
            {
                if (!hitPrism)
                {
                    foreach (PrismTower p in PrismManager.Instance.allPrismTowers)
                    {
                        if (p.laserProfiles.Contains(GetComponent<LaserProfile>()))
                        {
                            p.laserProfiles.Remove(GetComponent<LaserProfile>());
                            if (!p.gameObject.transform.GetComponent<TransTower>())
                            {
                                p.personalLaser.GetComponent<Laser>().laserEnabled = false;
                            }
                            

                            if (p.gameObject.transform.GetComponent<TransTower>())
                            {

                                for (int l = 0; l < 4; l++)
                                {
                                    if (p.gameObject.transform.GetComponent<TransTower>().tp[l] == 0)
                                    {
                                        p.gameObject.transform.GetComponent<TransTower>().personalLasers[l].GetComponent<Laser>().laserEnabled = false;

                                    }
                                }


                   

                                
                            }

                            int n = 1;
                            foreach (LaserProfile l in hitInfo.collider.gameObject.transform.parent.GetComponent<PrismTower>().laserProfiles)
                            {
                                n *= l.color;
                            }



                            p.personalLaser.GetComponent<LaserProfile>().GetComponent<LaserProfile>().color = n;
                            p.personalLaser.GetComponent<LaserProfile>().UpdateLaserColor();
                        }
                    }
                }
                

                hitwall = true;
                laserPoint.Add(hitInfo.point);

                bool matchingPostion = false;
                foreach (GameObject g in bounceParticles)
                {
                    if ((Vector2)g.transform.position == hitInfo.point)
                    {
                        matchingPostion = true;
                    }
                }

                if (!matchingPostion)
                {
                    var bounceFX = Instantiate(Resources.Load<GameObject>("LaserBounceFX"), hitInfo.point, Quaternion.identity);
                    bounceFX.GetComponent<LaserBounceFX>().associatedLaser = this;
                    var mainFX = bounceFX.GetComponent<ParticleSystem>().main;
                    mainFX.startColor = GameManager.Instance.colorMasterList[GetComponent<LaserProfile>().ColorToMasterListIndex(GetComponent<LaserProfile>().color)];


                    bounceParticles.Add(bounceFX);

                }

            }
            

            direction = Vector2.Reflect(hitInfo.point - (Vector2)startPoint, hitInfo.normal);

            //Set the starting point of the next launch as the hit point
            startPoint = (Vector3)hitInfo.point + direction * 0.01f;

            i++;

        } while (!hitwall && (i < maxBounces));
    }
}