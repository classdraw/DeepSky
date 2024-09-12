using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UpdateInfo{
    public enum Terrain_State_Enum{
        Request,
        Enable,
        Disable
    }
    public class TerrainCtrl{
        public Terrain m_kTerrain;
        public Terrain_State_Enum m_eState;
        public float m_fDestroyTime;
        public void Enable(){

        }
        public void Disable(){

        }

        public void Destroy(){
            
        }
    }

}
