using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

public class MasterDataManager :
SingletonMonoBehaviour<MasterDataManager>
{
	[SerializeField]
	private List<MstCharacter> _characterTable = new List<MstCharacter>();
	public List<MstCharacter> CharacterTable { get { return _characterTable; } }
	
	// キャラクターをIDで引っ張れるようにしておく
	public MstCharacter GetCharacterById(int id)
	{
		//print(id);
		//var aaaa = _characterTable.Find(c => c.ID == id);
		//print(aaaa);
		return _characterTable.Find(c => c.ID == id);
	}

	public int GetConsumptionMoney(Character data)
	{
		return (int) (data.Master.InitialCost * Mathf.Pow(1.1f, data.Level - 1));
	}
	
	// PublishしてゲットしたURL
	const string CsvUrl = "https://docs.google.com/spreadsheets/d/1gdQktWC2A8pLGFsz1aJDC_0YVHmbJ8o2b6HSgTVaSAQ/pub?output=csv";

	// GameManagerから呼んでもらう
	public void LoadData(UnityAction onFinish)
	{
		ConnectionManager.instance.ConnectionAPI(
			CsvUrl, 
			(string result) => {
				var csv = CsvReader.SplitCsvGrid(result);
				for (int i=1; i<csv.GetLength(1)-1; i++) 
				{
					var data = new MstCharacter();
					data.SetFromCsv(GetRaw(csv, i));
					_characterTable.Add(data);
				}
				onFinish();
			}
		);
	}

	private string[] GetRaw (string[,] csv, int row) {
		string[] data = new string[ csv.GetLength(0) ];
		for (int i=0; i<csv.GetLength(0); i++) {
			data[i] = csv[i, row];
		}
		return data;
	}
}
