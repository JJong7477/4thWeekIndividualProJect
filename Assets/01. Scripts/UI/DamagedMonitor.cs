using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamagedMonitor : MonoBehaviour
{
    private Image _image;
    private float _flashSpeed = 0.5f;

    private Coroutine _coroutine;

    private void Start()
    {
        _image = GetComponent<Image>();
        GameManager.Player.Condition.OnDamaged += Flash;
    }

    public void Flash()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        
        _image.enabled = true;
        _image.color = new Color(1f, 100f / 255f, 100f / 255f);
        _coroutine = StartCoroutine(FadeAway());
    }

    private IEnumerator FadeAway()
    {
        float startAlpha = 0.3f;
        float a = startAlpha;

        while (a > 0)
        {
            a -= (startAlpha / _flashSpeed) * Time.deltaTime;
            _image.color = new Color(1f, 100f / 255f, 100f / 255f, a);
            yield return null;
        }

        _image.enabled = false;
    }
}
