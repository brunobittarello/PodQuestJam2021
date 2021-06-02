using UnityEngine;

class WindowBehaviour : TouchableObjectBehaviour
{
    public ParticleSystem funfair;
    private Collider2D m_Collider;
    private SpriteRenderer m_Renderer;

    void Start()
    {
        m_Collider = GetComponent<Collider2D>();
        m_Renderer = GetComponent<SpriteRenderer>();
    }

    protected override Vector2 ReferencePosition() => this.transform.position;

    protected override void OnPlayerContact(Vector2Int dir)
    {
        if (CharacterBehaviour.HasAxe)
            MakeTransparent();
    }

    private void MakeTransparent()
    {
        m_Collider.enabled = false;
        Color color = m_Renderer.color;
        Color newColor = new Color(color.r, color.g, color.b, 0f);
        m_Renderer.color = newColor;

        if (funfair != null)
            funfair.Play();
    }
}
