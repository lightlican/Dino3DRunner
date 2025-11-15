using UnityEngine;

public class ScoreDetector : MonoBehaviour
{
    void Update()
    {
        GameObject[] cacti = GameObject.FindGameObjectsWithTag("Obstacle");

        foreach (GameObject cactus in cacti)
        {
            if (cactus.transform.position.z < transform.position.z)
            {
                if (!cactus.name.Contains("SCORED"))
                {
                    cactus.name += "SCORED";

                    if (ScoreManager.Instance != null)
                    {
                        ScoreManager.Instance.AddScore(1);
                        
                        AudioManager.Instance.PlayScoreSound();
                    }
                }
            }
        }
    }
}