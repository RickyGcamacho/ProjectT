using UnityEngine;
using UnityEngine.UI;

public class DoorScript : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float motorSpeed = 200f;
    [SerializeField] private float raycastDistance = 5f;

    private HingeJoint selectedHinge;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TrySelectDoor();
        }

        if (Input.GetMouseButton(0) && selectedHinge != null)
        {
            ApplyMotorControl();
        }

        if (Input.GetMouseButtonUp(0) && selectedHinge != null)
        {
            StopMotor();
        }
    }

    void TrySelectDoor()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, raycastDistance))
        {
            var hinge = hit.collider.GetComponent<HingeJoint>();
            if (hinge != null)
            {
                selectedHinge = hinge;
                Debug.Log("Puerta seleccionada: " + hinge.gameObject.name);
            }
        }
    }

    void ApplyMotorControl()
    {
        float mouseY = Input.GetAxis("Mouse Y");

        if (mouseY > 0.01f)
        {
            // Mover hacia arriba → cerrar (inverso)
            selectedHinge.motor = SetMotorSpeed(-motorSpeed);
        }
        else if (mouseY < -0.01f)
        {
            // Mover hacia abajo → abrir (inverso)
            selectedHinge.motor = SetMotorSpeed(motorSpeed);
        }
        else
        {
            StopMotor();
        }
    }

    JointMotor SetMotorSpeed(float speed)
    {
        JointMotor motor = selectedHinge.motor;
        motor.targetVelocity = speed;
        motor.force = 1000f;
        motor.freeSpin = false;
        return motor;
    }

    void StopMotor()
    {
        JointMotor motor = selectedHinge.motor;
        motor.targetVelocity = 0f;
        selectedHinge.motor = motor;
    }
}