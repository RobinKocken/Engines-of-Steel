using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject selectedObj;
    public TextMeshProUGUI objText;
    public GameObject selectUI;

    private void Start()
    {
        gameManager = gameObject.GetComponent<BuildManager>().gameManager;
    }
    private void Update()
    {
        //check if the player is in buildmode and if they press the left mouse button
        if (Input.GetMouseButtonDown(0) && gameManager.playerState == GameManager.PlayerState.build)
        {
            if (gameManager.buildManager.pendingObj == null)
            {
                LayerMask layerMask = gameManager.buildManager.layerMask;
                RaycastHit hit;
                if (Physics.Raycast(gameManager.playerCamera.transform.position, gameManager.playerCamera.transform.forward, out hit, 1000, layerMask))
                {
                    //check if the object hit has a CheckPlacement component
                    hit.transform.gameObject.TryGetComponent(out CheckPlacement component);
                    if (component)
                    {
                        //select object
                        Select(hit.collider.gameObject);
                    }

                }
            }

        }
        //if an object is selected and right mouse button is pressed deselect the selected object
        if (Input.GetMouseButtonDown(1) && selectedObj != null && gameManager.playerState == GameManager.PlayerState.build)
        {
            Deselect();
        }
    }
    //Selecting the Object for further input
    void Select(GameObject target)
    {
        if (target == selectedObj)
        {
            return;
        }
        if (selectedObj != null)
        {
            Deselect();
        }
        target.AddComponent<Outline>();

        selectedObj = target;
        gameManager.buildManager.pendingObj = selectedObj;
    }

    public void Move()
    {
        gameObject.GetComponent<BuildManager>().pendingObj = selectedObj;
    }
    //Deselecting the selected object
    void Deselect()
    {
        if (selectedObj != null)
        {
            selectedObj = null;
            gameManager.buildManager.pendingObj = null;
        }
    }
    public void Delete()
    {
        GameObject objToDestroy = selectedObj;
        Deselect();
        Destroy(objToDestroy);
    }
}
