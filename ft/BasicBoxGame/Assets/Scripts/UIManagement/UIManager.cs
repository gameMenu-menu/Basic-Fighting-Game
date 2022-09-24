using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject PlayScreen, LooseScreen, WinScreen;

    void OnEnable()
    {
        SceneManager.Instance.controller.OnLevelFail += OnLevelFail;
        SceneManager.Instance.controller.OnLevelVictory += OnLevelVictory;
        SceneManager.Instance.controller.OnClickContinue += OnClickContinue;
    }

    void OnDisable()
    {
        SceneManager.Instance.controller.OnLevelFail -= OnLevelFail;
        SceneManager.Instance.controller.OnLevelVictory -= OnLevelVictory;
        SceneManager.Instance.controller.OnClickContinue -= OnClickContinue;
    }

    void OnClickContinue()
    {
        LooseScreen.SetActive(false);
        WinScreen.SetActive(false);
        PlayScreen.SetActive(true);
    }

    void OnLevelFail()
    {
        LooseScreen.SetActive(true);
        PlayScreen.SetActive(false);
    }

    void OnLevelVictory()
    {
        WinScreen.SetActive(true);
        PlayScreen.SetActive(false);
    }

    public void ClickContinue()
    {
        SceneManager.Instance.controller.ClickContinue();
    }
}
