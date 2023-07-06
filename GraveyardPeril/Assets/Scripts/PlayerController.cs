using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject reticle;
    [SerializeField] GameObject currentWeapon;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float fireRateTimer = 0;
    [SerializeField] PlayerInventory playerInventory;
    [SerializeField] WeaponData currentWeaponData;
    [SerializeField] BoxCollider2D fireableArea;
    [SerializeField] bool isRealoading = false;
    //[SerializeField] Inventory.InventorySlot currentSlot;

    private void Update()
    {
        if (playerInventory.currentWeaponSlot.weaponData == null) return;
        if (playerInventory.currentWeapon == null) return;
        fireRateTimer += Time.deltaTime;
        if (currentWeapon != null)
        {
            Aim();
            if (CanShoot())
            {
                switch (currentWeaponData.weaponFireMode)
                {
                    case WeaponFireMode.auto:
                        if (Input.GetMouseButton(0))
                        {
                            Shoot();
                            fireRateTimer = 0;

                        }
                        break;
                    case WeaponFireMode.semi:
                        if (Input.GetMouseButtonDown(0))
                        {
                            Shoot();
                            fireRateTimer = 0;
                        }
                        break;
                }
            }
        }
    }

    private void Shoot()
    {
        if (playerInventory.currentWeaponSlot.ammoInMagazine > 0)
        {
            playerInventory.currentWeaponSlot.ammoInMagazine--;
            UIAmmoController.OnAmmoValueChanged?.Invoke(playerInventory.currentWeaponSlot);
            spawnPoint = currentWeapon.GetComponent<Weapon>().bulletSpawn.transform;
            Debug.Log("Disparó");
            GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);
            Vector3 bulletDir = (reticle.transform.position - transform.position).normalized;
            bullet.GetComponent<Bullet>().damage = currentWeaponData.damage;
            bullet.GetComponent<Bullet>().SetDirection(bulletDir);
        }
        else
        {
            Reload();
        }
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
        currentWeapon.gameObject.transform.right = dir;
        Vector3 scale = currentWeapon.transform.localScale;
        if (dir.x < 0f)
        {
            scale.y = -1;
        }
        else if (dir.x > 0f)
        {
            scale.y = 1;
        }
        currentWeapon.transform.localScale = scale;
    }

    public bool CanShoot()
    {
        if (currentWeaponData == null) return false;
        if (fireRateTimer > 1 / currentWeaponData.fireRate && ReticleInsideArea())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Reload()
    {
        if (playerInventory.currentWeaponSlot.maxAmmo > 0 && playerInventory.currentWeaponSlot.ammoInMagazine == 0 && !isRealoading)
        {
            isRealoading = true;
            StartCoroutine(ReloadCO());
        }
    }

    public IEnumerator ReloadCO()
    {

        if (playerInventory.currentWeaponSlot.totalAmmo >= playerInventory.currentWeaponSlot.magazineSize)
        {
            yield return new WaitForSeconds(2f);
            playerInventory.currentWeaponSlot.ammoInMagazine = playerInventory.currentWeaponSlot.magazineSize;
            playerInventory.currentWeaponSlot.totalAmmo -= playerInventory.currentWeaponSlot.magazineSize;
            UIAmmoController.OnAmmoValueChanged?.Invoke(playerInventory.currentWeaponSlot);
        } else if (playerInventory.currentWeaponSlot.totalAmmo < playerInventory.currentWeaponSlot.magazineSize)
        {
            yield return new WaitForSeconds(2f);
            playerInventory.currentWeaponSlot.ammoInMagazine = playerInventory.currentWeaponSlot.totalAmmo;
            playerInventory.currentWeaponSlot.totalAmmo = 0;
            UIAmmoController.OnAmmoValueChanged?.Invoke(playerInventory.currentWeaponSlot);
        }
        isRealoading = false;
    }

    public bool ReticleInsideArea()
    {
        if (reticle.transform.position.x < fireableArea.bounds.max.x && reticle.transform.position.x > fireableArea.bounds.min.x &&
            reticle.transform.position.y < fireableArea.bounds.max.y && reticle.transform.position.y > fireableArea.bounds.min.y)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetWeapon(WeaponData weaponData, GameObject weapon)
    {
        currentWeaponData = weaponData;
        currentWeapon = weapon;
        UIAmmoController.OnAmmoValueChanged?.Invoke(playerInventory.currentWeaponSlot);
    }
}
