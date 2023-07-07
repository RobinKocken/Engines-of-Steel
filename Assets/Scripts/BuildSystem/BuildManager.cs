using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{

    public GameManager gameManager;
    public Transform buildParent;
    public Transform baseParent;

    public LayerMask layerMask;
    [Header("Objects")]
    public GameObject[] objects;
    public GameObject pendingObj;
    public float rotateAmount;
    public bool canPlace;

    [Header("Grid Stuff")]
    public float offset;
    public float gridSize;
    public int gridHeight;
    public int yPos;
    private bool gridOn = true;
    private Vector3 pos;

    private RaycastHit hit;
    private List<GameObject> buildChanges = new List<GameObject>();
    [SerializeField] private Material[] materials;
    [SerializeField] private Material oldMaterial;

    public void SelectObject(int index)
    {
        pendingObj = Instantiate(objects[index], pos, transform.rotation, buildParent);
        pendingObj.GetComponent<CheckPlacement>().buildingID = index;
        Debug.Log("building object");
    }

    void RotateObject()
    {
        pendingObj.transform.Rotate(Vector3.up, rotateAmount);
    }

    public void Update()
    {
        if (gameManager.playerState == GameManager.PlayerState.build)
        {
            if (pendingObj != null)
            {
                pendingObj.transform.localPosition = new Vector3(
                RoundToNearestGrid(pos.x),
                yPos + offset,
                RoundToNearestGrid(pos.z));

                //Debug.Log(pos);

                if (Input.GetKeyDown(KeyCode.Q))
                {
                    yPos += gridHeight;
                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    RotateObject();
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    yPos -= gridHeight;
                }

                if (Input.GetMouseButton(0) && canPlace)
                {
                    PlaceObject();
                }
                UpdateMaterials();
            }
        }
    }
    void PlaceObject()
    {
        gameManager.inventoryManager.RemoveItemFromHotbar(1);
        pendingObj.GetComponent<MeshRenderer>().material = oldMaterial;
        oldMaterial = null;
        buildChanges.Add(pendingObj.gameObject);
        pendingObj = null;
        yPos = 0;
    }

    private void FixedUpdate()
    {
        if (gameManager.playerState == GameManager.PlayerState.build)
        {
            GameObject curSlot = gameManager.inventoryManager.GetSelectedGameObject();

            if (curSlot != null)
            {
                curSlot.TryGetComponent(out CheckPlacement component);
                if (Input.GetKeyDown(KeyCode.E) && pendingObj == null)
                {
                    if (component != null)
                    {
                        Debug.Log(component);
                        SelectObject(component.buildingID);

                    }
                    if(component == null)
                    {

                    }
                }

            }
            //update objects position to screenpoint posistion
            if (Physics.Raycast(gameManager.playerCamera.transform.position, gameManager.playerCamera.transform.forward, out hit, 1000, layerMask))
            {
                //Debug.Log(layerMask);
                //Debug.Log(hit.transform.gameObject.name);
                pos = hit.point;
                pos.y += offset;
                pos -= transform.position;
            }
        }
    }

    //snap cords of object to nearest int
    float RoundToNearestGrid(float pos)
    {
        //Changeable grid system
        float xDiff = pos % gridSize;
        pos -= xDiff;
        if (xDiff > (gridSize / 2))
        {
            pos += gridSize;
        }
        //pos = Mathf.RoundToInt(pos);
        return pos;
    }

    public void TransferBuildings()
    {
        buildParent = gameManager.buildManager.transform;
        baseParent = gameManager.baseController.gameObject.GetComponentInChildren<FarmManager>().transform;

        

        for(int bIndex = 0; bIndex < buildChanges.Count; bIndex++)
        {
            Debug.Log(buildChanges[bIndex].gameObject);
            int _buildingID = buildParent.GetChild(bIndex).GetComponent<CheckPlacement>().buildingID;
            var newBuilding = Instantiate(buildChanges[bIndex], baseParent.transform.position, Quaternion.identity, baseParent);
            newBuilding.transform.localPosition = buildChanges[bIndex].transform.localPosition;
            newBuilding.transform.localEulerAngles = buildChanges[bIndex].transform.localEulerAngles;
        }

        buildChanges.Clear();
    }

    void UpdateMaterials()
    {
        if(pendingObj != null)
        {
            if (oldMaterial == null)
            {
                oldMaterial = pendingObj.GetComponent<MeshRenderer>().material;
            }

            if (canPlace)
            {
                pendingObj.GetComponent<MeshRenderer>().material = materials[0];
            }
            else
            {
                pendingObj.GetComponent<MeshRenderer>().material = materials[1];
            }
        }
        
    }
}
