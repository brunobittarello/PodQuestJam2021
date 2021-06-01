using UnityEngine;

public class MapBehaviour : BaseObjectBehaviour
{
    private Collider2D m_Collider;
    private SpriteRenderer m_Renderer;

    [SerializeReference]
    GameObject map;
    bool isMapEnabled;

    void Start()
    {
        m_Collider = GetComponent<Collider2D>();
        m_Renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!isMapEnabled || CharacterBehaviour.instance == null)
            return;

        if (Vector3.SqrMagnitude(this.transform.position - CharacterBehaviour.instance.transform.position) > 1)
        {
            isMapEnabled = false;
            map.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var character = collision.gameObject.GetComponent<CharacterBehaviour>();

        if (character != null)
        {
            isMapEnabled = true;
            map.SetActive(true);
        }
    }

}
