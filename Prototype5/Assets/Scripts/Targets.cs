using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targets : MonoBehaviour
{
    private Rigidbody targetRb;
    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float torqueConstant = 10;
    private float xRange = 4;
    private float yPos = -2;
    private GameManager gameManager;
    public int pointValue;
    public ParticleSystem explosion;
    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        targetRb.AddForce(randomForce(minSpeed, maxSpeed), ForceMode.Impulse);
        targetRb.AddTorque(randomTorque(torqueConstant), randomTorque(torqueConstant), randomTorque(torqueConstant), ForceMode.Impulse);
        transform.position = GenerateStartPos(xRange, yPos);

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnMouseDown()
    {
        if (gameManager.isGameRunning)
        {
            Destroy(gameObject);
            gameManager.UpdateScore(pointValue);
            Instantiate(explosion, transform.position, explosion.transform.rotation);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!gameObject.CompareTag("Pizza"))
        {
            gameManager.UpdateScore(-5);
        }
        Destroy(gameObject);
    }

    Vector3 randomForce(float minS, float maxS)
    {
        return Vector3.up * Random.Range(minS, maxS);
    }
    float randomTorque(float t)
    {
        return Random.Range(-t, t);
    }
    Vector3 GenerateStartPos(float xRange, float y)
    {
        return new Vector3(Random.Range(-xRange, xRange), y);
    }
}
