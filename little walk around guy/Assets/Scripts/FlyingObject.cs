using System.Collections;
using UnityEngine;

public class FlyingObject : MonoBehaviour
{
    public Transform targetPosition;
    public float speed = 5.0f;
    public float bobbingSpeed = 2.0f;
    public float bobbingHeight = 0.5f;
    public float sideToSideSpeed = 1.0f;
    public float sideToSideDistance = 0.5f;

    bool triggered = false;
    bool isBobbing = false;
    Vector2 startingPosition;

    private void Start()
    {
        startingPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            triggered = true;
        }
    }

    private void Update()
    {
        if (triggered)
        {
            MoveTowardsTargetPosition();
        }
    }

    private void MoveTowardsTargetPosition()
    {
        if (targetPosition != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition.position, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, targetPosition.position) < 0.1f && !isBobbing)
            {
                StartCoroutine(BobObject());
            }
        }
        else
        {
            Debug.LogWarning("Target position not set for the flying object.");
            triggered = false;
        }
    }

    private IEnumerator BobObject()
    {
        isBobbing = true;
        Vector2 originalPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < bobbingSpeed)
        {
            float yOffset = Mathf.Sin(elapsedTime / bobbingSpeed * Mathf.PI) * bobbingHeight;
            float xOffset = Mathf.Sin(Time.time * sideToSideSpeed) * sideToSideDistance;

            transform.position = originalPosition + new Vector2(xOffset, yOffset);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition.position;
        isBobbing = false;
    }
}
