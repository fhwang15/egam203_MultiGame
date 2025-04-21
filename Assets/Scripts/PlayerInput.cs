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

                // Ű �Է� ����
                if (!inputReceived && CheckInput())
                {
                    inputReceived = true;
                    score += 10;
                    StartCoroutine(FlashColor(Color.green));
                    break;
                }

                yield return null;
            }

            // �ð� �� �Է� �ȵưų� Ʋ�ȴٸ� ����
            if (!inputReceived)
            {
                score -= 10;
                StartCoroutine(FlashColor(Color.red));
            }

            UpdateScoreText();
            yield return new WaitForSeconds(1f); // ���� �������� ��� ������
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

        Debug.Log($"�Էµ� ����: {input}, ����: {currentCombo}");
        return input == currentCombo;
    }

    void UpdateScoreText()
    {
        scoreText.text = $"Score: {score}";
    }

}
