using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

    [Header("General")]
    [SerializeField] LayerMask hittableLayers;
    [SerializeField] GameObject bulletHolePrefab;

    [Header("Shoot Parameters")]
    [SerializeField] float fireRange = 200;
    [SerializeField] float hitForce;
    [SerializeField] float fireRate = 0.6f;
    [SerializeField] AudioClip shot;
    [SerializeField] private Transform weaponMuzzle;
    [SerializeField] private GameObject flashEffect;
    [SerializeField] float damage;


    private GameObject owner;
    private float lastTimeShoot = Mathf.NegativeInfinity;
    private Transform cameraPlayerTransform;

    // Start is called before the first frame update
    void Start() {
        cameraPlayerTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void Update() {
        if (UIController.CanDoThings()) {
            HandleShoot();
        }
        //transform.localPosition = Vector3.Lerp(transform.position, new Vector3(0.64f, -0.2593f, 0.86f), Time.deltaTime * 5f);
    }

    private void HandleShoot() {
        if (Input.GetMouseButtonDown(0)) {
           if(lastTimeShoot + fireRate < Time.time) {
                Instantiate(flashEffect, weaponMuzzle.position, Quaternion.Euler(weaponMuzzle.forward), transform);
                AudioSource.PlayClipAtPoint(shot, this.transform.position, 0.5f);
                Debug.Log("Pressed primary button.");
                //AddRecoil();
                RaycastHit hit;
                if (Physics.Raycast(cameraPlayerTransform.position, cameraPlayerTransform.forward, out hit, fireRange, hittableLayers)) {
                    //GameObject bulletHoleClone = Instantiate(bulletHolePrefab, hit.point + hit.normal * 0.00001f, Quaternion.LookRotation(hit.normal));
                    //hit.rigidbody.AddForce(cameraPlayerTransform.forward * recoilForce);
                    
                    if (hit.rigidbody) {
                        hit.transform.GetComponent<EnemyHealthManager>().TakeDamage(damage);
                        hit.rigidbody.AddForceAtPosition(hitForce * transform.forward, hit.point);
                    }
                    //Destroy(bulletHoleClone, 30f);
                }
            }
            lastTimeShoot = Time.time;
        }
    }

    private void AddRecoil() {
        transform.Rotate(-hitForce, 0f, 0f);
        transform.position = transform.position - transform.forward * (hitForce / 50f);
    }

    public void SetOwner(GameObject _owner) {
        owner = _owner;
    }

    public GameObject GetOwner() => owner;

}
