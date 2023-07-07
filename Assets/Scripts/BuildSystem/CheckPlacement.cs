using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlacement : MonoBehaviour
{
    [SerializeField] private BuildManager buildManager;
    public int buildingID;
    // Start is called before the first frame update
    void Start()
    {
        if(buildManager == null)
        {
            buildManager = GameObject.Find("Buildings").GetComponent<BuildManager>();
        }
        
        buildManager.canPlace = true;
    }
    //If the Object colides with another object, make it unable to be placed
    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log(collision.gameObject.name);
    //    if (collision.gameObject.TryGetComponent(out CheckPlacement check))
    //    {
    //        if (check != null)
    //        {
    //            if (check.buildingID != 4)
    //            {
    //                buildManager.canPlace = false;
    //            }
    //        }
    //    }
    //}
    //private void OnCollisionExit(Collision collision)
    //{
    //    Debug.Log(collision.gameObject.name);
    //    if (collision.gameObject.TryGetComponent(out CheckPlacement check))
    //    {
    //        if (check == null)
    //        {
    //            buildManager.canPlace = true;
    //        }
    //    }
    //}

    private void Update()
    {
        buildManager.canPlace = true;
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out CheckPlacement check))
        {
            if (check != null)
            {
                if (check.buildingID != 4)
                {
                    buildManager.canPlace = false;
                }
            }
        }
    }
}
