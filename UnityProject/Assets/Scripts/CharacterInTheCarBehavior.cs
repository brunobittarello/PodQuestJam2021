using UnityEngine;

public class CharacterInTheCarBehavior : CharacterBehaviour
{

    public Sprite CarSprite;
    public bool InTheCar = false;
    private Collider2D m_CarCollider;

    protected override void Start()
    {
        base.Start();
        m_CarCollider = GetComponent<BoxCollider2D>();
    }

    protected override Sprite GetMovingSprite(Vector2Int direction)
    {
        if (InTheCar)
        {

            if (direction == Vector2Int.left)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, -90f);

            }
            else if (direction == Vector2Int.right)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 90f);

            }
            else if (direction == Vector2Int.up)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 180f);

            }
            else if (direction == Vector2Int.down)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }

            return CarSprite;
        }
        return base.GetMovingSprite(direction);
    }

    protected override Sprite GetIdleSprite(Vector2Int direction)
    {
        if (InTheCar)
        {
            return CarSprite;
        }
        return base.GetIdleSprite(direction);
    }

    public void GetInTherCar()
    {
        InTheCar = true;
        m_CarCollider.enabled = true;
        CanBreak = true;
    }


}
