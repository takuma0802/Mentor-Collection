using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DiscriptionPopupController : BasePopupController
{

	[SerializeField] private Text
		_nameLabel, _featureLabel, _rarityLabel, _levelLabel, _productivityLabel;

	[SerializeField] private Button _closeButton;

	private UnityAction _onCloseFinish;
	
	// Use this for initialization
	void Start () {
		_closeButton.onClick.AddListener(() =>
		{
			if (_onCloseFinish != null) Close(_onCloseFinish);
			else Close(null);
		});
	}

	public void SetValue(Character data, UnityAction OnCloseFinish = null)
	{
		var master = data.Master;
		_nameLabel.text = master.Name;
		_featureLabel.text = master.FeatureText;
		_productivityLabel.text = string.Format("生産性：¥{0:#,0}/tap", data.Power);
		_levelLabel.text = "";
		for (var i = 0; i < master.Rarity; i++) { _rarityLabel.text += "★";}
		if (OnCloseFinish != null) _onCloseFinish = OnCloseFinish;
	}
}
