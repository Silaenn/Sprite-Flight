/*
* Copyright (d) deoo
*/

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class PlayerController : MonoBehaviour
{
    float elapsedTime = 0f;
    float score = 0f;
    [SerializeField] float scoreMultiplier = 10f;
    [SerializeField] float thrustForce = 1f;
    [SerializeField] float maxSpeed = 5f;
    [SerializeField] GameObject boosterFlame;
    [SerializeField] UIDocument uIDocument;
    Label scoretext;
    Label highScoreText;
    [SerializeField] GameObject explosionEffect;
    Button restartButton;

    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scoretext = uIDocument.rootVisualElement.Q<Label>("ScoreLabel");
        highScoreText = uIDocument.rootVisualElement.Q<Label>("HighScoreLabel");
        restartButton = uIDocument.rootVisualElement.Q<Button>("RestartButton");
        restartButton.style.display = DisplayStyle.None;
        highScoreText.style.display = DisplayStyle.None;
        restartButton.clicked += ReloadScene;
    }

    void Update()
    {
        UpdateScore();
        MovePlayer();
    }

    void UpdateScore()
    {
        elapsedTime += Time.deltaTime;
        score = Mathf.FloorToInt(elapsedTime * scoreMultiplier);
        Debug.Log("Score: " + score);
        scoretext.text = "Score: " + score;


        int currentHighScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > currentHighScore)
        {
            PlayerPrefs.SetInt("HighScore", (int)score);
            PlayerPrefs.Save();
        }

        highScoreText.text = "HighScore: " + PlayerPrefs.GetInt("HighScore");
    }

    void MovePlayer()
    {
        if (Mouse.current.leftButton.isPressed)
        {

            // Calculate  mouse direction
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
            Vector2 direction = (mousePos - transform.position).normalized;

            // Move player in direction of mouse
            transform.up = direction;
            rb.AddForce(direction * thrustForce);

            if (rb.linearVelocity.magnitude > maxSpeed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
            }
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            boosterFlame.SetActive(true);
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            boosterFlame.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        Instantiate(explosionEffect, transform.position, transform.rotation);
        restartButton.style.display = DisplayStyle.Flex;
        highScoreText.style.display = DisplayStyle.Flex;
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

