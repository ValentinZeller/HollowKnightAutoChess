using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Sell : MonoBehaviour, IDropHandler
{
    #region IdropHandler implementation
    public PlayerManager player;

    public void OnDrop(PointerEventData eventData)
    {
        player.UpdateMoney(eventData.pointerDrag.gameObject.GetComponent<Unit>().GetTier()
            /* * Mathf.RoundToInt(Mathf.Pow(3,eventData.pointerDrag.gameObject.GetComponent<Unit>().GetLevel() - 1))*/);
        Destroy(eventData.pointerDrag.gameObject);
    }
    #endregion
}
