using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DistanceComparer : IComparer
{
    private Transform compareTransform;
    public DistanceComparer(Transform compTransform)
    {
        compareTransform = compTransform;
    }
    public int Compare(object x, object y)
    {
        Collider2D xCollider = x as Collider2D;
        Collider2D yCollider = y as Collider2D;

        Vector2 offset = xCollider.transform.position - compareTransform.position;
        float xDistance = offset.sqrMagnitude;

        offset = yCollider.transform.position - compareTransform.position;
        float yDistance = offset.sqrMagnitude;

        return xDistance.CompareTo(yDistance);
    }
}
