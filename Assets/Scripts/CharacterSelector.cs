using UnityEngine; 
using UnityEngine.TextCore.Text;

public class CharacterSelector : MonoBehaviour
{

    public static CharacterSelector instance;
    public CharacterScriptableObject characterData;


    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogWarning("EXTRA" + this + "DElETED");
            Destroy(gameObject);
        }
    }

    public static CharacterScriptableObject GetData()
    {
        return instance.characterData;
    }

    public void SelectCharacter(CharacterScriptableObject character)
    {
        characterData = character;
        Debug.LogWarning($"Selected Character: {characterData.name}, MaxHealth: {characterData.MaxHealth}," +
            $" MoveSpeed: {characterData.MoveSpeed}, Spawned Weapon: {characterData.StartingWeapon.name}");
    }

    public void DestroySingleton()
    {
        instance = null;
        Destroy(gameObject);
    }

}
