using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCharacter : MonoBehaviour
{
    [SerializeField]
    private CharacterController m_vPlayer;
    [SerializeField]
    private float m_fVelocity=2f;//速度
    [SerializeField]
    private float m_fRoVelocity=10f;//旋转速度


    private float m_fMousePosition;
    private float m_fVertical;
    private float m_fHorizontal;

    private Vector3 m_vMoveDir;

    private void Awake() {
        

    }

    private void Update(){
        float h=Input.GetAxisRaw("Horizontal");
        float v=Input.GetAxisRaw("Vertical");
        float mousePos=Input.GetAxis("Mouse X");
        transform.localRotation*=Quaternion.Euler(0,mousePos*m_fRoVelocity,0f);
        if(h!=0||v!=0){
            if(m_vPlayer!=null){
                m_vPlayer.SimpleMove(transform.forward*v*m_fVelocity);
                m_vPlayer.SimpleMove(transform.right*h*m_fVelocity);
            }

        }
    }


}
