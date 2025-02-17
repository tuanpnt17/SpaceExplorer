using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public float spawnRate = 1;
    float lastSpawn;

    public float minSpeed = 2f;
    public float maxSpeed = 4f;
    public float minAngle = 265f;
    public float maxAngle = 275f;

    public float minRotationSpeed = -60f;
    public float maxRotationSpeed = 60f;

    public float minSize = 0.5f;
    public float maxSize = 1.5f;

    public float minSpawnPos;
    public float maxSpawnPos;

    public GameObject asteroidPrefab;

    void Start()
    {
        maxSpawnPos = Camera.main.orthographicSize * Camera.main.aspect - 1f;
        minSpawnPos = -maxSpawnPos;
        lastSpawn = Time.time;
    }

    void Update()
    {
        if (ShouldSpawn())
        {
            SpawnAsteroid();
        }
    }

    private void SpawnAsteroid()
    {
        GameObject asteroidObject = Instantiate(asteroidPrefab);
        AsteroidController asteroid = asteroidObject.GetComponent<AsteroidController>();
        asteroid.transform.position = RandomSpawnPosition();
        asteroid.transform.localScale = RandomSize();
        asteroid.SetRotationSpeed(Random.Range(minRotationSpeed, maxRotationSpeed));
        asteroid.SetSpeed(Random.Range(minSpeed, maxSpeed));
        asteroid.SetMoveDirection(RandomDirection());

        lastSpawn = Time.time;
    }

    private bool ShouldSpawn()
    {
        return Time.time - lastSpawn > spawnRate;
    }

    private UnityEngine.Vector3 RandomSpawnPosition()
    {
        float randomX = Random.Range(minSpawnPos, maxSpawnPos);
        return new UnityEngine.Vector3(randomX, Camera.main.orthographicSize + 1f, 0);
    }

    private UnityEngine.Vector3 RandomSize()
    {
        float randomSize = Random.Range(minSize, maxSize);
        return new UnityEngine.Vector3(randomSize, randomSize, 1);
    }

    private UnityEngine.Vector2 RandomDirection()
    {
        float randomAngle = Random.Range(minAngle, maxAngle);
        return Quaternion.Euler(0, 0, randomAngle) * Vector2.right;
    }
}
