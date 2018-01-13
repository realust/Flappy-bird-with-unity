using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    Rigidbody2D rb2d;
    Animator animator;
    float angle;
    bool isDead;

    public float maxHeight;
    public float flapVelocity;
    public float relativeVelocityX;
    public GameObject sprite;
    public AudioClip bird_sound;

    public bool IsDead() {
        return isDead;
    }

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = sprite.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && transform.position.y < maxHeight)
        {
            Flap();
        }

        ApplyAngle();

        animator.SetBool("flap", angle >= 0.0f);
    }

    public void Flap()
    {
        if (isDead) return;

        if (rb2d.isKinematic) return;

        AudioSource.PlayClipAtPoint(bird_sound, transform.position);
        rb2d.velocity = new Vector2(0.0f, flapVelocity);
    }

    void ApplyAngle() {
        float targetAngle;

        if (isDead)
        {
            targetAngle = -90.0f;
        }
        else
        {
            targetAngle = Mathf.Atan2(rb2d.velocity.y, relativeVelocityX) * Mathf.Rad2Deg;
        }
        angle = Mathf.Lerp(angle, targetAngle, Time.deltaTime * 10.0f);

        sprite.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, angle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;

        isDead = true;
    }

    public void SetSteerAction(bool active) {
        rb2d.isKinematic = !active;
    }
}
