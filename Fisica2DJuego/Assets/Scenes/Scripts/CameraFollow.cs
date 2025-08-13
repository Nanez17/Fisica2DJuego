using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // El jugador
    public Vector3 offset;   // Distancia entre cámara y jugador
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (target == null) return;
        transform.position = new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z);
    }

}
