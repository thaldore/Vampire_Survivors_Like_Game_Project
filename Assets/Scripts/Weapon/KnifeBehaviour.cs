using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class KnifeBehaviour : ProjectileWeaponBehaviour
{
    KnifeController kc;

    [System.Obsolete]
    protected override void Start()
    {
        base.Start();
        kc = FindAnyObjectByType<KnifeController>();
    }

 
    void Update()
    {
        transform.position += direction * kc.speed * Time.deltaTime;
    }
}
