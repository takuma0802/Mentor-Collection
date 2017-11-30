using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class MentorParchaseCell : MonoBehaviour
{

	[SerializeField] private Image _faceImage;

	[SerializeField] private Text
		_nameLabel,
		_rarityLabel,
		_featureLabel,
		_productivityLabel,
		_recruitCostLabel;
	
	[SerializeField] private Button _purchaseButton;
	[SerializeField] private CanvasGroup _buttonGroup;
	private bool _isSold = false;
	private MstCharacter _characterData;
	
	public void SetValue(MstCharacter data)
	{
		_faceImage.sprite = Resources.Load<Sprite>("Face/" + data.ImageId);
		_characterData = data;
		_nameLabel.text = data.Name;
		_rarityLabel.text = "";
		for (int i = 0; i < data.Rarity; i++) { _rarityLabel.text += "★"; }
		_featureLabel.text = data.FeatureText;
		_productivityLabel.text = "生産性(lv.1)：" + data.LowerEnergy;
		_recruitCostLabel.text = "¥" + data.InitialCost;

		var user = GameManager.instance.User;
		var ch = user.Characters.Find(c => c.MasterId == data.ID);
		_isSold = (ch != null);
		if (_isSold) SoldView();
		if (!_characterData.PurchaseAvailable(user.Money.Value)) _buttonGroup.alpha = 0.5f;
		
		_purchaseButton.onClick.AddListener(() =>
		{
			if (_isSold) return;
			if (!_characterData.PurchaseAvailable(user.Money.Value)) return;
			_isSold = true;
			SoldView();
			var chara = user.NewCharacter(_characterData);
			PortrateUIManager.instance.MentorTrainingView.AddCharacter(chara);
			AvatarManager.instance.SpawnAvatar(chara);
		});

		user.Money.Subscribe(value =>
		{
			if (_isSold) return;
			_buttonGroup.alpha = value < data.InitialCost ? 0.5f : 1.0f;
		});
	}

	private void SoldView()
	{
		_buttonGroup.alpha = 0.5f;
		_recruitCostLabel.text = "sold out";
	}
}
