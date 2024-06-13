using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.UI.Extensions;
using System;
using DG.Tweening;
using Utilities;

[AddComponentMenu("UI/Extensions/ProgressBar")]
[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Image))]

public class DProgressBar : UIBehaviour
{
    public enum TextFormat
    {
        Number,
        Percent
    }
    [SerializeField]
	private float m_MinValue = 0;//进度条最小值
    public float minValue { get { return m_MinValue; } set { if (SetPropertyUtility.SetStruct(ref m_MinValue, value)) { Set(m_Value); UpdateVisuals(); } } }

    [SerializeField]
    private float m_MaxValue = 1;//进度条最大值
    public float maxValue { get { return m_MaxValue; } set { if (SetPropertyUtility.SetStruct(ref m_MaxValue, value)) { Set(m_Value); UpdateVisuals(); } } }

    [SerializeField]
    private bool m_WholeNumbers = false;
    public bool wholeNumbers { get { return m_WholeNumbers; } set { if (SetPropertyUtility.SetStruct(ref m_WholeNumbers, value)) { Set(m_Value); UpdateVisuals(); } } }

    [SerializeField]
    protected float m_Value = 1;

    [SerializeField]
    protected bool m_UseGrowAnimationProgress = false;
    [SerializeField]
    protected bool m_UseLessenAnimationProgress = false;
    [SerializeField][Range(1f,0.1f)]
    protected float m_AnimationTime = 0.5f;

    public Text TextComponent;//进度文本描述
	public TextFormat m_TextFormat = TextFormat.Number;

	private long m_currVal = 100;//进度文本描述当前值
	private long m_maxVal = 100;//进度文本描述最大值
    private Tween m_progressTween = null; //进度条增幅动画
    public virtual float value
    {
        get
        {
            if (wholeNumbers)
                return Mathf.Round(m_Value);
            return m_Value;
        }
        set
        {
            Set(value);
        }
    }

    public float normalizedValue
    {
        get
        {
            if (Mathf.Approximately(minValue, maxValue))
                return 0;
            return Mathf.InverseLerp(minValue, maxValue, value);
        }
        set
        {
            this.value = Mathf.Lerp(minValue, maxValue, value);
        }
    }

    private float mTextValue = 0f;
    private float mImgValue = 0f;

    private Image m_FillImage;

    protected DProgressBar()
    { }

    protected override void Awake()
    {
        base.Awake();
        m_FillImage = GetComponent<Image>();
		m_FillImage.type = Image.Type.Filled;
    }

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();

        m_FillImage = GetComponent<Image>();

        if (wholeNumbers)
        {
            m_MinValue = Mathf.Round(m_MinValue);
            m_MaxValue = Mathf.Round(m_MaxValue);
        }

        //Onvalidate is called before OnEnabled. We need to make sure not to touch any other objects before OnEnable is run.
        if (IsActive())
        {
            Set(m_Value);
            // Update rects since other things might affect them even if value didn't change.
            UpdateVisuals();
        }
    }

#endif // if UNITY_EDITOR

    protected override void OnEnable()
    {
        base.OnEnable();
        Set(m_Value);
        
    }

    protected override void OnDidApplyAnimationProperties()
    {
        // Has value changed? Various elements of the slider have the old normalisedValue assigned, we can use this to perform a comparison.
        // We also need to ensure the value stays within min/max.
        m_Value = ClampValue(m_Value);
        float oldNormalizedValue = m_FillImage.fillAmount;
       
        if (!Mathf.Approximately(oldNormalizedValue, normalizedValue))
            UpdateVisuals();
    }

    float ClampValue(float input)
    {
        float newValue = Mathf.Clamp(input, minValue, maxValue);
        if (wholeNumbers)
            newValue = Mathf.Round(newValue);
        return newValue;
    }

    // Set the valueUpdate the visible Image.
    void Set(float input)
    {
        //Debug.Log(string.Format("DprogressBar Set:{0}", input));
        // Clamp the input
        float newValue = ClampValue(input); 

        m_Value = newValue;
        UpdateVisuals();
    }

    [XLua.LuaCallCSharp]
    public void SetShowValue(long currVal, long maxVal)
    {
        //Debug.Log(String.Format("DprogressBar SetShowValue: {0},{1}", currVal, maxVal));
        // If the stepped value doesn't match the last one, it's time to update
        if (m_currVal == currVal && m_maxVal == maxVal)
            return;
        m_currVal = currVal;
        m_maxVal = maxVal;
        if(maxVal == -1)
        {
            Set(1.0f);
        } 
        else
        {
            Set(m_maxVal == 0 ? 0 : (float)m_currVal / m_maxVal);
        }                   
    }

    [XLua.LuaCallCSharp]
    public void SetPercent(float newValue)
    {
        // If the stepped value doesn't match the last one, it's time to update
        if (Mathf.Approximately(m_Value, newValue))
            return;
        Set(newValue);
    }

    [XLua.LuaCallCSharp]
    public void SetTextPercent(float textPercent)
    {
        mTextValue = textPercent;

        if(TextComponent != null){
            TextComponent.text = String.Format("{0}%", mTextValue * 100);
        }
    }

    [XLua.LuaCallCSharp]
    public void SetTextPercent2(float textPercent)
    {
        mTextValue = textPercent;
        if (TextComponent != null){
            TextComponent.text = String.Format("{0}%",textPercent);
        }
    }

    [XLua.LuaCallCSharp]
    public void SetPercentRound(float percent,int round){
        float temp=(float)Math.Round((double)percent,round);
        mTextValue = temp;

        if(TextComponent != null){
            TextComponent.text = String.Format("{0}%", mTextValue * 100);
        }
    }

    [XLua.LuaCallCSharp]
    public void SetImgPercent(float imgPercent){
        mImgValue = imgPercent;
        if (m_FillImage != null){
            m_FillImage.fillAmount = mImgValue;
        }
        m_Value = mImgValue;
    }

    // Force-update the slider. Useful if you've changed the properties and want it to update visually.
    private void UpdateVisuals()
    {
        if(m_FillImage != null)
        {
            if ((m_UseGrowAnimationProgress && m_FillImage.fillAmount < normalizedValue) || (m_UseLessenAnimationProgress && m_FillImage.fillAmount > normalizedValue))
            {
                KillProgressTween();
                m_progressTween = m_FillImage.DOFillAmount(normalizedValue, m_AnimationTime);
                m_progressTween.SetAutoKill(true);
                m_progressTween.onComplete=OnTweenComplete;
            }
            else
            {
                m_FillImage.fillAmount = normalizedValue;
            }
        }
        if(TextComponent != null)
        {
            //Debug.Log(String.Format("DprogressBar UpdateVisuals: {0},{1}", m_currVal, m_maxVal));
            switch (m_TextFormat)
            {
                case TextFormat.Number:
				TextComponent.text = String.Format("{0}/{1}", m_currVal, m_maxVal);break;
                case TextFormat.Percent:
				TextComponent.text = String.Format("{0}%", normalizedValue * 100);break;
            }
        }
    }

    private void OnTweenComplete(){
        m_progressTween=null;
    }

    private void KillProgressTween()
    {
        if(m_progressTween != null && m_progressTween.IsPlaying())
        {
            m_progressTween.Kill();
        }
        m_progressTween = null;
    }

    protected override void OnDestroy()
    {
        KillProgressTween();
    }
}