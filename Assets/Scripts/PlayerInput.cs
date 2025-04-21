using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;

public class PlayerInput : MonoBehaviour
{

    public CircularTimer timer;

    public TextMeshProUGUI currentComboText;
    public TextMeshProUGUI scoreText;

    private Color defaultColor;

    private string currentCombo = "WD";
    private int score = 0;
    private float inputTimeLimit = 2f;
    private bool inputReceived = false;

    void Start()
    {
        defaultColor = currentComboText.color;
        scoreText.text = $"Score: {score}";
        StartCoroutine(ComboRoutine());
    }

    IEnumerator ComboRoutine()
    {
        while (true)
        {
            GenerateCombo();
            inputReceived = false;

            float timer = 0f;
            while (timer < inputTimeLimit)
            {
                timer += Time.deltaTime;

                // 키 입력 감지
                if (!inputReceived && CheckInput())
                {
                    inputReceived = true;
                    score += 10;
                    StartCoroutine(FlashColor(Color.green));
                    break;
                }

                yield return null;
            }

            // 시간 내 입력 안됐거나 틀렸다면 감점
            if (!inputReceived)
            {
                score -= 10;
                StartCoroutine(FlashColor(Color.red));
            }

            UpdateScoreText();
            yield return new WaitForSeconds(1f); // 다음 문제까지 잠시 딜레이
        }
    }

    IEnumerator FlashColor(Color targetColor, float duration = 0.3f)
    {
        currentComboText.color = targetColor;
        yield return new WaitForSeconds(duration);
        currentComboText.color = defaultColor;
    }

    void GenerateCombo()
    {
        timer.StartTimer();
        List<string> validCombos = new List<string> { "WS", "WD", "AS", "AD" };

        string rawCombo = validCombos[Random.Range(0, validCombos.Count)];

        char[] comboChars = rawCombo.ToCharArray();
        System.Array.Sort(comboChars);
        currentCombo = new string(comboChars);
        currentComboText.text = currentCombo;
    }

    bool CheckInput()
    {
        List<char> keys = new List<char>();

        if (Input.GetKey(KeyCode.W)) keys.Add('W');
        if (Input.GetKey(KeyCode.A)) keys.Add('A');
        if (Input.GetKey(KeyCode.S)) keys.Add('S');
        if (Input.GetKey(KeyCode.D)) keys.Add('D');

        if (keys.Count != 2) return false;

        keys.Sort();
        string input = new string(new char[] { keys[0], keys[1] });

        Debug.Log($"입력된 조합: {input}, 정답: {currentCombo}");
        return input == currentCombo;
    }

    void UpdateScoreText()
    {
        scoreText.text = $"Score: {score}";
    }

}
