using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Vector3 moveDir = Vector3.zero;
    [SerializeField] float speed;

    private void Start()
    {
        Destroy(gameObject, 2f);
    }
    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * moveDir);
    }

    public void SetDirection(Vector3 dir)
    {
        moveDir = dir;
    }
}
