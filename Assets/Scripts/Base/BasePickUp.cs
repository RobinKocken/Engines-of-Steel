using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePickUp : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public float distance;
    public LayerMask mask;

    private void Update()
    {
        if(Physics.BoxCast(transform.position, transform.localScale, transform.forward, out RaycastHit hit, transform.rotation, mask))
        {
            Debug.Log("Hello");
            if(hit.transform.gameObject.TryGetComponent<ItemPickUp>(out ItemPickUp itemPickUp))
            {
                inventoryManager.AddItem(itemPickUp.item, itemPickUp.currentAmount, -1);
                itemPickUp.DestroyItem();
            }
            
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, transform.forward * distance);
        Gizmos.DrawWireCube(transform.position + transform.forward * distance, transform.localScale);
    }
}
