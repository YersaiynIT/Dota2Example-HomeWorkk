using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private const int LeftMouseButton = 0;

    [SerializeField] private LayerMask _groundLayer;
    private Character _character;
    private DestinationMarkerManager _markerManager;

    public void Initialize(Character character, DestinationMarkerManager markerManager)
    {
        _character = character;
        _markerManager = markerManager;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(LeftMouseButton))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 movePointPosition = RaycastHelper.GetRaycastHitPoint(ray, _groundLayer);

            _markerManager.SetDestinationMarker(movePointPosition);

            _character.ProcessMoveTo(movePointPosition);
        }

    }
}
