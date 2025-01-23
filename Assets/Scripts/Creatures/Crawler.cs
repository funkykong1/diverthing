using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Crawler : MonoBehaviour
{

    public int hp;
    private SpriteRenderer rend;
    private GameObject shell;

    public Sprite[] slugs;
    public Sprite[] shells;
    bool facingRight = true;
    private float timer, flipTimer;




    void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        shell = transform.Find("Shell").gameObject;
    }
    void Start()
    {
        hp = 3;
    }


    
    void FixedUpdate()
    {
        if(flipTimer > 0)
            flipTimer--;
        if(timer <= 0)
        {   
            if(rend.sprite == slugs[0])
                rend.sprite = slugs[1];
            else
                rend.sprite = slugs[0];
            timer = 40;
        }

        if(rend.sprite != slugs[2])
        {
            if(facingRight)
            {
                timer--;
                transform.Translate(Vector3.right * Time.fixedDeltaTime * 0.3f, Space.Self);
            }

            else
            {
                timer--;               
                transform.Translate(Vector3.left * Time.fixedDeltaTime * 0.3f, Space.Self);
            }
        }
        
        //RaycastHit2D[] forward = Physics2D.RaycastAll(transform.position, Vector2.right/4);

        // Vector2 rayPos = new Vector2(transform.position.x+0.5f, transform.position.y);
        // RaycastHit2D[] down = Physics2D.RaycastAll(rayPos, Vector2.down);


        // for (int i = 0; i < down.Length; i++)
        //     {
        //     if(down[i].collider != tilemap.GetComponent<CompositeCollider2D>())
        //         StartCoroutine(Rotate());
        //     }

        // for (int i = 0; i < forward.Length; i++)
        //     {
        //     if(forward[i].collider != GameObject.Find("Tilemap Base"))
        //         StartCoroutine(RotateDown());
        //     }

        //Debug.DrawRay(transform.position, Vector2.down);
        // if(Input.GetKeyDown(KeyCode.Space))
        //     StartCoroutine(RotateDown());
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Ground"))
        {
            Flip();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //delegate all colliders into child gameobjects, if there's no shell
        //and a collision with THE SLUG ITSELF then collect it
        if(col.gameObject.CompareTag("Player") && !shell)
            GetComponent<Treasure>().PickMeUp();
    }


    //internal is pretty cool because it lets the children use the parent's function
	internal void Flip()
	{
        //crude way of making it not flip more than once in a set amount of time
        if(flipTimer > 0)
            return;
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
        flipTimer = 10;
            
	}



    // public IEnumerator RotateDown()
    // {
    //     rotating = true;

    //     Vector3 rotation = new Vector3(0,0,-90);
    //     transform.Rotate(rotation * Time.deltaTime * rotatingSpeed);

    //     //yield return new WaitUntil(() => );+
    // }


    //ROTATE UP A CORNER
    // public IEnumerator RotateUp()
    // {
    //     rotating = true;



    //     yield return rotating = false;
    // }

    //snail is struck by harpoon, reduce hp and make it cower in fear
    internal IEnumerator Struck()
    {
        
        hp--;
        rend.sprite = slugs[2];

        if(hp >= 0)
            shell.GetComponent<SpriteRenderer>().sprite = shells[hp];
        else
        {
            //destroy the shell and make the snail obtainable as treasure
            Destroy(shell);
        }
            

        yield return new WaitForSeconds(3);
        rend.sprite = slugs[0];
    }

    public IEnumerator Hide()
    {
        // if already hiding then dont bother starting another coroutine
        if(rend.sprite == slugs[2])
            yield break;

        Sensor sensor = GetComponentInChildren<Sensor>();
        rend.sprite = slugs[2];

        while(true)
        {
            //check for enemies every 3 seconds
            if(sensor.enemies > 0)
            {
                yield return new WaitForSeconds(3);
            }
            else
            {
               break;
            }
        }
        //yield return new WaitUntil(() => sensor.enemies < 1);
        rend.sprite = slugs[0];
        
    }


}
