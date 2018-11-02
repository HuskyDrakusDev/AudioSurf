using UnityEngine;
using ChartAndGraph;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Linq;
public class GraphChartFeed : MonoBehaviour
{
     public List<List<DateTime>> dataX;
    //ydata for each sector
     public List<List<float>> dataY;
    public GameManager gameManager;
    public float graphThickness;
    public int skip;
    public List<Material> materials;
    public List<Material> fillMaterials;
    GraphChartBase graph;
    public List<GameObject> graphTitles;
    public List<int> clearedGraphs;
    public bool is3dGraph;
    public float graphMaxY;
    public float graphMaxX;
    public int numberOfRewards;
    public float scale;

    public void UpdateGraphPoints ()
    {
        scale = transform.localScale.x;
        graph = GetComponent<GraphChartBase>();
        graphMaxY = graph.HeightRatio;
        graphMaxX = graph.WidthRatio;


        if (graph != null)
        {
           //Debug.Log("yaiii");

            graph.DataSource.StartBatch();
            for (int i = 0; i < 1; i++)
            {
                string categoryName = gameManager.categoryNames[0];
                graph.DataSource.ClearCategory(categoryName);
            }


            //graph.DataSource.ClearCategory("Player 1");
            int count = 0;
            for (int i = 0; i < gameManager.categoryNames.Count; i++)
            {
                //if graph is selected add data
                if (gameManager.selectedMaps[i] == 1)
                {
                    string categoryName = gameManager.categoryNames[i];
                                        
                        
                        
                        graph.DataSource.ClearAndMakeBezierCurve(categoryName);

                    graph.DataSource.SetCategoryFill(categoryName, fillMaterials[count], false);



                    for (int j = 0; j < dataY[i].Count; j++)
                    {
                        if (j % skip == 0 || j == dataY[0].Count - 1)
                        {

                            if (j == 0)
                            {
                                    graph.DataSource.SetCurveInitialPoint(categoryName, 0f, dataY[i][0]);

                            }
                            else
                            {
                               
                                    graph.DataSource.AddLinearCurveToCategory(categoryName, new DoubleVector2(j , dataY[i][j] ));
                                
                            }
                        }
                            graph.DataSource.MakeCurveCategorySmooth(categoryName);
                    }
                  
                    count++;
                }


            }
            //Debug.Log("count: " + count);
            graph.DataSource.EndBatch();

        }
        SetupPoints();
    }
    public void SetupPoints()
    {
        float minY = gameManager.dataY.Min();
        float maxY = gameManager.dataY.Max();
       


        float xIncrement = dataY[0].Count;
        for (int i = 0; i < dataY[0].Count; i++)
        {
            float y_val = ((dataY[0][i] - minY) / (maxY - minY))*graphMaxY;
           // Debug.Log("i: "+i+" y_val: " + y_val);
            gameManager.dataYPoints.Add(new Vector3((((float)i/xIncrement)*graphMaxX)*scale,y_val*scale,0));

        }
    }
    public void SetupRewardAtPoints(float maxY, float maxX,float maxYAtPoint){
        GameObject player = gameManager.GetComponent<GameManager>().player.gameObject;
        for (int i = 0; i < numberOfRewards;i++){
            float xRand = UnityEngine.Random.Range(0, maxX);
            float yRand = maxYAtPoint;

            GameObject rewardObject = Instantiate(gameManager.rewards[0],new Vector3(xRand,yRand,player.GetComponent<Player>().zConst),gameManager.rewards[0].transform.rotation);
        }
    }
    public void ClearGraphs(){
       
      //  Debug.Log("graphtitles count: "+graphTitles.Count);
        int count = graphTitles.Count;
        ;
        for (int j = count-1; j >=0; j--)
        {
            GameObject g = graphTitles[j];
            graphTitles.Remove(g);
            Destroy(g);

        }
        for (int i = 0; i < gameManager.categoryNames.Count; i++)
        {
            if (i == 1)
            {
                string categoryName = gameManager.categoryNames[i];

                graph.DataSource.ClearCategory(categoryName);


            }
        }
        //Debug.Log("cleared");
    }
}
