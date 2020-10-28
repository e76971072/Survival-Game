using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public float dmg = 35f;
    public float range = 100f;
    public float fireRate = 10f;
    public Camera playerCamera;
    public ParticleSystem muzzleFlash;
    private float timeToFire = 0f;
    public GameObject impact;
    public float bulletForce = 30f;

    public int maxAmmoCapacity = 30;
    private int currentClipAmmo;
    public float reloadTime = 1f;

    private bool reloading = false;
    private bool isScoped = false;

    public Animator animator;
    public GameObject sniperScope, crosshair;
    public GameObject weaponCamera;
    public Text ammo;
    public Text reload;
    public Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        currentClipAmmo = maxAmmoCapacity;
        ammo.text = "" + currentClipAmmo + "/" + maxAmmoCapacity;
    }
    void OnEnable()
    {
        reloading = false;

        animator.SetBool("Reloading", false);
    }

    // Update is called once per frame
    void Update()
    {
        ammo.text = "" + currentClipAmmo + "/" + maxAmmoCapacity;

        if (reloading)
        {
            return;
        }

        if (currentClipAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (currentClipAmmo < (maxAmmoCapacity/2) / 2)
        {
            reload.gameObject.SetActive(true);
        }

        if (Input.GetButton("Fire1") && Time.time >= timeToFire)
        {
            timeToFire = Time.time + 1f / fireRate;
            gameObject.GetComponent<AudioSource>().Play();
            Fire();
        }

        if (Input.GetKeyDown(KeyCode.R) && currentClipAmmo != maxAmmoCapacity)
        {
            StartCoroutine(Reload());
        }

        if (Input.GetButtonDown("Fire2"))
        {
            isScoped = !isScoped;

            sniperScope.SetActive(isScoped);
            crosshair.SetActive(!isScoped);

            weaponCamera.SetActive(!isScoped);

            if (gameObject.name.Equals("Sniper_04") && isScoped)
            {
                mainCamera.fieldOfView = 15f;
            }
            else if (gameObject.name.Equals("Pistol_01") && isScoped)
            {
                mainCamera.fieldOfView = 50f;
            }
            else if (gameObject.name.Equals("Heavy") && isScoped)
            {
                mainCamera.fieldOfView = 40f;
            }
            else
                mainCamera.fieldOfView = 60f;
        }
    }

    // Use raycasting here
    void Fire()
    {
        muzzleFlash.Play();
        
        RaycastHit hitInfo;

        currentClipAmmo--;

        ammo.text = "" + currentClipAmmo + "/" + maxAmmoCapacity;

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hitInfo, range))
        {

            Enemy target = hitInfo.transform.GetComponent<Enemy>();

            if (target != null)
            {
                target.DamageTaken(dmg);
            }

            if (hitInfo.rigidbody != null)
                hitInfo.rigidbody.AddForce(-hitInfo.normal * bulletForce);

            GameObject impactDestroy = Instantiate(impact, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            Destroy(impactDestroy, 2f);
        }

    }

    IEnumerator Reload()
    {
        reloading = true;

        Debug.Log("Reloading . . .");

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - .25f);

        animator.SetBool("Reloading", false);

        yield return new WaitForSeconds(.25f);

        currentClipAmmo = maxAmmoCapacity;
        ammo.text = "" + currentClipAmmo + "/" + maxAmmoCapacity;
        reload.gameObject.SetActive(false);

        reloading = false;
    }

}
