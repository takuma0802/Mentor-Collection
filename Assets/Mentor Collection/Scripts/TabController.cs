using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
	[SerializeField] private Button _recluitButton, _trainingButton, _closeButton;
	
	void Start ()
	{
		_recluitButton.onClick.AddListener(() =>
		{
			PortrateUIManager.instance.ChangeView(Const.View.Purchase);
		});

		_trainingButton.onClick.AddListener(() =>
		{
			PortrateUIManager.instance.ChangeView(Const.View.Training);
		});

		_closeButton.onClick.AddListener(() =>
		{
			PortrateUIManager.instance.ChangeView(Const.View.Close);
		});
	}
}
