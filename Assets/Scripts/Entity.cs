using Unity.VisualScripting;
using UnityEngine;

public abstract class Entity: MonoBehaviour
{
    public string name;
    [Tooltip("Should Stop when run ends")]
    public bool shouldStop;
    public float baseSpeed;

}
