using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class MobileAxisSliderAdaptor : MonoBehaviour {

    private Slider _slider;
    private MobileAxis _axis;

    public string axisName = "CameraDistance";
    public float unityAxisMultiplier = 1f;

	// Use this for initialization
	void Start () {
        _slider = GetComponent<Slider>();
		_axis = new MobileAxis(axisName);
        _slider.onValueChanged.AddListener(this.OnSliderValueChanged);
        _axis.UpdateAxis(_slider.value, false, false);
    }

    void OnSliderValueChanged(float val)
    {
        _axis.UpdateAxis(val, false, false);
    }

    private void Update()
    {
        _slider.value += _axis.GetUnityAxis()* unityAxisMultiplier;
    }
}
