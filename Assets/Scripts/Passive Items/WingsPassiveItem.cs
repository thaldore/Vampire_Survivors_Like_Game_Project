using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class WingsPassiveItem : PassiveItem
{
    protected override void ApplyModifier()
    {
        player.CurrentMoveSpeed *= 1 + passiveItemData.Multipler / 100f;

    }
}
