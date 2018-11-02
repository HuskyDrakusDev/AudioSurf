using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public GameManager gameManager;
    public int health;
    public int currentHealth;
    public int lives;
    public Material material;
    public string playerName;
    public float jumpValue;
    public float speed;
    public int distancer;


    public List<Vector3> transformPoints;
    public bool start = true;
    public float waitTime;
    public float currentTime;
    public bool goToNextPoint = false;
    public float resolution;
    public List<Vector3> points;
    public int currentPosition = 0;
    public float constY;
    public float constYConst;
    public bool jumped;
    public bool jumpHeightReached;
    public float targetHeight;
    public float constJump;
    public float scale;
    public float zConst;
    public GameObject graph;

    // Use this for initialization
    public void CreateBelzierPoints () {
        scale = graph.transform.localScale.x;
        constJump = scale;
        speed = speed * scale;
        points = new List<Vector3>();
        transformPoints = gameManager.dataYPoints;
        for (int i = 0; i < transformPoints.Count; i++)
        {

            CreatePoints(i);


        }
    }
	//find position of points in real world space and just move the player based on that using the belzier thing online

	// Update is called once per frame
	void Update () {
        if (points.Count > 0)
        {
            //wait to move object to allow for 
            //ease in ease out 
            if (currentTime < waitTime)
            {
                currentTime += Time.deltaTime;
            }
            //move and rotate object
            else
            {
                //////accelerate
                //if (currentPosition >= 0 && currentPosition < points.Count / 2)
                //{
                //    waitTime /= ((2));
                //}
                ////decelerate
                //else
                //{
                //    waitTime *= (2);
                //}
                //move object
               // Debug.Log("currentPosition: "+points[currentPosition]);
                transform.localPosition = new Vector3(points[currentPosition].x, (points[currentPosition].y)+constY, zConst);
                if(!jumped&&Input.GetKeyDown(KeyCode.Space)){
                    targetHeight = CalculateJump(points[currentPosition].y);
                    Debug.Log("constY after jump: " + targetHeight);
                    jumped = true;
                }
                if(jumped){
                    if (!jumpHeightReached)
                    {
                       
                        float increaseFactor = (Time.deltaTime*speed);
                        Debug.Log(increaseFactor);
                        constY += increaseFactor;
                        if(constY>targetHeight){
                            jumpHeightReached = true;
                        }
                    }
                    else{
                        float decreaseFactor = (Time.deltaTime*speed);
                        Debug.Log(decreaseFactor);
                        constY -= decreaseFactor;
                        if (constY < constYConst)
                        {
                            constY = constYConst;
                            jumped = false;
                            jumpHeightReached = false;

                        }
                    }
                }
                //rotate object towards next point
                //if (currentPosition + 1 < points.Count)
                //    transform.LookAt(points[currentPosition + 1]);
                //else
                    //transform.LookAt(points[0]);

                currentTime = 0;
                //allows for looping 
                if (currentPosition + 1 == points.Count)
                {
                    currentPosition = 0;
                }
                else
                {
                    currentPosition++;
                }
            }
        }

    }
    public float CalculateJump(float currentYPosition){
        float result = constY+constJump;
        result += ((currentYPosition / 2f)*jumpValue);
        return result;
    }
    public void CreatePoints(int pos)
    {
        Vector3 p0 = transformPoints[ClampListPos(pos - 1)];
        Vector3 p1 = transformPoints[pos];
        Vector3 p2 = transformPoints[ClampListPos(pos + 1)];
        Vector3 p3 = transformPoints[ClampListPos(pos + 2)];

        Vector3 lastPos = p1;



        int loops = Mathf.FloorToInt(1f / resolution);

        for (int i = 1; i <= loops; i++)
        {

            //Which t position are we at?
            float t = i * resolution;

            //Find the coordinate between the end points with a Catmull-Rom spline
            Vector3 newPos = GetCatmullRomPosition(t, p0, p1, p2, p3);
            points.Add(newPos);

            //Save this pos so we can draw the next line segment
            lastPos = newPos;
            goToNextPoint = false;
        }


    }

    //Clamp the list positions to allow looping
    int ClampListPos(int pos)
    {
        if (pos < 0)
        {
            pos = transformPoints.Count - 1;
        }

        if (pos > transformPoints.Count)
        {
            pos = 1;
        }
        else if (pos > transformPoints.Count - 1)
        {
            pos = 0;
        }

        return pos;
    }
    //Returns a position between 4 Vector3 with Catmull-Rom spline algorithm
    //http://www.iquilezles.org/www/articles/minispline/minispline.htm
    Vector3 GetCatmullRomPosition(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        //The coefficients of the cubic polynomial (except the 0.5f * which I added later for performance)
        Vector3 a = 2f * p1;
        Vector3 b = p2 - p0;
        Vector3 c = 2f * p0 - 5f * p1 + 4f * p2 - p3;
        Vector3 d = -p0 + 3f * p1 - 3f * p2 + p3;

        //The cubic polynomial: a + b * t + c * t^2 + d * t^3
        Vector3 pos = 0.5f * (a + (b * t) + (c * t * t) + (d * t * t * t));

        return pos;
    }
    public bool isDead(){

        return false;
    }
    public void Die(){

    }
    public void Jump(){

    }
}
