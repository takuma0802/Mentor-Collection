using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MentorParchaseView : MonoBehaviour
{

	[SerializeField] private GameObject _mentorPurchaseCellPrefab;
	[SerializeField] private Transform _scrollContentTransform;

	public void SetCells()
	{
		var characters = MasterDataManager.instance.CharacterTable;
		foreach (var c in characters)
		{
			var obj = Instantiate(_mentorPurchaseCellPrefab) as GameObject;
			obj.transform.SetParentWithReset(_scrollContentTransform);
			var cell = obj.GetComponent<MentorParchaseCell>();
			cell.SetValue(c);
		}
		Instantiate(_mentorPurchaseCellPrefab);
	}
}
