using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject instruction;
    public GameObject menuButton;
    public GameObject buttonInstruction;
    public GameObject playInstruction;
    public GameObject nextBtn;
    public GameObject previousBtn;
    public GameObject blackScene;

    public void Resume()
    {
        float blackAlpha = 0;
        blackScene.SetActive(false);
        blackScene.GetComponent<UnityEngine.UI.Image>().color = new Color(0, 0, 0, blackAlpha);

        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        this.gameObject.SetActive(false);
    }

    public void Instruction()
    {
        menuButton.SetActive(false);
        instruction.SetActive(true);
    }

    public void backToPause()
    {
        menuButton.SetActive(true);
        instruction.SetActive(false);
    }

    public void NextButton()
    {
        buttonInstruction.SetActive(false);
        playInstruction.SetActive(true);
        nextBtn.SetActive(false);
        previousBtn.SetActive(true);
    }

    public void PreviousButton()
    {
        playInstruction.SetActive(false);
        buttonInstruction.SetActive(true);
        previousBtn.SetActive(false);
        nextBtn.SetActive(true);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
}
