
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
        this.transform.GetChild(0).gameObject.SetActive(!hasPossession);
        CharacterBehaviour.instance.HasRemoteController = hasPossession;
        openDoor.SetActive(!hasPossession);
        switchObject.SetActive(hasPossession);
    }

}
