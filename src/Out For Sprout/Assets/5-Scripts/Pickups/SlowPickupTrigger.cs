using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowPickupTrigger : PickupTrigger
{
    public float speedSlowdown;
    public float slowTime;
    public override void TriggerPickup(Player player)
    {
        base.TriggerPickup(player);
        var playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement.AddSpeedupDebuff(speedSlowdown, slowTime);
    }
}
