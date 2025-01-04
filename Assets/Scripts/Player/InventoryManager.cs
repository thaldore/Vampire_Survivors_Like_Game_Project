using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public List<WeaponController> weaponSlots = new List<WeaponController>(6);
    public int[] weaponLevels = new int[6];
    public List<Image> weaponUISlots = new List<Image>(6);
    public List<PassiveItem> passiveItemSlots = new List<PassiveItem>(6);
    public int[] passiveItemLevels = new int[6];
    public List<Image> passiveItemUISlots = new List<Image>(6);

    public void AddWeapon(int slotIndex,WeaponController weapon) //add weapon to specific slot
    {
        weaponSlots[slotIndex] = weapon;
        weaponLevels[slotIndex] = weapon.weaponData.Level;
        weaponUISlots[slotIndex].enabled = true;
        weaponUISlots[slotIndex].sprite =  weapon.weaponData.Icon;
    }

    public void AddPassiveItem(int slotIndex, PassiveItem passiveItem) // add a passive item to a specific slot
    {
        passiveItemSlots[slotIndex] = passiveItem;
        passiveItemLevels[slotIndex] = passiveItem.passiveItemData.Level;
        passiveItemUISlots[slotIndex].enabled = true; 
        passiveItemUISlots[slotIndex].sprite = passiveItem.passiveItemData.Icon;
    }

    public void LevelUpWeapon(int slotIndex)
    {
        Debug.Log("LevelUpWeapon called with slotIndex: " + slotIndex);

        if (weaponSlots.Count > slotIndex)
        {
            Debug.Log("weaponSlots.Count: " + weaponSlots.Count + ", slotIndex: " + slotIndex);

            WeaponController weapon = weaponSlots[slotIndex];
            Debug.Log("Selected weapon: " + (weapon != null ? weapon.name : "null"));

            if (!weapon.weaponData.NextLevelPrefab)
            {
                Debug.LogError("No next level prefab for weapon: " + weapon.name);
                return;
            }

            GameObject upgradeWeapon = Instantiate(weapon.weaponData.NextLevelPrefab, transform.position, Quaternion.identity);
            Debug.Log("Created upgraded weapon: " + upgradeWeapon.name);

            upgradeWeapon.transform.SetParent(transform); // set the weapon to be a child of the player
            AddWeapon(slotIndex, upgradeWeapon.GetComponent<WeaponController>());
            Debug.Log("Upgraded weapon added to slot: " + slotIndex);

            Debug.Log("Destroying old weapon: " + weapon.name);
            Destroy(weapon.gameObject);

            weaponLevels[slotIndex] = upgradeWeapon.GetComponent<WeaponController>().weaponData.Level; // to make sure we have the correct weapon level
            Debug.Log("Weapon level updated to: " + weaponLevels[slotIndex]);
        }
        else
        {
            Debug.LogError("Invalid slotIndex: " + slotIndex);
        }



    }

    public void LevelUpPassiveItem(int slotIndex)
    {
        if (passiveItemSlots.Count > slotIndex)
        {
            PassiveItem passiveItem = passiveItemSlots[slotIndex];

            if (!passiveItem.passiveItemData.NextLevelPrefab)   // check if there is a next level for the current passive item
            {
                Debug.LogError("no nex level for: " + passiveItem.name);
                return;
            }

            GameObject upgradedPassiveItem = Instantiate(passiveItem.passiveItemData.NextLevelPrefab, transform.position, Quaternion.identity);
            upgradedPassiveItem.transform.SetParent(transform); // set the weapon to be a child of the player
            AddPassiveItem(slotIndex, upgradedPassiveItem.GetComponent<PassiveItem>());
            Destroy(passiveItem.gameObject);
            passiveItemLevels[slotIndex] = upgradedPassiveItem.GetComponent<PassiveItem>().passiveItemData.Level; // to make sure we have the correct passive item  level
        }
    }

}
