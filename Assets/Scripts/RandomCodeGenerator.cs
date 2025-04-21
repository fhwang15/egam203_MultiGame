using UnityEngine;
using System.Collections.Generic;
using TMPro;
public class RandomCodeGenerator : MonoBehaviour
{
    public List<string> validCombos = new List<string> { "WS", "WD", "AS", "AD" };

    public List<char> codeLetters = new List<char>();

    //Generating a code that does not care about the order of the letters.
    //The code should be 2 characters long.

    public string Generate()
    {
        string combo = validCombos[Random.Range(0, validCombos.Count)];

        // 정렬해서 순서 통일 (ex: "SW" -> "WS")
        char[] letters = combo.ToCharArray();
        System.Array.Sort(letters);
        return new string(letters);
    }
}
