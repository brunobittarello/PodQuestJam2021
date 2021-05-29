using UnityEngine;

public class RunStraightBehaviour : BaseObjectBehaviour, IDestructible, IInterrupter
{
    public bool isTurnedOn;
    public int speed;
    public Vector2Int direction;
    public Collider2D collider2d;

    Vector3 deltaMovement;

    void Start()
    {
        deltaMovement = (Vector2)direction * speed;
    }

    void Update()
    {
        if (isTurnedOn)
            Move();
    }

    void Move()
    {
        //Move tudo
        this.transform.parent.position = this.transform.parent.position + deltaMovement * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision");
        var character = collision.collider.GetComponent<CharacterBehaviour>();
        if (character != null)
        {
            TurnOn();
            return;
        }

        TurnOff();
        var destructible = collision.collider.GetComponent<IDestructible>();
        if (destructible == null)
            return;

        destructible.DestroyObject();
        DestroyObject();
    }

    public void TurnOn()
    {
        isTurnedOn = true;
    }

    public void TurnOff()
    {
        isTurnedOn = false;        
        this.transform.parent.position = Vector3Int.RoundToInt(this.transform.parent.position);
    }

    public void DestroyObject()
    {
        GameObject.Destroy(this.transform.parent.gameObject);
    }
}
