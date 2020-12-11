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

    public void SetTime(string time)
    {
        //TODO : time이 string이 아닐 수 있음
        _text_Time.text = time;
    }

    public void SetScore(int score)
    {
        _text_Score.text = score.ToString();
    }

    public void SetProgress(float value)
    {
        _slider_Progress.value = value;
    }
}
