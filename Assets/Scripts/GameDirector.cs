using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    public GptApiFetcher gptApiFetcher;
    GameObject objectCountText;
    int objectCount = 0;

    public string[,] informationArray { get; private set; }

    public string[,] CreateInformationArray(string topic)
    {
        string[,] infoArray = new string[10, 2];
        string response = gptApiFetcher.FetchGPTResponse(topic + "��� ������ ���� �ѱ����� 10���� ������ �տ� ��ȣ�� �ٿ��� �˷���. �ٸ� �ΰ����� �ؽ�Ʈ ���� �˷���� ��.");

        var lines = response.Split(new[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        int currentRow = 0;

        foreach (var line in lines)
        {
            if (currentRow >= 10)
            {
                break;
            }

            if (Regex.IsMatch(line, @"^\d+\.\s"))
            {
                infoArray[currentRow, 0] = line;
                infoArray[currentRow, 1] = "false";
                currentRow++;
            }
        }
        Debug.Log("Contents of InformationArray:");
        for (int i = 0; i < 10; i++)
        {
            Debug.Log($" {infoArray[i, 0]}, {infoArray[i, 1]}");
        }

        return infoArray;
    }

    public void PlayerGetObject()
    {
        this.objectCount++;
    }
    void Start()
    {
        string topic = PlayerPrefs.GetString("Topic");
        this.objectCountText = GameObject.Find("ObjectNumCount");
        informationArray = CreateInformationArray(topic);

    }

    // Update is called once per frame
    void Update()
    {
        this.objectCountText.GetComponent<Text>().text = this.objectCount.ToString();
    }
}
