
using UnityEngine;

abstract class TouchableObjectBehaviour : BaseObjectBehaviour
{

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        var character = collision.gameObject.GetComponent<CharacterBehaviour>();

        if (character != null)
            OnCharacterCollision(collision);
    }

    protected void OnCharacterCollision(Collision2D collision)
    {
        if (collision.contactCount != 0)
            HandleContact(collision.contacts[0].point);
    }

    void HandleContact(Vector2 point)
    {
        var localPos = point - ReferencePosition();
        point = localPos.normalized;
        point *= -1;
        if (Mathf.Abs(point.y) > Mathf.Abs(point.x))
        {
            if (point.y > 0)
                OnPlayerContact(Vector2Int.up);
            else
                OnPlayerContact(Vector2Int.down);
        }
        else
        {
            if (point.x > 0)
                OnPlayerContact(Vector2Int.right);
            else
                OnPlayerContact(Vector2Int.left);
        }
    }

    abstract protected Vector2 ReferencePosition();
    abstract protected void OnPlayerContact(Vector2Int dir);
}
