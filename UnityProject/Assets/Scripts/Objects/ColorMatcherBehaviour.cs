using UnityEngine;
using UnityEngine.Events;

public class ColorMatcherBehaviour : MonoBehaviour
{

    public Color Color;
    public bool IsController;
    public UnityEvent OnAllColorMathersMatch;

    public string ColliderTagName = "NPC";

    public ColorMatcherBehaviour Controller;

    [SerializeField]
    private bool Matched = false;
    [SerializeField]
    private int ColorMatcherCounter = 0;
    [SerializeField]
    private int ColorMatchCounter = 0;

    #region ColorMatcherController
    public void RegisterColorMatcher(ColorMatcherBehaviour colorMatcher) => ColorMatcherCounter++;

    public void NotifyMatch(ColorMatcherBehaviour colorMatcher)
    {
        ColorMatchCounter++;
        if (ColorMatchCounter == ColorMatcherCounter)
        {
            if (OnAllColorMathersMatch != null)
            {
                OnAllColorMathersMatch.Invoke();
            }
        }
    }
    #endregion

    #region ColorMatcher
    // Start is called just before any of the Update methods is called the first time
    private void Start()
    {
        if (!IsController && Controller != null)
        {
            Controller.RegisterColorMatcher(this);
        }
    }

    // OnTriggerEnter2D is called when the Collider2D other enters the trigger (2D physics only)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsController && !Matched)
        {
            if (collision.CompareTag(ColliderTagName))
            {
                SpriteRenderer spriteRenderer = collision.gameObject.GetComponent<SpriteRenderer>();

                if (spriteRenderer != null)
                {
                    if (Color.ToString() == spriteRenderer.color.ToString())
                    {
                        Matched = true;
                        if (Controller != null) 
                        {
                            Controller.NotifyMatch(this);
                        }
                    }
                }
            }
        }
    }
    #endregion


}
