using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInput : MonoBehaviour
{

    public float Score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI comboText;

    private Dictionary<KeyCode, float> keyTimes = new Dictionary<KeyCode, float>();

    public float inputWindow = 0.5f; // Time threshhold to check for key presses

    // Update is called once per frame
    void Update()
    {

        CheckKey(KeyCode.W);
        CheckKey(KeyCode.A);
        CheckKey(KeyCode.S);
        CheckKey(KeyCode.D);

        if (IsComboMatched("W", "D"))
        {
            Debug.Log("Combo W + D matched!");
        }
        else if (IsComboMatched("A", "S"))
        {
            Debug.Log("Combo D + F matched!");
        } else if (IsComboMatched("W", "S"))
        {
            Debug.Log("Combo W + S matched!");
        }
        else if (IsComboMatched("A", "D"))
        {
            Debug.Log("Combo A + D matched!");
        }

        scoreText.text = "Score: " + Score.ToString();

    }

    void CheckKey(KeyCode key)
    {
        if (Input.GetKeyDown(key))
        {
            keyTimes[key] = Time.time;
        }
    }

    private bool IsComboMatched(string Key1, string Key2)
    {
        if (!keyTimes.ContainsKey(ToKeyCode(Key1)) || !keyTimes.ContainsKey(ToKeyCode(Key2))
        {
            return false;
        }

        float t1 = keyTimes[ToKeyCode(Key1)];
        float t2 = keyTimes[ToKeyCode(Key2)];

        return Mathf.Abs(t1 - t2) <= inputWindow;
    }

    KeyCode ToKeyCode(string s)
    {
        return (KeyCode)System.Enum.Parse(typeof(KeyCode), s.ToUpper());
    }

}
