using UnityEngine;

public class DoorOpenBehaviour : BaseObjectBehaviour
{
    public ParticleSystem funfair;
    public Collider2D collider2d;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision");
        var character = collision.collider.GetComponent<CharacterBehaviour>();
        if (character == null)
            return;

        DoTheThing(character);
    }

    void DoTheThing(CharacterBehaviour character)
    {
        collider2d.enabled = false;
        funfair.Play();
    }
}
