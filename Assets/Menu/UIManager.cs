using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]

public class UIManager : MonoBehaviour
{
    public AudioClip buttonHit;
    public GameObject instruction;
    public GameObject mainMenu;
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

    public void PlayButtonClicked()
    {
        playBtnSound();
        SceneManager.LoadScene(1);
    }

    public void InstructionButtonClicked()
    {
        playBtnSound();
        instruction.SetActive(true);
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
        buttonInstruction.SetActive(false);
        playInstruction.SetActive(true);
        nextBtn.SetActive(false);
        previousBtn.SetActive(true);
    }

    public void PreviousButtonClicked()
    {
        playInstruction.SetActive(false);
        buttonInstruction.SetActive(true);
        previousBtn.SetActive(false);
        nextBtn.SetActive(true);
    }
}

