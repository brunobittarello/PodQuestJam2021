using UnityEngine;

public class DoorWithKeyBehaviour : BaseObjectBehaviour, IDestructible
{
    public ParticleSystem funfair;
    public Collider2D collider2d;
    public Sprite openDoorSprite;
    public PopUpBehaviour popupItem;

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
        popupItem.Show();

        this.gameObject.GetComponent<SpriteRenderer>().sprite = openDoorSprite;
        collider2d.enabled = false;
        if (funfair)
            funfair.Play();
    }

    public void DestroyObject()
    {
        GameObject.Destroy(this.transform.parent.gameObject);
    }
}
