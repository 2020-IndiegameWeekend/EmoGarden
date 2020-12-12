using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGameMainUI : MonoBehaviour
{
    [SerializeField]
    private Slider _slider_Progress;

    [SerializeField]
    private Text _text_Score;
    [SerializeField]
    private Text _text_Time;

    public void SetTime(float time)
    {
        int min = Mathf.FloorToInt(time / 60F);
        int sec = Mathf.FloorToInt(time % 60F);
        int msec = Mathf.FloorToInt((time * 100F) % 100F);

        _text_Time.text = string.Format("<size=100>{0}</size>.", min + sec) + msec.ToString("00");
    }

    public void SetScore(int score)
    {
        _text_Score.text = "<color=#00FFFF><size=63>SCORE</size></color> " + score.ToString("00000");
    }

    public void SetProgress(float value)
    {
        _slider_Progress.value = value;
    }
}
