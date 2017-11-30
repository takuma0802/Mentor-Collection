using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BasePopupController : MonoBehaviour, IPopupController
{
    private Transform m_Origin;
    public Transform origin {get { return m_Origin; }}
    
    protected virtual void OnOpenBeforeActive() { }
    protected virtual void OnOpenAfterActive() { }
    protected virtual void OnCloseBeforeActive() { }
    protected virtual void OnCloseAfterActive() { }

    private Animator _animator;
    private UnityAction m_OnOpenFinish;
    private UnityAction m_OnCloseFinish;

    private void SetUp()
    {
        m_Origin = transform.Find("Popup");
        _animator = GetComponent<Animator>();
        transform.localScale = Vector3.one;
    }

    public void Open(UnityAction onOpenFinish = null)
    {
        SetUp();
        m_OnOpenFinish = onOpenFinish;
        this.OnOpenBeforeActive();
    }
    
    public void Close(UnityAction onCloseFinish = null)
    {
        m_OnCloseFinish = onCloseFinish;
        this.OnCloseBeforeActive();
        _animator.SetTrigger("Close");
    }

    private void OnOpenFinish()
    {
        if (m_OnOpenFinish != null)
        {
            m_OnOpenFinish();
        }
        this.OnOpenAfterActive();
    }
    
    private void OnCloseFinish()
    {
        PopupManager.instance.RemoveLastPopup();
        if (m_OnCloseFinish != null)
        {
            m_OnCloseFinish();
        }
        this.OnCloseAfterActive();
        Destroy(gameObject);
    }

}
