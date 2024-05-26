using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class DSliderCountDown : MonoBehaviour
{
    private float _fullCountDown = 0f;
    private float _countDown = 0f;
    private Slider _slider;
	// Use this for initialization
	void Start ()
	{
	    _slider = GetComponent<Slider>();
	    _slider.enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
        _slider.enabled = _countDown > 0;
	    if (_countDown > 0)
	    {
	        _countDown -= Time.deltaTime;
	        _slider.value = _countDown/_fullCountDown;
	    }
	}

    public void StartCountDown(float countDown)
    {
        _countDown = countDown;
        _fullCountDown = countDown;
    }
}
