using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{

    public LevelControl levelControl;
    public GameObject devOptionsMenu;
    public bool devOptionsEnabled;
    // Start is called before the first frame update
    void Start()
    {
        if(levelControl == null)
        {
            levelControl = Resources.FindObjectsOfTypeAll<LevelControl>()[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayDevOptions()
    {
        if (!devOptionsEnabled)
        {
            devOptionsEnabled = true;
            devOptionsMenu.SetActive(true);
        }
        else
        {
            devOptionsEnabled = false;
            devOptionsMenu.SetActive(false);
        }
    }

    

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Resume()
    {
        GameManager.Instance.PublicPause(true);
    }




}
