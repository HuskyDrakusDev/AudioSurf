using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public List<string> categoryNames;
    public List<int> selectedMaps;
    public List<float> dataY;
    public GraphChartFeed map;

	// Use this for initialization
	void Start () {
        map.dataY = new List<List<float>>();
        map.dataY.Add(new List<float>());
        for (int i = 0; i < dataY.Count;i++){
            map.dataY[0].Add(dataY[i]);
        }
        map.UpdateGraphPoints();
 
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
