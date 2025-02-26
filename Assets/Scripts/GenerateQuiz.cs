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

    //���� ����1 ����2 ����3 ����4 �� �� �� 5�� �����ϴ� ����迭
    public static string[,] quizArray = new string[5, 6]; 

    public async Task GenerateQuizArray()
    {
        string combinedInfo = "";

        // ù 10���� ���� ���ڿ� ��ġ��
        for (int i = 0; i < 10; i++)
        {
            combinedInfo += gameDirector.informationArray[i, 0] + "\n";
        }

        // �߰��� �������� �亯 ���ڿ� ��ġ��
        combinedInfo += InquiryHandler.allApiInquiryResponses;

        //�Է� ������Ʈ
        string request = combinedInfo + "�� ������ �������� ���� ������ ������ ��� ���ּ���. 0. �־��� �������� �̿��ϵ�, �ϳ��� �������� ������ �������� ������ ���� ������. 1. �ټ� ���� ������ ������ ������ּ���. 2. �� ������ �빮�� A, B, C, D�� ǥ�õ� �� ���� ��Ȯ�� ��� �������� ������ �մϴ�. 3. ��� ���� ������ �������ٿ� �Ѳ����� �����Ǿ�� �մϴ�. 4. ������ ������ �������θ� �־�� �ϸ�, ��/���� �Ǵ� �ٸ� ������ �ƴ� ���� Ȯ�����ּ���. 5. ������ ���ô� ������ �����ϴ�. '1.��������\nA.����\nB.����\nC.����\nD.����\n2.��������\nA.����\nB.����\nC.����\nD.����\n3.��������\nA.����\nB.����\nC.����\nD.����\n4.��������\nA.����\nB.����\nC.����\nD.����\n5.��������\nA.����\nB.����\nC.����\nD.����\n��:1.��� 2.��� 3.��� 4.��� 5.���'";

        int requestCount=3;
        while (requestCount > 0) {
            //quizResponse -> ������� ���ڿ� ����
            string quizResponse = await Task.Run(() => apiFetcher.FetchGPTResponse(request));

            //quizResponse�� parsing�� ����迭�� �ֱ�
            string[] lines = quizResponse.Split(new[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries); //���δ����� ������
            int currentQuestionIndex = -1;
            int i,j;

            for (i = 0; i < lines.Length; i++)
            {
                if (Regex.IsMatch(lines[i], @"^\d+\.")) // Check if the line is a question
                {
                    currentQuestionIndex++;
                    quizArray[currentQuestionIndex, 0] = lines[i]; // ���� ����

                    //���� ����
                    for (j = 1; j <= 4; j++)
                    {
                        if ((i + j) < lines.Length)
                            quizArray[currentQuestionIndex, j] = lines[i + j];
                    }

                    i += 4; // ���� ������ ���κ��� Ȯ��
                }
                if (currentQuestionIndex == 4) break; //5�������� ������� �� �����ϸ� Ż��
            }

            string answerContentFromCurrentLine = string.Join(" ", lines.Skip(i)); //���� ���κ��� ������ ��� ���ڿ� ��ħ

            //�� A,B,C,D�� ���ö����� �� ���ڸ� �迭�� ������ ���� ���ʴ�� ����
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

            //���� ������ ����� ���Դ��� üũ�ϰ� �ƴ϶�� �ִ�3���� �������û
            bool isValid = true;

            for (i = 0; i < 5; i++)
            {
                for (j = 0; j < 6; j++)
                {
                    if (string.IsNullOrEmpty(quizArray[i, j])) //�迭�� ��� index�ȿ� ���ڿ��� �����ϴ��� Ȯ��
                    {
                        isValid = false;
                    }

                    if (j == 5 && !Regex.IsMatch(quizArray[i, j], "^[ABCD]$")) //�� index�� A,B,C,D�� �Ѱ��� �����ϴ��� Ȯ��
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
