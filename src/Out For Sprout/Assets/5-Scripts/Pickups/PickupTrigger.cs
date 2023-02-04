using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickupTrigger : MonoBehaviour
{
    public bool destroyOnTrigger;
    public virtual void TriggerPickup(Player player)
    {
        if (destroyOnTrigger)
        {
            Destroy(gameObject);
        }
    }
}
