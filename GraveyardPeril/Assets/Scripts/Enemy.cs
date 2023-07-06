using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyState
    {
        moving, attacking, dying, dead
    }

    [SerializeField] LayerMask layerMask;
    [SerializeField] EnemyState enemyState;
    [SerializeField] float health;
    [SerializeField] int damage;
    [SerializeField] float speed;
    [SerializeField] GameObject target;
    [SerializeField] float attackRate;
    [SerializeField] float attackTimer;
    [SerializeField] float viewDistance;


    private void Start()
    {
        enemyState = EnemyState.moving;
    }

    private void OnEnable()
    {
        GameManager.instance.EnemySpawn();
    }

    private void OnDisable()
    {
        GameManager.instance.EnemyDestroyed();
    }

    private void Update()
    {
        LookForward();
        switch (enemyState)
        {
            case EnemyState.moving:
                Move();
                break;
            case EnemyState.attacking:
                Attack();
                break;
        }
    }

    public void Attack()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer > 1 / attackRate) 
        {
            if (target.GetComponent<PlayerStats>()  != null)
            {
                target.GetComponent<PlayerStats>().TakeDamage(damage);
                attackTimer = 0;
            }
        }
    }

    public void Move()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    public void LookForward()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector3.up, viewDistance, layerMask);
        if (hit.collider != null)
        {
            //Debug.Log(hit.distance);
            //Debug.Log("Chocó con: " + hit.collider.gameObject.name);
            target = hit.collider.gameObject;
            enemyState = EnemyState.attacking;
        } else
        {
            target = null;
            enemyState = EnemyState.moving;
        }
    }

    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health < 1)
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, -Vector3.up * viewDistance);
    }
}
