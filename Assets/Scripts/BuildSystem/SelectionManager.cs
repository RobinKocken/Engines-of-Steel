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
        //toggle menu to show selected object info
        Outline outline = target.GetComponent<Outline>();
        if (outline == null)
        {
            target.AddComponent<Outline>();
            outline = target.GetComponent<Outline>();
            outline.enabled = true;
            objText.text = target.name;
            selectedObj = target;
            selectedObj.gameObject.GetComponent<BoxCollider>().isTrigger = true;
            selectedObj.gameObject.GetComponent<BoxCollider>().size = new Vector3(0.99f, 0.99f, 0.99f);
            selectUI.SetActive(true);
        }
        else
        {
            outline.enabled = true;
            objText.text = target.name;
            selectedObj = target;
            selectedObj.gameObject.GetComponent<BoxCollider>().isTrigger = true;
            selectedObj.gameObject.GetComponent<BoxCollider>().size = new Vector3(0.99f, 0.99f, 0.99f);
            selectUI.SetActive(true);
        }
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
            selectedObj.gameObject.GetComponent<BoxCollider>().isTrigger = false;
            selectedObj.gameObject.GetComponent<BoxCollider>().size = new Vector3(1, 1, 1);
            selectedObj.GetComponent<Outline>().enabled = false;
            selectedObj = null;
            selectUI.SetActive(false);
        }

    }
    public void Delete()
    {
        GameObject objToDestroy = selectedObj;
        Deselect();
        Destroy(objToDestroy);
    }
}
