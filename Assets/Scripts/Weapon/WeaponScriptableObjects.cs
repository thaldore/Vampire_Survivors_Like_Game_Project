using UnityEngine;


[CreateAssetMenu(fileName = "weaponScriptableObject", menuName = "ScriptableObject/Weapon")]

public class WeaponScriptableObjects : ScriptableObject
{
    [SerializeField]
    GameObject prefab;
    public GameObject Prefab { get => prefab; private set => prefab = value; }

    // base stats for weapons
    [SerializeField]
    float damage;
    public float Damage { get => damage; private set => damage = value; }

    [SerializeField]
    float speed;
    public float Speed { get => speed; private set => speed = value; }

    [SerializeField]
    float cooldownDuration;
    public float CooldownDuration { get => cooldownDuration; private set => cooldownDuration = value; }

    [SerializeField]
    float currentCooldown;
    public float CurrentCooldown { get => currentCooldown; private set => currentCooldown = value; }

    [SerializeField]
    int pierce;
    public int Pierce { get => pierce; private set => pierce = value; }
}
