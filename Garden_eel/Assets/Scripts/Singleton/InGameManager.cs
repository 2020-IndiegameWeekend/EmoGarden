using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    [SerializeField]
    private Cinemachine.CinemachineVirtualCamera _cam;

    private int _score;

    private int[] _maxProgressValues = new int[4] { 12, 60, 135, 285 };

    [SerializeField]
    private int _curProgressValue = 0;

    [SerializeField]
    private int _curLevel = 0;
    [SerializeField]
    private int _maxLevel = 4;

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

        if(_curProgressValue >= _maxProgressValues[_curLevel])
        {
            _curProgressValue = 0;
            _curLevel++;
            _cam.m_Lens.OrthographicSize *= 1.1f;

            if(_curLevel >= _maxLevel)
            {
                _curLevel = _maxLevel;
            }
        }

        if(_curLevel == _maxLevel)
        {
            _curProgressValue = 1;
        }

        InGameUIManager.instance.ui_InGameMainUI.SetProgress((float)_curProgressValue/_maxProgressValues[_curLevel]);
    }

    private void Update()
    {
        _curTime += Time.deltaTime;
        InGameUIManager.instance.ui_InGameMainUI.SetTime(_curTime);

        if (Input.GetKeyDown(KeyCode.A))
        {
            AddProgress(1);
        }

    }
}
