using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Bson.Serialization.Attributes;
using System;
using MongoDB.Driver;

public class DatabaseManager : MonoBehaviour
{
    private MongoClient m_kMongoClient;
    private IMongoDatabase m_kMongoDatabase;
    private IMongoCollection<DB_PlayerData> m_kUserInfo;
    public void Init(){
        string connectStr="mongodb://localhost:27017";
        m_kMongoClient=new MongoClient(connectStr);
        //没有自动创建
        m_kMongoDatabase=m_kMongoClient.GetDatabase("MMONetCode");
        //查找库建立集合
        m_kUserInfo=m_kMongoDatabase.GetCollection<DB_PlayerData>("UserInfo");
    }
    public void UnInit(){
        m_kMongoClient=null;
        m_kMongoDatabase=null;
    }

    public DB_PlayerData GetPlayerData(int userId){
        var t=m_kUserInfo.Find(Builders<DB_PlayerData>.Filter.Eq("m_iUseId",userId)).FirstOrDefault();
        return t;
    }

    public void SavePlayerData(DB_PlayerData playerData){
        m_kUserInfo.ReplaceOne(Builders<DB_PlayerData>.Filter.Eq("m_iUseId",playerData.m_iUseId),playerData);
    }
}
