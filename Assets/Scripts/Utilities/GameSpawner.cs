using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class GameSpawner : MonoBehaviour
{
    [Header("Asteroid Settings")]
    public GameObject asteroidPrefab;
    public float minBaseSpeed = 2f;
    public float maxBaseSpeed = 4f;
    public float minRotationSpeed = -60f;
    public float maxRotationSpeed = 60f;
    public float minSize = 0.5f;
    public float maxSize = 1.5f;
    public float minAngle = 265f;
    public float maxAngle = 275f;
    private float lastAsteroidSpawn;
    private Queue<GameObject> asteroidPool = new Queue<GameObject>();

    [Header("Star Settings")]
    public GameObject starPrefab;
    public float starSpawnDelay = 2f;
    public float spacing = 1f;
    private float animationFractionIncrement = 0.1f;
    private float currentAnimationFraction = 0f;
    private float maxWidthSpawn;
    private float minWidthSpawn;
    private float lastStarActiveTime;
    private int activeStars = 0;

    private enum PatternType
    {
        Rectangle,
        Triangle,
        Diamond,
        Circle,
    }

    private int patternCount;
    private Queue<GameObject> starPool = new Queue<GameObject>();
    private float starSpawnFromAsteroidFactor = 2f;

    [Header("Power-Up Settings")]
    public GameObject[] powerUpPrefabs; // Shield, Health, Ammo
    private List<int> powerUpDropBuckets = new List<int> { 1, 2, 3, 4, 5 };
    private int asteroidKillCount = 0;
    private int nextPowerUpDrop = 5;

    [Header("Game Acceleration by Score")]
    public int scorePerLevel = 150;
    public float scrollSpeedIncrementPerLevel = 0.25f;
    public float asteroidSpeedImcrementPerLevel = 2f;
    public float asteroidSpawnRate = 1f;
    public float asteroidSpawnRateDecrementPerLevel = 0.2f;
    public float asteroidMinSpawnRate = 0.4f;
    private float scrollSpeedIncrementPerScore;
    private float asteroidSpawnRateDecrementPerScore;
    private float currentAsteroidSpeedIncrement = 0f;

    public float speed = 1f;
    private float maxSpawnPos;
    private int asteroidPoolSize = 5;
    private int starPoolSize = 50;
    private int asteroidsInPool; // for debugging purposes
    private int starsInPool; // for debugging purposes
    private ScrollingBackgound scrollingBackground;
    private int score;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        InitializeData();
        InitializePool(asteroidPool, asteroidPrefab, asteroidPoolSize);
        InitializePool(starPool, starPrefab, starPoolSize);
        ShufflePowerUpBuckets();
    }

    void Update()
    {
        if (Time.time - lastAsteroidSpawn > asteroidSpawnRate)
        {
            SpawnAsteroid();
        }

        if (Time.time - lastStarActiveTime > starSpawnDelay && activeStars <= 0)
        {
            SpawnPattern((PatternType)Random.Range(0, patternCount));
        }
    }

    // -------------------- [INITIALIZATION FUNCTIONS] --------------------
    private void InitializeData()
    {
        scrollingBackground = (ScrollingBackgound)FindAnyObjectByType(typeof(ScrollingBackgound));
        Debug.Log("Background object found: " + scrollingBackground.name);
        maxSpawnPos = Camera.main.orthographicSize * Camera.main.aspect - 1f;
        lastStarActiveTime = Time.time - starSpawnDelay + 2;
        minWidthSpawn = maxSpawnPos / 2;
        maxWidthSpawn = maxSpawnPos;
        patternCount = System.Enum.GetValues(typeof(PatternType)).Length;
        scrollSpeedIncrementPerScore = scrollSpeedIncrementPerLevel / scorePerLevel;
        asteroidSpawnRateDecrementPerScore = asteroidSpawnRateDecrementPerLevel / scorePerLevel;
    }

    private void InitializePool(Queue<GameObject> pool, GameObject prefab, int poolSize)
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
            if (pool.Equals(asteroidPool))
                asteroidsInPool++; // for debugging purposes
            else if (pool.Equals(starPool))
                starsInPool++; // for debugging purposes
        }
    }

    // -------------------- [POOL MANAGEMENT FUNCTIONS] --------------------
    private GameObject GetObjectFromPool(Queue<GameObject> pool, GameObject prefab)
    {
        if (pool.Count > 0)
        {
            if (pool.Equals(asteroidPool))
                asteroidsInPool--; // for debugging purposes
            else if (pool.Equals(starPool))
                starsInPool--; // for debugging purposes
            var obj = pool.Dequeue();
            Debug.Log("obj take from pool: " + obj);
            return obj;
        }
        Debug.Log("pool count: " + pool.Count);
        return Instantiate(prefab);
    }

    private void ReturnObjectToPool(Queue<GameObject> pool, GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
        if (pool.Equals(asteroidPool))
            asteroidsInPool++; // for debugging purposes
        else if (pool.Equals(starPool))
            starsInPool++; // for debugging purposes
    }

    // -------------------- [ASTEROID SPAWNING FUNCTIONS] --------------------
    private void SpawnAsteroid()
    {
        GameObject asteroidObject = GetObjectFromPool(asteroidPool, asteroidPrefab);
        Debug.Log("Asteroid object found: " + asteroidObject);
        asteroidObject.transform.position = RandomSpawnPosition();
        asteroidObject.transform.localScale = RandomSize();
        asteroidObject.SetActive(true);

        AsteroidController asteroid = asteroidObject.GetComponent<AsteroidController>();
        asteroid.SetRotationSpeed(Random.Range(minRotationSpeed, maxRotationSpeed));
        asteroid.SetSpeed(Random.Range(minBaseSpeed, maxBaseSpeed) + currentAsteroidSpeedIncrement);
        asteroid.SetMoveDirection(RandomDirection());
        asteroid.SetSpawner(this); // to call ExplodeAsteroid() on this script
        asteroid.StartForObjectPooling();

        lastAsteroidSpawn = Time.time;
    }

    public void HandleAsteroidDestroyed(GameObject asteroid)
    {
        ReturnObjectToPool(asteroidPool, asteroid);
    }

    public void ExplodeAsteroid(Vector2 position, float size)
    {
        DropStar(position, size);
        asteroidKillCount++;
        if (asteroidKillCount >= nextPowerUpDrop)
        {
            DropPowerUp(position);
            UpdateNextPowerUpDrop();
        }
    }

    // -------------------- [STAR SPAWNING FUNCTIONS] --------------------
    private void SpawnStar(Vector2 pos)
    {
        GameObject star = GetObjectFromPool(starPool, starPrefab);
        star.transform.position = pos;
        star.SetActive(true);

        StarCollectiable starComponent = star.GetComponent<StarCollectiable>();
        starComponent.SetSpeed(speed);
        starComponent.SetStartTime(currentAnimationFraction);
        starComponent.SetSpawner(this);
        currentAnimationFraction = (currentAnimationFraction + animationFractionIncrement) % 1f;
        activeStars++;
    }

    public void HandleStarDestroyed(GameObject star)
    {
        ReturnObjectToPool(starPool, star);
        activeStars--;
        lastStarActiveTime = Time.time;
    }

    public void HandleStarCollected()
    {
        score++;
        asteroidSpawnRate = Mathf.Max(
            asteroidSpawnRate - asteroidSpawnRateDecrementPerScore,
            asteroidMinSpawnRate
        );
        currentAsteroidSpeedIncrement = score * asteroidSpeedImcrementPerLevel / scorePerLevel;
        var scrollSpeed = scrollingBackground.GetScrollSpeed();
        scrollingBackground.SetScrollSpeed(scrollSpeed + scrollSpeedIncrementPerScore);
        //Debug.Log($"Score {score}");
        //Debug.Log($"-Scroll speed: {scrollingBackground.GetScrollSpeed()}");
        //Debug.Log($"--Asteroid speed increment: {currentAsteroidSpeedIncrement}");
        //Debug.Log($"---Asteroid spawn rate: {asteroidSpawnRate}");
        GameUIHandler.Instance.SetStarValue(score);
        //if (score == 10)
        //{
        //    SceneManager.LoadScene("Level_02");
        //    Debug.Log("asteroid pool count after load scene: " + asteroidPool.Count);
        //    foreach (var s in asteroidPool)
        //    {
        //        Debug.Log("asteroid pool: " + s);
        //    }
        //}
        //else if (score == 20)
        //{
        //    SceneManager.LoadScene("Level_03");
        //}
    }

    // -------------------- [PATTERN SPAWNING FUNCTIONS] --------------------
    private void SpawnPattern(PatternType pattern)
    {
        float width = GetRandomPatternWidth();
        Vector2 center = GetRandomSpawnCenter(width);
        int halfSideColumns = (int)(width / spacing) / 2;

        switch (pattern)
        {
            case PatternType.Rectangle:
                SpawnRectangle(center, halfSideColumns, Random.Range(1, 6));
                break;
            case PatternType.Triangle:
                SpawnTriangle(center, halfSideColumns);
                break;
            case PatternType.Diamond:
                SpawnDiamond(center, halfSideColumns);
                break;
            case PatternType.Circle:
                SpawnCircle(center, width / 2);
                break;
        }
    }

    private float GetRandomPatternWidth()
    {
        return Random.Range(minWidthSpawn, maxWidthSpawn);
    }

    private Vector2 GetRandomSpawnCenter(float width)
    {
        float randomX = Random.Range(-maxSpawnPos + width / 2, maxSpawnPos - width / 2);
        return new Vector2(randomX, Camera.main.orthographicSize + 1f);
    }

    // -------------------- [PATTERN SHAPE SPAWNING FUNCTIONS] --------------------
    private void SpawnRectangle(Vector2 center, int halfColumnsWithoutCenter, int numRows)
    {
        for (int i = 0; i < numRows; i++)
        {
            for (int j = -halfColumnsWithoutCenter; j <= halfColumnsWithoutCenter; j++)
            {
                SpawnStar(new Vector2(center.x + j * spacing, center.y + i * spacing));
            }
        }
    }

    private void SpawnTriangle(Vector2 center, int halfColumnsWithoutCenter)
    {
        int numOfRows = halfColumnsWithoutCenter + 1;

        for (int i = 0; i < numOfRows; i++)
        {
            for (int j = -i; j <= i; j++)
            {
                SpawnStar(new Vector2(center.x + j * spacing, center.y + i * spacing));
            }
        }
    }

    private void SpawnDiamond(Vector2 center, int halfColumnsWithoutCenter)
    {
        int halfRowsIncludeCenter = halfColumnsWithoutCenter + 1;

        for (int i = 0; i < halfRowsIncludeCenter; i++)
        {
            for (int j = -i; j <= i; j++)
            {
                SpawnStar(new Vector2(center.x + j * spacing, center.y + i * spacing));
            }
        }
        for (int i = halfRowsIncludeCenter - 2; i >= 0; i--)
        {
            for (int j = -i; j <= i; j++)
            {
                SpawnStar(
                    new Vector2(
                        center.x + j * spacing,
                        center.y + (2 * halfRowsIncludeCenter - 2 - i) * spacing
                    )
                );
            }
        }
    }

    private void SpawnCircle(Vector2 center, float radius)
    {
        float radiusSquared = radius * radius;
        int halfNumOfRows = (int)(radius / spacing);

        float spacingSquared = spacing * spacing;

        for (int i = -halfNumOfRows; i <= halfNumOfRows; i++)
        {
            for (int j = -halfNumOfRows; j <= halfNumOfRows; j++)
            {
                if ((i * spacing) * (i * spacing) + (j * spacing) * (j * spacing) <= radiusSquared)
                {
                    SpawnStar(new Vector2(center.x + j * spacing, center.y + i * spacing));
                }
            }
        }
    }

    // -------------------- [POWER-UP SPAWNING FUNCTIONS] --------------------
    private void DropPowerUp(Vector2 position)
    {
        int randomIndex = Random.Range(0, powerUpPrefabs.Length);
        GameObject powerUp = Instantiate(powerUpPrefabs[randomIndex]);
        powerUp.transform.position = position;
        powerUp.GetComponent<PowerUp>().SetSpeed(speed);
    }

    private void DropStar(Vector2 center, float zoneSize)
    {
        int numStars = Mathf.RoundToInt(zoneSize * starSpawnFromAsteroidFactor);
        for (int i = 0; i < numStars; i++)
        {
            Vector2 spawnPos = center + Random.insideUnitCircle * zoneSize;
            SpawnStar(spawnPos);
        }
    }

    private void UpdateNextPowerUpDrop()
    {
        powerUpDropBuckets.RemoveAt(0);
        if (powerUpDropBuckets.Count == 0)
        {
            powerUpDropBuckets = new List<int> { 1, 2, 3, 4, 5 };
            ShufflePowerUpBuckets();
        }
        nextPowerUpDrop += powerUpDropBuckets[0];
    }

    private void ShufflePowerUpBuckets()
    {
        for (int i = powerUpDropBuckets.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (powerUpDropBuckets[i], powerUpDropBuckets[j]) = (
                powerUpDropBuckets[j],
                powerUpDropBuckets[i]
            );
        }
    }

    // -------------------- [UTILITY FUNCTIONS] --------------------
    private Vector3 RandomSpawnPosition()
    {
        float randomX = Random.Range(-maxSpawnPos, maxSpawnPos);
        return new Vector3(randomX, Camera.main.orthographicSize + 1f, 0);
    }

    private Vector3 RandomSize()
    {
        float randomSize = Random.Range(minSize, maxSize);
        return new Vector3(randomSize, randomSize, 1);
    }

    private Vector2 RandomDirection()
    {
        float randomAngle = Random.Range(minAngle, maxAngle);
        return Quaternion.Euler(0, 0, randomAngle) * Vector2.right;
    }
}
