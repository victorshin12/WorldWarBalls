using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScore : MonoBehaviour
{

    public TextMeshProUGUI MyScoreText;
    public TextMeshProUGUI HighScoreText;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        MyScoreText.text = "My Score: " + PlayerPrefs.GetInt("GameScore");
        HighScoreText.text = "High Score: " + PlayerPrefs.GetInt("GameHighScore");
    }
}
