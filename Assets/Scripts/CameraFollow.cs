using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 5, -7);
    public Vector3 firstPersonOffset = new Vector3(0, 1.7f, 0);
    public float smoothTime = 0.3f;
    public float rotationSpeed = 5f;

    public float zoomSpeed = 800f;
    public float minZoom = 2f;
    public float maxZoom = 15f;

    public float rotateSpeed = 400f;
    public float returnSpeed = 2f;
    public float pitchLimit = 45f;

    public float minFov = 30f;
    public float maxFov = 80f;
    public float fovScrollSpeed = 5000f;

    private float currentZoom;
    private float currentYaw = 0f;
    private float currentPitch = 0f;
    private bool isFirstPerson = false;
    private bool transitioningToThirdPerson = false;
    private bool isManualRotation = false;

    private float yawVelocity = 0f;
    private float pitchVelocity = 0f;
    private Vector3 velocity = Vector3.zero;
    private Vector3 smoothedTargetPosition;

    private Renderer[] playerRenderers;
    private Camera cam;
    private UIManager uiManager;

    public LayerMask collisionMask;

    void Start()
    {
        currentZoom = offset.magnitude;
        currentYaw = target.eulerAngles.y;
        currentPitch = 0f;

        cam = GetComponent<Camera>();
        uiManager = FindObjectOfType<UIManager>();

        playerRenderers = target.GetComponentsInChildren<Renderer>();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        if (uiManager != null && uiManager.IsHelpMenuOpen())
            return;

        if (Input.GetKeyDown(KeyCode.C))
        {
            isFirstPerson = !isFirstPerson;

            foreach (Renderer r in playerRenderers)
                r.enabled = !isFirstPerson;

            Cursor.lockState = isFirstPerson ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !isFirstPerson;

            if (!isFirstPerson)
            {
                transitioningToThirdPerson = true;
                cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, minFov, maxFov);
            }
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (isFirstPerson)
        {
            float fov = cam.fieldOfView;
            fov -= scroll * fovScrollSpeed * Time.deltaTime;
            cam.fieldOfView = Mathf.Clamp(fov, minFov, maxFov);

            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            target.Rotate(Vector3.up * mouseX * rotateSpeed * Time.deltaTime);
            currentPitch -= mouseY * rotateSpeed * Time.deltaTime;
            currentPitch = Mathf.Clamp(currentPitch, -pitchLimit, pitchLimit);
        }
        else
        {
            currentZoom -= scroll * zoomSpeed * Time.deltaTime;
            currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

            if (Input.GetMouseButtonDown(1))
                isManualRotation = true;

            if (Input.GetMouseButtonUp(1))
                isManualRotation = false;

            if (Input.GetMouseButton(1))
            {
                currentYaw += Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
                currentPitch -= Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;
                currentPitch = Mathf.Clamp(currentPitch, -pitchLimit, pitchLimit);
            }
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        smoothedTargetPosition = Vector3.Lerp(
            smoothedTargetPosition == Vector3.zero ? target.position : smoothedTargetPosition,
            target.position,
            Time.deltaTime * 10f
        );

        if (isFirstPerson)
        {
            Vector3 desiredPosition = target.position + new Vector3(0, firstPersonOffset.y, 0);
            transform.position = desiredPosition;
            transform.rotation = Quaternion.Euler(currentPitch, target.eulerAngles.y, 0);
        }
        else
        {
            if (transitioningToThirdPerson || !isManualRotation)
            {
                currentYaw = Mathf.SmoothDampAngle(currentYaw, target.eulerAngles.y, ref yawVelocity, smoothTime);
                currentPitch = Mathf.SmoothDamp(currentPitch, 0f, ref pitchVelocity, smoothTime);

                if (Mathf.Abs(Mathf.DeltaAngle(currentYaw, target.eulerAngles.y)) < 0.5f &&
                    Mathf.Abs(currentPitch) < 0.5f)
                {
                    transitioningToThirdPerson = false;
                }
            }

            Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0);
            Vector3 direction = rotation * offset.normalized;
            Vector3 desiredPosition = smoothedTargetPosition + direction * currentZoom;

            Vector3 camDirection = (desiredPosition - smoothedTargetPosition).normalized;
            float camDotUp = Vector3.Dot(camDirection, Vector3.up);

            if (Mathf.Abs(camDotUp) < 0.9f)
            {
                if (Physics.Raycast(smoothedTargetPosition, camDirection, out RaycastHit hit, currentZoom, collisionMask))
                {
                    float hitDistance = Vector3.Distance(smoothedTargetPosition, hit.point);
                    if (hitDistance < currentZoom - 0.5f)
                    {
                        desiredPosition = hit.point - camDirection * 0.2f;
                    }
                }
            }

            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
            Quaternion targetRotation = Quaternion.LookRotation(smoothedTargetPosition - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    public bool IsFirstPerson()
    {
        return isFirstPerson;
    }
}
