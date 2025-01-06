using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public CharacterScriptableObject characterData;

    //current stats
<<<<<<< Updated upstream
<<<<<<< Updated upstream
    float currentHealth;
    float currentRecovery;
    float currentMoveSpeed;
    float currentMight;
    float currentProjectileSpeed;
=======
float currentHealth;
float currentRecovery;
float currentMoveSpeed;
float currentMight;
float currentProjectileSpeed;
float currentMagnet;

    #region Current Stat Properties
    public float CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            // Check if the value has changed
            if (currentHealth != value)
            {
                currentHealth = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.CurrentHealthDisplay.text = "Health: " + currentHealth;
                }
            }
        }
    }

    public float CurrentRecovery
    {
        get { return currentRecovery; }
        set
        {
            // Check if the value has changed
            if (currentRecovery != value)
            {
                currentRecovery = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.CurrentRecoveryDisplay.text = "Recovery: " + currentRecovery;
                }
            }
        }
    }

    public float CurrentMoveSpeed
    {
        get { return currentMoveSpeed; }
        set
        {
            // Check if the value has changed
            if (currentMoveSpeed != value)
            {
                currentMoveSpeed = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.CurrentMoveSpeedDisplay.text = "Move Speed: " + currentMoveSpeed;
                }
            }
        }
    }

    public float CurrentMight
    {
        get { return currentMight; }
        set
        {
            // Check if the value has changed
            if (currentMight != value)
            {
                currentMight = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.CurrentMightDisplay.text = "Might: " + currentMight;
                }
            }
        }
    }
    
=======
    
    public float currentHealth;
    
    float currentRecovery;
    float currentMoveSpeed;
    float currentMight;
    float currentProjectileSpeed;
    float currentMagnet;


    #region Current Stats Properties

    public float CurrentHealth
    {
        get { return currentHealth; }
        set 
        {
            // check if the value changed
            if (currentHealth != value)
            {
                currentHealth = value;
                
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentHealthDisplay.text = "Health : " + currentHealth;
                }
                //add any additional logic here  that needs  to be  executed when the value changes
            }
        }
    }

    public float CurrentRecovery
    {
        get { return currentRecovery; }
        set
        {
            // check if the value changed
            if (currentRecovery != value)
            {
                currentRecovery = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentRecoveryDisplay.text = "Recovery : " + currentRecovery;
                }
                //add any additional logic here  that needs  to be  executed when the value changes
            }
        }
    }

    public float CurrentMoveSpeed
    {
        get { return currentMoveSpeed; }
        set
        {
            // check if the value changed
            if (currentMoveSpeed != value)
            {
                currentMoveSpeed = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentMoveSpeedDisplay.text = "Move Speed : " + currentMoveSpeed;
                }
                //add any additional logic here  that needs  to be  executed when the value changes
            }
        }
    }

    public float CurrentMight
    {
        get { return currentMight; }
        set
        {
            // check if the value changed
            if (currentMight != value)
            {
                currentMight = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentMightDisplay.text = "Might : " + currentMight;
                }
                //add any additional logic here  that needs  to be  executed when the value changes
            }
        }
    }


>>>>>>> Stashed changes
    public float CurrentProjectileSpeed
    {
        get { return currentProjectileSpeed; }
        set
        {
<<<<<<< Updated upstream
            // Check if the value has changed
=======
            // check if the value changed
>>>>>>> Stashed changes
            if (currentProjectileSpeed != value)
            {
                currentProjectileSpeed = value;
                if (GameManager.instance != null)
                {
<<<<<<< Updated upstream
                    GameManager.instance.CurrentProjectileSpeedDisplay.text = "Projectile Speed: " + currentProjectileSpeed;
                }
            }
        }
    }
=======
                    GameManager.instance.currentProjectileSpeedDisplay.text = "Projectile Speed : " + currentProjectileSpeed;
                }
                //add any additional logic here  that needs  to be  executed when the value changes
            }
        }
    }

    public float CurrentMagnet
    {
        get { return currentMagnet; }
        set
        {
            // check if the value changed
            if (currentMagnet != value)
            {
                currentMagnet = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentMagnetDisplay.text = "Magnet : " + currentMagnet;
                }
                //add any additional logic here  that needs  to be  executed when the value changes
            }
        }
    }
    #endregion


>>>>>>> Stashed changes

    public float CurrentMagnet
    {
        get { return currentMagnet; }
        set
        {
            // Check if the value has changed
            if (currentMagnet != value)
            {
                currentMagnet = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.CurrentMagnetDisplay.text = "Magnet: " + currentMagnet;
                }
            }
        }
    }
    #endregion



