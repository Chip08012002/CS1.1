
using System.Collections;
using TMPro;
using UnityEngine;


public class Weapon : MonoBehaviour
{
    internal Animator anim;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed = 500f;
    public float bulletLifeTime = 3f;

    //Shooting
    public bool isShooting;
    public bool readyToShoot;
    bool allowReset = true;

    public float shootingDelay = 0.2f;

    //Burst
    public int bulletsPerBurst = 3;
    private int burstBulletsLeft;

    public float spreadIntensity = 0f;

    //Muzze Effect
    public GameObject muzzleEffect;

    // Reload magazine
    public float bulletTotal;
    public float bulletBurst;
    public float reloadTime;
    bool isReloading;
    public bool isWeaponActive;

    //Spawn Position
    public Vector3 spawnPosition;
    public Vector3 spawnRotation;

    public enum WeaponModel
    {
        Ak47,
        M1911,
    }
    public WeaponModel thisWeaponModel;

    public enum ShootingMode
    {
        Auto,
        Single,
        Burst,
    }
    public ShootingMode currentShootingMode;



    void Awake()
    {
        bulletBurst = bulletTotal;

    }

    void Start()
    {
        readyToShoot = true;
        burstBulletsLeft = bulletsPerBurst;
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        HandleInputShooting();

        //Play EmptyMagazineClip when out of bullet

        if (isWeaponActive)
        {
            if (bulletBurst == 0 && isShooting)
            {
                if (SoundManager.Instance != null && SoundManager.Instance.emptyManagizeSound != null)
                {
                    SoundManager.Instance.emptyManagizeSound.Play();
                }
                else
                {
                    Debug.LogWarning("SoundManager instance or emptyManagizeSound is null.");
                }
            }

            if (readyToShoot && isShooting && bulletBurst > 0)
            {
                Fire();
                readyToShoot = false;
                isShooting = false;
            }

            //Reload magazine

            if (Input.GetKeyDown(KeyCode.R) && !isReloading && bulletBurst < bulletTotal)
            {
                Reload();
                SoundManager.Instance.PlayReloadSound(thisWeaponModel);
            }

            //Automatically Reload
            if (!isReloading && bulletBurst <= 0 && !readyToShoot && !isShooting)
            {
                // Reload();
                // soundManager.PlayReloadClip();
            }

        }
    }

    void HandleInputShooting()
    {
        if (currentShootingMode == ShootingMode.Burst)
        {
            isShooting = Input.GetKeyDown(KeyCode.Mouse0);
        }
        else if (currentShootingMode == ShootingMode.Auto)
        {
            isShooting = Input.GetKey(KeyCode.Mouse0);
        }
        else if (currentShootingMode == ShootingMode.Single)
        {
            isShooting = Input.GetKeyDown(KeyCode.Mouse0);
        }
    }

    void Fire()
    {
        //Reload Magazine
        bulletBurst--;


        anim.SetTrigger("RECOIL");
        muzzleEffect.GetComponent<ParticleSystem>().Play();
        SoundManager.Instance.PlayShootingSound(thisWeaponModel);

        readyToShoot = false;

        Vector3 shootingDirection = CaculateDirectionAndSpread().normalized;

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);

        bullet.transform.forward = shootingDirection;

        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletSpeed, ForceMode.Impulse);

        StartCoroutine(DestroyBulletAfterLifeTime(bullet, bulletLifeTime));

        if (currentShootingMode == ShootingMode.Burst)
        {
            burstBulletsLeft--;

            if (burstBulletsLeft > 0)
            {
                Invoke("Fire", shootingDelay);
            }
            else
            {
                burstBulletsLeft = bulletsPerBurst; // Reset burst bullet count
                Invoke("ResetShoot", shootingDelay);
            }
        }
        else
        {
            Invoke("ResetShoot", shootingDelay);
        }
    }


    private void Reload()
    {
        anim.SetTrigger("RELOAD");
        isReloading = true;

        Invoke("ReloadCompleted", reloadTime);
    }

    private void ReloadCompleted()
    {
        bulletBurst = bulletTotal;
        isReloading = false;
    }



    private void ResetShoot()
    {
        readyToShoot = true;
        allowReset = true;
    }

    public Vector3 CaculateDirectionAndSpread()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;

        //Hit something
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(100);
        }

        Vector3 direction = targetPoint - bulletSpawn.position;
        float x = Random.Range(-spreadIntensity, spreadIntensity);
        float y = Random.Range(-spreadIntensity, spreadIntensity);

        return direction + new Vector3(x, y, 0);
    }

    private IEnumerator DestroyBulletAfterLifeTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
}