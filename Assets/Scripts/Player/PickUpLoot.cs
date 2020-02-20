using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpLoot : MonoBehaviour {
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private bool canClick = true;

    void Start() {

    }

    void Update() {
        Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction * 100f, 1f, layerMask: layerMask);
#if UNITY_ANDROID
        for (int i = 0; i < Input.touchCount; ++i)
            if (Input.GetTouch(i).phase.Equals(TouchPhase.Began)) {
                if (hit && hit.collider.tag == "Use" && canClick) {
                    StartCoroutine(CullDown());
                }
            }
#endif


#if UNITY_EDITOR || UNITY_WEBGL
        if (hit && hit.collider.tag == "PickUp" && Input.GetMouseButtonDown(0) && canClick) {
            ItemScenePickup itemScenePickup = hit.collider.gameObject.GetComponent<ItemScenePickup>();
            itemScenePickup.PickUp();
            StartCoroutine(CullDown());
        }
#endif
    }

    IEnumerator CullDown() {
        canClick = false;
        yield return new WaitForSeconds(0.1f);
        canClick = true;
    }
}
