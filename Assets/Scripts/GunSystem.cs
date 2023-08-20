using UnityEngine;
using TMPro;
using EZCameraShake;

public class GunSystem : MonoBehaviour
{
    //Gun stats
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots, adsSpeed = 5f;
    public int magazineSize, bulletsPerTap, magazineAmount, totalAmmo, scopePower;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    //bools 
    bool shooting, readyToShoot, reloading;

    //Reference
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    //Graphics
    public GameObject muzzleFlash, bulletHoleGraphic;
    public TextMeshProUGUI text;

    private void Start()
    {
        totalAmmo = magazineSize * magazineAmount;
    }

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }
    private void Update()
    {
        MyInput();
        Aiming();
        UpdateAmmo();
        

        // Reload if no ammo left in magazine
        if (bulletsLeft <= 0 && !reloading && totalAmmo > 0)
        {
            Reload();
        }
    }

    public void UpdateAmmo()
    {
        text.SetText(bulletsLeft + " / " + totalAmmo);
    }
    private void MyInput()
    {
        muzzleFlash.SetActive(false);
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();

        //Shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            Shoot();
        }
    }

    private void Aiming()
    {
        // Aiming by holding right mouse button
        if (Input.GetKey(KeyCode.Mouse1))
        {
            fpsCam.fieldOfView = Mathf.Lerp(fpsCam.fieldOfView, scopePower, adsSpeed * Time.deltaTime);
        }
        else
        {
            fpsCam.fieldOfView = Mathf.Lerp(fpsCam.fieldOfView, 60f, adsSpeed * Time.deltaTime);
        }
    }
    private void Shoot()
    {
        readyToShoot = false;

        //Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculate Direction with Spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

        Ray ray = fpsCam.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
        ray.origin = fpsCam.transform.position;

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log("WE'RE SHOOTING!!!!!!!!!" + hit.collider.gameObject.name);
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {

                hit.transform.parent.gameObject.GetComponent<EnemyAi>().TakeDamage(damage);
            }
            GameObject bulletImpactObject = Instantiate(bulletHoleGraphic, hit.point + hit.normal * .002f, Quaternion.LookRotation(hit.normal, Vector3.up));
            Destroy(bulletImpactObject, 10f);
        }


        //Graphics
        //ShakeCamera
        CameraShaker.Instance.ShakeOnce(0.1f, 0.1f, .1f, 1f);
        // Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
        muzzleFlash.SetActive(true);

        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);

        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);
    }
    private void ResetShot()
    {
        readyToShoot = true;
    }
    private void Reload()
    {
        reloading = true;
        if (totalAmmo > 0)
        {
            Invoke("ReloadFinished", reloadTime);

        }
        else
        {
            reloading = false;
        }

    }
    private void ReloadFinished()
    {
        int temp = totalAmmo;
        totalAmmo -= magazineSize - bulletsLeft;
        if (totalAmmo < 0)
        {
            totalAmmo = 0;
        }

        if (totalAmmo >= magazineSize)
        {
            bulletsLeft += magazineSize - bulletsLeft;
        }
        else
        {
            if (magazineSize - bulletsLeft >= totalAmmo)
            {
                if (totalAmmo <= 0)
                {
                    bulletsLeft += temp;
                }
                else
                {
                    bulletsLeft += totalAmmo;
                }
            }
            else
            {
                bulletsLeft = magazineSize;
            }
        }

        reloading = false;
    }
}