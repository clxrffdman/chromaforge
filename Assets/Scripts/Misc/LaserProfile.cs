using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProfile : MonoBehaviour
{
    public int color;

    
    /*
     * RED = 2
     * GREEN = 3
     * BLUE = 5
     * YELLOW = 6
     * MAGENTA = 10
     * CYAN = 15
     * WHITE = 30
     * 
     */

    public int oldColor;

    public LineRenderer laserBeam;
    public int colorSoundId;
    public GameObject audioInstance;

    // Start is called before the first frame update
    void Start()
    {

        laserBeam = transform.GetChild(0).GetComponent<LineRenderer>();
        

        


        UpdateLaserColor();
    }

    public int ColorToMasterListIndex(int input)
    {

        int rv = -1;

        if(input % 2 == 0)
        {
            rv = 0;
        }

        if (input % 3 == 0)
        {
            rv = 1;
        }

        if (input % 5 == 0)
        {
            rv = 2;
        }

        if (input % 6 == 0)
        {
            rv = 3;
        }

        if (input % 10 == 0)
        {
            rv = 4;
        }

        if (input % 15 == 0)
        {
            rv = 5;
        }

        if (input % 30 == 0)
        {
            rv = 6;
        }






        return rv;

        /*
        switch (input)
        {
            case 2:
                return 0;
 
            case 3:
                return 1;
              
            case 5:
                return 2;
              
            case 6:
                return 3;
               
            case 10:
                return 4;
                
            case 15:
                return 5;
               
            case 30:
                return 6;
             
        }

        return -1;
                */
    }
    public void UpdateLaserColor()
    {
       

        int c = 1;

        if(color % 2 == 0)
        {
            c = 2;
        }
        if (color % 3 == 0)
        {
            c = 3;
        }

        if (color % 5 == 0)
        {
            c = 5;
        }

        if (color % 6 == 0)
        {
            c = 6;
        }

        if (color % 15 == 0)
        {
            c = 15;
        }
        if (color % 10 == 0)
        {
            c = 10;
        }

        if (color % 30 == 0)
        {
            c = 30;
        }


        switch (c)
        {
            case 2:
                laserBeam.startColor = GameManager.Instance.colorMasterList[0];
                laserBeam.endColor = GameManager.Instance.colorMasterList[0];
                break;
            case 3:
                laserBeam.startColor = GameManager.Instance.colorMasterList[1];
                laserBeam.endColor = GameManager.Instance.colorMasterList[1];
                break;
            case 5:
                laserBeam.startColor = GameManager.Instance.colorMasterList[2];
                laserBeam.endColor = GameManager.Instance.colorMasterList[2];
                break;
            case 6:
                laserBeam.startColor = GameManager.Instance.colorMasterList[3];
                laserBeam.endColor = GameManager.Instance.colorMasterList[3];
                break;
            case 10:
                laserBeam.startColor = GameManager.Instance.colorMasterList[4];
                laserBeam.endColor = GameManager.Instance.colorMasterList[4];
                break;
            case 15:
                laserBeam.startColor = GameManager.Instance.colorMasterList[5];
                laserBeam.endColor = GameManager.Instance.colorMasterList[5];
                break;
            case 30:
                laserBeam.startColor = GameManager.Instance.colorMasterList[6];
                laserBeam.endColor = GameManager.Instance.colorMasterList[6];
                break;
        }

        color = c;


        if (color != oldColor && oldColor != colorSoundId)
        {
            

            
            switch (color)
            {

                case 2:

                    break;
                case 3:

                    break;
                case 5:

                    break;
                case 6:
                    if(audioInstance == null)
                    {
                        var soundOneShot = Instantiate(GameManager.Instance.audioOneshotPrefab, transform.position, Quaternion.identity);
                        audioInstance = soundOneShot;
                        soundOneShot.transform.parent = GameManager.Instance.gameObject.transform;
                        soundOneShot.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Audio/Color" + 1 + "_" + Random.Range(1, 4));
                        colorSoundId = color;
                        oldColor = color;
                    }
                    
                    break;
                case 10:
                    if(audioInstance == null)
                    {
                        var soundOneShot1 = Instantiate(GameManager.Instance.audioOneshotPrefab, transform.position, Quaternion.identity);
                        audioInstance = soundOneShot1;
                        soundOneShot1.transform.parent = GameManager.Instance.gameObject.transform;
                        soundOneShot1.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Audio/Color" + 2 + "_" + Random.Range(1, 4));
                        colorSoundId = color;
                        oldColor = color;
                    }
                    
                    break;
                case 15:

                    if(audioInstance == null)
                    {
                        var soundOneShot2 = Instantiate(GameManager.Instance.audioOneshotPrefab, transform.position, Quaternion.identity);
                        audioInstance = soundOneShot2;
                        soundOneShot2.transform.parent = GameManager.Instance.gameObject.transform;
                        soundOneShot2.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Audio/Color" + 3 + "_" + Random.Range(1, 4));
                        colorSoundId = color;
                        oldColor = color;
                    }
                   
                    break;
                case 30:
                    if(audioInstance == null)
                    {
                        var soundOneShot3 = Instantiate(GameManager.Instance.audioOneshotPrefab, transform.position, Quaternion.identity);
                        audioInstance = soundOneShot3;
                        soundOneShot3.transform.parent = GameManager.Instance.gameObject.transform;
                        soundOneShot3.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Audio/Color" + 4 + "_" + Random.Range(1, 4));
                        colorSoundId = color;
                        oldColor = color;
                    }
                   
                    
                    break;
            }
        }

       


    }


}
