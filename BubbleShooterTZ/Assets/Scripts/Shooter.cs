using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooter : MonoBehaviour
{

    Bubble[] bubbls;
    public int bubbleCount;
    public Matrix bubbleMatrix;
    int currentBubble;
    Bubble bubble;
    public GameObject bubblsSpawn;
    public Bubble bubblePrefab;

    public string[] tags;
    public Color[] colors;

    float distance;
    float power;
    float alpha;
    float beta;

    Vector2 startPosition;
    Vector2 touchPosition;

    bool isShoot = true;

    public Aim aim;
    public Aim leftAim;
    public Aim rightAim;

    public GameObject danger;
    public Text score;
    public Text counts;
    public Text maxScore;
  




    void Start()
    {
        if (!PlayerPrefs.HasKey("Score"))
        {

            PlayerPrefs.SetInt("Score", 0);
            PlayerPrefs.Save();

        }
      
            maxScore.text ="MaxScore = "+ PlayerPrefs.GetInt("Score") + " ";
            score.text = "Score = "+bubbleMatrix.score;
            
        colors = new Color[] { Color.red, Color.blue, Color.green, Color.yellow };
        tags = new string[] { "Red", "Blue", "Green", "Yellow" };
       
       

      
    init();
    }

    // Update is called once per frame
    void Update()
    {
        if (!bubbleMatrix.isWin())
        {

            setText();
            touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (isShoot && GetComponent<Collider2D>().OverlapPoint(touchPosition))
            {
                if (Input.GetMouseButton(0) && currentBubble < bubbls.Length)
                {
                    getShoot();
                    getAim();
                }


                if (Input.GetMouseButtonUp(0) && currentBubble < bubbls.Length)
                {
                    shoot();

                }

            }
            reload();
        }
        else bubbleMatrix.winPanel(bubbleMatrix.isWin());
          
        


    }

    void setText() {
        score.text = "Score = " + bubbleMatrix.score;
        if (bubbleCount > currentBubble)
        {
            counts.text = bubbleCount - currentBubble - 1 + "";
        }
        else { counts.text = "";

           
        }
    }


    void reload()
    {
        
        if (bubble.isCollision)
        {
            aim.transform.position = transform.position + new Vector3(0, 0.5f);
            aim.alpha = Mathf.PI / 2;
            aim.reset();
            rightAim.reset();
            leftAim.reset();
            danger.SetActive(false);

            leftAim.alpha = Mathf.PI / 2;
            

            rightAim.alpha = Mathf.PI / 2;
            




            if (currentBubble < bubbls.Length)
            {
                bubble = bubbls[currentBubble];
                bubble.transform.position = transform.position;

                isShoot = true;


            }

            if (currentBubble + 1 < bubbls.Length)
            {
                bubbls[currentBubble + 1].gameObject.SetActive(true);
            }


        }
    }



    void shoot()
    {
        
            currentBubble++;

            bubble.power = power / 5;
            bubble.alpha = alpha + beta;
            bubble.isShoot = true;
            isShoot = false;


        

    }

    void getAim()
    {

        aim.transform.position = bubble.transform.position - new Vector3(Mathf.Cos(alpha), Mathf.Sin(alpha))/2;
        aim.alpha = alpha + Mathf.PI;
        aim.drawLine();

        leftAim.transform.position = bubble.transform.position - new Vector3(Mathf.Cos(alpha), Mathf.Sin(alpha)) / 2;
        leftAim.alpha = alpha + Mathf.PI + leftAim.beta;
       

        rightAim.transform.position = bubble.transform.position - new Vector3(Mathf.Cos(alpha), Mathf.Sin(alpha)) / 2;
        rightAim.alpha = alpha + Mathf.PI + rightAim.beta;
        


    }

    void getShoot()
    {
        
        alpha = Mathf.Atan2(touchPosition.y - transform.position.y, touchPosition.x - transform.position.x);

        


        power = (touchPosition - startPosition).magnitude;

        if (power >= distance)
        {
            power = distance;
            bubble.isSuperball = true;
            beta = Random.Range(-0.2f, 0.2f);
            leftAim.drawLine();
            rightAim.drawLine();
            danger.SetActive(true);
        }
        else if (power < distance) {
            danger.SetActive(false);
            bubble.isSuperball = false;
            rightAim.reset();
            leftAim.reset();
            beta = 0;
        }
        bubble.transform.position = new Vector2(power * Mathf.Cos(alpha), power * Mathf.Sin(alpha)) + startPosition;
       

    }


    void init()
    {
        bubbls = new Bubble[bubbleCount];
        for (int i = 0; i < bubbleCount; i++)
        {
            Bubble bubble = Instantiate(bubblePrefab, bubblsSpawn.transform.position, Quaternion.identity);
            if (i > 1)
            {
                bubble.gameObject.SetActive(false);
            }
            int k = (int)Random.Range(0, 4);
           
            bubble.tag = tags[k];
            bubble.GetComponent<MeshRenderer>().material.color = colors[k];
            bubble.isShoot = true;
            bubbls[i] = bubble;
        }
        bubble = bubbls[currentBubble];

       
       distance = bubble.GetComponent<Renderer>().bounds.size.x;
       bubble.transform.position = transform.position;
        startPosition = transform.position;


    }

}
