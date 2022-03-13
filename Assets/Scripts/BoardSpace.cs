using System.Collections.Generic;
using UnityEngine;

public class BoardSpace : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject marker;
    [SerializeField] Color xColor;
    [SerializeField] Color oColor;
    
    public bool canInteract;
    public IndicationType Type => _type;

    private IndicationType _type = IndicationType.None;
    private Color _color;
    private Color _initialColor;
    private Renderer _markerRenderer;

    private void Awake()
    {
        _markerRenderer = marker.GetComponent<Renderer>();
        _initialColor = _markerRenderer.material.color;
        _color = _initialColor;
        canInteract = true;
    }

    private void OnMouseEnter()
    {
        if (!canInteract)
            return;

        _markerRenderer.material.color = 
            gameManager.CurrentPlayer() == IndicationType.X ? xColor : oColor;
    }

    private void OnMouseExit()
    {
        _markerRenderer.material.color = _color;
    }

    private void OnMouseDown()
    {
        if (!canInteract)
            return;

        gameManager.ReportInteraction(this);
    }

    public void ChangeValue(IndicationType type)
    {
        _type = type;
        _color =
            type == IndicationType.X ? xColor :
            type == IndicationType.O ? oColor :
            _initialColor;
        _markerRenderer.material.color = _color;

        canInteract = _type == IndicationType.None;
    }

    public void Reset()
    {
        ChangeValue(IndicationType.None);
    }
}
