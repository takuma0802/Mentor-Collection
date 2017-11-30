using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRUIManager : MonoBehaviour
{
	[SerializeField] private Button _toPortrateButton;
	
	// Use this for initialization
	void Start () {
		_toPortrateButton.onClick.AddListener(() =>
		{
			DeviceManager.instance.ToPortrate();
		});
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
