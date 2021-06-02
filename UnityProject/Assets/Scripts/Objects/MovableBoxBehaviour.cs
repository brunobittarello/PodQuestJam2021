
using UnityEngine;

class MovableBoxBehaviour : TouchableObjectBehaviour
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

    protected override Vector2 ReferencePosition() => targetTransform.position;

    protected override void OnPlayerContact(Vector2Int dir)
    {
        var intPos = Vector2Int.RoundToInt(targetTransform.position);
        var newPos = (intPos + dir);
        if (!limitArea.Contains(newPos))
            return;

        var collider = Physics2D.OverlapBox(newPos, Vector2.one * 0.2f, 0);
        if (collider != null)
            return;

        isMoving = true;
        targetPos = newPos;
    }
}
