using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject enemyPrefab;
    public float minInstantiateValue;
    public float maxInstantiateValue;
    [SerializeField] private float enemyDestroyTime = 3f;

    [Header("Score System")]
    [SerializeField] private Text ScoreText;    // Reference to a UI Text for displaying the score
    private int score = 0;           // Score variable to track player's score

    [Header("Particle Effects")]
    public GameObject explosion;
    public GameObject muzzlee;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UpdateScore(0);  // Initialize the score display
        InvokeRepeating("InstantiateEnemy", 1f, 1f);
    }

    private void InstantiateEnemy()
    {
        Vector3 enemyPos = new Vector3(Random.Range(minInstantiateValue, maxInstantiateValue), 6f);
        GameObject enemy = Instantiate(enemyPrefab, enemyPos, Quaternion.Euler(0f, 0f, 180f));
        Destroy(enemy, enemyDestroyTime);
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScore(score);
    }

    private void UpdateScore(int newScore)
    {
        if (ScoreText != null)
        {
            ScoreText.text = "Score: " + newScore;
            Debug.Log("Score updated to: " + newScore);
        }
    }
}
