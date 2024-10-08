using UnityEngine;
using UnityEngine.UI;

public class PlayerKeypressSpawner : MonoBehaviour
{
    private GameObject unitPrefab;
    private int gearCostPerSpawn;

    private Camera mainCamera;  // Can't spawn outside this camera
    private bool isReadyToSpawn = false;
    private Gears gears;
    [SerializeField] private Button UpgradeButton1;
    [SerializeField] private Button UpgradeButton2;
    // [SerializeField] private Button UpgradeButton3;
    [SerializeField] private Button UpgradeButton4;
    public string lastPressedButton = "";
    public Text updateMessage;
    private GameObject prefabInstance; 
    private bool firstSpawn = true;

    void Start()
    {
        UpgradeButton1.onClick.AddListener(() => ButtonPressed("UpgradeButton1"));
        UpgradeButton2.onClick.AddListener(() => ButtonPressed("UpgradeButton2"));
        // UpgradeButton3.onClick.AddListener(() => ButtonPressed("UpgradeButton3"));
        UpgradeButton4.onClick.AddListener(() => ButtonPressed("UpgradeButton4"));

        unitPrefab = Resources.Load<GameObject>("Prefabs/FriendlyPrefab");
        gears = GameObject.Find("GearCountText").GetComponent<Gears>();
        gearCostPerSpawn = gears.getGearCostPerSpawn();
        mainCamera = Camera.main;
        gears.UpdateGearUI(); 
    }

    void Update()
    {
        // For PC: Detect pressing 'Q'
        if (Input.GetKeyDown(KeyCode.Q)) {
            isReadyToSpawn = true;
        }

        // For Mobile: Detect touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                isReadyToSpawn = true;
            }
        }

        // For PC: Detect left mouse click
        if (Input.GetMouseButtonDown(0) && isReadyToSpawn) {
            TrySpawnUnit();
        }

        // For Mobile: Detect touch input (equivalent to left mouse click)
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended && isReadyToSpawn)
            {
                TrySpawnUnit();
            }
        }
    }

    void TrySpawnUnit()
    {
        if (gears.getGearCount() >= gearCostPerSpawn)
        {
            SpawnUnitAtHoveredTile();
            isReadyToSpawn = false;
        }
        else
        {
            Debug.Log("Not enough gears to spawn a unit!");
        }
    }

    public void ButtonPressed(string buttonName)
    {
        lastPressedButton = buttonName;
        Debug.Log("Last pressed button: " + lastPressedButton);

        // Create a new GameObject (a copy of the prefab) without adding it to the scene yet
        
        switch (lastPressedButton)
        {
            case "UpgradeButton1":
                // unitPrefab = Resources.Load<GameObject>("Prefabs/FriendlyPrefab");
                if(gears.getGearCount() >= 20){
                    prefabInstance = Instantiate(unitPrefab);
                    FriendlyTrooper trooper = prefabInstance.GetComponent<FriendlyTrooper>();
                    trooper.updateRange(2); 
                    gears.UpdateGearCount(-20);
                    gears.UpdateGearUI();
                }
                updateMessage.text = "Range +2";
                break;
            case "UpgradeButton2":
                if(gears.getGearCount() >= 10){
                    prefabInstance = Instantiate(unitPrefab);
                    FriendlyTrooper trooper = prefabInstance.GetComponent<FriendlyTrooper>();
                    trooper.updateSpeed(1); 
                    gears.UpdateGearCount(-10);
                    gears.UpdateGearUI();
                }
                updateMessage.text = "Speed +1";
                break;
            // case "UpgradeButton3":
            //     break;
            case "UpgradeButton4":
                if(gears.getGearCount() >= 20){
                    prefabInstance = Instantiate(unitPrefab);
                    FriendlyTrooper trooper = prefabInstance.GetComponent<FriendlyTrooper>();
                    trooper.updateMaxHp(10);
                    gears.UpdateGearCount(-20);
                    gears.UpdateGearUI();
                }
                updateMessage.text = "Hp +10";
                break;
            default:
                Debug.LogError("Unknown button pressed");
                break;
        }
        unitPrefab = prefabInstance;
        prefabInstance.SetActive(false);
        firstSpawn = false;
    }

    public string GetLastPressedButton()
    {
        return lastPressedButton;
    }

    void SpawnUnitAtHoveredTile()
    {
        print("spawning");
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
        
        Tile hoveredTile = hit.collider.GetComponent<Tile>();
        if (hoveredTile != null && hoveredTile.IsMouseOver()) {
            Vector2 spawnPosition = hoveredTile.GetCenterPosition();
            if (firstSpawn){
                Instantiate(unitPrefab, spawnPosition, Quaternion.identity);
            }else{
                GameObject newSpawn = Instantiate(prefabInstance, spawnPosition, Quaternion.identity);
                newSpawn.SetActive(true);
                updateMessage.text = "";
            }
            
            gears.UpdateGearCount(-gearCostPerSpawn);
            gears.UpdateGearUI();
        }
    }
}
