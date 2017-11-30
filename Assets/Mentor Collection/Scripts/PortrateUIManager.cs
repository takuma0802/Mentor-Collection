using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UniRx;

public class PortrateUIManager : SingletonMonoBehaviour<PortrateUIManager>
{

	[SerializeField] private MentorParchaseView _mentorParchaseView;
	[SerializeField] private MentorTrainingView _mentorTrainingView;

	public MentorParchaseView MentorParchaseView
	{
		get { return _mentorParchaseView; }
	}

	public MentorTrainingView MentorTrainingView
	{
		get { return _mentorTrainingView; }
	}

	[SerializeField] private Button _workButton, _dataCleaButton;
	[SerializeField] private Text _moneyLabel, _autoWorkLabel, _employeesCountLabel, _productivityLabel;
	[SerializeField] private Const.View _currentView = Const.View.Close, _lastView = Const.View.Purchase;

	private static User User {
		get { return GameManager.instance.User; }
	}

	public void SetUp()
	{
		_mentorParchaseView.SetCells();
		_mentorTrainingView.SetCells();
		
		_openButton.onClick.AddListener(() =>
		{
			_openButton.gameObject.SetActive(false);
			ChangeView(_lastView);
		});
		
		UpdateView();
		
		_workButton.onClick.AddListener(() =>
		{
			var power = User.Characters.Sum(c => c.Power);
			if (power == 0) power = 1;
			User.AddMoney(power);
			UpdateView();
		});
		
		_dataCleaButton.onClick.AddListener(() =>
		{
			PlayerPrefs.DeleteAll();
			SceneManager.LoadScene("Main");
		});

		User.Money.Subscribe(_ => { UpdateView(); });
		
		float time = 0.0f;
		Observable.Interval(TimeSpan.FromSeconds(0.5f)).Subscribe(_ =>
		{
			time += 3.1f;
			var color = _autoWorkLabel.color;
			color.a = Mathf.Sin(time) * 0.5f + 0.5f;
			_autoWorkLabel.color = color;
		}).AddTo(this);
	}

	public void UpdateView()
	{
		_moneyLabel.text = String.Format("¥{0:#,0}", User.Money);
		_autoWorkLabel.text = "+" + User.ProductivityPerTap;
		_employeesCountLabel.text = string.Format("{0:#,0}人", User.Characters.Count);
		_productivityLabel.text = "¥" + User.ProductivityPerTap;
	}

	public void ChangeView(Const.View nextView)
	{
		if (_currentView == nextView) return;
		_lastView = _currentView;
		_currentView = nextView;
		_isMoving = true;
		switch (nextView)
		{
				case Const.View.Purchase:
					_mentorParchaseView.gameObject.SetActive(true);
					_mentorTrainingView.gameObject.SetActive(false);
					break;
				case Const.View.Training:
					_mentorParchaseView.gameObject.SetActive(false);
					_mentorTrainingView.gameObject.SetActive(true);
					break;
				case Const.View.Close:
					_openButton.gameObject.SetActive(true);
					break;
		}

	}

	
	[SerializeField] private Button _openButton;
	[SerializeField] private Transform _mainPanel, _openPoint, _closePoint;
	private bool _isMoving = false;
	private float _easing = 15f, _maxSpeed = 55f, _stopDistance = 0.001f;

	private void Update()
	{
		if(!_isMoving)return;
		var target = (_currentView == Const.View.Close) ? _closePoint : _openPoint;
		
		//position
		Vector3 v = Vector3.Lerp(_mainPanel.position, target.position, Time.deltaTime * _easing) - _mainPanel.position;
		if (v.magnitude > _maxSpeed) v = v.normalized * _maxSpeed;
		_mainPanel.position += v;
		
		//rotation
		_mainPanel.rotation = Quaternion.Lerp(transform.rotation, target.rotation, Time.deltaTime * _easing);
		
		if (_isMoving)
		{
			float distance = Vector3.Distance(_mainPanel.position, target.position);
			if (_stopDistance > distance) _isMoving = false;
		}
	}
}