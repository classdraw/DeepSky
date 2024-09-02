using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XEngine.Define;

/// <summary>
/// Auto Generate Class!!!
/// </summary>
[System.Serializable]
public class EDItem_Hello : Excel_ItemData
{
	/// <summary>
	/// 名字
	/// </summary>
	public string name;

	/// <summary>
	/// 攻击
	/// </summary>
	public int attack;

	/// <summary>
	/// 生命值
	/// </summary>
	public int health;

	/// <summary>
	/// 速度
	/// </summary>
	public float speed;

	/// <summary>
	/// 为Boss?
	/// </summary>
	public bool isBoss;

	/// <summary>
	/// 属性值
	/// </summary>
	public float[] paramVals;


}

/// <summary>
/// Auto Generate Class!!!
/// </summary>
public class EDTable_Hello : Excel_TableData<EDItem_Hello>
{

}