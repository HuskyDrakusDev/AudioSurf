using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSparks.Core;
using UnityEngine.UI;
public class GameScores : MonoBehaviour {

    private static GameScores _instance;
    public static GameScores Instance{ get { return _instance; }}
    public List<string> names;
    public List<int> scores;
    public List<GameObject> scorePanels;

    void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        LoadAllMessages();

    }
    public void LoadAllMessages()
    {

        scores = new List<int>();
        names = new List<string>();
        new GameSparks.Api.Requests.LogEventRequest().SetEventKey("LOAD_SCORES").Send((response) => {
            if (!response.HasErrors)
            {
                names = new List<string>();
                scores = new List<int>();
                Debug.Log("Received Player Data From GameSparks...");
                List<GSData> locations = response.ScriptData.GetGSDataList("all_Messages");
                for (var e = locations.GetEnumerator(); e.MoveNext();)
                {             
                    scores.Add(int.Parse(e.Current.GetString("SCORE")));
                    names.Add(e.Current.GetString("NAME"));
                                   
                }
            }
            else
            {
                Debug.Log("Error Loading Message Data...");
            }
        });
    }

    public void SaveMessage(string name, int score)
    {
        new GameSparks.Api.Requests.LogEventRequest()

            .SetEventKey("SAVE_SCORE")
            .SetEventAttribute("SCORE", score.ToString())
            .SetEventAttribute("NAME", name.ToString())
            .Send((response) =>
            {

                if (!response.HasErrors)
                {
                    Debug.Log("Message Saved To GameSparks...");
                }
                else
                {
                    Debug.Log("Error Saving Message Data...");
                }
            });
    }

        public void LoadHighScores()
        {

        int i = 0;
            for (i = 0; i < names.Count; i++)
            {
                GameObject nameCopy = scorePanels[i].transform.GetChild(0).gameObject;
                nameCopy.GetComponent<Text>().text = names[i];
                GameObject scoreCopy = scorePanels[i].transform.GetChild(1).gameObject;
                scoreCopy.GetComponent<Text>().text = scores[i].ToString();
            }
        for (i = i; i < scorePanels.Count;i++){
            GameObject nameCopy = scorePanels[i].transform.GetChild(0).gameObject;
            nameCopy.GetComponent<Text>().text = "NONE";
            GameObject scoreCopy = scorePanels[i].transform.GetChild(1).gameObject;
            scoreCopy.GetComponent<Text>().text = "NONE";
        }


        }
    public void RemoveHighScore(Text text){
        Debug.Log("text: "+ text.text);
        new GameSparks.Api.Requests.LogEventRequest()

            .SetEventKey("REMOVE_SCORES")
                      .SetEventAttribute("NAME", text.text)
            .Send((response) =>
            {

                if (!response.HasErrors)
                {
                    Debug.Log("Message Saved To GameSparks...");
                }
                else
                {
                    Debug.Log("Error Saving Message Data...");
                }
            });
        LoadAllMessages();
        GetComponent<GameScores>().LoadHighScores();
    }


    }

