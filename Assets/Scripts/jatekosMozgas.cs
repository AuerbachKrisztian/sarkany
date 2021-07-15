using UnityEngine;

public class jatekosMozgas : MonoBehaviour
{
    [SerializeField] private float sebesseg;
    [SerializeField] private LayerMask foldLayer;
    private Rigidbody2D test;
    private Animator anim;
    private bool ugras;
    private BoxCollider2D boxCollidder;
    private void Awake()
    {
        test = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        test.velocity = new Vector2(horizontalInput * sebesseg, test.velocity.y);

        if (horizontalInput > 0.0f)
        {
            transform.localScale = Vector3.one;
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1); // megfordul balra
        }
        if (Input.GetKey(KeyCode.UpArrow) && isUgras())
        {
            Ugras();
        }

        // Animáció paraméterek
        anim.SetBool("Futas", horizontalInput != 0);
        anim.SetBool("Ugras", isUgras());
    }
    private void Ugras()
    {
        test.velocity = new Vector2(test.velocity.x, sebesseg);
        anim.SetTrigger("Fold");
        ugras = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Palya")
        {
            ugras = true;
        }
    }

    private bool isUgras() {
        RaycastHit2D raycasthit = Physics2D.BoxCast(boxCollidder.bounds.center,
            boxCollidder.bounds.size, 0, Vector2.down,0.1f, foldLayer);
        return false;
    }
}
