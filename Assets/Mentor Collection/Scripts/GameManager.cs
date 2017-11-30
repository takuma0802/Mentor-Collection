using UnityEngine;
using System.Collections;
using UniRx;

public class GameManager
	: SingletonMonoBehaviour<GameManager>
{
	[SerializeField] private  User _userData = new User();

	public User User
	{
		get { return _userData; }
	}

	private const string SaveKey = "SaveData";

	private void Start()
	{
		if (PlayerPrefs.HasKey(SaveKey))
		{
			_userData = JsonUtility.FromJson<User>(PlayerPrefs.GetString(SaveKey));
		}
		
		MasterDataManager.instance.LoadData(() => 
			{
				PortrateUIManager.instance.SetUp();
				AvatarManager.instance.SetUp();
			});

		_userData.Money.Subscribe(_ => { Save(); });
	}

	public void Save()
	{
		PlayerPrefs.SetString(SaveKey,JsonUtility.ToJson(_userData));
	}
	
	public static void Log (object log)
	{
		if (Debug.isDebugBuild)
		{
			Debug.Log(log);
		}
	}
}