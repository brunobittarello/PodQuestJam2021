using UnityEngine;

class RunStraightBehaviour : TouchableObjectBehaviour, IDestructible, IInterrupter
{
    public bool isTurnedOn;
    public int speed;
    public Vector2Int direction;
    public Collider2D collider2d;
    private CyclicAnimation ca;

    Vector3 deltaMovement;
    bool canBeMoved;

    void Start()
    {
        deltaMovement = (Vector2)direction * speed;
        ca = gameObject.GetComponent<CyclicAnimation>();
        ca.enabled = false;
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

    protected override Vector2 ReferencePosition() => this.transform.position;

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision");
        if (!canBeMoved)
            return;

        var character = collision.collider.GetComponent<CharacterBehaviour>();
        if (character != null)
        {
            base.OnCharacterCollision(collision);
            return;
        }

        TurnOff();
        var destructible = collision.collider.GetComponent<IDestructible>();
        if (destructible == null)
            return;

        destructible.DestroyObject();
        DestroyObject();
    }

    protected override void OnPlayerContact(Vector2Int dir)
    {
        if (dir == Vector2Int.up && canBeMoved)
            TurnOn();
    }

    public void TurnOn()
    {
        ca.enabled = true;
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Ball/BallRoll", transform.position);        
        isTurnedOn = true;
    }

    public void TurnOff()
    {
        isTurnedOn = false;
        this.transform.parent.position = Vector3Int.RoundToInt(this.transform.parent.position);
    }

    internal override void OnPlayerTargetExit()
    {
        canBeMoved = true;
    }

    public void DestroyObject()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Ball/WallBreak", transform.position);
        GameObject.Destroy(this.transform.parent.gameObject);
    }
}
