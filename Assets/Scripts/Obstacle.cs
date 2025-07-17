/*
* Copyright (d) deoo
*/

using UnityEngine;


public class Obstacle : MonoBehaviour
{
    [SerializeField] float minSize = 0.5f;
    [SerializeField] float maxSize = 2.0f;
    Rigidbody2D rb;
    [SerializeField] float minSpeed = 50f;
    [SerializeField] float maxSpeed = 150f;
    [SerializeField] float maxSpinSpeed = 10f;
    void Start()
    {
        float randomSize = Random.Range(minSize, maxSize);
        transform.localScale = new Vector3(randomSize, randomSize, 1);

        rb = GetComponent<Rigidbody2D>();

        float randomSpeed = Random.Range(minSpeed, maxSpeed) / randomSize;
        Vector2 randomDirection = Random.insideUnitCircle;
        rb.AddForce(randomDirection * randomSpeed);

        float randomTorque = Random.Range(-maxSpinSpeed, maxSpinSpeed);
        rb.AddTorque(randomTorque);
    }

    void Update()
    {
        
    }
}

