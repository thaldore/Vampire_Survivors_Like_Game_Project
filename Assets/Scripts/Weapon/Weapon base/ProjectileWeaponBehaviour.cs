using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.IO;

public class ProjectileWeaponBehaviour : MonoBehaviour
{
    public WeaponScriptableObjects weaponData;

    protected Vector3 direction;
    public float destroyAfterSeconds;


    // current stats 
    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCooldownDuration;
    protected int currentPierce;

     void Awake()
    {
        currentDamage = weaponData.Damage;
        currentSpeed = weaponData.Speed;
        currentCooldownDuration = weaponData.CooldownDuration;
        currentPierce = weaponData.Pierce;
    }

    public float GetCurrentDamage()
    {
        return currentDamage *= FindAnyObjectByType<PlayerStats>().currentMight;
    }


    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }


    public void DirectionChecker(Vector3 dir)
    {
        direction = dir;

        float dirx = direction.x;
        float diry = direction.y;

        Vector3 scale = transform.localScale;
        Vector3 rotation = transform.rotation.eulerAngles;

        if (dirx < 0 && diry == 0) //left
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
        } 
        else if (dirx == 0 && diry < 0) // down
        {
            scale.y = scale.y * -1;
        }
        else if(dirx == 0 && diry > 0) // up 
        {
            scale.x = scale.x * -1;
        }
        else if (dir.x > 0 && dir.y > 0) // right up
        {
            rotation.z = 0f;    
        }
        else if (dir.x > 0 && dir.y < 0 ) // right down
        {
            rotation.z = -90f;

        }
        else if (dir.x < 0 && dir.y > 0) // left up
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
            rotation.z = -90f; 
        }
        else if (dir.x < 0 && dir.y <0) // left 
        {
            scale.x = scale.x * -1;
            scale.y= scale.y * -1;
            rotation.z = 0f;
        }



        transform.localScale = scale;   
        transform.rotation = Quaternion.Euler(rotation);    

    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        // refference the script from the collider and deal damage using takedamage() 

        if (col.CompareTag("Enemy"))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(GetCurrentDamage()); // make sure to use current damage instead of weaponDatadamage in case any damage multipliers in the future
            ReducePierce();
        }

        else if (col.CompareTag("Prop"))
        {
            if (col.gameObject.TryGetComponent(out BreakableProps breakable))
            {
                breakable.TakeDamage(GetCurrentDamage());
                ReducePierce();
            }
        }
    }

    void ReducePierce() // destroy once the pierce reaches 0
    {
        currentPierce--;
        if (currentPierce <= 0 )
        {
            Destroy(gameObject);
        }
    }
}
