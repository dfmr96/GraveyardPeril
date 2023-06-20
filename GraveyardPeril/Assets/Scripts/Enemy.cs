using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] int damage;
    [SerializeField] float speed;
    [SerializeField] float attackRate;

    private void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }
}
