using System.Collections.Generic;
using System.Collections;
using UnityEngine;


[CreateAssetMenu(fileName = "PassiveItemsSciptableObject",menuName = "ScriptableObject/Passive Item")]
public class PassiveItemsSciptableObject : ScriptableObject 
{
    [SerializeField]
    float multipler;
    public float Multipler { get => multipler; private set => multipler = value; }


    [SerializeField]
    int level; // not meant to be modified in the game [only in editor]
    public int Level { get => level; private set => level = value; }

    [SerializeField]
    GameObject nextLevelPrefab; // the prefab of the next level ie what the object becomes when it levels up
                                // not to be confused with the prefab to be spawned at the next level
    public GameObject NextLevelPrefab { get => nextLevelPrefab; private set => nextLevelPrefab = value; }

    [SerializeField]
    Sprite icon;        // not meant to be modified in game [only in editor]
    public Sprite Icon { get => icon; private set => icon = value; }
}
