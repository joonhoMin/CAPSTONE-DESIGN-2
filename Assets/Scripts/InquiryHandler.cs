using UnityEngine;
using TMPro;
using System.Threading.Tasks;

public class InquiryHandler : MonoBehaviour
{
    public TMP_InputField inquiryInputField;
    public TMP_Text inquiryText;
    public GptApiFetcher apiFetcher;

    public static string allApiInquiryResponses = "";

    public async void HandleInquiry()
    {
        string userInput = inquiryInputField.text;
        inquiryInputField.text = "";
        inquiryText.text += $"\n  �� : {userInput}\n";

        string apiResponse = await Task.Run(() => apiFetcher.FetchGPTResponse(userInput));

        allApiInquiryResponses += apiResponse + "\n";

        inquiryText.text += $"\n ������ : {apiResponse}\n";
    }
}