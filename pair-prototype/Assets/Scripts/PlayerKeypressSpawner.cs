using UnityEngine;
using UnityEngine.UI;

public class PlayerKeypressSpawner : MonoBehaviour
{
    public GameObject unitPrefab;
    public Text gearText;
    public int gearCount = 100;
    public int gearCostPerSpawn = 10;

    private Camera mainCamera;  // Can't spawn outside this camera
    private bool isReadyToSpawn = false;

    void Start()
    {
        mainCamera = Camera.main;
        UpdateGearUI();                
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
            isReadyToSpawn = true;
        }

        if (Input.GetMouseButtonDown(0) && isReadyToSpawn) {
            if (gearCount >= gearCostPerSpawn) {
                // SpawnUnitAtMouseX();
                SpawnUnitAtHoveredTile();
                isReadyToSpawn = false; 
            } else {
                Debug.Log("Not enough gears to spawn a unit!");
            }
        }
    }

    public void AddGear(int amount) 
    {
        gearCount += amount;
        UpdateGearUI();
    }

    // Spawn a unit with X-axis aligned to the mouse's X position 
    // and Y aligned to the top of the object this script is attached to
    // void SpawnUnitAtMouseX()
    void SpawnUnitAtHoveredTile()
    {
        if (mainCamera == null) {
            Debug.LogError("Main camera not found!");
            return;
        }

        Vector3 mousePosition = Input.mousePosition;  // Unity always returns Vector3 for mouse position
        Vector3 worldMousePosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, mainCamera.nearClipPlane));

        // Raycast to check if there's a Tile under the mouse
        RaycastHit2D hit = Physics2D.Raycast(worldMousePosition, Vector2.zero);

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null) {
            Debug.LogError("No SpriteRenderer found on this GameObject!");
            return;
        }
        // float topY = transform.position.y + (spriteRenderer.bounds.size.y / 2);
        // Vector2 spawnPosition = new Vector2(worldMousePosition.x, topY);  // X from mouse and Y from top of the GameObject
        // Vector2 spawnPosition = new Vector2(worldMousePosition.x, worldMousePosition.y);
        // Instantiate(unitPrefab, spawnPosition, Quaternion.identity);
        // gearCount -= gearCostPerSpawn;
        // UpdateGearUI();
        
        Tile hoveredTile = hit.collider.GetComponent<Tile>();
        if (hoveredTile != null && hoveredTile.IsMouseOver()) {
            Vector2 spawnPosition = hoveredTile.GetCenterPosition();
            // print(spawnPosition);
            Instantiate(unitPrefab, spawnPosition, Quaternion.identity);
            gearCount -= gearCostPerSpawn;
            UpdateGearUI();
        }
    }


    void UpdateGearUI()
    {
        if (gearText != null) {
            gearText.text = "Gears: " + gearCount.ToString();
        }
    }
}
