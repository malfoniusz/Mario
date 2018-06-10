using UnityEngine;

public class StringUtility : MonoBehaviour
{
    public static string AddZerosToBeginning(int targetedTextLength, string text)
    {
        int numberOfZeros = targetedTextLength - text.Length;
        string zerosText = "";
        for (int i = 0; i < numberOfZeros; i++)
        {
            zerosText += "0";
        }

        return zerosText + text;
    }

}
