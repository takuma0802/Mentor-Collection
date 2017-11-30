using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceManager : SingletonMonoBehaviour<DeviceManager>
{

	[SerializeField]
	private GameObject _mainCamera, _diveCamera, _gvrViewer;

	private AvatarController _avatarController;

	public void SetUp()
	{
		Screen.autorotateToPortrait = false;
		Screen.autorotateToLandscapeLeft = false;
		Screen.autorotateToLandscapeRight = false;
		Screen.autorotateToPortraitUpsideDown = false;
		ToPortrate();
	}

	public void ToVR(AvatarController avatar = null)
	{
		StartCoroutine(ToVRCoroutine(avatar));
	}

	private IEnumerator ToVRCoroutine(AvatarController avatar)
	{
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		_avatarController = avatar;
		_mainCamera.SetActive(false);
		_mainCamera.GetComponent<Camera>().enabled = false;
		_diveCamera.SetActive(true);
		if (avatar != null) _diveCamera.transform.SetParentWithReset(avatar.VRView());
		yield return null;
		_gvrViewer.SetActive(true);
	}

	public void ToPortrate()
	{
		if (_avatarController != null) _avatarController.InactiveVR();
		_mainCamera.SetActive(true);
		_mainCamera.GetComponent<Camera>().enabled = true;
		_diveCamera.SetActive(false);
		_gvrViewer.SetActive(false);
		Screen.orientation = ScreenOrientation.Portrait;
	}

	private void EnableGvrView()
	{
		_gvrViewer.SetActive(true);
	}
	
	private void UnEnableGvrView()
	{
		_gvrViewer.SetActive(false);
	}
}
