using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PopupManager : SingletonMonoBehaviour<PopupManager>
{
    [SerializeField] private GameObject _commonPopup, _discriptionPopup;
    private List<IPopupController> _popupList = new List<IPopupController>();
    
    public bool IsOpened
    {
        get { return _popupList.Count > 0 ? true : false; }
    }

    public void RemoveLastPopup()
    {
        if(_popupList.Count > 0) _popupList.RemoveAt(_popupList.Count -1);
    }

    public void OpenCommon(string message, UnityAction onCloseFinish = null)
    {
        var popup = CreatePopup(_commonPopup);
        var popupController = popup.GetComponent<CommonPopupController>();
        popupController.SetValue(message, onCloseFinish);
        _popupList.Add(popupController);
        popupController.Open(null);
    }

    public void OpenDiscription(Character data, UnityAction onCloseFinish = null)
    {
        var popup = CreatePopup(_discriptionPopup);
        var popupController = popup.GetComponent<DiscriptionPopupController>();
        popupController.SetValue(data, onCloseFinish);
        _popupList.Add(popupController);
        popupController.Open(null);
    }

    public GameObject CreatePopup(GameObject prefab)
    {
        GameObject popup = Instantiate(prefab);
        popup.transform.SetParentWithReset(this.transform);
        return popup;
    }

}
