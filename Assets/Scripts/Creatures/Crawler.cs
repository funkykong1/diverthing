using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crawler : MonoBehaviour
{

    public int hp;
    private SpriteRenderer rend;
    private GameObject shell;

    public Sprite[] slugs;
    public Sprite[] shells;
    private bool rotating;
    public GameObject tilemap;

    public float rotatingSpeed;
    public float speed;




    void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        shell = GameObject.Find("Shell");
    }
    void Start()
    {
        hp = 4;
    }


    
    void FixedUpdate()
    {
        //slug sprite acts as a flag for if its been hit
        if(rend.sprite != slugs[2])
            Crawl();

        
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
        if(Input.GetKeyDown(KeyCode.Space))
            StartCoroutine(RotateDown());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Harpoon") && rend.sprite != slugs[2] && hp > 0)
            StartCoroutine(Struck());

        Health health;
        if(health = other.GetComponent<Health>())
        {
            health.GetHit(1,transform.gameObject);
        }
    }

    //walk round
    private void Crawl()
    {
        transform.position += transform.right/200;


    }

    public IEnumerator RotateDown()
    {
        rotating = true;

        Vector3 rotation = new Vector3(0,0,-90);
        transform.Rotate(rotation * Time.deltaTime * rotatingSpeed);

        yield return new WaitUntil(()=>);+
    }


    //ROTATE UP A CORNER
    public IEnumerator RotateUp()
    {
        rotating = true;



        yield return rotating = false;
    }

    private IEnumerator Struck()
    {

        hp--;
        rend.sprite = slugs[2];

        if(hp > 0)
            shell.GetComponent<SpriteRenderer>().sprite = shells[hp];
        else
            Destroy(shell);

        yield return new WaitForSeconds(3);
        rend.sprite = slugs[0];
    }

}
