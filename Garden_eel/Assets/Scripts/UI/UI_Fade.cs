using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Fade : MonoBehaviour
{
    private Image _image_Fade;

    private Color _startColor;
    private Color _targetColor;

    private float _fadeTime;

    private IEnumerator UpdateColorCoroutine()
    {
        float t = 0;
        while (t < 1)
        {
            _image_Fade.color = Color.Lerp(_startColor, _targetColor, t);
            t += Time.deltaTime / _fadeTime;
            yield return new WaitForEndOfFrame();
        }

        _image_Fade.color = _targetColor;
        _image_Fade.gameObject.SetActive(false);
    }
}
