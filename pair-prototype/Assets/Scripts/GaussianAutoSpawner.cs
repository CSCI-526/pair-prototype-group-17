using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GaussianAutoSpawner : MonoBehaviour
{
    public GameObject spawnedUnit;
    public float spawnRate = 0.5f; 

    // Spawner spawns with a gaussian distribution, with mean and variance
    public float meanX = 0f;  
    public float varianceX = 3f; 
    public float meanY = 0f; 
    public float varianceY = 0f;
    private Rect spawnArea;
    private float spawnInterval;
    private int numCol = 12;
    [SerializeField] private Tile _tilePrefab;
    private float tileSize;
    float[] coords;


    public void Initialize(
        GameObject spawnedUnit, 
        float? meanX = null, 
        float? varianceX = null, 
        float? meanY = null, 
        float? varianceY = null)
    {
        this.spawnedUnit = spawnedUnit;
        this.meanX = meanX ?? this.meanX; 
        this.varianceX = varianceX ?? this.varianceX;
        this.meanY = meanY ?? this.meanY;
        this.varianceY = varianceY ?? this.varianceY;
    }


    void Start()
    {
        tileSize = _tilePrefab.GetSpriteDimensions().x;
        coords = new float[numCol];
        ColumnXCoordinates();
        spawnArea = new Rect(transform.position.x - transform.localScale.x / 2,
                             transform.position.y - transform.localScale.y / 2,
                             transform.localScale.x, transform.localScale.y);
        spawnInterval = 1.0f / spawnRate;
        StartCoroutine(SpawnObjects());
    }


    IEnumerator SpawnObjects()
    {
        while (true)
        {
            SpawnObject();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // Find all x coordinates of all columns;
    void ColumnXCoordinates(){
        GameObject obj = GameObject.Find("Background (0)"); // Get background
        float backgroundWidth = obj.GetComponent<SpriteRenderer>().bounds.size.x;  // Get background's x-coordinate
        float tileSize = _tilePrefab.GetSpriteDimensions().x; // Get tile's size

        float xCoord = 0 - (backgroundWidth /2 + tileSize/2);
        for (int x = 0; x < numCol; x++) {
            coords[x] = xCoord;
            xCoord += tileSize;
        }
    }

    void SpawnObject()
    {
        float spawnX = GaussianRandom(meanX, varianceX);
        float spawnY = GaussianRandom(meanY, varianceY);
        spawnX = Mathf.Clamp(spawnX, spawnArea.xMin, spawnArea.xMax);
        spawnY = Mathf.Clamp(spawnY, spawnArea.yMin, spawnArea.yMax);

        // Find which column is the closest to the randomized x coordinate
        float nearestCol = FindNearestCol(spawnX);

        // Vector3 spawnPosition = new Vector2(spawnX, spawnY);
        Vector3 spawnPosition = new Vector2(nearestCol, spawnY); // move enemy to the closest column
        Instantiate(spawnedUnit, spawnPosition, Quaternion.identity);
    }

    //Find the column that is closet to a given x-coord
    float FindNearestCol(float spawnX)
    {
        float nearestX = coords.OrderBy(n => Mathf.Abs(n - spawnX)).First();
        return nearestX;
    }


    // returns 1 calculated gaussian value according to mean and variance
    float GaussianRandom(float mean, float variance)
    {
        // generate uniform distributed u1 and u2
        // u1 ~ U(0,1), u2 ~ U(0,1)
        float u1 = Random.value;
        float u2 = Random.value;

        // apply Box–Muller transform to transform 2 uniformly distributed
        // values to 1 gaussian distributed value
        // https://en.wikipedia.org/wiki/Box–Muller_transform
        float randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) * Mathf.Sin(2.0f * Mathf.PI * u2); // Standard normal distribution
        return mean + Mathf.Sqrt(variance) * randStdNormal;  // Scale and shift to mean and variance
    }
}
