using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationMarkerManager : MonoBehaviour
{
    [SerializeField] private DestinationMarker _markerPrefab;
    private DestinationMarker _currentMarker;

    public void SetDestinationMarker(Vector3 position)
    {
        if (_currentMarker == null)
            _currentMarker = Instantiate(_markerPrefab, position, Quaternion.identity);
        else
            _currentMarker.transform.position = position;
    }
}
