using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenEelHead : GardenEelBody
{
    public GameObject body;
    
    private float _lengthOfBody;
    private Transform _tail;
    private Rigidbody2D _rigidbody2D;
    private List<GardenEelBody> _bodyList;
    
    private const float StartSize = 1f;
    private const float StartLength = 10f;
    private const int StartCountOfBody = 10;

    private static readonly float[] LengthArray = new[] {1f, 1.9f, 3.3f, 4.3f, 5f};
    private static readonly float[] SizeArray = new[] {1f, 1.4f, 2f, 2.6f, 3.2f};
    
    protected override void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        
        _bodyList = new List<GardenEelBody>();
        _bodyList.Add(this);

        _lengthOfBody = StartLength;
        transform.localScale = Vector3.one * StartSize;

        AddBody(StartCountOfBody);
        
        // for (int i = 0; i < StartCountOfBody; i++)
        // {
        //     var obj = Instantiate(body, Vector3.zero, Quaternion.identity);
        //     var bodyComponent = obj.GetComponent<GardenEelBody>();
        //     bodyComponent.parent = _bodyList[i].transform;
        //     bodyComponent.spaceOfBody = StartLength / StartCountOfBody;
        //     _tail = obj.transform;
        // }
    }

    protected override void FixedUpdate()
    {
        _target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var velocity = _rigidbody2D.velocity;
            
        if (velocity.y < -1f)
        {
            velocity.y = -1f;
            _rigidbody2D.velocity = velocity;
        }
        
        if (Input.GetMouseButton(0) || Input.touchCount > 0)
        {
            MoveToTarget(_target);
        }
    }

    private void AddBody(int n)
    {
        int size = _bodyList.Count - 1;
        
        for (int i = 0; i < n; i++)
        {
            var obj = Instantiate(body, Vector3.zero, Quaternion.identity);
            var bodyComponent = obj.GetComponent<GardenEelBody>();
            Debug.Log($"now size : {size}");
            bodyComponent.parent = _bodyList[size + i].transform;
            bodyComponent.spaceOfBody = _lengthOfBody / (size + n);
            _tail = obj.transform;
            _bodyList.Add(bodyComponent);
        }
    }

    protected override bool MoveToTarget(Vector3 target)
    {
        if ((_tail.position - transform.position).magnitude < _lengthOfBody)
        {
            return base.MoveToTarget(target);
        }

        return false;
    }
}
