using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int damage;
    public int health;

    private SpriteRenderer m_SpriteRenderer;
    private Color originalColor;

    public float flashTime;

    // Start is called before the first frame update
    public void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = m_SpriteRenderer.color;
    }

    // Update is called once per frame
    public void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }

    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        FlashColor(flashTime);
    }
    void FlashColor(float time)
    {
        m_SpriteRenderer.color = Color.red;
        //在time之后调用ResetColor方法
        Invoke("ResetColor", time);
    }

    void ResetColor()
    {
        m_SpriteRenderer.color = originalColor;
    }
}
