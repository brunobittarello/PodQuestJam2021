
using UnityEngine;

class MovableBoxBehaviour : BaseObjectBehaviour
{
    [SerializeField]
    RectInt limitArea;

    [SerializeField]
    float speed = 3;

    Transform targetTransform;
    bool isMoving;
    Vector2Int targetPos;

    private void Start()
    {
        targetTransform = this.transform.parent;
    }

    void Update()
    {
        if (isMoving)
            Move();
    }

    void Move()
    {
        var pos = Vector2.MoveTowards(targetTransform.position, targetPos, speed * Time.deltaTime);
        if (Vector2.SqrMagnitude(targetPos - pos) > 0.1f)
            targetTransform.position = pos;
        else
        {
            targetTransform.position = (Vector2)targetPos;
            isMoving = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        CharacterBehaviour character = collision.gameObject.GetComponent<CharacterBehaviour>();

        if (character != null && collision.contactCount != 0)
            TryToMove(collision.contacts[0].point);
    }

    void TryToMove(Vector2 point)
    {
        var localPos = point - (Vector2)targetTransform.position;
        point = localPos.normalized;
        point *= -1;
        if (Mathf.Abs(point.y) > Mathf.Abs(point.x))
        {
            if (point.y > 0)
                TryToMoveByDirection(Vector2Int.up);
            else
                TryToMoveByDirection(Vector2Int.down);
        }
        else
        {
            if (point.x > 0)
                TryToMoveByDirection(Vector2Int.right);
            else
                TryToMoveByDirection(Vector2Int.left);
        }

    }

    void TryToMoveByDirection(Vector2Int dir)
    {
        var intPos = Vector2Int.RoundToInt(targetTransform.position);
        var newPos = (intPos + dir);
        if (!limitArea.Contains(newPos))
            return;

        isMoving = true;
        targetPos = newPos;
    }
}
