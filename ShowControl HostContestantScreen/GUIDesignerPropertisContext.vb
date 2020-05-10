Public Class GUIDesignerPropertisContext

    Public Shared QuestionFontSize As Short = 27
    Public Shared AnswerFontSize As Short = 27
    Public Shared ExplanationFontSize As Short = 20
    Public Shared DirectorsChatFontSize As Short = 21
    Public Shared PronunciationFontSize As Short = 20
    Public Shared QuestionFontCutOff As Short = 1150
    Public Shared AnswerFontCutOff As Short = 500

    Public Shared DefaultQuestionFont = New Font(FontFamily.GenericSansSerif, QuestionFontSize)
    Public Shared DefaultAnswerFont = New Font(FontFamily.GenericSansSerif, AnswerFontSize)

    Public Shared GreenColorBox As Color = System.Drawing.Color.FromArgb(0, 192, 0)
    Public Shared RedColorBox As Color = System.Drawing.Color.FromArgb(219, 23, 24)
    Public Shared GrayColorBox As Color = Color.Gray

    Shared Sub SetDesignerData(ScreenResolution As String)
        Select Case ScreenResolution.ToUpper.Trim
            Case "1080", "1080P", "FULLHD", "1920X1080"
                QuestionFontSize = 38
                AnswerFontSize = 38
                ExplanationFontSize = 32
                DirectorsChatFontSize = 34
                PronunciationFontSize = 28

                QuestionFontCutOff = 1490
                AnswerFontCutOff = 640

            Case "768", "720P", "HDREADY", "1366X768", "1280X720"
                QuestionFontSize = 30
                AnswerFontSize = 30
                ExplanationFontSize = 20
                DirectorsChatFontSize = 21
                PronunciationFontSize = 20

                QuestionFontCutOff = 1150
                AnswerFontCutOff = 500

        End Select

    End Sub

End Class
