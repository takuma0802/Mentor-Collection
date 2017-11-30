using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MentorTrainingView : MonoBehaviour
{

	[SerializeField] private GameObject _mentorTrainingCellPrefab;
	[SerializeField] private Transform _scrollContent;
	private List<MentorTrainingCell> _cells = new List<MentorTrainingCell>();

	private void Start ()
	{
		gameObject.SetActive(false);
	}

	// ゲーム開始時に保存してるデータが持ってるCharacterのCellを全部生成する用
	public void SetCells()
	{
		var characters = GameManager.instance.User.Characters;
		characters.ForEach(c =>
		{
			CreateCell(c);
		});
	}

	public void AddCharacter(Character data)
	{
		CreateCell(data);
	}

	private void CreateCell(Character data)
	{
		var obj = Instantiate(_mentorTrainingCellPrefab) as GameObject;
		obj.transform.SetParentWithReset(_scrollContent);
		var cell = obj.GetComponent<MentorTrainingCell>();
		cell.SetValue(data);
		_cells.Add(cell);
	}
}
