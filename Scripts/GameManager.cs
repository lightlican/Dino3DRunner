using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Text scoreText;
    public Text gameOverText;
    public Button restartButton;
    public Button menuButton;
    public GameObject gameOverPanel;

    private int score = 0;
    public float worldSpeed = 5f;
    private bool isGameOver = false;

    void Awake()
    {
        Instance = this;
        HideGameOverUI();
    }

    void Update()
    {
        if (!isGameOver)
        {
            worldSpeed += Time.deltaTime * 0.1f;
            if (worldSpeed > 15f) worldSpeed = 15f;
        }

        if (isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    public float GetWorldSpeed()
    {
        return worldSpeed;
    }

    public float GetSpawnSpeed()
    {
        return Mathf.Clamp(3f - (worldSpeed - 5f) * 0.05f, 1.5f, 3f);
    }

    public void AddScore(int points)
    {
        if (!isGameOver)
        {
            score += points;
            scoreText.text = "Score: " + score;
        }
    }

    public void GameOver()
    {
        isGameOver = true;

        
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.StopMusic();
        }

        
        DinoFall();

        ShowGameOverUI();
    }

    void DinoFall()
    {
        
        GameObject dino = GameObject.FindGameObjectWithTag("Player");
        if (dino != null)
        {
            Rigidbody rb = dino.GetComponent<Rigidbody>();
            if (rb != null)
            {
                
                Collider collider = dino.GetComponent<Collider>();
                if (collider != null)
                {
                    collider.enabled = false;
                }

                
                rb.constraints = RigidbodyConstraints.None;

                
                rb.velocity = Vector3.zero; 
                rb.AddForce(Vector3.down * 50f, ForceMode.Impulse); 
                rb.AddTorque(Random.insideUnitSphere * 10f, ForceMode.Impulse); 

                
                DinoController dinoController = dino.GetComponent<DinoController>();
                if (dinoController != null)
                {
                    dinoController.enabled = false;
                }

                Debug.Log("Динозавр проваливается!");
            }
        }
    }

    void ShowGameOverUI()
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
    }

    void HideGameOverUI()
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }
}