>>>>>>> Stashed changes

    //Experience and level of the player
    [Header("Experience/Level")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap;

    //Class for defining a level range and the corresponding experience cap increase for that range
    [System.Serializable]
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int experienceCapIncrease;
    }

    // I-Frames
    [Header("I-Frames")]
    public float invicibilityDuration;
    float invicibilityTimer;
    bool isInvicibile;



    public List<LevelRange> levelRanges;

    void Awake()
    {
        //assign the variables
<<<<<<< Updated upstream
<<<<<<< Updated upstream
        currentHealth = characterData.MaxHealth;
        currentRecovery = characterData.Recovery;
        currentMoveSpeed = characterData.MoveSpeed;
        currentMight = characterData.Might;
        currentProjectileSpeed = characterData.ProjectileSpeed;
=======
=======
>>>>>>> Stashed changes
        CurrentHealth = characterData.MaxHealth;
        CurrentRecovery = characterData.Recovery;
        CurrentMoveSpeed = characterData.MoveSpeed;
        CurrentMight = characterData.Might;
        CurrentProjectileSpeed = characterData.ProjectileSpeed;
        CurrentMagnet = characterData.Magnet;

        SpawnWeapon(characterData.StartingWeapon);

        //silincek sonradan
        SpawnWeapon(secondWeaponTest);
        SpawnPassiveItem(firstPassiveItemTest);
        SpawnPassiveItem(secondPassiveItemTest);
>>>>>>> Stashed changes
    }

    void Start()
    {
        // Initialize the experience cap as the first experience cap increase
        experienceCap = levelRanges[0].experienceCapIncrease;

        //set the  current stats display
        GameManager.instance.currentHealthDisplay.text = "Magnet : " + currentMagnet;
        GameManager.instance.currentRecoveryDisplay.text = "Recovery : " + currentRecovery;
        GameManager.instance.currentMoveSpeedDisplay.text = "Move Speed : " + currentMoveSpeed;
        GameManager.instance.currentMightDisplay.text = "Might : " + currentMight;
        GameManager.instance.currentProjectileSpeedDisplay.text = "Projectile Speed : " + currentProjectileSpeed;
        GameManager.instance.currentMagnetDisplay.text = "Magnet : " + currentMagnet;


        GameManager.instance.AssignChosenCharacterUI(characterData);
    }

    private void Update()
    {
        if (invicibilityTimer > 0 ) 
        {
            invicibilityTimer -= Time.deltaTime;
        }
        // if the invicibility timer has reached 0, set the invicibility flag to false;
        else if (isInvicibile)
        {
            isInvicibile = false;
        }

    }
    public void IncreaseExperience(int amount)
    {
        experience += amount;

        LevelUpChecker();
    }

    void LevelUpChecker()
    {
        if (experience >= experienceCap)
        {
            level++;
            experience -= experienceCap;

            int experienceCapIncrease = 0;
            foreach (LevelRange range in levelRanges)
            {
                if (level >= range.startLevel && level <= range.endLevel)
                {
                    experienceCapIncrease = range.experienceCapIncrease;
                    break;
                }
            }
            experienceCap += experienceCapIncrease;
        }
    }


    public void TakeDamage(float dmg)
    {
        // if the player is not invicible, reduce health and start inviciblility

        if (!isInvicibile) 
        {
            CurrentHealth -= dmg;

            invicibilityTimer = invicibilityDuration;
            isInvicibile = true;

            if (CurrentHealth <= 0)
            {
                Kill();
            }
        }

        
    }

    public void Kill()
    {
        if (!GameManager.instance.isGameOver)
        {

            GameManager.instance.AssignLevelReachedUI(level);
            GameManager.instance.AssignWeaponsAndPassiveItemsUI(inventory.weaponUISlots , inventory.passiveItemUISlots);
            GameManager.instance.GameOver();
        }
    }


    public void RestoreHealth(float amount)
    {

        // only heal the player if their current health is less than their maximum health
        if (CurrentHealth < characterData.MaxHealth)
        {
            CurrentHealth += amount;

            if (CurrentHealth > characterData.MaxHealth)
            {
                CurrentHealth = characterData.MaxHealth;
            }
        }
    }
<<<<<<< Updated upstream
=======

    void Recover()
    {
        if(CurrentHealth < characterData.MaxHealth)
        {
            CurrentHealth += CurrentRecovery * Time.deltaTime;    

            //Make sure the player's health doesn't exceed their max health
            if (CurrentHealth > characterData.MaxHealth)
            {
                CurrentHealth = characterData.MaxHealth;
            }
        }
    }

    public void SpawnWeapon(GameObject weapon) 
    {
        // checking if the slots are full, and returning if it is
        if(weaponIndex >= inventory.weaponSlots.Count - 1) // must be -1 because a list starst from 0
        {
            Debug.LogError("weapon inventory slots already full");
            return;
        }

        //Spawn the starting weapon
        GameObject spawnedWeapon = Instantiate(weapon,transform.position,Quaternion.identity);
        spawnedWeapon.transform.SetParent(transform); //set the weapon to be a child the player
        inventory.AddWeapon(weaponIndex, spawnedWeapon.GetComponent<WeaponController>()); // add weapon to its inventory slot

        weaponIndex++;
    }

    public void SpawnPassiveItem(GameObject passiveItem)
    {
        // checking if the slots are full, and returning if it is
        if (passiveItemIndex >= inventory.passiveItemSlots.Count - 1) // must be -1 because a list starst from 0
        {
            Debug.LogError("passive item inventory slots already full");
            return;
        }

        //Spawn the starting passive item
        GameObject spawnedPassiveItem = Instantiate(passiveItem, transform.position, Quaternion.identity);
        spawnedPassiveItem.transform.SetParent(transform); //set the weapon to be a child the player
        inventory.AddPassiveItem(passiveItemIndex, spawnedPassiveItem.GetComponent<PassiveItem>()); // add weapon to its inventory slot

        passiveItemIndex++;
    }
>>>>>>> Stashed changes
}
