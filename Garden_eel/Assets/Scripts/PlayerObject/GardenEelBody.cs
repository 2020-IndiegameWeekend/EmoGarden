using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenEelBody : MonoBehaviour
{
    public int speed;
    public float spaceOfBody;
    public bool isHead;
    public Transform parent;
    
    private Vector3 _target;
    private Rigidbody2D _rigidbody2D;
    
    void Start()
    {
        if (!isHead)
        {
            _target = parent.position;
        }
        else
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
    }

    void FixedUpdate()
    {
        if (isHead)
        {
            _target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            var velocity = _rigidbody2D.velocity;
            if (velocity.y < -1f)
            {
                velocity.y = -1f;
                _rigidbody2D.velocity = velocity;
            }
        }
        else
        {
            _target = parent.position;
        }

        if (Input.GetMouseButton(0) || Input.touchCount > 0 || !isHead)
        {
            MoveToTarget(_target);
        }
    }

    private bool MoveToTarget(Vector3 target)
    {
        target.z = transform.position.z;
        Vector2 move = (Vector2) (target - transform.position);
            
        if (move.sqrMagnitude > spaceOfBody)
        {
            transform.Translate(move.normalized * Time.deltaTime * speed);
            return true;
        }

        return false;
    }
}
