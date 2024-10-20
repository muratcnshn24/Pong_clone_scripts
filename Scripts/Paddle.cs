using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Paddle : MonoBehaviour
{
    protected Rigidbody2D rb;

    public float speed = 8f;
    [Tooltip("Topun raketin neresine �arpt���na ba�l� olarak raketten nas�l sekece�ini de�i�tirir. " +
        "Raketin merkezinden ne kadar uzaksa s��rama a��s� da o kadar dik olur.")]
    public bool useDynamicBounce = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void ResetPosition()
    {
        rb.velocity = Vector2.zero;
        rb.position = new Vector2(rb.position.x, 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (useDynamicBounce && collision.gameObject.CompareTag("Ball"))
        {
            Rigidbody2D ball = collision.rigidbody;
            Collider2D paddle = collision.otherCollider;

            // �arp��ma hakk�nda bilgi toplama
            Vector2 ballDirection = ball.velocity.normalized;
            Vector2 contactDistance = ball.transform.position - paddle.bounds.center;
            Vector2 surfaceNormal = collision.GetContact(0).normal;
            Vector3 rotationAxis = Vector3.Cross(Vector3.up, surfaceNormal);

            // Oyunu daha dinamik ve ilgin� hale getirmek i�in
            // Temas mesafesine g�re topun y�n�n� d�nd�r
            float maxBounceAngle = 75f;
            float bounceAngle = contactDistance.y / paddle.bounds.size.y * maxBounceAngle;
            ballDirection = Quaternion.AngleAxis(bounceAngle, rotationAxis) * ballDirection;

            // Yeni y�n� topa yeniden uygula
            ball.velocity = ballDirection * ball.velocity.magnitude;
        }
    }
}
