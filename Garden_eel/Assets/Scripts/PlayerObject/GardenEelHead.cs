using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenEelHead : GardenEelBody
{
    public float lengthOfBody;
    public GameObject body;
    
    private Transform _tail;
    private List<GardenEelBody> _bodyList;

    private const float StartSize = 1f;
    private const float StartLength = 10f;

    private static readonly float[] LengthArray = new[] {1f, 1.9f, 3.3f, 4.3f, 5f};
    private static readonly float[] SizeArray = new[] {1f, 1.4f, 2f, 2.6f, 3.2f};
    
    void Start()
    {
        
    }

    protected override bool MoveToTarget(Vector3 target)
    {
        if ((_tail.position - transform.position).magnitude < lengthOfBody)
        {
            return base.MoveToTarget(target);
        }

        return false;
    }
}
