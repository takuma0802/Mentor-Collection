using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MentorParchaseView : MonoBehaviour
{

	[SerializeField] private GameObject _mentorCellPrefab;
	[SerializeField] private Transform _scrollContentTransform;

	public void SetCells()
	{
		var characters = MasterDataManager.instance.CharacterTable;
		foreach (var c in characters)
		{
			var obj = Instantiate(_mentorCellPrefab) as GameObject;
			var cell = obj.GetComponent<MentorParchaseCell>();
			obj.transform.SetParentWithReset(_scrollContentTransform);
			cell.SetValue(c);
		}
		Instantiate(_mentorCellPrefab);
	}
}
