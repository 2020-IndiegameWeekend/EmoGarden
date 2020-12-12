using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenEelHead : GardenEelBody
{
    public GameObject body;

    private int level = 0;
    private float _lengthOfBody;
    private Transform _tail;
    private Rigidbody2D _rigidbody2D;
    private List<GardenEelBody> _bodyList;
    
    private const float StartSize = 1f;
    private const float StartLength = 10f;
    private const int StartCountOfBody = 10;

    private static readonly float[] LengthArray = new[] {1f, 1.9f, 3.3f, 4.3f, 5f};
    private static readonly float[] SizeArray = new[] {1f, 1.4f, 2f, 2.6f, 3.2f};
    private static readonly int[] ObjectCountArray = new[] {10, 13, 16, 16, 16};
    
    protected override void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        
        _bodyList = new List<GardenEelBody>();
        _bodyList.Add(this);

        _lengthOfBody = StartLength;
        Debug.Log($"length : {_lengthOfBody}");
        transform.localScale = Vector3.one * StartSize;

        AddBody(StartCountOfBody, SizeArray[level]);
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

    private void LevelUp(int level)
    {
        int size = _bodyList.Count - 1;
        float before = _lengthOfBody;
        
        _lengthOfBody = StartLength * LengthArray[level] * 5;

        foreach (var bodys in _bodyList)
        {
            bodys.transform.localScale = Vector2.one * StartSize * SizeArray[level];
            bodys.spaceOfBody = _lengthOfBody / (float) ObjectCountArray[level];
        }
        
        Debug.Log($"level : {level}, bodyCount : {_bodyList.Count}");
        
        AddBody(ObjectCountArray[level] - _bodyList.Count - 1, SizeArray[level]);
    }

    private void AddBody(int n, float sizeValue)
    {
        if (n <= 0)
        {
            return;
        }
        
        int size = _bodyList.Count - 1;
        
        _bodyList[_bodyList.Count - 1].SetIsTail(false);
        
        for (int i = 0; i < n; i++)
        {
            var obj = Instantiate(body, Vector3.zero, Quaternion.identity);
            var bodyComponent = obj.GetComponent<GardenEelBody>();
            Debug.Log($"now size : {size}");
            bodyComponent.parent = _bodyList[size + i].transform;
            bodyComponent.spaceOfBody = _lengthOfBody / (size + n);
            _tail = obj.transform;
            _tail.localScale *= sizeValue;
            _bodyList.Add(bodyComponent);
        }

        for (int i = _bodyList.Count - 2; i >= 0; i--)
        {
            _bodyList[i].child = _bodyList[i + 1].transform;
        }

        _lengthOfBody += 1f * n;
        _bodyList[_bodyList.Count - 1].SetIsTail(true);
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
