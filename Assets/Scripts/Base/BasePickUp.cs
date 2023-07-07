using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePickUp : MonoBehaviour
{
    public InventoryManager inventoryManager;

    public float sphereRadius;
    public float rayDistance;
    public LayerMask mask;

    private void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, sphereRadius, mask);

        foreach(var hitCollider in hitColliders)
        {
            if(hitCollider.transform.gameObject.TryGetComponent<ItemPickUp>(out ItemPickUp itemPickUp))
            {
                inventoryManager.AddItem(itemPickUp.item, itemPickUp.currentAmount, -1);
                itemPickUp.DestroyItem();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawRay(transform.position, -transform.up * rayDistance);
        Gizmos.DrawWireSphere(transform.position + -transform.up * rayDistance, sphereRadius);
    }
}
