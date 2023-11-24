using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.UI;
using TMPro;


//Build your Credits.txt with an ! to denote headers, # to denote comments, everything else will be considered a name.
//Place your Credits.txt in Assets/Resources
public class CreditsHandler : MonoBehaviour
{
    [SerializeField] private string fileName = "Credits";
    [SerializeField] private TMP_FontAsset font;
    [SerializeField] private Color headerColor = Color.red;
    [SerializeField] private Color nameColor = Color.white;
    [SerializeField] private int headerSize = 35;
    [SerializeField] private int nameSize = 25;
    [SerializeField] Scrollbar scrollbar;
    [SerializeField] private float scrollSpeed = 0.1f;
    private bool isDragging = false;


    List<string> headers = new List<string>();
    List<List<string>> titles = new List<List<string>>();
    List<GameObject> creditsTexts = new List<GameObject>();

    public void SetIsDragging(bool val)
    {
        isDragging = val;
    }

    public void Awake()
    {
        // Read in from credits list
        bool newStart = false;
        TextAsset theList = (TextAsset)Resources.Load(fileName, typeof(TextAsset));
        string[] linesFromfile = theList.text.Split("\n"[0]);
        foreach (string line in linesFromfile)
        {
            string firstCharacter = line.Substring(0, 1);
            bool isIgnore = firstCharacter.Equals("#");
            bool isHeader = firstCharacter.Equals("!");
            if (firstCharacter.Equals("#")) continue;

            if (isHeader)
            {
                newStart = true;
                headers.Add(line.Substring(1));
            }
            else
            {
                if (newStart)
                {
                    titles.Add(new List<string>());
                    newStart = false;
                }
                titles[titles.Count - 1].Add(line);
            }
        }
    }

    void Start()
    {
        // Go through each header
        for (int i = 0; i < headers.Count; i++)
        {
            GameObject newObj = newText(headers[i], true);
            newObj.name = "Header";
            newObj.transform.localScale = new Vector3(1f, 1f, 1f);
            newObj.transform.localPosition = new Vector3(0f, 0f, 0f);
            creditsTexts.Add(newObj);

            // Go through each name in the header
            for (int j = 0; j < titles[i].Count; j++)
            {
                GameObject oObj = newText(titles[i][j], false);
                oObj.name = "Entry";
                oObj.transform.localScale = new Vector3(1f, 1f, 1f);
                oObj.transform.localPosition = new Vector3(0f, 0f, 0f);
                creditsTexts.Add(oObj);
            }
        }

        // Set the scrollbar to start
        scrollbar.value = 1f;
    }

    public GameObject newText(string labelText, bool isHeader)
    {
        GameObject textObj = new GameObject();
        TextMeshProUGUI text = textObj.AddComponent<TextMeshProUGUI>();
        textObj.transform.SetParent(this.transform);

        text.text = labelText;
        text.font = font;

        if (isHeader)
        {
            text.fontStyle = FontStyles.Bold;
            text.color = headerColor;
            text.fontSize = headerSize;
        }
        else
        {
            text.color = nameColor;
            text.fontSize = nameSize;
        }

        return textObj;
    }

    void Update()
    {
        if (!isDragging)
        {
            // Gradually scroll down
            scrollbar.value -= scrollSpeed * Time.deltaTime;

            // Clamp value between 0 and 1
            scrollbar.value = Mathf.Clamp01(scrollbar.value);
        }
    }
}
