Public Class GUIDesignerPropertisContext

    Public Shared QuestionFontSize As Short = 27
    Public Shared AnswerFontSize As Short = 27
    Public Shared ExplanationFontSize As Short = 20
    Public Shared DirectorsChatFontSize As Short = 21
    Public Shared PronunciationFontSize As Short = 20
    Public Shared QuestionFontCutOff As Short = 1150
    Public Shared AnswerFontCutOff As Short = 500

    Shared Sub SetDesignerData(ScreenResolution As String)
        Select Case ScreenResolution.ToUpper.Trim
            Case "1080", "1080P", "FULLHD", "1920X1080"
                QuestionFontSize = 35
                AnswerFontSize = 35
                ExplanationFontSize = 32
                DirectorsChatFontSize = 34
                PronunciationFontSize = 28

                QuestionFontCutOff = 1490
                AnswerFontCutOff = 640

            Case "768", "720P", "HDREADY", "1366X768", "1280X720"
                QuestionFontSize = 27
                AnswerFontSize = 27
                ExplanationFontSize = 20
                DirectorsChatFontSize = 21
                PronunciationFontSize = 20

                QuestionFontCutOff = 1150
                AnswerFontCutOff = 500

        End Select

    End Sub

End Class
