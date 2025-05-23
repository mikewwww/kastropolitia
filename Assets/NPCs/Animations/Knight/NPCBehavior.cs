using UnityEngine;
using System.Collections;

public class NPCDistanceCheck : MonoBehaviour
{
    public Transform player;
    public float triggerDistance = 5f;
    private Animator animator;

    private bool hasPlayedAlert = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < triggerDistance)
        {
            if (!hasPlayedAlert)
            {
                hasPlayedAlert = true;
                StartCoroutine(PlayAlertThenIdle());
            }
        }
        else
        {
            hasPlayedAlert = false;
            animator.SetBool("isPlayerNear", false);
        }
    }

    IEnumerator PlayAlertThenIdle()
    {
        animator.SetBool("isPlayerNear", true);

        yield return new WaitForSeconds(3f);

        animator.SetBool("isPlayerNear", false);
    }
}
