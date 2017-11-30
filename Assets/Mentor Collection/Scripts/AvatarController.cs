using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AvatarController : MonoBehaviour
{
	[SerializeField] private SpriteRenderer _face;
	[SerializeField] private Transform _diveCameraPoint, _mainCameraPoint;
	public Transform MainCameraPoint{ get { return _mainCameraPoint; }}

	private Character _characterData;
	public Character Character { get { return _characterData; }}

	private NavMeshAgent _agent;
	private Transform _target;

	public void SetValue(Character data)
	{
		_characterData = data;
		_face.sprite = Resources.Load<Sprite>("Face/" + data.Master.ImageId);
	}

	public Transform VRView()
	{
		_face.gameObject.SetActive(false);
		return _diveCameraPoint;
	}

	public void InactiveVR()
	{
		_face.gameObject.SetActive(true);
	}
	
	// Use this for initialization
	void Start ()
	{
		_agent = GetComponent<NavMeshAgent>();
		_target = AvatarManager.instance.StartPoint;
	}
	
	// Update is called once per frame
	void Update () {
		if(_target == null) return;
		_agent.SetDestination(_target.position);

		float distance = Vector3.Distance(transform.position, _target.position);
		if (distance < 0.2f)
		{
			_target = AvatarManager.instance.GetTarget();
		}
	}
}
