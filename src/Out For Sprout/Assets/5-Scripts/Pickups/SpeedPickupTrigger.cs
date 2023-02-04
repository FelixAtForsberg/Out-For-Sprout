using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickupTrigger : PickupTrigger
{
    public float speedBonus;
    public float bonusTime;
    public override void TriggerPickup(Player player)
    {
        var playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement.AddSpeedupBuff(speedBonus, bonusTime);
        base.TriggerPickup(player);
    }
}