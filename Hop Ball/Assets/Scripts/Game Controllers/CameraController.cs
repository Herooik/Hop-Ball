using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 _offset;
    private Vector3 _cameraPos;

    private void Start()
    {
        if (player != null)
        {
            _offset = transform.position - player.transform.position;
            _cameraPos = transform.position;
        }
    }

    private void Update()
    {
        if (player != null)
        {
            _cameraPos.z = player.transform.position.z + _offset.z;
            transform.position = _cameraPos;
        }
    }
}