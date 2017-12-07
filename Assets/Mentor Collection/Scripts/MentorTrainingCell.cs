using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class MentorTrainingCell : MonoBehaviour
{

	[SerializeField] private Text _nameLabel, _levelLabel, _rarityLabel, _productivityLabel, _levelUpCostLabel;
	[SerializeField] private Image _faceImage;
	[SerializeField] private Button _levelUpButton, _descriptionButton, _vrViewButton;
	[SerializeField] private CanvasGroup _levelUpButtonGroup;
	private Character _characterData;

	private User User
	{
		get { return GameManager.instance.User; }
	}

	private AvatarController Avatar
	{
		get { return AvatarManager.instance.GetAvatar(_characterData.UniqueId); }
	}

	public void SetValue(Character data)
	{
		var master = data.Master;
		_characterData = data;
		_faceImage.sprite = Resources.Load<Sprite>("Face/" + master.ImageId);
		_nameLabel.text = master.Name;
		_rarityLabel.text = "";
		for (int i = 0; i <= master.Rarity; i++) _rarityLabel.text += "*";
		UpdateValue();
		
		_levelUpButton.onClick.AddListener(() =>
		{
			var cost = CulcLevelUpCost();
			if (User.Money.Value < cost) return;
			if (_characterData.IsLevelMax) return;
			_characterData.LevelUp();
			User.ConsumptionLevelUpCost(cost);
			UpdateValue();
		});
		
		_descriptionButton.onClick.AddListener(() =>
		{
			if (Avatar == null) return;
			if(PopupManager.instance.IsOpened) return;
			MainCameraController.instance.ToZoomIn(Avatar, () =>
			{
				PopupManager.instance.OpenDiscription(_characterData, () =>
				{
					MainCameraController.instance.ToZoomOut();
				});
			});
		});
		
//		_vrViewButton.onClick.AddListener(() =>
//		{
//			if(Avatar == null) return;
//			DeviceManager.instance.ToVR(Avatar);
//		});

		if (User.Money.Value < CulcLevelUpCost()) _levelUpButtonGroup.alpha = 0.5f;
		User.Money.Subscribe(value =>
		{
			if (_characterData.IsLevelMax) return;
			UpdateValue();
		});
	}

	private int CulcLevelUpCost()
	{
		return MasterDataManager.instance.GetConsumptionMoney(_characterData);
	}
	
	private void UpdateValue()
	{
		var master = _characterData.Master;
		_levelLabel.text = "Lv." + _characterData.Level;
		_productivityLabel.text = string.Format("生産性：¥{0:#,0}/tap", _characterData.Power);
		var cost = CulcLevelUpCost();
		_levelUpCostLabel.text = string.Format("¥{0:#,0}", cost);

		if (User.Money.Value < cost) _levelUpButtonGroup.alpha = 0.5f;
		else _levelUpButtonGroup.alpha = 1.0f;
		
		if (_characterData.IsLevelMax)
		{
			_levelUpButtonGroup.alpha = 0.5f;
			_levelUpCostLabel.text = "Level Max";
		}

	}
}
