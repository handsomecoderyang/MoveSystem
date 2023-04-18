using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damage;
    public float time;

    private Animator m_animator;
    private PolygonCollider2D m_polygonCollider;

    public float startTime;

    // Start is called before the first frame update
    void Start()
    {
        m_animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        m_polygonCollider = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if (Input.GetButtonDown("Attack"))
        {
            m_animator.SetTrigger("Attack");
            StartCoroutine(StartAttack());

        }
    }
    IEnumerator disableHitBox()
    {
        yield return new WaitForSeconds(time);
        m_polygonCollider.enabled=false;

    }

    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(startTime);
        m_polygonCollider.enabled = true;
        StartCoroutine(disableHitBox());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
        }

    }
}
