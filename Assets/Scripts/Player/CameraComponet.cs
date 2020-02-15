using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraComponet : MonoBehaviour {
    [SerializeField] public float cameraMin = 0;
    [SerializeField] public float cameraMax = 0.5f;
    [SerializeField] public float damping = 2f;
    [SerializeField] public Vector2 offest = new Vector2(2, 0);
    [SerializeField] [Range(0f, 10f)] public float cameraSpeed = 1f;
}
