Public Class HostContestantDataContext


    Public _currentQuestionLevel As Integer = -15
    Public _numberOfGameQuestions As Integer = 15

    Public _isHost As Boolean = False

    Public _questionText As String = "Question::Test"
    Public _answer1Text As String = "Answer1::Test"
    Public _answer2Text As String = "Answer2::Test"
    Public _answer3Text As String = "Answer3::Test"
    Public _answer4Text As String = "Answer4::Test"

    Public _finalAnswer As String = "0"
    Public _correctAnswer As String = "0"

    Public activeLifelines As String = "3"
    Public _lifelinesState As String = ""

    Public ReadOnly Property lifelinesState() As String
        Get
            Return $"{Lifeline1State};{Lifeline2State};{Lifeline3State};{Lifeline4State}"
        End Get
    End Property

    Public Lifeline1Name As String = "5050"
    Public Lifeline2Name As String = "PAF"
    Public Lifeline3Name As String = "ATA"
    Public Lifeline4Name As String = "STQ"

    Public Lifeline1State As String = "" '"UNUSED"
    Public Lifeline2State As String = "" '"UNUSED"
    Public Lifeline3State As String = "" '"UNUSED"
    Public Lifeline4State As String = "" '"UNUSED"

    Public fiftyFifty = ""
    Public paf = ""

    Public ataVotes As String = "0;0;0;0"
    'Public _ataUsedOnThisQ As Boolean = False
    'Public _atavote1 As Double = 0
    'Public _atavote2 As Double = 0
    'Public _atavote3 As Double = 0
    'Public _atavote4 As Double = 0

    Public _explanationAnswerText As String = "Explanation::Test"
    Public _pronunciationQaText As String = "Pronunciation::Test"

    Public _moneyTree As New List(Of String)
    Public lastTimeSetMoneyTree As DateTime = DateTime.Today.AddYears(-100)
    Public lastTimeContestantNameLoad As DateTime = DateTime.Now.AddMinutes(-100)

    Public _producerMessages As New List(Of String)
    Public _producerMessageBlink As Integer
    Public _producerTalkText As String

    Public _mark1Label As String = "A"
    Public _mark2Label As String = "B"
    Public _mark3Label As String = "C"
    Public _mark4Label As String = "D"

    Public _totalPrizeWonLocalizedMark As String = "TOTAL PRIZE WON:"
    Public _totalPrizeWonSume As String = ""

    Public _contestantNameCity As String = "John Doe, San Francisco, California"
    Public _contestantPartner As String = "Siena"

    'For Optimization
    Public currentQAState As String = ""
    Public currentAtaState As String = "NONE"
    Public currentExplState As String = ""
    Public currentStqState As String = ""
    Public currentDoubleDipState As String = ""
    Public currentSecondMilestone As String = ""
    Public currentGameStatusData As String = ""

    Sub New()

    End Sub

    Public Sub QuestionLoad(Question As String, Answer1 As String, Answer2 As String, Answer3 As String, Answer4 As String, CorrectAnswer As String, ExplanationAnswer As String, Pronunciation As String)
        'QuestionDataReset()
        _questionText = (Question)          'HttpUtility.HtmlDecode(Question)
        _answer1Text = (Answer1)            'HttpUtility.HtmlDecode(Answer1)
        _answer2Text = (Answer2)            'HttpUtility.HtmlDecode(Answer2)
        _answer3Text = (Answer3)            'HttpUtility.HtmlDecode(Answer3)
        _answer4Text = (Answer4)            'HttpUtility.HtmlDecode(Answer4)
        _correctAnswer = (CorrectAnswer)    'HttpUtility.HtmlDecode(CorrectAnswer)
        _explanationAnswerText = (ExplanationAnswer)      'HttpUtility.HtmlDecode(ExplanationAnswer)
        _pronunciationQaText = (Pronunciation)            'HttpUtility.HtmlDecode(Pronunciation)

    End Sub

    Public Sub QuestionDataReset()
        _questionText = ""
        _answer1Text = ""
        _answer2Text = ""
        _answer3Text = ""
        _answer4Text = ""
        _finalAnswer = 0
        _correctAnswer = 0
        _explanationAnswerText = ""
        _pronunciationQaText = ""

    End Sub

End Class
