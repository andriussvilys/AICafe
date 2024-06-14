using UnityEngine;

public class CameraFacingPlane : MonoBehaviour
{
    [SerializeField]
    Camera _camera;

    private void Start() {
        _camera = Camera.main;
    }

    void Update()
    {
        if(_camera){
            TurtToCamera(_camera);
        }
    }

    private void TurtToCamera(Camera camera){
        Vector3 toCamera = _camera.transform.position - transform.position;
        toCamera.y = 0f;
        transform.rotation = Quaternion.LookRotation(toCamera, Vector3.up);
    }

}
