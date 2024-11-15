using UnityEngine;

public class InputMovementHandler : MonoBehaviour
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

            RaycastHit hitInfo;
            Vector3 movePointPosition;

            if (Physics.Raycast(ray, out hitInfo, 200, _groundLayer.value))
            {
                movePointPosition = hitInfo.point;

                _markerManager.SetDestinationMarker(movePointPosition);

                _character.ProcessMoveTo(movePointPosition);
            }
        }

    }
}
