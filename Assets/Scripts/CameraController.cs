using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform target, pivot, obstruction;
    public Vector3 offset;
    public bool useOffsetValues;
    public float rotateSpeed, zoomSpeed;

    // Start is called before the first frame update
    void Start() {
        obstruction = target;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        if (!useOffsetValues) {
            offset = target.position - this.transform.position;
        }
        pivot.transform.position = target.transform.position;
        pivot.transform.parent = null;
        //pivot.transform.parent = target.transform;
    }

    // Update is called once per frame
    void LateUpdate() {
        if (UIController.CanDoThings()) {
            CamControl();
        }
        
        //ViewObstructed();
    }

    private void CamControl() {
        pivot.transform.position = target.transform.position;
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        pivot.Rotate(0f, horizontal, 0f);

        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        pivot.Rotate(-vertical, 0f, 0f);

        //Limit up/down camera rotation
        if(pivot.rotation.x > 45f && pivot.rotation.x < 180f) {
            pivot.rotation = Quaternion.Euler(45f, 0, 0);
        }

        if(pivot.rotation.eulerAngles.x > 180f && pivot.rotation.eulerAngles.x < 315f) {
            pivot.rotation = Quaternion.Euler(315, 0, 0);
        }

        //float desiredYAngle = target.eulerAngles.y;
        //float desiredXAngle = pivot.eulerAngles.x;
        //Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
        //this.transform.position = target.position - (rotation * offset);

        //this.transform.position = target.position - offset;

        if (transform.position.y < target.position.y) {
            transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
        }

        this.transform.LookAt(target);
    }

    private void ViewObstructed() {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, target.position - transform.position, out hit, 4.5f)) {
            if(hit.collider.gameObject.tag != "Player") {
                obstruction = hit.transform;
                obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;

                if (Vector3.Distance(obstruction.position, transform.position) >= 3f && Vector3.Distance(transform.position, target.position) >= 1.5f)
                    transform.Translate(Vector3.forward * zoomSpeed * Time.deltaTime);
            } else {
                obstruction.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                if (Vector3.Distance(transform.position, target.position) < 4.5f)
                    transform.Translate(Vector3.back * zoomSpeed * Time.deltaTime);
            }
        }
    }
}
