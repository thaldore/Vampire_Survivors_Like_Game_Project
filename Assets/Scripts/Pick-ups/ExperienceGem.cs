using UnityEngine;

public class ExperienceGem : MonoBehaviour, ICollectible
{
    public int experienceGranted;

    public void Collect()
    {
        PlayerStats player = FindAnyObjectByType<PlayerStats>(); // buranin kodda bir sikinti var
        player.IncreaseExperience(experienceGranted);
        Destroy(gameObject);
    }


}
