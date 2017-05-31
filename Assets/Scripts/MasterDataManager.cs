using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterDataManager : MonoBehaviour
{
	[SerializeField]
	private List<MstCharacter> characterTable = new List<MstCharacter>();

	private void Start()
	{
		var characterCSV = Resources.Load("CSV/Character.csv") as TextAsset;
		var csv = CSVReader.SplitCsvGrid(characterCSV.text);
		for (int i=1; i<csv.GetLength(1)-1; i++) 
		{
			var data = new MstCharacter();
			data.SetFromCSV( GetRaw(csv, i) );
			characterTable.Add(data);
		}
	}

	private string[] GetRaw (string[,] csv, int row) {
		string[] data = new string[ csv.GetLength(0) ];
		for (int i=0; i<csv.GetLength(0); i++) {
			data[i] = csv[i, row];
		}
		return data;
	}
}
