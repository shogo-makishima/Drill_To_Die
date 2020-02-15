using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private Rigidbody2D rigidbody = null;
    [SerializeField] private Animator animator = null;
    [SerializeField][Range(0f, 20000f)] private float _forceX = 0;
    [SerializeField][Range(0f, 20000f)] private float _forceY = 0;
    [SerializeField] private Camera mainCamera = null;
    [SerializeField] private CameraComponet cameraComponet = null;
    [SerializeField][Range(0f, 2f)] private float paddingX = 0.5f;
    [SerializeField][Range(0f, 2f)] private float paddingY = 0.5f;
    

    private void Awake() {
        TryGetComponent<Rigidbody2D>(out rigidbody);
        TryGetComponent<Animator>(out animator);
        mainCamera = Camera.main;
        mainCamera.TryGetComponent<CameraComponet>(out cameraComponet);
    }

    void FollowCamera() {
        int currentX = Mathf.RoundToInt(transform.position.x);
        Vector3 target;
        target = new Vector3(transform.position.x + cameraComponet.offest.x, cameraComponet.gameObject.transform.position.y, cameraComponet.gameObject.transform.position.z);

        Vector3 currentPosition = Vector3.Lerp(cameraComponet.gameObject.transform.position, target, cameraComponet.damping * Time.deltaTime);
        cameraComponet.gameObject.transform.position = currentPosition;
    }

    void MoveBorders() {
        mainCamera.transform.position = new Vector3(Mathf.Clamp(mainCamera.transform.position.x, cameraComponet.cameraMin, cameraComponet.cameraMax), mainCamera.transform.position.y, mainCamera.transform.position.z);

        float xMin = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + paddingX;
        float xMax = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - paddingX;

        float yMin = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + paddingY;
        float yMax = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - paddingY;

        transform.position = new Vector2(Mathf.Clamp(transform.position.x, xMin, xMax), Mathf.Clamp(transform.position.y, yMin, yMax));
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.I))
            print(Main.Player.InventoryString());
        else if (Input.GetKeyDown(KeyCode.C))
            Main.SaveManager.Save();
        else if (Input.GetKeyDown(KeyCode.Delete))
            Main.SaveManager.DestroySave();

        float axisValue = Mathf.Abs(CnControls.CnInputManager.GetAxis("Horizontal")) + Mathf.Abs(CnControls.CnInputManager.GetAxis("Vertical"));
        Main.Player.fuel -= axisValue * Time.deltaTime * 2f * Main.Player.engine;

        if (Main.Player.fuel > 0) {
            animator.SetFloat("X", CnControls.CnInputManager.GetAxis("Horizontal"));
            animator.SetFloat("Y", CnControls.CnInputManager.GetAxis("Vertical"));
        }

        FollowCamera();
        MoveBorders();
    }

    private void FixedUpdate() {
        if (rigidbody & Main.Player.fuel > 0) {
            rigidbody.AddForce(transform.up * 100 * Time.deltaTime * CnControls.CnInputManager.GetAxis("Vertical") * _forceY, ForceMode2D.Force);
            rigidbody.AddForce(transform.right * 100 * Time.deltaTime * ((CnControls.CnInputManager.GetAxis("Horizontal") > 0) ? CnControls.CnInputManager.GetAxis("Horizontal") : 0) * _forceX, ForceMode2D.Force);
        }
    }

    private void Reset() {
        TryGetComponent<Rigidbody2D>(out rigidbody);
        TryGetComponent<Animator>(out animator);

        mainCamera = Camera.main;

        _forceX = 3000f;
        _forceY = 3000f;
    }
}
