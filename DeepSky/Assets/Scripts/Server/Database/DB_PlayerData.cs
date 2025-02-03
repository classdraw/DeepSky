using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Bson.Serialization.Attributes;
using System;
using MongoDB.Driver;

public class DB_PlayerData 
{
    [BsonId]
    public int m_iUseId;
    public string m_sName;
    public string m_kPwd;
    [BsonDateTimeOptions(Kind =System.DateTimeKind.Local)]
    public DateTime m_iCreateTime;
}
