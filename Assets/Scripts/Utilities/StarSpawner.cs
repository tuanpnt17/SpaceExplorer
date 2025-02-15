using System.Collections.Generic;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class StarSpawner : MonoBehaviour
{
    public float spawnDelay = 2f;
    private float lastActiveTime;

    public float minSpawnX;
    public float maxSpawnX;
    public float minWidthSpawn;
    public float maxWidthSpawn;

    public float speed = 1f;

    public float spacing = 1f;

    public GameObject starPrefab;
    private int activeStars = 0;

    private float currentAnimationFraction = 0f;
    public float animationFractionIncrement = 0.1f;

    private int starsInPool = 0; // for debugging purposes

    private enum PatternType
    {
        Rectangle,
        Triangle,
        Diamond,
        Circle,
    }

    private int patternCount;

    private Queue<GameObject> starPool = new Queue<GameObject>();

    void Start()
    {
        maxSpawnX = Camera.main.orthographicSize * Camera.main.aspect - 1f;
        minSpawnX = -maxSpawnX;
        float maxWidth = maxSpawnX - minSpawnX;
        minWidthSpawn = maxWidth / 4;
        maxWidthSpawn = maxWidth * 3 / 4;
        lastActiveTime = Time.time - spawnDelay + 2;
        patternCount = System.Enum.GetValues(typeof(PatternType)).Length;

        InitializePool(50);
    }

    void Update()
    {
        if (ShouldSpawn())
        {
            PatternType randomPattern = (PatternType)Random.Range(0, patternCount);
            SpawnPattern(randomPattern);
        }
    }

    // -------------------- [OBJECT POOLING FUNCTIONS] --------------------
    private void InitializePool(int poolSize)
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject star = Instantiate(starPrefab);
            star.SetActive(false);
            starPool.Enqueue(star);
            starsInPool++;
        }
    }

    private GameObject GetStarFromPool()
    {
        if (starPool.Count > 0)
        {
            starsInPool--;
            return starPool.Dequeue();
        }
        return Instantiate(starPrefab);
    }

    private void ReturnStarToPool(GameObject star)
    {
        star.SetActive(false);
        starPool.Enqueue(star);
        starsInPool++;
    }

    // -------------------- [SPAWN STAR & HANDLE DESTRUCTION] --------------------
    private void SpawnStar(Vector2 pos)
    {
        GameObject starObject = GetStarFromPool();
        starObject.transform.position = pos;
        starObject.SetActive(true);

        StarCollectiable starComponent = starObject.GetComponent<StarCollectiable>();
        starComponent.SetSpeed(speed);
        starComponent.SetStartTime(currentAnimationFraction);
        currentAnimationFraction = (currentAnimationFraction + animationFractionIncrement) % 1f;
        activeStars++;
        starComponent.AddOnDestroyedListener(() => HandleStarDestroyed(starObject));
    }

    private void HandleStarDestroyed(GameObject star)
    {
        ReturnStarToPool(star);
        activeStars--;
        lastActiveTime = Time.time;
    }

    // -------------------- [SPAWN PATTERNS] --------------------
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

    private bool ShouldSpawn()
    {
        return Time.time - lastActiveTime > spawnDelay && activeStars <= 0;
    }

    private float GetRandomPatternWidth()
    {
        return Random.Range(minWidthSpawn, maxWidthSpawn);
    }

    private Vector2 GetRandomSpawnCenter(float width)
    {
        float randomX = Random.Range(minSpawnX + width / 2, maxSpawnX - width / 2);
        return new Vector2(randomX, Camera.main.orthographicSize + 1f);
    }

    // -------------------- [SPAWN PATTERN SHAPES] --------------------
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
}
