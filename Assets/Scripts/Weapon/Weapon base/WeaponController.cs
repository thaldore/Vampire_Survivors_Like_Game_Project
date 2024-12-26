using UnityEngine;
using System.Collections.Generic;
using System.Collections;



public class WeaponController: MonoBehaviour
{

    [Header("Weapon Stats")]
    public WeaponScriptableObjects weaponData;
   
    float currentCooldown;
   

    protected PlayerMovement pm;

    protected virtual void Start()
    {
        pm = FindAnyObjectByType<PlayerMovement>();
        currentCooldown = weaponData.CooldownDuration;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown <= 0f)
        {
            Attack();
        }

    }

    protected virtual void Attack()
    {
        currentCooldown = weaponData.CooldownDuration;
    }
}
