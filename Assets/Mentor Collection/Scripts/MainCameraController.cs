using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainCameraController 
	: SingletonMonoBehaviour<MainCameraController> 
{

	[SerializeField] private Transform _mainCamera;

	private float 
		easing = 6f,
		maxSpeed = 2f,
		stopDistance = 0.2f;

	private UnityAction _onZoomInFinish;

	private AvatarController _targetAvatar;
	private Transform _target;
	public void ToZoomIn (AvatarController nextTarget, UnityAction OnZoomInFinish) {
		_targetAvatar = nextTarget;
		_target = _targetAvatar.MainCameraPoint;
		_onZoomInFinish = OnZoomInFinish;
	}

	public void ToZoomOut () {
		_onZoomInFinish = null;
		_target = this.transform;
	}

	private void Start ()
	{
		_target = this.transform;
	}

	private void Update ()
	{
		// position
		Vector3 v = Vector3.Lerp(
			            _mainCamera.position, 
			            _target.position, 
			            Time.deltaTime * easing) - _mainCamera.position;
		if (v.magnitude > maxSpeed) v = v.normalized * maxSpeed;
		_mainCamera.position += v;

		// rotation
		_mainCamera.rotation = Quaternion.Lerp(
			_mainCamera.rotation,
			_target.rotation,
			Time.deltaTime * easing);

		float distance = Vector3.Distance(_mainCamera.position, _target.position);
		if (stopDistance > distance) 
		{
			if (_onZoomInFinish != null) 
			{
				_onZoomInFinish();
				_onZoomInFinish = null;
			}
		}
	}

}
