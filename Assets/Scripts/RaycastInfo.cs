using UnityEngine;

public static class RaycastHelper
{
    public static Vector3 GetRaycastHitPoint(Ray ray)
    {
        RaycastHit hitInfo;
        Vector3 hitPoint = Vector3.zero;

        if (Physics.Raycast(ray, out hitInfo))
            hitPoint = hitInfo.point;

        return hitPoint;
    }
}
