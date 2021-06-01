using UnityEngine;

public class DrawerBehaviour : BaseObjectBehaviour
{
    public PopUpBehaviour collectable;
    public ParticleSystem funfair;
    public Collider2D collider2d;

    bool itemIsGone;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision");
        if (itemIsGone) return;

        var character = collision.collider.GetComponent<CharacterBehaviour>();
        if (character == null)
            return;

        DoTheThing(character);
    }

    void DoTheThing(CharacterBehaviour character)
    {
        itemIsGone = true;
        character.hasItem = true;
        var item = GameObject.Instantiate(collectable, character.transform);
        item.transform.position = this.transform.position;//TODO melhorar isso
        item.Show();

        if (funfair != null)
            funfair.Play();
    }
}
