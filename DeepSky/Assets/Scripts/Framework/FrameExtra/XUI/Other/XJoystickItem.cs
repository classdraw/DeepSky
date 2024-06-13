
using System;
using UnityEngine;
using UnityEngine.Events;

//GameObject

namespace XEngine.UI
{
    public class XJoystickItem : XBaseComponent
    {
        private MobileJoystick m_Joystick;

        private UnityAction<MobileJoystick, Vector2> m_OnMoveStartCallback;
        private UnityAction<MobileJoystick, Vector2> m_OnMoveCallback;
        private UnityAction<MobileJoystick> m_OnMoveEndCallback;

        private UnityAction<MobileJoystick, Vector2> m_OnTouchStartCallback;
        private UnityAction<MobileJoystick> m_OnTouchingCallback;
        private UnityAction<MobileJoystick> m_OnTouchUpCallback;

        private UnityAction<MobileJoystick> m_OnClickCallback;

        private UnityAction<MobileJoystick, bool> m_OnCancelingStateCallback;
        private UnityAction<MobileJoystick> m_OnTouchCancelCallback;

        public override void SetData(object _data)
        {
            //donothing
        }
        public override object GetData()
        {
            return null;
        }

        public MobileJoystick Joystick
        {
            get { return m_Joystick; }
        }

        private void Awake()
        {
            InitComponent();
        }

        override protected void OnInitComponent()
        {
            m_Joystick = GetComponent<MobileJoystick>();
            if (m_Joystick != null)
            {
                m_Joystick.onMoveStart.AddListener(OnMoveStart);
                m_Joystick.onMove.AddListener(OnMove);
                m_Joystick.onMoveEnd.AddListener(OnMoveEnd);

                m_Joystick.onTouchStart.AddListener(OnTouchStart);
                m_Joystick.onTouching.AddListener(OnTouching);
                m_Joystick.onTouchUp.AddListener(OnTouchUp);

                m_Joystick.onClick.AddListener(OnClick);

                m_Joystick.OnCancelingState.AddListener(OnCancelingState);
                m_Joystick.OnTouchCancel.AddListener(OnTouchCancel);
            }
        }

        public void SetOnMoveStartCallback(UnityAction<MobileJoystick, Vector2> call)
        {
            m_OnMoveStartCallback = call;
        }

        public void SetOnMoveCallback(UnityAction<MobileJoystick, Vector2> call)
        {
            m_OnMoveCallback = call;
        }

        public void SetOnMoveEndCallback(UnityAction<MobileJoystick> call)
        {
            m_OnMoveEndCallback = call;
        }

        public void SetOnTouchStartCallback(UnityAction<MobileJoystick, Vector2> call)
        {
            m_OnTouchStartCallback = call;
        }

        public void SetOnTouchingCallback(UnityAction<MobileJoystick> call)
        {
            m_OnTouchingCallback = call;
        }

        public void SetOnTouchUpCallback(UnityAction<MobileJoystick> call)
        {
            m_OnTouchUpCallback = call;
        }

        public void SetOnClickCallback(UnityAction<MobileJoystick> call)
        {
            m_OnClickCallback = call;
        }

        public void SetOnCancelingStateCallback(UnityAction<MobileJoystick, bool> call)
        {
            m_OnCancelingStateCallback = call;
        }

        public void SetOnTouchCancelCallback(UnityAction<MobileJoystick> call)
        {
            m_OnTouchCancelCallback = call;
        }

        private void OnMoveStart(MobileJoystick joystick, Vector2 axisValue)
        {
            if (m_OnMoveStartCallback != null)
            {
                m_OnMoveStartCallback(joystick, axisValue);
            }
        }

        private void OnMove(MobileJoystick joystick, Vector2 axisValue)
        {
            if (m_OnMoveCallback != null)
            {
                m_OnMoveCallback(joystick, axisValue);
            }
        }

        private void OnMoveEnd(MobileJoystick joystick)
        {
            if (m_OnMoveEndCallback != null)
            {
                m_OnMoveEndCallback(joystick);
            }
        }

        private void OnTouchStart(MobileJoystick joystick, Vector2 axisValue)
        {
            if (m_OnTouchStartCallback != null)
            {
                m_OnTouchStartCallback(joystick, axisValue);
            }
        }

        private void OnTouching(MobileJoystick joystick)
        {
            if (m_OnTouchingCallback != null)
            {
                m_OnTouchingCallback(joystick);
            }
        }

        private void OnTouchUp(MobileJoystick joystick)
        {
            if (m_OnTouchUpCallback != null)
            {
                m_OnTouchUpCallback(joystick);
            }
        }

        private void OnClick(MobileJoystick joystick)
        {
            if (m_OnClickCallback != null)
            {
                m_OnClickCallback(joystick);
            }
        }

        private void OnCancelingState(MobileJoystick joystick, bool isCanceling)
        {
            if (m_OnCancelingStateCallback != null)
            {
                m_OnCancelingStateCallback(joystick, isCanceling);
            }
        }

        private void OnTouchCancel(MobileJoystick joystick)
        {
            if (m_OnTouchCancelCallback != null)
            {
                m_OnTouchCancelCallback(joystick);
            }
        }

        protected override void OnDestroyComponent()
        {
            m_OnMoveStartCallback = null;
            m_OnMoveCallback = null;
            m_OnMoveEndCallback = null;
            m_OnTouchStartCallback = null;
            m_OnTouchingCallback = null;
            m_OnTouchUpCallback = null;
            m_OnClickCallback = null;
            m_OnCancelingStateCallback = null;
            m_OnTouchCancelCallback = null;
        }
    }

}

