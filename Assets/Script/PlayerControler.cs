using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;

    [Header("Missile")]
    public GameObject missile;
    public Transform missileSpawnPosition;
    public float destroyTime;
    public Transform muzzleSpawnPos;

    private void Update()
    {
        PlayerMovement();
        PlayerShoot();
        CheckPauseInput();
    }

    // Handles player movement based on input
    void PlayerMovement()
    {
        float xPos = Input.GetAxis("Horizontal");
        float yPos = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(xPos, yPos, 0) * speed * Time.deltaTime;
        transform.Translate(movement);
    }

    // Checks for shoot input and triggers missile and muzzle effects
    void PlayerShoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnMissile();
            SpawnMuzzleEffect();
        }
    }

    // Spawns missile and detaches it from the player
    void SpawnMissile()
    {
        GameObject gm = Instantiate(missile, missileSpawnPosition);
        gm.transform.SetParent(null);
        Destroy(gm, destroyTime);
    }

    // Spawns muzzle effect at specified position and detaches it
    void SpawnMuzzleEffect()
    {
        GameObject muzzle = Instantiate(GameManager.instance.muzzlee, muzzleSpawnPos);
        muzzle.transform.SetParent(null);
        Destroy(muzzle, destroyTime);
    }

    // Checks for the Escape key input to pause the game
    void CheckPauseInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0; // Pause the game
            SceneManager.LoadScene(3, LoadSceneMode.Additive); // Load the pause menu scene in additive mode
        }
    }

    // Handles collision with enemies and triggers explosion effect
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GameObject gm = Instantiate(GameManager.instance.explosion, transform.position, transform.rotation);
            Destroy(gm, 2f);
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
            Debug.Log("Game Over");
            SceneManager.LoadSceneAsync(2); // Load game over scene
        }
    }
}
