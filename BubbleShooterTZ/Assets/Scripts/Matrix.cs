using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Matrix : MonoBehaviour
{

    public Bubble bubblePrefab;
    public Bubble[,] bubbls;

    public Cell cellPrefab;
    public Cell[,] cells;

    public GameObject cellParent;
    public GameObject bubbleParent;


    public int width;
    public int height;
    public int maxHeight;
    float size;

    //Для создания шарика
    public int[,] Map;
    public string[] tags;
    public Color[] colors;
    public int score = 0;

    int matches = 0;
    List<Bubble> matchBubbls;
    List<Bubble> downBubbls;

    public GameObject panel;
    public Text text;



    // Start is called before the first frame update


    void Start()
        {
        matchBubbls = new List<Bubble>();
       downBubbls = new List<Bubble>();
        size = bubblePrefab.GetComponent<Renderer>().bounds.size.x;
            bubbls = new Bubble[width, height + maxHeight];
        cells = new Cell[width, height];
            init();
          
        }



    // Update is called once per frame
    void Update()
    {
       
    }

    


    public void init()
    {



        Map = new int[,] { { 0,0,0,2,2,1,1,2,2,2,2,0,2,0 },
                           { 0,0,1,1,0,1,1,0,1,2,2,1,2,0 },
                           { 3,0,2,0,0,0,0,0,0,0,0,2,2,0},
                           { 3,3,0,0,3,2,0,0,3,3,0,0,0,0}};


        colors = new Color[] { Color.red, Color.blue, Color.green, Color.yellow };
        tags = new string[] { "Red", "Blue", "Green", "Yellow" };
        Vector2 bubblePosition;
        Vector2 cellPosition;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (j < 4)
                {
                   
                    bubblePosition = new Vector3(i * 1.1f * size + j%2*size/2, -j * 1.1f * size) + transform.position;


                    Bubble bubble = Instantiate(bubblePrefab, bubblePosition, Quaternion.identity);
                    bubble.i = i;
                    bubble.j = j;

                    bubble.transform.parent = bubbleParent.transform;
                    bubble.name = "(" + i + " , " + j + ")";
                    bubble.GetComponent<MeshRenderer>().material.color = colors[Map[j, i]];
                    bubble.tag = tags[Map[j, i]];
                    bubble.addJoint();
                    bubbls[i, j] = bubble;
                }
                cellPosition = new Vector3(i * 1.1f * size + j % 2 * size / 2, -j * 1.1f * size) + transform.position;
                Cell cell = Instantiate(cellPrefab, cellPosition, Quaternion.identity);
                cell.transform.parent = cellParent.transform;
                cell.name = "cell " + i + ""+ j;
                cell.tag = "cell";
                cell.i = i;
                cell.j = j;
                cells[i, j] = cell;







            }
        }
    }


  public  void boom(Bubble buble)
    {
        int row = 0;
        match(buble);
        
        for (int k = 0; k < matchBubbls.Count; k++)
        {
            if (matches > 2)
            {
                matchBubbls[k].tag = "dead";
            

                matchBubbls[k].gameObject.SetActive(false);
                if (matchBubbls[k].j > row) {
                    row = matchBubbls[k].j;
                }
                
            }
            else
                matchBubbls[k].isMatch = false;

        }

        if (matchBubbls.Count > 2) {
            score += matchBubbls.Count * matchBubbls.Count;
        }
      
        matches = 0;
        matchBubbls.Clear();


        downBubble();


    }

   

    

   public void match(Bubble bubble)
    {
       
        matches++;
        bubble.isMatch = true;
        matchBubbls.Add(bubble);
     
        int i = bubble.i;
        int j = bubble.j;

        
    

        //проверяем верхний
        if (j > 0 && bubbls[i, j - 1] != null)
        {
            if (bubbls[i, j - 1].isMatch != true)
            {
                if (bubble.tag == bubbls[i, j - 1].tag)
                {
                    match(bubbls[i, j - 1]);
                    
                }
            }


        }

        

        //проверяем правый
        if (i + 1 < width && bubbls[i + 1, j] != null)
        {
            if (bubbls[i + 1, j].isMatch != true)
            {
                if (bubble.tag == bubbls[i + 1, j].tag)
                {
                    match(bubbls[i + 1, j]);

                }
            }

            
            
            

        }

        //проверяем левый
        if (i > 0 && bubbls[i - 1, j] != null)
        {
            if (bubbls[i - 1, j].isMatch != true)
            {
                if (bubble.tag == bubbls[i - 1, j].tag)
                {
                    match(bubbls[i - 1, j]);

                }
            }





        }
        
        //Правый верхний
        if (j > 0 && i + 1 < width && bubbls[i+1, j - 1] != null && j%2!=0)
        {
            if (bubbls[i+1, j - 1].isMatch != true)
            {
                if (bubble.tag == bubbls[i+1, j - 1].tag)
                {
                    match(bubbls[i+1, j - 1]);

                }
            }


        }

        //Левый верхний
        if (j > 0 && i >0  && bubbls[i - 1, j - 1] != null && j%2==0)
        {
            if (bubbls[i - 1, j - 1].isMatch != true)
            {
                if (bubble.tag == bubbls[i - 1, j - 1].tag)
                {
                  
                    match(bubbls[i - 1, j - 1]);

                }
            }


        }

        // проверяем нижний
        if (j + 1 < height && bubbls[i, j + 1] != null)
        {
            if (bubbls[i, j + 1].isMatch != true)
            {
                if (bubble.tag == bubbls[i, j + 1].tag)
                {
                    match(bubbls[i, j + 1]);

                }
              
               
            }


        }


        //Левый нижний
        if (j + 1 < height && i > 0 && bubbls[i - 1, j + 1] != null && j%2==0)
        {
            if (bubbls[i - 1, j + 1].isMatch != true)
            {
                if (bubble.tag == bubbls[i - 1, j + 1].tag)
                {
                  
                    match(bubbls[i - 1, j+ 1]);

                }
             
                
            }


        }

        //  Правый нижний
        if (j + 1 < height && i+1< width && bubbls[i + 1, j + 1] != null && j%2!=0)
        {
            if (bubbls[i + 1, j + 1].isMatch != true)
            {
                if (bubble.tag == bubbls[i + 1, j + 1].tag)
                {
                    match(bubbls[i + 1, j + 1]);

                }
             
                
            }


        }




    }

    public void downBubble()
    {
       //Потом убрать
            for (int j = 1; j < height; j++)
            {
            for (int i = 0; i < width; i++)
            {

                if (bubbls[i, j] != null)
                {

                    if (bubbls[i, j - 1].isMatch || bubbls[i, j - 1] == null)
                    {
                        
                        if (i > 0 && i + 1 < width && (bubbls[i - (int)Mathf.Pow(-1, j % 2), j - 1].isMatch || bubbls[i - (int)Mathf.Pow(-1, j % 2), j - 1] != null))
                        {
                            if (bubbls[i, j].tag != "dead")
                            {
                                bubbls[i, j].isMatch = true;
                                downBubbls.Add(bubbls[i, j]);
                            }

                        }

                        if (i == 0) {
                            if (j % 2 == 0) {
                                if (bubbls[i, j].tag != "dead")
                                {
                                    bubbls[i, j].isMatch = true;
                                    downBubbls.Add(bubbls[i, j]);
                                }
                            }

                            if(j % 2 == 1 && (bubbls[i+1,j-1].isMatch|| bubbls[i + 1, j - 1]==null)) {
                                if (bubbls[i, j].tag != "dead")
                                {
                                    bubbls[i, j].isMatch = true;
                                    downBubbls.Add(bubbls[i, j]);
                                }
                            }
                        }

                        if (i == width - 1 && (bubbls[i - 1, j - 1].isMatch || bubbls[i - 1, j - 1] == null)) {

                            if (j % 2 == 1)
                            {
                                if (bubbls[i, j].tag != "dead")
                                {
                                    bubbls[i, j].isMatch = true;
                                    downBubbls.Add(bubbls[i, j]);
                                }
                            }
                        }





                        //условие срыва
                        if (bubbls[i, j].tag == "dead" || bubbls[i, j] == null || i == width - 1)
                        {
                            func();
                        }


                    }
                    

                    else decline();





                }












            }

        }
    }


    void func() {
        for (int i = 0; i < downBubbls.Count; i++) {
            //downBubbls[i].GetComponent<SpringJoint2D>().breakForce = 0.1f; 

            downBubbls[i].GetComponent<SpringJoint2D>().breakForce = 0;

            downBubbls[i].GetComponent<Rigidbody2D>().gravityScale = Random.Range(1,2);
            downBubbls[i].GetComponent<CircleCollider2D>().isTrigger = true;
            downBubbls[i].GetComponent<MeshRenderer>().material.color = Color.magenta;
            downBubbls[i].transform.tag = "dead";
        }
        downBubbls.Clear();
    }

    void decline()
    {
        for (int i = 0; i < downBubbls.Count; i++)
        {
            downBubbls[i].isMatch = false;

        }
        downBubbls.Clear();
    }



    public bool isWin() {
        float s = 0;
        bool a;

        for (int i = 0; i < width; i++) {
            if (bubbls[i, 0].tag != "dead") {
                s++;
            }
         }
            if (s / width > 0.3f) a = false;
            else a=true;
            
        
         return a;
    }


    public void winPanel(bool a)
    {

        if (a)
        {
            panel.SetActive(true);


            text.text = "WIIIN";
        }
        else
        {
            panel.SetActive(true);


            text.text = "LOOSE";
        }

    }
}
