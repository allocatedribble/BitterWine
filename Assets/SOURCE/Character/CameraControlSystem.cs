using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;


struct CameraControl : IComponentData
{
    public float3 Position;
    public quaternion Rotation;
}

public partial class CameraControlSystem: SystemBase
{
    private Vector2 _rotation;
    private Transform _cameraTransform;

    private InputAction _lookAction;
    public float Sensitivity = 10;
    
    protected override void OnCreate()
    {
        _lookAction = InputSystem.actions.FindAction("Look");
        _cameraTransform = GameObject.Find("MainCamera").transform;
        Cursor.lockState = CursorLockMode.Locked;
        // RequireForUpdate<CameraControl>();
    }
    
    protected override void OnUpdate()
    {
        var look = _lookAction.ReadValue<Vector2>();
        Debug.Log(look);
        _rotation = new Vector2(_rotation.x + (look.x * Sensitivity), _rotation.y + (look.y * Sensitivity));

        var pitch = Quaternion.AngleAxis(_rotation.x * 0.01f, Vector3.up );
        var yaw = Quaternion.AngleAxis(-_rotation.y * 0.01f, Vector3.right );

        //TODO: Figure out why pitch * yaw modifies the Z axis rotation.
        var combined_euler = (pitch * yaw).eulerAngles;

        _cameraTransform.rotation = Quaternion.Euler(combined_euler.x, combined_euler.y, 0);
    }
}
