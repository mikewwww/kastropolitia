using UnityEngine;
using UnityEngine.UI;

public class CrosshairController : MonoBehaviour
{
    public Image crosshair;
    public CameraFollow cameraFollow;

    void Update()
    {
        if (crosshair != null && cameraFollow != null)
            crosshair.enabled = cameraFollow.IsFirstPerson();
    }
}
