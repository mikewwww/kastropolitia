using UnityEngine;
using UnityEngine.Splines;

[ExecuteInEditMode]
public class SplineFlattener : MonoBehaviour
{
    public float targetY = 0f;

    void Start()
    {
        var splineContainer = GetComponent<SplineContainer>();
        if (splineContainer == null) return;

        for (int i = 0; i < splineContainer.Spline.Count; i++)
        {
            var knot = splineContainer.Spline[i];

            // Επίπεδο ύψος
            Vector3 pos = knot.Position;
            pos.y = targetY;

            // Λήψη euler από Unity Quaternion
            Quaternion rotUnity = knot.Rotation;
            Vector3 rotEuler = rotUnity.eulerAngles;
            rotEuler.x = 0;
            rotEuler.z = 0;

            Quaternion newRot = Quaternion.Euler(rotEuler);

            splineContainer.Spline.SetKnot(i, new BezierKnot(pos, knot.TangentIn, knot.TangentOut, newRot));
        }

        Debug.Log("Spline flattened.");
    }
}
