using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IPopupController 
{
    Transform origin { get; }
    void Open(UnityAction onOpenFinish);
    void Close(UnityAction onCloseFinish);
}
