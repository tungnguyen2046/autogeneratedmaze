using UnityEngine;

public class PlayerLook : MonoBehaviour 
{
    public Transform cam;
    public float sens;

    private Vector2 XYRotation;

    private void Update() 
    {
        Vector2 mouseInput = new Vector2
        {
            x = Input.GetAxis("Mouse X"),
            y = Input.GetAxis("Mouse Y")
        };

        XYRotation.x += mouseInput.x * sens;
        XYRotation.y -= mouseInput.y * sens;

        XYRotation.y = Mathf.Clamp(XYRotation.y, -90f, 90f);

        transform.eulerAngles = new Vector3(0, XYRotation.x, 0);
        cam.localEulerAngles = new Vector3(XYRotation.y, 0, 0);
    }
}