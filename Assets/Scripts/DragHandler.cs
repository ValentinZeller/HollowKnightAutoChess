using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    PlayerManager player;
    private SynergyManager synergyManager;
    public static GameObject unit; //Unité à déplacer
    Vector3 startPosition; //Position de départ
    Transform startParent; // Parent de départ de l'unité à déplacer
    private Vector3 screenPoint; //Position du pointeur à l'écran
    private Camera mainCamera;
    private Unit unitData;

    private void Start()
    {
        player = GameObject.Find("GameManager").GetComponent<PlayerManager>();
        synergyManager = GameObject.Find("GameManager").GetComponent<SynergyManager>();
        mainCamera = Camera.main;
        unitData = GetComponent<Unit>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        unit = gameObject;
        startPosition = transform.position;
        startParent = transform.parent;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        transform.SetParent(transform.root);
        if (startParent.parent.name == "Board")
        {
            player.countUnit--;
            foreach (var synergy in unitData.synergyList)
            {
                synergyManager.UpdateSynergyCount(synergy.name,name,-1);
            }
        }
    }
    public void OnDrag(PointerEventData eventData)
    { 
        screenPoint = Input.mousePosition;
        screenPoint.z = 1f;
        transform.position = mainCamera.ScreenToWorldPoint(screenPoint);
    }
    public void OnEndDrag(PointerEventData eventData)
    { 
        unit = null;
        if (transform.parent == startParent || transform.parent == transform.root || transform.parent.childCount > 1 )
        {
            transform.position = startPosition;
            transform.SetParent(startParent);
        }
        else
        {
            if (transform.parent.parent.name == "Board")
            {
                if (player.countUnit >= player.GetLevel())
                {
                    transform.position = startPosition;
                    transform.SetParent(startParent);
                }
                else
                {
                    player.countUnit++;
                    foreach (var synergy in unitData.synergyList)
                    {
                        synergyManager.UpdateSynergyCount(synergy.name,name,1);
                    }
                }
            }
        }
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
