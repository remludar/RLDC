using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
public class MouseOrbitImproved : MonoBehaviour
{

    public Transform target;
    public float distance = 1.0f;
    public float xSpeed = 50.0f;
    public float ySpeed = 150.0f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    //public float distanceMin = .5f;
    //public float distanceMax = 15f;

    float x = 0.0f;
    float y = 0.0f;

    Texture2D crosshairImage;


    // Use this for initialization
    void Start()
    {
        crosshairImage = Resources.Load("Textures/Crosshair") as Texture2D;
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    void LateUpdate()
    {
        if (target)
        {
            if (!target.gameObject.GetComponent<Player>().isMenuItemOpen)
            {
                UpdatePositionAndRotation();
            }
        }
    }

    void OnGUI()
    {
        float xMin = (Screen.width / 2) - (crosshairImage.width / 2);
        float yMin = (Screen.height / 2) - (crosshairImage.height / 2);
        GUI.DrawTexture(new Rect(xMin, yMin, crosshairImage.width, crosshairImage.height), crosshairImage);
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }

    void UpdatePositionAndRotation()
    {
        x += Input.GetAxis("Mouse X") * xSpeed * distance * Time.deltaTime;
        y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;

        y = ClampAngle(y, yMinLimit, yMaxLimit);

        Quaternion rotation = Quaternion.Euler(y, x, 0);
        //distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

        Vector3 negDistance = new Vector3(0.75f, 2.0f, -distance);
        Vector3 position = rotation * negDistance + target.position;

        transform.rotation = rotation;
        transform.position = position;
    }

    public void Snipe(bool isSniping)
    {
        if (isSniping)
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 10.0f, 0.1f);
        }
        else
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 60.0f, 0.1f);
        }
    }
}