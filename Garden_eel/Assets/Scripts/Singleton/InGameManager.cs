using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : Singleton<InGameManager>
{
    [SerializeField]
    private GardenEelHead _head;
    
    [SerializeField]
    private Cinemachine.CinemachineVirtualCamera _cam;

    private int _score;

    [SerializeField]
    private int[] _maxProgressValues = new int[4] { 12, 60, 135, 285 };
    private int[] _lvUpScore = new int[4] { 500, 2000, 10000, 30000 };

    [SerializeField]
    private int _curProgressValue = 0;

    [SerializeField]
    private int _curLevel = 0;
    [SerializeField]
    private int _maxLevel = 4;

    private float _curTime;
    private float _scoreTime;
    private float lastESC = -1000f;
    
    [SerializeField]
    private float _cameraUpTime;

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
            AddScore(_lvUpScore[_curLevel - 1]);
            SoundManager.instance.PlayEffectSound("LevelUp_Effect");
            _head.LevelUp(_curLevel);
            StartCoroutine(CameraSizeUpCoroutine());
        }

        float value = 0;

        if(_curLevel < _maxLevel)
            value = (float)_curProgressValue / _maxProgressValues[_curLevel];
        else
            value = 1;

        InGameUIManager.instance.ui_InGameMainUI.SetProgress(value);
    }

    public Vector2 GetHeadPosition()
    {
        return _head.transform.position;
    }

    public void GameOver(bool success)
    {
        InGameUIManager.instance.ui_ResultUI.GameOver(success, _curTime, _score);
    }

    private void Update()
    {
        _curTime += Time.deltaTime;
        InGameUIManager.instance.ui_InGameMainUI.SetTime(_curTime);

        _scoreTime += Time.deltaTime;
        if (_scoreTime >= 60)
        {
            _scoreTime = 0;
            AddScore(10);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_curTime - lastESC < 0.5f)
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }

            lastESC = _curTime;
        }
    }

    private IEnumerator CameraSizeUpCoroutine()
    {
        float minsize = _cam.m_Lens.OrthographicSize;
        float maxsize = _cam.m_Lens.OrthographicSize * 1.1f;

        float offset = (maxsize - minsize) / _cameraUpTime;
        float time = 0;

        while(time <= _cameraUpTime)
        {
            time += Time.deltaTime;
            minsize += offset * Time.deltaTime;
            _cam.m_Lens.OrthographicSize = minsize;
            yield return null;
        }

        _cam.m_Lens.OrthographicSize = maxsize;
    }
}
