using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMainGame : Singleton<UIMainGame>
{
    [Header("HUD")]
    [SerializeField] private UIButton btnRestart;
    [SerializeField] private UIButton btnHint;
    [SerializeField] private UIButton btnHome;

    [SerializeField] private TextMeshProUGUI txtTimeCD;
    [SerializeField] private TextMeshProUGUI txtRemain;
    [SerializeField] private TextMeshProUGUI txtLevel;


    [Header("UI sub")]
    [SerializeField] private GameObject m_HUDGameplay;
    [SerializeField] private UIGameOver m_UIGameOver;

    private void Start()
    {
        btnRestart.SetUpEvent(Action_btnRestart);
        btnHint.SetUpEvent(Action_btnHint);
        btnHome.SetUpEvent(Action_btnHome);

        //HUD
        txtLevel.text = "Level "+ GameplayCtrl.Instance.level;

    }


    #region Actions
    private void Action_btnRestart()
    {
        Module.LoadScene("MainGame");
    }

    private void Action_btnHint()
    {
        //Hint
        GameplayCtrl.Instance.ShowHint();
    }

    private void Action_btnHome() 
    {
        Module.LoadScene("Home");
    }

    public void ShowTimeRemain(int _timeRemain)
    {
        txtTimeCD.text = Module.SecondCustomToTime(_timeRemain);
    }


    public void ShowButtonRemain(int _buttonRemain)
    {
        txtRemain.text = "Remain: " + _buttonRemain.ToString("00");
    }

    public void ShowUIGameOver(bool _isWin = true)
    {
        m_UIGameOver.gameObject.SetActive(true);
        
        if(_isWin )
        {
            m_UIGameOver.ShowWin();
        }
        else
        {
            m_UIGameOver.ShowLose();
        }
    }
    #endregion

}
