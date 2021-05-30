using UnityEngine;

public class CharacterInTheCarBehavior : CharacterBehaviour
{

    public bool InTheCar = false;
    public GameObject Car;

    private SpriteRenderer m_CarSpriteRenderer;
    private SpriteRenderer m_CharacterSpriteRenderer;

    protected override void Start()
    {
        base.Start();
        m_CarSpriteRenderer = Car.GetComponentInChildren<SpriteRenderer>();
        m_CharacterSpriteRenderer = GetComponent<SpriteRenderer>();
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

            return m_CarSpriteRenderer.sprite;
        }
        return base.GetMovingSprite(direction);
    }

    protected override Sprite GetIdleSprite(Vector2Int direction)
    {
        if (InTheCar)
        {
            return m_CarSpriteRenderer.sprite;
        }
        return base.GetIdleSprite(direction);
    }

    public void GetInTherCar()
    {
        InTheCar = true;
        m_CharacterSpriteRenderer.enabled = false;
        Car.SetActive(true);
        CanBreak = true;
    }


}
