using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]

public class UIManager : MonoBehaviour
{

    private string levelToLoad;
    public AudioClip buttonHit;
    public RawImage arrow,mouse,shift,escape,e,q;
    public Text text1, text2, text3, text4,text5,text6,text7;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void playBtnSound()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = buttonHit;
        audio.Play();
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(levelToLoad);
    }

    public void PlayButtonClicked()
    {
        levelToLoad = "MapScene 1";
        playBtnSound();
        StartCoroutine(Wait());
       
    }

    public void InstructionButtonClicked()
    {
        levelToLoad = "Instruction Scene";
        playBtnSound();
        StartCoroutine(Wait());

    }
    public void QuitButtonClicked()
    {
        Application.Quit();
    }
    public void BackButtonClicked()
    {
        levelToLoad = "Menu Scene";
        playBtnSound();
        StartCoroutine(Wait());
    }
    public void NextButtonClicked()
    {
        arrow.gameObject.SetActive(false);
        mouse.gameObject.SetActive(false);
        shift.gameObject.SetActive(false);
        escape.gameObject.SetActive(false);
        e.gameObject.SetActive(false);
        q.gameObject.SetActive(false);

        text1.gameObject.SetActive(false);
        text2.gameObject.SetActive(false);
        text3.gameObject.SetActive(false);
        text4.gameObject.SetActive(false);
        text5.gameObject.SetActive(true);
        text6.gameObject.SetActive(false);
        text7.gameObject.SetActive(false);
    }

    public void PreviousButtonClicked()
    {
        arrow.gameObject.SetActive(true);
        mouse.gameObject.SetActive(true);
        shift.gameObject.SetActive(true);
        escape.gameObject.SetActive(true);
        e.gameObject.SetActive(true);
        q.gameObject.SetActive(true);

        text1.gameObject.SetActive(true);
        text2.gameObject.SetActive(true);
        text3.gameObject.SetActive(true);
        text4.gameObject.SetActive(true);
        text5.gameObject.SetActive(false);
        text6.gameObject.SetActive(true);
        text7.gameObject.SetActive(true);
    }
}

