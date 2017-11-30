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
	
	private bool _loadFromLocal;
	
	// キャラクターをIDで引っ張れるようにしておく
	public MstCharacter GetCharacterById(int id)
	{
		return _characterTable.Find(c => c.ID == id);
	}

	// レベルアップに必要な金額を計算
	// 初期コストから1レベルごとに1.1倍したコストに
	public int GetConsumptionMoney(Character data)
	{
		return (int) (data.Master.InitialCost * Mathf.Pow(1.1f, data.Level - 1));
	}
	
	// マスターデータのURL
	const string CsvUrl = "https://docs.google.com/spreadsheets/d/e/2PACX-1vTUyv7l75icxCSOSPHzyoxHA-DQoVARNQ5RusMFLLJldCG4hJwkIgG44kYjhtKVmrT9XlQg-ScXv4F3/pub?output=csv";

	// GameManagerから呼んでもらう
	public void LoadData(UnityAction onFinish)
	{
		//ネットワークに接続不可な場合の処理
		if (Application.internetReachability != NetworkReachability.NotReachable)
		{
			ConnectionManager.instance.ConnectionAPI(
				CsvUrl,
				(string result) =>
				{
					var csv = CSVReader.SplitCsvGrid(result);
					for (int i = 1; i < csv.GetLength(1) - 1; i++)
					{
						var data = new MstCharacter();
						data.SetFromCsv(GetRaw(csv, i));
						_characterTable.Add(data);
					}
					onFinish();
				}
			);
		}
//		else
//		{
//			print("ネットワーク繋がってないなう");
//			var characterCSV = Resources.Load("CSV/Character.csv") as TextAsset;
//			print(characterCSV);
//			var csv = CSVReader.SplitCsvGrid(characterCSV.text);
//			print(csv[3, 3]);
//			for (int i = 1; i < csv.GetLength(1) - 1; i++)
//			{
//				var data = new MstCharacter();
//				data.SetFromCsv(GetRaw(csv, i));
//				_characterTable.Add(data);
//				print(_characterTable[i]);
//			}
//		}
	}

	private string[] GetRaw (string[,] csv, int row) {
		string[] data = new string[ csv.GetLength(0) ];
		for (int i=0; i<csv.GetLength(0); i++) {
			data[i] = csv[i, row];
		}
		return data;
	}
}
