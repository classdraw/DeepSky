#if UNITY_SERVER || UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Fsm;
using XEngine.Utilities;
using UpdateCommon.Utilities;
using Common.Utilities;
using Common.Define;

namespace UpdateCommon.Role{
    public class PlayerMoveState : BasePlayerState
    {
        public PlayerMoveState(BaseFsm fsm) : base(fsm)
        {
        }
        public override void Enter(params object[] objs)
        {
            // XLogger.LogError("PlayerMoveState Enter");
            this.GetOwner().PlayAnimation("Move");
            //注册
            // this.GetOwner().SelfAnimeEventComponent.SetRootMotionAction(OnPlayerMotionCallback);
        }

        public override void Exit(){
            // this.GetOwner().SelfAnimeEventComponent.ClearRootMotionAction();
        }

        public override void Tick(){
            if(GetOwner()==null){
                return;
            }
            var moveDir=GetOwner().m_kInputData.m_vMoveDir;
            if(Tools.IsNearVector2(moveDir,Vector2.zero)){
                GetOwner().ChangeState(Player_State_Enum.Idle);
            }else{
                float dtTime=Time.deltaTime;
                var oldPos=GetOwner().transform.position;
                var dir=dtTime*GetOwner().MoveSpeed*moveDir;
                var deltaPos=new Vector3(dir.x,0f,dir.y);

                //旋转
                var newPos=oldPos+deltaPos;
                var viewTran=GetOwner().SelfAnimeEventComponent.transform;

                var lookPos=newPos-viewTran.position;
                lookPos.y=0f;
                var needRotate=Quaternion.LookRotation(lookPos);
                // viewTran.rotation=Quaternion.RotateTowards(viewTran.rotation,Quaternion.LookRotation(lookRotPos),Time.deltaTime*GetOwner().RotateSpeed);
                viewTran.rotation=Quaternion.Lerp(viewTran.rotation,needRotate,dtTime*GetOwner().RotateSpeed);
                

                deltaPos.y-=9.8f*dtTime;//可以走配表 模拟重力
                // GetOwner().transform.position=newPos;
                GetOwner().SelfCharacterController.Move(deltaPos);

                if(!Tools.IsNearVector3(dir,Vector2.zero)){
                    GetOwner().UpdateAOICoord();
                }
            }
            
            //状态类走别的逻辑
            // var dir=0.02f*MoveSpeed*(inputDir.normalized);
            // transform.position=transform.position+dir;
            // var newIntPos=AOIUtilities.ConvertWorldPositionToCoord(transform.position);
            // //aoi相关
            // if(newIntPos!=m_kCurrentAOICoord){
            //     AOIUtilities.UpdatePlayerCoord(this,m_kCurrentAOICoord,newIntPos);
            //     m_kCurrentAOICoord=newIntPos;
            // }
        }

        //动画位移逻辑 目前不用
        private void OnPlayerMotionCallback(Vector3 deltaPos,Quaternion deltaRotate){

            this.GetOwner().SelfAnimator.speed=this.GetOwner().MoveSpeed;
            deltaPos.y-=9.8f*Time.deltaTime;//可以走陪表 模拟重力
            this.GetOwner().SelfCharacterController.Move(deltaPos);

            if(!Tools.IsNearVector3(deltaPos,Vector3.zero)){
                GetOwner().UpdateAOICoord();
            }


        }
        public override void Release(){

        }

        public override void Reset(){

        }

        public override bool CanChangeNext(int fsmEnum,params object[]objs){
            return true;
        }
    }
}

#endif