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

    Enum Resolution
        FullHD = 1
        HDReady = 2
    End Enum

    Public Shared CurrentResolution As Resolution

    Shared Sub SetDesignerData(ScreenResolution As Resolution)
        Select Case ScreenResolution
            Case Resolution.FullHD

                QuestionFontSize = 38
                AnswerFontSize = 38
                ExplanationFontSize = 32
                DirectorsChatFontSize = 34
                PronunciationFontSize = 28

                QuestionFontCutOff = 1490
                AnswerFontCutOff = 640

            Case Resolution.HDReady

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
