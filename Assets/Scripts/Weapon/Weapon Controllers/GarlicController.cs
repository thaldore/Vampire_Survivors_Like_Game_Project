using UnityEngine;

public class GarlicController : WeaponController
{
    
    protected override void Start()
    {
        base.Start();   
    }

    
    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedGarlic = Instantiate(prefab);
        spawnedGarlic.transform.position = transform.position;
        spawnedGarlic.transform.parent = transform;
    }
}
