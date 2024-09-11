using UnityEngine;

public class MovimientoCamara : MonoBehaviour
{
    [Header("Required")]
    public Transform target;

    [Header("Config")]
    public float targetdistance = 5.0f;
    private float xSpeed = 100f;
    private float ySpeed = 80f;

    private float yMinLimit = 27.5f;
    private float yMaxLimit = 45f;

    private float distanceMin = 5.0f;
    private float distanceMax = 12.5f;

    public float ScrollSensativity = 16f;

    private new Rigidbody rigidbody;

    float x = 0.0f;
    float y = 0.0f;

    float targetx = 0.0f;
    float targety = 0.0f;
    float distance = 5f;

    private float smoothDistanceVelocity = 0.0f;
    public float smoothTime = 0.1f;


    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        rigidbody = GetComponent<Rigidbody>();

        if (rigidbody != null)
        {
            rigidbody.freezeRotation = true;
        }
    }


    void FixedUpdate()
    {
        if (target)
        {
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 targetPosition = Quaternion.Euler(targety, targetx, 0) * negDistance + target.position;

            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothTime);
        }
    }

    void Update()
    {
        if (target)
        {
            targetdistance = Mathf.Clamp(targetdistance - Input.GetAxis("Mouse ScrollWheel") * ScrollSensativity, distanceMin, distanceMax);
            distance = Mathf.SmoothDamp(distance, targetdistance, ref smoothDistanceVelocity, smoothTime);

            transform.LookAt(target.position);

            if (Input.GetMouseButton(1))
            {
                targetx += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime * 15;
                targety -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime * 15;
            }

            targety = ClampAngle(targety, yMinLimit, yMaxLimit);
            Quaternion targetRotation = Quaternion.Euler(targety, targetx, 0);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.smoothDeltaTime);
        }
    }


    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);

    }
}