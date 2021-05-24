using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBehaviour : MonoBehaviour
{
    const float SPEED = 1;
    const float TIMER = 1;

    float timer;

    void Update()
    {
        timer -= Time.deltaTime;
        Animate();

        if (timer < 0) { 
            this.transform.SetParent(CharacterBehaviour.instance.transform);
            this.gameObject.SetActive(false);
        }
    }

    void Animate()
    {
        this.transform.position = this.transform.position + Vector3.up * Time.deltaTime * SPEED;
    }

    public void Show()
    {
        timer = TIMER;
        this.gameObject.SetActive(true);
    }
}
