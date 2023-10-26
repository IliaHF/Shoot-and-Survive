using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Leaderboard : MonoBehaviour
{
	public Dictionary<int, int> playersScore = new Dictionary<int, int>();
	public Dictionary<int, string> playersName = new Dictionary<int, string>();

    public void AddPlayer(int id, string name) {
        playersScore.Add(id, 0);
        playersName.Add(id, name);
        UpdateLeaderboard();
    }

    public void RemovePlayer(int id) {
        if(!playersScore.ContainsKey(id))
            return;

        playersScore.Remove(id);
        playersName.Remove(id);
        UpdateLeaderboard();
    }

    public void AddScore(int id) {
        if(!playersScore.ContainsKey(id))
            return;

        playersScore[id]++;
        UpdateLeaderboard();
    }

    public void UpdateLeaderboard() {
        Debug.Log("Updated: " + playersScore);

        var playersScoreSorted = playersScore.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        Debug.Log("Sorted: " + playersScoreSorted);

        for (int i = 0; i < GameManager.Instance.uiManager.playersScore.Length; i++) {
            GameManager.Instance.uiManager.playersScore[i].text = "";
            GameManager.Instance.uiManager.playersName[i].text = "";
        }

        for(int i = 0; i < playersScoreSorted.Count; i++) {
            GameManager.Instance.uiManager.playersScore[i].text = playersScoreSorted.ElementAt(i).Value.ToString();
            GameManager.Instance.uiManager.playersName[i].text = (i+1).ToString() + ". " + playersName[playersScoreSorted.ElementAt(i).Key];
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateLeaderboard();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
