using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInput : MonoBehaviour
{

    public float Score;
    public TextMeshProUGUI scoreText;

    // The text that shows the current combo
    public TextMeshProUGUI currentComboText;

    public string currentCombo;

    private Dictionary<KeyCode, float> keyTimes = new Dictionary<KeyCode, float>();
    public RandomCodeGenerator randomCodeGenerator;

    public float inputWindow = 0.5f; // Time threshhold to check for key presses

    private void Start()
    {
        //Initialize the combo
        currentCombo = randomCodeGenerator.Generate();
        currentComboText.text = "Current Combo: " + currentCombo;

        // Initialize the score
        Score = 0;
        scoreText.text = "Score: " + Score.ToString();
    }

    // Update is called once per frame
    void Update()
    {

        CheckKey(KeyCode.W);
        CheckKey(KeyCode.A);
        CheckKey(KeyCode.S);
        CheckKey(KeyCode.D);

        if (IsComboMatched("W", "D"))
        {
            Score += 10;
            Debug.Log("Combo W + D matched!");
            // Generate a new random code
            currentCombo = randomCodeGenerator.Generate();

        }
        else if (IsComboMatched("A", "S"))
        {
            Score += 10;
            Debug.Log("Combo D + F matched!");
            currentCombo = randomCodeGenerator.Generate();
        } 
        else if (IsComboMatched("W", "S"))
        {
            Score += 10;
            Debug.Log("Combo W + S matched!");
            currentCombo = randomCodeGenerator.Generate();
        }
        else if (IsComboMatched("A", "D"))
        {
            Score += 10;
            Debug.Log("Combo A + D matched!");
            currentCombo = randomCodeGenerator.Generate();
        }
        else
        {
            // If no combo matched, minus the score
            Score -= 10;
            Debug.Log("No combo matched.");
            currentCombo = randomCodeGenerator.Generate();
        }

            scoreText.text = "Score: " + Score.ToString();
            currentComboText.text = "Current Combo: " + currentCombo;
    }

    //Check how many seconds it took for players to press the key.
    void CheckKey(KeyCode key)
    {
        if (Input.GetKeyDown(key))
        {
            keyTimes[key] = Time.time;
        }
    }

    private bool IsComboMatched(string Key1, string Key2)
    {
        //If it's different, return false. player is wrong.
        if (!keyTimes.ContainsKey(ToKeyCode(Key1)) || !keyTimes.ContainsKey(ToKeyCode(Key2)))
        {
            return false;
        }

        // Check the pressed time difference between the two keys
        float t1 = keyTimes[ToKeyCode(Key1)];
        float t2 = keyTimes[ToKeyCode(Key2)];

        return Mathf.Abs(t1 - t2) <= inputWindow;
    }

    KeyCode ToKeyCode(string s)
    {
        // Convert the string to uppercase and parse it to KeyCode
        return (KeyCode)System.Enum.Parse(typeof(KeyCode), s.ToUpper());
    }

}
