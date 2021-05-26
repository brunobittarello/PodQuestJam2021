using UnityEngine;

public class DoorWithKeyBehaviour : BaseObjectBehaviour
{
    public ParticleSystem funfair;
    public Collider2D collider2d;

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
        item.transform.position = this.transform.position;
        item.Show();
        collider2d.enabled = false;
        funfair.Play();
    }
}
