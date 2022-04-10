using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sway : MonoBehaviour {

    private Quaternion originLocalRotation;
    public float swayStrength;
    private float sway;

    // Start is called before the first frame update
    void Start() {
        originLocalRotation = transform.localRotation;
        sway = swayStrength / 100 + 1;
    }

    // Update is called once per frame
    void Update() {
        updateSway();
    }

    private void updateSway() {
        float xLookInput = Input.GetAxis("Mouse X");
        float yLookInput = Input.GetAxis("Mouse Y");

        Quaternion xAngleAdjustment = Quaternion.AngleAxis(-xLookInput * sway, Vector3.up);
        Quaternion yAngleAdjustment = Quaternion.AngleAxis(-yLookInput * sway, Vector3.right);
        Quaternion targetRotation = originLocalRotation * xAngleAdjustment * yAngleAdjustment;

        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * 10f);
    }
}
