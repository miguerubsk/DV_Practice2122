using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour {

    public List<WeaponController> startingWeapons = new List<WeaponController>();

    public Transform weaponParentSocket;
    public Transform defaultWeaponPosition;
    public Transform aimingPosition;
    public int activeWeaponIndex { get; private set; }
    private WeaponController[] weaponSlots = new WeaponController[1];

    // Start is called before the first frame update
    void Start() {
        activeWeaponIndex = -1;
        foreach(WeaponController startingWeapon in startingWeapons) {
            AddWeapon(startingWeapon);
        }
        SwitchWeapon(0);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            SwitchWeapon(0);
        }
    }

    private void SwitchWeapon(int weaponIndex) {
        if(weaponIndex != activeWeaponIndex && weaponIndex >= 0) {
            if(activeWeaponIndex >= 0) {
                weaponSlots[activeWeaponIndex].gameObject.SetActive(false);
            }
            weaponSlots[weaponIndex].gameObject.SetActive(true);
            activeWeaponIndex = weaponIndex;
        }
    }

    private void AddWeapon(WeaponController weaponPrefabs) {
        weaponParentSocket.position = defaultWeaponPosition.position;
        for(int i = 0; i < weaponSlots.Length; ++i) {
            if(weaponSlots[i] == null) {
                WeaponController weaponClone = Instantiate(weaponPrefabs, weaponParentSocket);
                weaponClone.SetOwner(this.gameObject);
                weaponClone.gameObject.SetActive(false);
                weaponSlots[i] = weaponClone;
                return;
            }
        }
    }

}
