using UnityEngine;

public class ComputerPaddle : Paddle
{
    [SerializeField]
    private Rigidbody2D ball;

    private void FixedUpdate()
    {
        if (ball.velocity.x > 0f)
        {
            if (ball.position.y > rb.position.y)
            {
                rb.AddForce(Vector2.up * speed);
            }
            else if (ball.position.y < rb.position.y)
            {
                rb.AddForce(Vector2.down * speed);
            }
        }
        else
        {
            if (rb.position.y > 0f)
            {
                rb.AddForce(Vector2.down * speed);
            }
            else if (rb.position.y < 0f)
            {
                rb.AddForce(Vector2.up * speed);
            }
        }
    }

}
