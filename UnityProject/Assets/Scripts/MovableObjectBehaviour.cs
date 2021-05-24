using UnityEngine;

public class MovableObjectBehaviour : InstableObjectBehaviour
{
    const float SPEED = 4f;

    protected override void EnableObjectFunction()
    {
        collider2d.enabled = false;
        if (collectable != null)
        {
            collectable.Show();
            CharacterBehaviour.instance.hasItem = true;
        }
    }
}
