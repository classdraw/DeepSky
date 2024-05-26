using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof(Slider))]
public class DSliderText : MonoBehaviour
{
    private Slider _slider;
    public Text text;
    public bool percent = false;
	// Use this for initialization
	void Awake ()
	{
	    _slider = GetComponent<Slider>();
        _slider.onValueChanged.AddListener(this.OnValueChanged);
	}

    void OnValueChanged(float val)
    {
        if (text != null)
        {
            string fmt = "";
            if (percent)
            {
                fmt = "P0";
            }
            text.text = val.ToString(fmt);
        }
    }

}
