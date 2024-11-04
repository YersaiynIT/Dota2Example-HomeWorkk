using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private const int LeftMouseButton = 0;

    [SerializeField] private LayerMask _groundLayer;
    private Character _character;
    private CharacterView _view;

    public void Initialize(Character character, CharacterView view)
    {
        _character = character;
        _view = view;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(LeftMouseButton))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 movePointPosition = RaycastHelper.GetRaycastHitPoint(ray, _groundLayer);

            _view.SetDestinationMarkerTo(movePointPosition);

            _character.ProcessMoveTo(movePointPosition);
        }

    }
}
