using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenEelHead : GardenEelBody
{
    public float lengthOfBody;
    public Transform tail;

    protected override bool MoveToTarget(Vector3 target)
    {
        if ((tail.position - transform.position).magnitude < lengthOfBody)
        {
            return base.MoveToTarget(target);
        }

        return false;
    }
}
