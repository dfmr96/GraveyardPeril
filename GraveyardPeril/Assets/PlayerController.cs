using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject reticle;
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform spawnPoint;
    private void Update()
    {
        Aim();

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);
        Vector3 bulletDir = (reticle.transform.position - transform.position).normalized;
        bullet.GetComponent<Bullet>().SetDirection(bulletDir);
    }

    public void Aim()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;
        reticle.transform.position = mouseWorldPos;
        RotateWeapon();
        Debug.DrawLine(transform.position, mouseWorldPos);
    }

    public void RotateWeapon()
    {
        Vector3 dir = (reticle.transform.position - transform.position).normalized;
        weapon.transform.right = dir;
        
        Vector3 scale = weapon.transform.localScale;
        if (dir.x < 0f)
        {
            scale.y = -1;
        } else if (dir.x > 0f) 
        {
            scale.y = 1;
        }
        weapon.transform.localScale = scale;
    }
}
