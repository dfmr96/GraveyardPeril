using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AdaptivePerformance.VisualScripting;

public class Bullet : MonoBehaviour
{
    [SerializeField] Vector3 moveDir = Vector3.zero;
    [SerializeField] float speed;
    public float damage;

    private void Start()
    {
        Destroy(gameObject, 1f);
    }
    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * moveDir);
    }

    public void SetDirection(Vector3 dir)
    {
        moveDir = dir;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>() != null)
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
