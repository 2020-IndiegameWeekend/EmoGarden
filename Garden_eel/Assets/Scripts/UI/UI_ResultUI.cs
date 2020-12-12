using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UI_ResultUI : MonoBehaviour
{
    [SerializeField]
    private RectTransform _image_Stamp;

    [SerializeField]
    private Text _text_Score;
    [SerializeField]
    private Text _text_Time;
    [SerializeField]
    private Text _text_Restart;

    [SerializeField]
    private Button _button_Restart;

    [SerializeField]
    private Color _color_Success;
    [SerializeField]
    private Color _color_Fail;

    [SerializeField]
    private float _stamp_Time;

    private bool success = true;
    private float time = 0;
    private int score = 0;

    public void GameOver(bool isSuccess, float overTime, int overScore)
    {
        success = isSuccess;
        time = overTime;
        score = overScore;

        gameObject.SetActive(true);
    }

    public void Restart()
    {
        SoundManager.instance.PlayEffectSound("Touch_Effect");
        UnityEngine.SceneManagement.SceneManager.LoadScene("InGame");
    }

    private void OnEnable()
    {
        if (success)
            _text_Score.color = _color_Success;
        else
            _text_Score.color = _color_Fail;

        _text_Score.text = "00000";
        _text_Time.text = string.Format("<size=100>0</size>.00");
        _image_Stamp.gameObject.SetActive(false);

        StartCoroutine(ResultCoroutine(success));
    }

    private IEnumerator ResultCoroutine(bool success)
    {
        yield return StartCoroutine(CountingTimeCoroutine(0, time, 1));

        bool stampEnd = false;

        if (success)
        {
            _image_Stamp.gameObject.SetActive(true);
            _image_Stamp.localScale = new Vector3(5, 5, 1);
            _image_Stamp.DOScale(Vector3.one, _stamp_Time).OnComplete(() =>
            {
                SoundManager.instance.PlayEffectSound("Clear_Effect");

                _image_Stamp.DOShakeAnchorPos(0.1f).OnComplete(() =>
                {
                    stampEnd = true;
                });
            });
        }
        else
        {
            stampEnd = true;
        }

        yield return new WaitUntil(() => stampEnd == true);

        _text_Restart.gameObject.SetActive(true);
        _button_Restart.gameObject.SetActive(true);
    }

    private void StampAnimation()
    {
        _image_Stamp.localScale = new Vector3(5, 5, 1);
        _image_Stamp.DOScale(Vector3.one, _stamp_Time).OnComplete(() => { _image_Stamp.DOShakeAnchorPos(0.1f); });
    }

    private IEnumerator CountingScoreCoroutine(float _min, float _max, float _time, bool success = true)
    {
        float offset = (_max - _min) / _time;

        while (_min < _max)
        {
            _min += offset * Time.deltaTime;
            _text_Score.text = ((int)_min).ToString("00000");
            yield return null;
        }

        _min = _max;
        _text_Score.text = ((int)_min).ToString("00000");
    }

    private IEnumerator CountingTimeCoroutine(float _min, float _max, float _time)
    {
        yield return StartCoroutine(CountingScoreCoroutine(0, score, 1, success));

        float offset = (_max - _min) / _time;

        while (_min < _max)
        {
            _min += offset * Time.deltaTime;

            int min = Mathf.FloorToInt(time / 60F);
            int sec = Mathf.FloorToInt(_min % 60F);
            int msec = Mathf.FloorToInt((_min * 100F) % 100F);

            _text_Time.text = string.Format("<size=100>{0}</size>.", (min * 60) + sec) + msec.ToString("00");
            yield return null;
        }

        _min = _max;

        int time_min = Mathf.FloorToInt(time / 60F);
        int time_sec = Mathf.FloorToInt(_min % 60F);
        int time_msec = Mathf.FloorToInt((_min * 100F) % 100F);

        _text_Time.text = string.Format("<size=100>{0}</size>.", (time_min * 60) + time_sec) + time_msec.ToString("00");
    }
}
