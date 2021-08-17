using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    #region IdropHandler implementation
    public void OnDrop(PointerEventData eventData)
    {
       
        eventData.pointerDrag.transform.SetParent(transform);
    }
    #endregion
}
