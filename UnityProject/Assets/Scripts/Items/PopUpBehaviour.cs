using UnityEngine;

public class PopUpBehaviour : MonoBehaviour
{
    public SpriteRenderer spriteRender;

    [SerializeField]
    protected float speed;
    [SerializeField]
    protected float time;
    float timer;

    void Update()
    {
        timer -= Time.deltaTime;
        Animate();

        if (timer < 0) { 
            this.gameObject.SetActive(false);
        }
    }

    void Animate()
    {
        this.transform.position = this.transform.position + Vector3.up * Time.deltaTime * speed;
    }

    public void Show()
    {
        timer = time;
        this.gameObject.SetActive(true);
    }
}
