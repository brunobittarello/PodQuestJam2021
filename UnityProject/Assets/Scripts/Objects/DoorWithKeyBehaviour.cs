using UnityEngine;

public class DoorWithKeyBehaviour : BaseObjectBehaviour
{
    public ParticleSystem funfair;
    public Collider2D collider2d;
    public Sprite openDoorSprite;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision");
        var character = collision.collider.GetComponent<CharacterBehaviour>();
        if (character == null)
            return;

        if (character.hasItem)
            DoTheThing(character);
    }

    void DoTheThing(CharacterBehaviour character)
    {
        var item = character.transform.GetChild(0).GetComponent<CollectableBehaviour>();
        item.transform.position = this.transform.position;//TODO melhorar isso
        item.Show();

        this.gameObject.GetComponent<SpriteRenderer>().sprite = openDoorSprite;
        collider2d.enabled = false;
        if (funfair)
            funfair.Play();
    }
}
