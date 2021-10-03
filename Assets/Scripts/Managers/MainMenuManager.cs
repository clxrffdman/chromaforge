using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{

    public GameObject levelUI;
    public GameObject mainUI;
    public GameObject optionUI;
    public GameObject creditUI;
    public GameObject indicatorText;
    public GameObject title;
    public GameObject optionsText;
    public GameObject levelText;
    public GameObject fallingButton;
    public GameObject audioOneshotPrefab;
    public bool isClicked = false;
    public Animator anim;

    
    public void LoadProjectList()
    {
        levelUI.SetActive(true);
        //mainUI.SetActive(false);
    }
    public void LoadOptionUI() {
        var pauseSound = Instantiate(audioOneshotPrefab, transform.position, Quaternion.identity);
        pauseSound.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Audio/UI_Confirm_0" + Random.Range(1, 3));
        optionUI.SetActive(true);
        //mainUI.SetActive(false);
    }

    public void LoadCreditUI() {
        var pauseSound = Instantiate(audioOneshotPrefab, transform.position, Quaternion.identity);
        pauseSound.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Audio/UI_Confirm_0" + Random.Range(1, 3));
        creditUI.SetActive(true);
        //mainUI.SetActive(false);
    }

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    public void ReloadMain() {
        mainUI.SetActive(true);
    
        optionUI.SetActive(false);
        creditUI.SetActive(false);

        var pauseSound = Instantiate(audioOneshotPrefab, transform.position, Quaternion.identity);
        pauseSound.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Audio/UI_Back_0" + Random.Range(1,3));

    }

    public void BackFromLevels()
    {
        anim.Play("mainMenuUnLevels");
        var pauseSound = Instantiate(audioOneshotPrefab, transform.position, Quaternion.identity);
        pauseSound.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Audio/UI_Back_0" + Random.Range(1, 3));
    }

    public void ToLevels()
    {
        anim.Play("mainMenuLevels");
        var pauseSound = Instantiate(audioOneshotPrefab, transform.position, Quaternion.identity);
        pauseSound.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Audio/UI_Confirm_0" + Random.Range(1, 3));
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!isClicked && Input.anyKeyDown) {
            isClicked = true;
            anim.Play("mainMenuMainLoad");
        }
        
    }
   
}
