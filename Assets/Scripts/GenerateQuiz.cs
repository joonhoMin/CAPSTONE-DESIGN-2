using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Linq;

public class GenerateQuiz : MonoBehaviour
{
    public GameDirector gameDirector;
    public InquiryHandler inquiryHandler;
    public GptApiFetcher apiFetcher;

    //문제 보기1 보기2 보기3 보기4 답 을 총 5개 저장하는 퀴즈배열
    public static string[,] quizArray = new string[5, 6]; 

    public async Task GenerateQuizArray()
    {
        string combinedInfo = "";

        // 첫 10개의 정보 문자열 합치기
        for (int i = 0; i < 10; i++)
        {
            combinedInfo += gameDirector.informationArray[i, 0] + "\n";
        }

        // 추가로 질의응답 답변 문자열 합치기
        combinedInfo += InquiryHandler.allApiInquiryResponses;

        //입력 프롬프트
        string request = combinedInfo + "위 정보를 바탕으로 다음 조건을 가지는 퀴즈를 내주세요. 0. 주어진 정보만을 이용하되, 하나의 정보만을 가지고 여러개의 문제를 내지 마세요. 1. 다섯 개의 객관식 문제를 만들어주세요. 2. 각 문제는 대문자 A, B, C, D로 표시된 네 개의 명확한 답안 선택지를 가져야 합니다. 3. 퀴즈에 대한 정답은 마지막줄에 한꺼번에 나열되어야 합니다. 4. 문제가 객관식 형식으로만 있어야 하며, 참/거짓 또는 다른 형식이 아닌 것을 확인해주세요. 5. 형식의 예시는 다음과 같습니다. '1.문제내용\nA.보기\nB.보기\nC.보기\nD.보기\n2.문제내용\nA.보기\nB.보기\nC.보기\nD.보기\n3.문제내용\nA.보기\nB.보기\nC.보기\nD.보기\n4.문제내용\nA.보기\nB.보기\nC.보기\nD.보기\n5.문제내용\nA.보기\nB.보기\nC.보기\nD.보기\n답:1.답안 2.답안 3.답안 4.답안 5.답안'";

        int requestCount=3;
        while (requestCount > 0) {
            //quizResponse -> 응답받은 문자열 전문
            string quizResponse = await Task.Run(() => apiFetcher.FetchGPTResponse(request));

            //quizResponse를 parsing해 퀴즈배열에 넣기
            string[] lines = quizResponse.Split(new[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries); //라인단위로 나누기
            int currentQuestionIndex = -1;
            int i,j;

            for (i = 0; i < lines.Length; i++)
            {
                if (Regex.IsMatch(lines[i], @"^\d+\.")) // Check if the line is a question
                {
                    currentQuestionIndex++;
                    quizArray[currentQuestionIndex, 0] = lines[i]; // 문제 저장

                    //보기 저장
                    for (j = 1; j <= 4; j++)
                    {
                        if ((i + j) < lines.Length)
                            quizArray[currentQuestionIndex, j] = lines[i + j];
                    }

                    i += 4; // 보기 이후의 라인부터 확인
                }
                if (currentQuestionIndex == 4) break; //5번문제와 보기까지 다 저장하면 탈출
            }

            string answerContentFromCurrentLine = string.Join(" ", lines.Skip(i)); //현재 라인부터 끝까지 모든 문자열 합침

            //답 A,B,C,D가 나올때마다 그 문자를 배열의 마지막 열에 차례대로 삽입
            currentQuestionIndex = -1;
            foreach (char c in answerContentFromCurrentLine)
            {
                if (c == 'A' || c == 'B' || c == 'C' || c == 'D')
                {
                    currentQuestionIndex++;
                    quizArray[currentQuestionIndex, 5] = c.ToString();
                }
                if (currentQuestionIndex == 4) break;
            }

            //응답 형식이 제대로 들어왔는지 체크하고 아니라면 최대3번의 재응답요청
            bool isValid = true;

            for (i = 0; i < 5; i++)
            {
                for (j = 0; j < 6; j++)
                {
                    if (string.IsNullOrEmpty(quizArray[i, j])) //배열의 모든 index안에 문자열이 존재하는지 확인
                    {
                        isValid = false;
                    }

                    if (j == 5 && !Regex.IsMatch(quizArray[i, j], "^[ABCD]$")) //답 index에 A,B,C,D중 한개가 존재하는지 확인
                    {
                        isValid = false;
                    }
                }
            }

            if (isValid) break;
            else requestCount -= 1;
        }


    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
