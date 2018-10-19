using UnityEngine;
using ChartAndGraph;
using System.Collections.Generic;
using System;
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


    public void UpdateGraphPoints ()
    {
        
         graph = GetComponent<GraphChartBase>();

        if (graph != null)
        {
            Debug.Log("yaiii");

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
                                    graph.DataSource.SetCurveInitialPoint(categoryName, 0f, dataY[i][0] * 10f + 10f);

                            }
                            else
                            {
                               
                                    graph.DataSource.AddLinearCurveToCategory(categoryName, new DoubleVector2(j * 10f / 30f, dataY[i][j] * 10f + 20f));
                                
                            }
                        }
                            graph.DataSource.MakeCurveCategorySmooth(categoryName);
                    }
                  
                    count++;
                }

            }
            Debug.Log("count: " + count);
            graph.DataSource.EndBatch();
        }
    }

    public void ClearGraphs(){
       
        Debug.Log("graphtitles count: "+graphTitles.Count);
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
        Debug.Log("cleared");
    }
}
