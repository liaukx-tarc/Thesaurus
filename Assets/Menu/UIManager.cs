using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]

public class UIManager : MonoBehaviour
{
    public AudioClip buttonHit;
    public AudioClip narration;
    public GameObject instruction;
    public GameObject mainMenu;
    public GameObject story;
    public GameObject credit;
    public GameObject buttonInstruction;
    public GameObject playInstruction;
    public GameObject nextBtn;
    public GameObject previousBtn;

    void playBtnSound()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = buttonHit;
        audio.Play();
    }

    void playNarration()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Stop();
        audio.clip = narration;
        audio.Play();
    }

    public void PlayButtonClicked()
    {
        playBtnSound();
        story.SetActive(true);
        playNarration();
    }

    public void ContinueButtonClicked()
    {
        playBtnSound();
        SceneManager.LoadScene(1);
    }

    public void InstructionButtonClicked()
    {
        playBtnSound();
        instruction.SetActive(true);
    }

    public void CreditButtonClicked()
    {
        playBtnSound();
        credit.SetActive(true);
    }

    public void CreditBackButtonClicked()
    {
        playBtnSound();
        credit.SetActive(false);
    }

    public void QuitButtonClicked()
    {
        Application.Quit();
    }

    public void BackButtonClicked()
    {
        playBtnSound();
        instruction.SetActive(false);
    }

    public void NextButtonClicked()
    {
        playBtnSound();
        buttonInstruction.SetActive(false);
        playInstruction.SetActive(true);
        nextBtn.SetActive(false);
        previousBtn.SetActive(true);
    }

    public void PreviousButtonClicked()
    {
        playBtnSound();
        playInstruction.SetActive(false);
        buttonInstruction.SetActive(true);
        previousBtn.SetActive(false);
        nextBtn.SetActive(true);
    }
}

