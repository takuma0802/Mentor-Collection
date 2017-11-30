using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[System.SerializableAttribute]
public class Character
{

	[SerializeField] private int _uniqueId, _masterId, _level;
	
	// コンストラクタ
	public Character (int uniqueId,MstCharacter data)
	{
		_uniqueId = uniqueId;
		_level = 1;
		_masterId = data.ID;
	}
	
	public int UniqueId
	{
		get { return _uniqueId; }
	}
	
	public int MasterId
	{
		get { return _masterId; }
	}

	public int Level
	{
		get { return _level; }
	}

	public MstCharacter Master
	{
		get
		{
			return MasterDataManager.instance.GetCharacterById(_masterId);
		}
	}

	public int Power
	{
		get
		{
			int power = Master.LowerEnergy + ((_level - 1) * (Master.UpperEnergy - Master.LowerEnergy) / (Master.MaxLebel - 1));
			return power;
		}
	}

	public bool IsLevelMax
	{
		get
		{
			return (_level >= Master.MaxLebel) ? true : false;
		}
	}

	public void LevelUp()
	{
		_level += 1;
	}
}
