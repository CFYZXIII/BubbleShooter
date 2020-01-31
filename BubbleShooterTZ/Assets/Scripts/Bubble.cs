using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bubble : MonoBehaviour
{
   public int i;
   public int j;

    public float alpha;
    public float beta;
    public float power;

  
    public bool isShoot;
    public bool isCollision;
    public bool isMatch;
    

    GameObject leftWall;
    GameObject rightWall;
    GameObject upWall;

    public bool isSuperball;

    SpringJoint2D joint;
    Rigidbody2D body;

    GameObject target;
    Matrix matrix;

    float reflectx = 1;
    float reflecty = 1;
    float size;
    public GameObject figure;

   


    // Start is called before the first frame update
    void Start()
    {
       

        matrix = FindObjectOfType<Matrix>();
        body = GetComponent<Rigidbody2D>();

        leftWall = GameObject.FindGameObjectWithTag("LeftWall");
        rightWall = GameObject.FindGameObjectWithTag("RightWall");
        upWall = GameObject.FindGameObjectWithTag("UpWall");

        size = GetComponent<Renderer>().bounds.size.x;

        

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //Будет встрелом
        if (isShoot)
        {
            move();
        }


        if (isCollision) {

            if (GetComponent<SpringJoint2D>() == null)
            {
                joint = gameObject.AddComponent<SpringJoint2D>();
                joint.connectedAnchor = target.transform.position;
                
            }

            body.position = Vector3.MoveTowards(transform.position, target.transform.position, 0.5f);
            if ((transform.position - target.transform.position).magnitude == 0)
            {
                
                isCollision = false;
                // addJoint();
                

            }

            if (isMatch) {
                isCollision = false;
            }


            

        }

        if (isMatch) {
            if (body.position.y < 0) {
                transform.gameObject.SetActive(false);
            }
        }
    }


    void move() {

        transform.position += new Vector3(3*power * Mathf.Cos(alpha + Mathf.PI) * reflectx, 3*power * Mathf.Sin(alpha + Mathf.PI) * reflecty, 0);
        if (transform.position.x + size / 2 >= rightWall.transform.position.x) reflectx *= -1;
        if (transform.position.x - size / 2 <= leftWall.transform.position.x) reflectx *= -1;
        if (transform.position.y + size / 2 >= upWall.transform.position.y) {
            gameObject.SetActive(false);isCollision = true;
        };

    }
    


    private void OnCollisionEnter2D(Collision2D collision)
    {


        if (isSuperball) {
            isSuperball = false;
            isShoot = false;
            isCollision = true;
            i = collision.gameObject.GetComponent<Bubble>().i;
            j = collision.gameObject.GetComponent<Bubble>().j;
            collision.gameObject.SetActive(false);
            target = collision.gameObject;
            matrix.bubbls[i, j] = this;
            this.name = "(" + i + " , " + j + ")";
            transform.parent = matrix.bubbleParent.transform;
            matrix.boom(this);
           
        }

        if (isShoot) {
                    
           
                isShoot = false;
                isCollision = true;
                matrix.bubbls[i, j] = this;
                this.name = "(" + i + " , " + j + ")";
                transform.parent = matrix.bubbleParent.transform;
                matrix.boom(this);


        }
        
    }



    


    private void OnTriggerStay2D(Collider2D collision)
    {
       

        if (isShoot)
        {
            if (collision.tag == "cell")
            {
                Cell cell = collision.GetComponent<Cell>();
                i = cell.i;
                j = cell.j;
                target = collision.gameObject;
            }

        }

        
    }

   

    public void addJoint() {
       joint = gameObject.AddComponent<SpringJoint2D>();
       joint.autoConfigureConnectedAnchor = true;
        
      
    }

   
   

}
