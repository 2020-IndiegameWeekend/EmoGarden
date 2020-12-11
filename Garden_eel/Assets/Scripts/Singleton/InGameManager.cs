using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    private int _score;

    private int _maxProgressValue;
    private int _curProgressValue;

    private float _curTime;

    public void AddScore(int score)
    {
        _score += score;

        InGameUIManager.instance.ui_InGameMainUI.SetScore(_score);
    }

    public void AddProgress(int vlaue)
    {
        _curProgressValue += vlaue;

        InGameUIManager.instance.ui_InGameMainUI.SetProgress((float)_curProgressValue/_maxProgressValue);
    }

    private void Update()
    {
        _curTime += Time.deltaTime;
        InGameUIManager.instance.ui_InGameMainUI.SetTime(_curTime);

    }
}
