using UnityEngine;

class DrawerBehaviour : TouchableObjectBehaviour
{
    public PopUpBehaviour collectable;
    public ParticleSystem funfair;
    public Collider2D collider2d;

    bool itemIsGone;

    protected override Vector2 ReferencePosition() => this.transform.position;

    protected override void OnPlayerContact(Vector2Int dir)
    {
        if (!itemIsGone)
            GiveItem();
    }

    void GiveItem()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Key/KeyGet", transform.position);
        var character = CharacterBehaviour.instance;
        itemIsGone = true;
        character.hasItem = true;
        var item = GameObject.Instantiate(collectable, character.transform);
        item.transform.position = this.transform.position;//TODO melhorar isso
        item.Show();

        if (funfair != null)
            funfair.Play();
    }
}
