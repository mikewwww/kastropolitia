using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Ο παίκτης που ακολουθεί η κάμερα
    public Vector3 offset = new Vector3(0, 5, -7); // Θέση της κάμερας σε σχέση με τον παίκτη
    public float smoothTime = 0.3f; // Χρόνος ομαλότητας για μετακίνηση
    public float rotationSpeed = 100f; // Ταχύτητα ομαλής περιστροφής

    public float zoomSpeed = 800f; // Ταχύτητα zoom
    public float minZoom = 2f; // Ελάχιστη απόσταση zoom
    public float maxZoom = 15f; // Μέγιστη απόσταση zoom

    public float rotateSpeed = 400f; // Ταχύτητα περιστροφής με το δεξί κλικ
    public float returnSpeed = 2f; // Ταχύτητα επιστροφής στην κατεύθυνση του παίκτη
    public float pitchLimit = 45f; // Μέγιστη γωνία ανύψωσης (πάνω-κάτω)

    private float currentZoom; // Τρέχουσα απόσταση zoom
    private float currentYaw = 0f; // Τρέχουσα γωνία περιστροφής
    private float currentPitch = 0f; // Τρέχουσα γωνία ανύψωσης (πάνω-κάτω)
    private bool isManualRotation = false; // Αν ο παίκτης περιστρέφει την κάμερα χειροκίνητα

    private Vector3 velocity = Vector3.zero; // Ταχύτητα για το SmoothDamp
    public LayerMask collisionMask; // Μάσκα για τα εμπόδια που ανιχνεύει η κάμερα

    void Start()
    {
        currentZoom = offset.magnitude; // Ρύθμιση αρχικού zoom
        currentYaw = target.eulerAngles.y; // Ρύθμιση αρχικής γωνίας περιστροφής
        currentPitch = 0f; // Ξεκινάμε με επίπεδη ανύψωση
    }

    void Update()
    {
        // Zoom με τον τροχό του ποντικιού
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        currentZoom -= scroll * zoomSpeed * Time.deltaTime;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        // Περιστροφή με το δεξί κλικ
        if (Input.GetMouseButton(1)) // 1 = δεξί κλικ
        {
            currentYaw += Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
            currentPitch -= Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime; // Χρήση του άξονα Y για πάνω-κάτω
            currentPitch = Mathf.Clamp(currentPitch, -pitchLimit, pitchLimit); // Περιορισμός γωνίας ανύψωσης
            isManualRotation = true; // Ο παίκτης περιστρέφει την κάμερα
        }
        else
        {
            isManualRotation = false; // Τερματισμός χειροκίνητης περιστροφής
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Αν ο χρήστης δεν περιστρέφει την κάμερα χειροκίνητα, επιστρέφει προς τον παίκτη
        if (!isManualRotation)
        {
            currentYaw = Mathf.LerpAngle(currentYaw, target.eulerAngles.y, returnSpeed * Time.deltaTime);
            currentPitch = Mathf.Lerp(currentPitch, 0f, returnSpeed * Time.deltaTime); // Επιστροφή στην αρχική γωνία ανύψωσης
        }

        // Υπολογισμός της επιθυμητής θέσης της κάμερας
        Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0);
        Vector3 direction = rotation * offset.normalized;
        Vector3 desiredPosition = target.position + direction * currentZoom;

        // Ανίχνευση σύγκρουσης με Raycast
        RaycastHit hit;
        if (Physics.Raycast(target.position, desiredPosition - target.position, out hit, currentZoom, collisionMask))
        {
            // Αν υπάρχει εμπόδιο, φέρε την κάμερα κοντά στο σημείο πρόσκρουσης
            desiredPosition = hit.point - (desiredPosition - target.position).normalized * 0.2f; // Προσθέτουμε ένα μικρό offset
        }

        // Ομαλή μετακίνηση της κάμερας
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);

        // Ομαλή περιστροφή της κάμερας προς τον παίκτη
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
