using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CommonPopupController : BasePopupController
{
    [SerializeField] private Text _contentLabel;
    [SerializeField] private Button _closebutton;

    private UnityAction _onCloseFinish;

    private void Start()
    {
        _closebutton.onClick.AddListener(() =>
        {
            if (_onCloseFinish != null) Close(_onCloseFinish);
            else Close(null);
        });
    }

    public void SetValue(string text, UnityAction OnCloseFinish = null)
    {
        _contentLabel.text = text;
        if (OnCloseFinish != null) _onCloseFinish = OnCloseFinish;
    }
}
