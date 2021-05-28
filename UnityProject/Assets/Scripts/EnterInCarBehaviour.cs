using UnityEngine;

public class EnterInCarBehaviour : BaseObjectBehaviour
{
    public string PlayerTagName = "Player";

    private CharacterInTheCarBehavior m_Character;

    private void Start()
    {
        GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
        if (gameObject != null)
        {
            m_Character = gameObject.GetComponent<CharacterInTheCarBehavior>();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var character = collision.gameObject.GetComponent<CharacterBehaviour>();
        if (character == null)
            return;

        m_Character.GetInTherCar();
        Destroy(gameObject);
    }

}
