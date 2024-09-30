using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class GridManager : MonoBehaviour {
    [SerializeField] private int _width, _height;
 
    [SerializeField] private Tile _tilePrefab;
 
    [SerializeField] private Transform _cam;
    [SerializeField] private Transform grid;
    [SerializeField] private SpriteRenderer background;
    [SerializeField] private Transform homeBase;
    private Dictionary<Vector2, Tile> _tiles;
    private int _currentHeight = 0;
 
    void Start() {
        
        _tiles = new Dictionary<Vector2, Tile>();
        for (int y = 0; y < _height; y++) {
            GenerateRow(y);
            _currentHeight++;
        }
        StartCoroutine(GenerateGridWithDelay());
    }
 
    // Generate a new row each .6 second
    private IEnumerator GenerateGridWithDelay() {
        while (true) {
            GenerateRow(_currentHeight); 
            _currentHeight++; 
            yield return new WaitForSeconds(.6f); 
        }
    }

    // Generate a single row at a specific Y coordinate
    private void GenerateRow(int y) {
        for (int x = 0; x < _width; x++) {
            GameObject obj = GameObject.Find("Background (0)");
            float backgroundWidth = obj.GetComponent<SpriteRenderer>().bounds.size.x;
            float tileSize = _tilePrefab.GetSpriteDimensions().x;
            
            var spawnedTile = Instantiate(_tilePrefab, new Vector3(x * tileSize - backgroundWidth /2 + tileSize/2, y * tileSize + homeBase.position.y), Quaternion.identity);
            spawnedTile.name = $"Tile {x} {y}";

            var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
            spawnedTile.Init(isOffset);

            _tiles[new Vector2(x, y)] = spawnedTile;
            spawnedTile.transform.SetParent(grid.transform);
        }
    }

    
    // TODO: Set lifespan of the tiles
}