using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour {

    [Header("General")]
    [SerializeField] LayerMask hittableLayers;
    [SerializeField] GameObject bulletHolePrefab;
    [SerializeField] GameManager ammoText;

    [Header("Shoot Parameters")]
    [SerializeField] float fireRange = 200;
    [SerializeField] float hitForce;
    [SerializeField] float fireRate = 0.6f;
    [SerializeField] AudioClip shot;
    [SerializeField] AudioClip crit;
    [SerializeField] private Transform weaponMuzzle;
    [SerializeField] private GameObject flashEffect;
    [SerializeField] float damage;
    [SerializeField] float critProb;

    [Header ("reload Parameters")]
    [SerializeField] int magazineSize;
    [SerializeField] float reloadTime;
    [SerializeField] AudioClip reloadAudio;
    [SerializeField] AudioClip noAmmoAudio;


    private GameObject owner;
    private float lastTimeShoot = Mathf.NegativeInfinity;
    private int currentAmmo;
    private Transform cameraPlayerTransform;

    // Start is called before the first frame update
    void Start() {
        ammoText = FindObjectOfType<GameManager>();
        ammoText.SetAmmoText("Ammo: " + magazineSize);
        currentAmmo = magazineSize;
        critProb = critProb / 100;
        cameraPlayerTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void Update() {
        if (UIController.CanDoThings()) {
            HandleShoot();
        }
        if(Input.GetKeyDown(KeyCode.R)) {
            StartCoroutine(Reload());
        }
        //transform.localPosition = Vector3.Lerp(transform.position, new Vector3(0.64f, -0.2593f, 0.86f), Time.deltaTime * 5f);
    }

    private void HandleShoot() {
        if (Input.GetMouseButtonDown(0)) {
           if(lastTimeShoot + fireRate < Time.time) {
                if(currentAmmo-- > 0) {
                    ammoText.SetAmmoText("Ammo: " + currentAmmo);
                    Instantiate(flashEffect, weaponMuzzle.position, Quaternion.Euler(weaponMuzzle.forward), transform);
                    AudioClip auxClip = shot;
                    //AddRecoil();
                    RaycastHit hit;
                    if (Physics.Raycast(cameraPlayerTransform.position, cameraPlayerTransform.forward, out hit, fireRange, hittableLayers))
                    {
                        //GameObject bulletHoleClone = Instantiate(bulletHolePrefab, hit.point + hit.normal * 0.00001f, Quaternion.LookRotation(hit.normal));
                        //hit.rigidbody.AddForce(cameraPlayerTransform.forward * recoilForce);

                        if (hit.rigidbody)
                        {
                            int multiplier = 1;
                            if (Random.value < critProb)
                            {
                                multiplier = 2;
                                auxClip = crit;
                            }
                            hit.transform.GetComponent<EnemyHealthManager>().TakeDamage(damage * multiplier);
                            hit.rigidbody.AddForceAtPosition(hitForce * transform.forward, hit.point);
                        }
                        //Destroy(bulletHoleClone, 30f);
                    }
                    AudioSource.PlayClipAtPoint(auxClip, this.transform.position, 0.5f);
                } else {
                    AudioSource.PlayClipAtPoint(noAmmoAudio, this.transform.position, 1f);
                }
            }
            lastTimeShoot = Time.time;
        }
    }

    IEnumerator Reload() {
        ammoText.SetAmmoText("RELOADING");
        AudioSource.PlayClipAtPoint(reloadAudio, this.transform.position, 1f);
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = magazineSize;
        ammoText.SetAmmoText("Ammo: " + currentAmmo);
    }
    private void AddRecoil() {
        transform.Rotate(-hitForce, 0f, 0f);
        transform.position = transform.position - transform.forward * (hitForce / 50f);
    }

    public void SetOwner(GameObject _owner) {
        owner = _owner;
    }

    public GameObject GetOwner() => owner;

    public int GetCurrentAmmo() => currentAmmo;

}
