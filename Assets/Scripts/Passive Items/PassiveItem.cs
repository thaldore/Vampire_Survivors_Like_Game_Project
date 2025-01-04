using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PassiveItem : MonoBehaviour
{
    protected PlayerStats player;
    public PassiveItemsSciptableObject passiveItemData;

    protected virtual void ApplyModifier()
    {

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindAnyObjectByType<PlayerStats>();
        ApplyModifier();
    }


}
