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
    private Color _color_Success;
    [SerializeField]
    private Color _color_Fail;

    [SerializeField]
    private float _stamp_Time;

    private bool success = false;
    private float time = 0;
    private int score = 0;

    public void GameOver(bool isSuccess, float overTime, int overScore)
    {
        success = isSuccess;
        time = overTime;
        overScore = score;

        gameObject.SetActive(true);
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
        _text_Score.text = ((int)_min).ToString();
    }

    private IEnumerator CountingTimeCoroutine(float _min, float _max, float _time)
    {
        yield return StartCoroutine(CountingScoreCoroutine(0, score, 1, success));

        float offset = (_max - _min) / _time;

        while (_min < _max)
        {
            _min += offset * Time.deltaTime;

            float min = Mathf.FloorToInt(_min / 60f);
            float sec = Mathf.FloorToInt(_min % 60f);

            _text_Time.text = string.Format("<size=100>{0}</size>.", min) + sec.ToString("00");
            yield return null;
        }

        _min = _max;

        float time_min = Mathf.FloorToInt(_max / 60f);
        float time_sec = Mathf.FloorToInt(_max % 60f);

        _text_Time.text = string.Format("<size=100>{0}</size>.", time_min) + time_sec.ToString("00");
    }
}
