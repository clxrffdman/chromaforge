using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    
    public bool isPaused;
    public bool pauseMenuActive;
    public bool isCompleted;

    public Color[] colorMasterList;

    public AudioClip[] lightSFX;
    public AudioClip[] uiButtonSFX;
    public AudioClip victoryStinger;

    public AudioClip[] backgroundMusic;

    public PlayerController playerController;

    public GameObject audioOneshotPrefab;
    public GameObject pauseUI;
    public GameObject completionUI;
    public GameObject tooltipUI;
    public AudioMixer mainAudioMixer;
    public GameObject zoomedOutCamera;
    public GameObject infoUI;

    public bool isTutorial;
    public GameObject tutorialCard;
    public GameObject tutorialCard2;
    public bool tutorialActive;
    public int tutorialState;

    public bool isZoomed;
    public bool isInfo;

    public string currentLevel;
    public string fileSaveName;
    public int saveIndex;


    public int ColorMath(int[] colorInputs)
    {
        if(colorInputs.Length == 1)
        {
            return colorInputs[0];
        }

        

        return -1;
    }
    // Start is called before the first frame update
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            if(Instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void Start()
    {
        Time.timeScale = 1;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        if(pauseUI == null)
        {
            if(Resources.FindObjectsOfTypeAll<PauseUI>() != null)
            {
                PauseUI[] allPauseUI = Resources.FindObjectsOfTypeAll<PauseUI>();
                if(allPauseUI.Length > 0)
                {
                    pauseUI = allPauseUI[0].gameObject;
                }
            }
        }

        if(infoUI == null)
        {
            infoUI = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
        }

        GetComponent<AudioSource>().clip = backgroundMusic[Random.Range(0, 3)];
        GetComponent<AudioSource>().Play();

        if (isTutorial)
        {
            tutorialCard.SetActive(true);
            tutorialActive = true;
            
        }


        Invoke("ZoomBackIn", 1.5f);

        
    }

    public void ZoomBackIn()
    {
        zoomedOutCamera.SetActive(false);
        playerController.canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isCompleted && !tutorialActive)
        {
            PublicPause(pauseMenuActive);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!isPaused && !tutorialActive)
            {
                Zoom(isZoomed);
            }
            
        }

        if (Input.GetKeyDown(KeyCode.I) && !isCompleted)
        {
            if (isTutorial && tutorialActive)
            {
                if(tutorialState == 1)
                {
                    tutorialCard2.SetActive(false);
                    tutorialActive = false;
                }
                else
                {
                    tutorialState += 1;
                    tutorialCard2.SetActive(true);
                    tutorialCard.SetActive(false);
                }

                
            }
            else if(!isPaused)
            {
                InfoUI(isInfo);
            }


        }
    }

    public void InfoUI(bool i)
    {
        if (i)
        {
            isInfo = false;
            playerController.canMove = true;
            infoUI.SetActive(false);
        }
        else
        {
            isInfo = true;
            playerController.canMove = true;
            infoUI.SetActive(true);
        }
    }

    public void Zoom(bool z)
    {
        if (z)
        {
            zoomedOutCamera.SetActive(false);
            isZoomed = false;
        }
        else
        {
            zoomedOutCamera.SetActive(true);
            isZoomed = true;
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
    }

    public void UnPauseGame()
    {
        isPaused = false;
        Time.timeScale = 1;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ToNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void DoPublicPause()
    {
        PublicPause(isPaused);
    }

    public void PublicPause(bool isPauseActive)
    {
        if (!isPauseActive)
        {
            pauseUI.SetActive(true);
            pauseMenuActive = true;
            AudioSource[] audioArray = GameObject.FindObjectsOfType<AudioSource>();
            foreach (AudioSource a in audioArray)
            {
                a.Pause();
            }
            var pauseSound = Instantiate(audioOneshotPrefab, transform.position, Quaternion.identity);
            pauseSound.GetComponent<AudioSource>().clip = uiButtonSFX[0];
            PauseGame();



        }
        else
        {
            pauseUI.SetActive(false);
            pauseMenuActive = false;
            AudioSource[] audioArray = GameObject.FindObjectsOfType<AudioSource>();
            foreach (AudioSource a in audioArray)
            {
                a.UnPause();
            }
            var pauseSound = Instantiate(audioOneshotPrefab, transform.position, Quaternion.identity);
            pauseSound.GetComponent<AudioSource>().clip = pauseSound.GetComponent<AudioSource>().clip = uiButtonSFX[1];
            UnPauseGame();
        }

        
    }

    public void Victory()
    {
        if (!isCompleted)
        {
            isCompleted = true;
            if (isPaused)
            {
                PublicPause(pauseMenuActive);
            }

            if (isInfo)
            {
                InfoUI(isInfo);
            }
            completionUI.SetActive(true);
            playerController.canMove = false;
            playerController.rb.velocity = new Vector2(0, 0);

            GetComponent<AudioSource>().clip = victoryStinger;
            GetComponent<AudioSource>().loop = false;
            GetComponent<AudioSource>().Play();
        }

        

    }
    

}
