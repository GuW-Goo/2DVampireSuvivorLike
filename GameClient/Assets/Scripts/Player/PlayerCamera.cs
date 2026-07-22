using Unity.VisualScripting;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("추적 대상")]
    [SerializeField] private GameObject _cameraTarget;

    [Header("카메라 설정")]
    [SerializeField] private Vector3 offset = new Vector3(0.0f, 0.0f, -10.0f);

    private void LateUpdate()
    {
        if(_cameraTarget != null)
        {
            Vector3 cameraPosition = _cameraTarget.transform.position + offset;
            transform.position = cameraPosition;
        }
    }
}
