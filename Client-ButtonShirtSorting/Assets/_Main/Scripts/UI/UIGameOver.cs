using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtTitle;
    [SerializeField] private TextMeshProUGUI txtDesc;

    [SerializeField] private UIButton btnHome;
    [SerializeField] private UIButton btnRestart;
    [SerializeField] private UIButton btnNext;

    [SerializeField] private AudioClip clipWin;
    [SerializeField] private AudioClip clipLose;

    private void OnEnable()
    {
        btnHome.SetUpEvent(Action_btnHome);
        btnRestart.SetUpEvent(Action_btnRestart);
        btnNext.SetUpEvent(Action_btnNext);
    }

    public void ShowWin()
    {
        SoundManager.Instance.PlayOnCamera(clipWin);
        btnNext.gameObject.SetActive(true);
        txtTitle.text = "Victory";
        txtDesc.text = "Level " + GameplayCtrl.Instance.level + " Completed!";

        btnRestart.gameObject.SetActive(Module.GameMode == EGameMode.Level);
        switch (Module.GameMode)
        {
            case EGameMode.Level:
                Module.lvCr_save++;
                break;
            case EGameMode.Endless:
                Module.crLevelEndLess++;
                break;
            default:
                break;
        }
    }

    public void ShowLose()
    {
        SoundManager.Instance.PlayOnCamera(clipLose);
        btnNext.gameObject.SetActive(false);
        txtTitle.text = "Lose";
        txtDesc.text = "Game Over! \n Time's up!";
    }


    private void Action_btnHome()
    {
        Module.LoadScene("Home");
    }

    private void Action_btnRestart()
    {
        Module.LoadScene("MainGame");
    }

    private void Action_btnNext()
    {
        switch (Module.GameMode)
        {
            case EGameMode.Level:
                Module.crLevel = Module.lvCr_save;
                break;
        }
        Module.LoadScene("MainGame");

    }

}
