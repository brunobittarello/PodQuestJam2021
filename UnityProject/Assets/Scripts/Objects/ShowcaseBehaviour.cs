
using UnityEngine;

class ShowcaseBehaviour : MonoBehaviour
{
    [SerializeField]
    GameObject openDoor;
    [SerializeField]
    GameObject switchObject;
    Collider2D collider2d;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        collider2d = this.gameObject.GetComponent<Collider2D>();
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision");
        var character = collision.collider.GetComponent<CharacterBehaviour>();
        if (character != null)
            ToggleRemoteControllerPossession();
    }

    void ToggleRemoteControllerPossession()
    {
        var hasPossession = !CharacterBehaviour.instance.HasRemoteController;
        CharacterBehaviour.instance.HasRemoteController = hasPossession;
        spriteRenderer.color = hasPossession ? Color.red : Color.white;
        openDoor.SetActive(!hasPossession);
        switchObject.SetActive(hasPossession);
    }

}
