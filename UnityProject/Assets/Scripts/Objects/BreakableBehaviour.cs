using UnityEngine;

public class BreakableBehaviour : PropObjectBehaviour
{

    public ParticleSystem funfair;
    private Collider2D m_Collider;
    private SpriteRenderer m_Renderer;

    private void Start()
    {
        m_Collider = GetComponent<Collider2D>();
        m_Renderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CharacterBehaviour character = collision.gameObject.GetComponent<CharacterBehaviour>();

        if (character != null && character.CanBreak)
            MakeTransparent();

        var car = collision.gameObject.GetComponent<RunStraightBehaviour>();

        if (car != null)
        {
            MakeTransparent();
            car.DestroyObject();
        }
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
