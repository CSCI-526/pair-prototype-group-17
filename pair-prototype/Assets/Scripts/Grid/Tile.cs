using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Tile : MonoBehaviour {
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    private bool isMouseOver = false;
 
    public void Init(bool isOffset) {
        _renderer.color = isOffset ? _offsetColor : _baseColor;
    }
 
    public void OnMouseEnter() {
        _highlight.SetActive(true);
        isMouseOver = true;
    }
 
    public void OnMouseExit()
    {
        _highlight.SetActive(false);
        isMouseOver = false;
    }

    public bool IsMouseOver() {
        return isMouseOver;
    }

    public Vector2 GetCenterPosition() {
        return _renderer.bounds.center;
    }

    public Vector2 GetSpriteDimensions() {
        return _renderer.bounds.size;  // Get dimensions of the sprite in world units
    }
}