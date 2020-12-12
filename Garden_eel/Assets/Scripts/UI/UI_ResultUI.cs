using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ResultUI : MonoBehaviour
{
    private IEnumerator CountingCoroutine(Text _text, float _min, float _max, float _time)
    {
        float offset = (_max - _min) / _time;

        while (_min < _max)
        {
            _min += offset * Time.deltaTime;
            _text.text = ((int)_min).ToString();
            yield return null;
        }

        _min = _max;
        _text.text = ((int)_min).ToString();
    }
}
