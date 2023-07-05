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
    public Transform buildingParent;

    [SerializeField] private LayerMask layerMask;
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

    public void SelectObject(int index)
    {
        pendingObj = Instantiate(objects[index], pos, transform.rotation, buildingParent);
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
            }
        }
    }
    void PlaceObject()
    {
        buildChanges.Add(pendingObj.gameObject);
        pendingObj = null;
        yPos = 0;
    }

    private void FixedUpdate()
    {
        if (gameManager.playerState == GameManager.PlayerState.build)
        {
            GameObject test = gameManager.inventoryManager.GetSelectedGameObject();

            if (test != null)
            {
                test.TryGetComponent(out CheckPlacement component);
                if (Input.GetKeyDown(KeyCode.E) && pendingObj == null)
                {
                    if (component != null)
                    {
                        Debug.Log(component);
                        SelectObject(component.buildingID);

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
        Transform buildParent = gameManager.buildManager.transform;
        Transform baseParent = gameManager.baseController.gameObject.GetComponentInChildren<FarmManager>().transform;

        

        for(int bIndex = 0; bIndex < buildChanges.Count; bIndex++)
        {
            Debug.Log(buildChanges[bIndex].gameObject);
            int _buildingID = buildParent.GetChild(bIndex).GetComponent<CheckPlacement>().buildingID;
            var newBuilding = Instantiate(buildChanges[bIndex], baseParent.transform.position, Quaternion.identity, baseParent);
            newBuilding.transform.localPosition = buildChanges[bIndex].transform.localPosition;
            newBuilding.transform.eulerAngles = buildChanges[bIndex].transform.eulerAngles + gameManager.baseController.transform.eulerAngles;
        }

        buildChanges.Clear();

        //for (int bIndex = 0; bIndex < buildingParent.childCount; bIndex++)
        //{
        //    int _buildingID = buildParent.GetChild(bIndex).GetComponent<CheckPlacement>().buildingID;
        //    Vector3 _buildingPostistion = buildParent.GetChild(bIndex).transform.localPosition;
        //    Vector3 _buildingRotation = buildParent.GetChild(bIndex).transform.eulerAngles;

        //    var newBuilding = Instantiate(this.objects[_buildingID], this.transform.position, Quaternion.identity, baseParent);
        //    newBuilding.transform.localPosition = _buildingPostistion;
        //    newBuilding.transform.eulerAngles = _buildingRotation;
        //}
    }
}
