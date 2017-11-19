using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGUIControl : MonoBehaviour
{

    protected string press_button = "";
    public GameObject StartImg;
    public GameObject SetImg;
    public GameObject IntImg;
    public GameObject DifficultyImg;
    GlobalSingleton globalSigton;



    // Use this for initialization
    void Start()
    {
        globalSigton = GlobalSingleton.GetInstance();
    }


    public void OnButtonAlone()
    {
        IntImg.SetActive(true);
        StartImg.SetActive(false);
        SetImg.SetActive(false);
        DifficultyImg.SetActive(false);
        globalSigton.mode = GlobalSingleton.Mode.Alone;
    }

    public void OnButtonNetwrok()
    {
        IntImg.SetActive(true);
        StartImg.SetActive(false);
        SetImg.SetActive(false);
        DifficultyImg.SetActive(false);
        globalSigton.mode = GlobalSingleton.Mode.Network;
    }

    public void OnButtonSetting()
    {
        IntImg.SetActive(false);
        StartImg.SetActive(false);
        SetImg.SetActive(true);
        DifficultyImg.SetActive(false);
    }

    public void OnButtonExit()
    {
        Application.Quit();
    }

    public void OnButtonReturn()
    {
        IntImg.SetActive(false);
        StartImg.SetActive(true);
        SetImg.SetActive(false);
        DifficultyImg.SetActive(false);
    }

    public void OnButtonStart()
    {
        if (globalSigton.mode == GlobalSingleton.Mode.Alone)
        {
            IntImg.SetActive(false);
            StartImg.SetActive(false);
            SetImg.SetActive(false);
            DifficultyImg.SetActive(true);
        }

        else if (globalSigton.mode == GlobalSingleton.Mode.Network)
            SceneManager.LoadScene("2-Select");

        else
            return;
    }

    public void OnButtonEasy()
    {
        globalSigton.difficulty = GlobalSingleton.Difficulty.Easy;
        SceneManager.LoadScene("Main");
    }

    public void OnButtonHard()
    {
        globalSigton.difficulty = GlobalSingleton.Difficulty.Hard;
        SceneManager.LoadScene("Main");
    }

    public void OnInputFieldName(string str)
    {
        globalSigton._name = str;
    }


    //public void OnButtonPressed(string button_name)
    //{
    //    this.press_button = button_name;
    //    switch (press_button)
    //    {
    //        case "Alone":
    //            IntImg.SetActive(true);
    //            StartImg.SetActive(false);
    //            SetImg.SetActive(false);
    //            break;

    //        case "Network":
    //            IntImg.SetActive(true);
    //            StartImg.SetActive(false);
    //            SetImg.SetActive(false);
    //            break;

    //        case "Setting":
    //            IntImg.SetActive(false);
    //            StartImg.SetActive(false);
    //            SetImg.SetActive(true);
    //            break;

    //        case "Exit":
    //            Application.Quit();
    //            break;

    //        case "Return":
    //            IntImg.SetActive(false);
    //            StartImg.SetActive(true);
    //            SetImg.SetActive(false);
    //            break;

    //        case "Start":
    //            SceneManager.LoadScene("");

    //        default:
    //            break;
    //    }
    //}


}
