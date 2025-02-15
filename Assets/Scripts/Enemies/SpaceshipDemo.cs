using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class SpaceshipDemo : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    public GameObject projectilePrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = 0.0f;
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            horizontal = -1.0f;
        }
        else if (Keyboard.current.rightArrowKey.isPressed)
        {
            horizontal = 1.0f;
        }

        float vertical = 0.0f;
        if (Keyboard.current.upArrowKey.isPressed)
        {
            vertical = 1.0f;
        }
        else if (Keyboard.current.downArrowKey.isPressed)
        {
            vertical = -1.0f;
        }

        Vector2 position = transform.position;
        position.x = position.x + 0.05f * horizontal;
        position.y = position.y + 0.05f * vertical;
        transform.position = position;

        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(
            projectilePrefab,
            rigidbody2d.position + Vector2.up * 1f,
            Quaternion.identity
        );
        ProjectileDemo projectile = projectileObject.GetComponent<ProjectileDemo>();
        projectile.Launch(Vector2.up, 300);
    }
}
