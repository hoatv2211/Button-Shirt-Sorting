using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIHome : MonoBehaviour
{
    [SerializeField] private UIButton btnPlay;
    [SerializeField] private UIButton btnEndless;

    void Start()
    {
        btnPlay.SetUpEvent(Action_btnPlay);
        btnEndless.SetUpEvent(Action_btnEndless);

        btnPlay.GetComponentInChildren<TextMeshProUGUI>().text = "PLAY\n" + "Lv." + Module.lvCr_save;
    }

    private void Action_btnPlay()
    {
        Module.LoadScene("MainGame");
        Module.crLevel = Module.lvCr_save;
        Module.GameMode = EGameMode.Level;
    }

    private void Action_btnEndless() 
    {
        Module.LoadScene("MainGame");
        Module.crLevelEndLess = 1;
        Module.GameMode = EGameMode.Endless;
    }
   
}
