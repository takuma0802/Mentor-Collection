using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarManager : SingletonMonoBehaviour<AvatarManager>
{
	[SerializeField] private GameObject _avatarPrefab;
	private List<AvatarController> _avatars = new List<AvatarController>();
	private GameObject[] _points;
	
	[SerializeField] private Transform _spawnPoint, _startPosint;
	public Transform SpawnPoint { get { return _spawnPoint; }}
	public Transform StartPoint { get { return _startPosint; }}

	public Transform GetTarget()
	{
		int target = Random.Range(0, _points.Length - 1);
		return _points[target].transform;
	}

	// Use this for initialization
	void Start ()
	{
		_points = GameObject.FindGameObjectsWithTag("WalkPoint");
	}

	public AvatarController GetAvatar(int uniqueId)
	{
		return _avatars.Find(a => a.Character.UniqueId == uniqueId);
	}
	
	public void SetUp()
	{
		StartCoroutine(SetupCoroutine());
	}

	private IEnumerator SetupCoroutine()
	{
		var characters = GameManager.instance.User.Characters;
		foreach (var c in characters)
		{
			SpawnAvatar(c);
			yield return new WaitForSeconds(5.0f);
		}
	}

	public void SpawnAvatar(Character data)
	{
		var obj = Instantiate(_avatarPrefab, _spawnPoint.position, _spawnPoint.rotation) as GameObject;
		var avatar = obj.GetComponent<AvatarController>();
		avatar.SetValue(data);
		_avatars.Add(avatar);
	}
}
