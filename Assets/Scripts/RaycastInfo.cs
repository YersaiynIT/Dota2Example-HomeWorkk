using UnityEngine;

public static class RaycastHelper
{
    public static Vector3 GetRaycastHitPoint(Ray ray, LayerMask layerMask)
    {
        RaycastHit hitInfo;
        Vector3 hitPoint = Vector3.zero;

        if (Physics.Raycast(ray, out hitInfo, 100, layerMask.value))
            hitPoint = hitInfo.point;

        return hitPoint;
    }
}
