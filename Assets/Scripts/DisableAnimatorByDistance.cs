using UnityEngine;

public class DisableAnimatorByDistance : MonoBehaviour
{
    public Transform player; // Drag Player here, or auto-find
    public float disableDistance = 30f;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        // Προαιρετικά: αυτόματο εύρεση player αν δεν έχει οριστεί
        if (player == null && GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > disableDistance && animator.enabled)
        {
            animator.enabled = false;
        }
        else if (distance <= disableDistance && !animator.enabled)
        {
            animator.enabled = true;
        }
    }
}
