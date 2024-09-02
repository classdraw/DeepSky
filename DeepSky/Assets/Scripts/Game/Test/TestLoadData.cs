using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLoadData : MonoBehaviour
{
    [SerializeField]
    private TextAsset m_kTextAsset;
    void Start()
    {
        var asset = LitJson.JsonMapper.ToObject<EDTable_Hello>(m_kTextAsset.text);
        Debug.LogError(asset.GetDataById(1).name);
    }


}
