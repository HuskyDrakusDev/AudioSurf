using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public List<string> categoryNames;
    public List<int> selectedMaps;
    public List<float> dataY;
    public List<Vector3> dataYPoints;
    public GraphChartFeed map;
    public Player player;
    public List<GameObject> rewards;
    public GameObject highScoresPanel;
    public GameObject menuPanel;
    public GameObject optionsPanel;
	// Use this for initialization
	void Start () {
        map.dataY = new List<List<float>>();
        dataYPoints = new List<Vector3>();
        map.dataY.Add(new List<float>());
        for (int i = 0; i < dataY.Count;i++){
            map.dataY[0].Add(dataY[i]);
        }
        map.UpdateGraphPoints();
        player.CreateBelzierPoints();
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<GameScores>().LoadHighScores();
	}
    public void HighScoresPressed(){
        optionsPanel.SetActive(false);
        menuPanel.SetActive(false);
        highScoresPanel.SetActive(true);
        GetComponent<GameScores>().LoadHighScores();
    }
    public void SaveMessage(string name, int score){
        GetComponent<GameScores>().SaveMessage(name,score);
    }
  
}
