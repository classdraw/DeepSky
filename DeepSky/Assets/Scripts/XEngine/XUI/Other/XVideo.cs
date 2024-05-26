using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Video;
using XEngine;
using XEngine.UI;
using XEngine.Loader;
using XEngine.Pool;

public class XVideo : XBaseComponent
{
    [SerializeField]
    private VideoPlayer m_VideoPlayer;
    [SerializeField]
    private int m_Width = 256;
    [SerializeField]
    private int m_Height = 256;

    private RawImage m_RawImage;
    private RenderTexture m_RenderTex;

    private UnityAction m_OnPrepareCompletedCallback;
    private UnityAction m_OnStartedCallback;
    private UnityAction m_OnLoopPointReachedCallback;

    private void Awake()
    {
        base.InitComponent();
    }

    protected override void OnInitComponent()
    {
        m_RawImage = GetComponent<RawImage>();

        if (m_VideoPlayer != null)
        {
            //
            m_RenderTex = RenderTexture.GetTemporary(m_Width, m_Height);
            RawImage ri = gameObject.GetComponent<RawImage>();
            ri.texture = m_RenderTex;
            //
            m_VideoPlayer.targetTexture = m_RenderTex;
            m_VideoPlayer.prepareCompleted += OnPrepareCompleted;
            m_VideoPlayer.started += OnStarted;
            m_VideoPlayer.loopPointReached += OnLoopPointReached;
        }
    }
    private ResHandle m_kResHandle;
    public override void SetData(object _data)
    {
        if (_data != null)
        {
            string vedioPath = (string)_data;
            m_kResHandle=GameResourceManager.GetInstance().LoadResourceSync(vedioPath);
            m_VideoPlayer.clip = m_kResHandle.GetObjT<VideoClip>();
            //m_VideoPlayer.Play();
        }
        else
        {
            if(m_kResHandle!=null){
                m_kResHandle.Dispose();
                m_kResHandle=null;
            }
            m_VideoPlayer.clip = null;
            m_VideoPlayer.Stop();
        }
    }

    public void SetOnPrepareCompletedCallback(UnityAction call)
    {
        m_OnPrepareCompletedCallback = call;
    }

    public void SetOnStartedCallback(UnityAction call)
    {
        m_OnStartedCallback = call;
    }

    public void SetOnLoopPointReachedCallback(UnityAction call)
    {
        m_OnLoopPointReachedCallback = call;
    }

    private void OnPrepareCompleted(VideoPlayer source)
    {
        if (m_OnPrepareCompletedCallback != null)
        {
            m_OnPrepareCompletedCallback();
        }
    }

    private void OnStarted(VideoPlayer source)
    {
        if (m_OnStartedCallback != null)
        {
            m_OnStartedCallback();
        }
    }

    private void OnLoopPointReached(VideoPlayer source)
    {
        if (m_OnLoopPointReachedCallback != null)
        {
            m_OnLoopPointReachedCallback();
        }
    }

    protected override void OnDestroyComponent()
    {
        if (m_RenderTex != null)
        {
            RenderTexture.ReleaseTemporary(m_RenderTex);
            m_RenderTex = null;
        }
        m_OnPrepareCompletedCallback = null;
        m_OnStartedCallback = null;
        m_OnLoopPointReachedCallback = null;
    }

    [ContextMenu("PlayVideo")]
    private void PlayVideo()
    {
        m_RenderTex = RenderTexture.GetTemporary(256, 256);
        RawImage ri = gameObject.GetComponent<RawImage>();
        ri.texture = m_RenderTex;
        //
        m_VideoPlayer.targetTexture = m_RenderTex;
        m_VideoPlayer.isLooping = true;
        m_VideoPlayer.Play();
    }

}
