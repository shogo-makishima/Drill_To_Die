using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Events {
    [System.Serializable] public class DamageEvent : UnityEvent<float, GameObject> { }
}
