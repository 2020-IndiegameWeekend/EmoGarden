using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoFish : MonoBehaviour
{
    [SerializeField]
    private float distance;
    [SerializeField]
    private float _moveSpeed;
    [SerializeField]
    private float _moveRange;

    [SerializeField]
    private bool _isRightMove;

    private float checkDistance;

    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        checkDistance = _moveRange * distance;
        _spriteRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine(MoveCoroutine());
    }

    private IEnumerator MoveCoroutine()
    {
        while (true)
        {
            Vector2 destination = (Vector2)transform.position;

            if (_isRightMove)
            {
                destination.x -= _moveRange;
                _spriteRenderer.flipX = false;
            }
            else
            {
                destination.x += _moveRange;
                _spriteRenderer.flipX = true;
            }

            while (Vector2.Distance(transform.position, destination) >= checkDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, destination, _moveSpeed * Time.deltaTime);
                yield return null;
            }

            _isRightMove = !_isRightMove;
        }
    }
}
