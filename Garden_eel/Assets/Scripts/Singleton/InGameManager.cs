using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    private int _score;

    [SerializeField]
    private int _maxProgressValue = 1;
    [SerializeField]
    private int _curProgressValue = 0;

    [SerializeField]
    private int _curLevel = 1;
    [SerializeField]
    private int _maxLevel = 5;

    private float _curTime;

    public void AddScore(int score)
    {
        _score += score;

        InGameUIManager.instance.ui_InGameMainUI.SetScore(_score);
    }

    public void AddProgress(int vlaue)
    {
        if (_curLevel == _maxLevel)
        {
            return;
        }

        _curProgressValue += vlaue;

        if(_curProgressValue >= _maxProgressValue)
        {
            _curProgressValue = 0;
            _curLevel++;

            if(_curLevel >= _maxLevel)
            {
                _curLevel = _maxLevel;
            }
        }

        if(_curLevel == _maxLevel)
        {
            _curProgressValue = 1;
        }

        InGameUIManager.instance.ui_InGameMainUI.SetProgress((float)_curProgressValue/_maxProgressValue);
    }

    private void Update()
    {
        _curTime += Time.deltaTime;
        InGameUIManager.instance.ui_InGameMainUI.SetTime(_curTime);

    }
}
