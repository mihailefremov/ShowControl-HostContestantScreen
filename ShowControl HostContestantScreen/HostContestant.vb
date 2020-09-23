Imports System.Threading
Imports System.Web

Public Class HostContestant

    Public GamePlayStateId As String = ""
    Public ProducerChatStateId As String = ""
    Public OneTimeMessagesStateId As String = ""

    Public QuizOperatorDataToExecute As String = ""
    Public ProducerDataToExecute As String = ""

    Public hostContestantData As New HostContestantDataContext

    Public ThreadProducerChatListener As New Thread(AddressOf ListenAndProcessLatestProducerChatState)
    Public ThreadGameStateListener As New Thread(AddressOf ListenAndProcessLatestGamePlayData)
    Public ThreadOneTimeMessageListener As New Thread(AddressOf ListenAndProcessOneTimeMessages)

    Public Overloads Sub Show(isHost As Boolean, Resolution As String)
        hostContestantData._isHost = isHost
        GUIDesignerPropertisContext.SetDesignerData(Resolution)
        Show()
    End Sub

    Private Sub HostContestant_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        EnableGUIIfHost()
        ConfigureLocalVersion()

        'ThreadGamePlayStateListener.Start()
        ThreadProducerChatListener.Start()
        ThreadGameStateListener.Start()
        ThreadOneTimeMessageListener.Start()

        If ThreadProducerChatListener.IsAlive And ThreadGameStateListener.IsAlive And ThreadOneTimeMessageListener.IsAlive Then
            MessageBox.Show(":: LISTENER STARTED ::" & vbCrLf)
        End If

    End Sub

    Private Sub HostContestant_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'ThreadGamePlayStateListener.Abort()
        ThreadProducerChatListener.Abort()
        ThreadGameStateListener.Abort()
        ThreadOneTimeMessageListener.Abort()
    End Sub

    Delegate Sub SetThisFormCloseCallback()
    Private Sub SetThisFormCloseThreadSafe()
        If Me.InvokeRequired Then
            Dim d As SetThisFormCloseCallback = New SetThisFormCloseCallback(AddressOf SetThisFormCloseThreadSafe)
            Me.Invoke(d)
        Else
            Me.Close()
        End If
    End Sub

#Region "Obsolete"
    Public Property MessageQue As String

    Sub ListenGamePlayStateAllTime()
        While True
            GetLatestGamePlayState()
            Thread.Sleep(200)
        End While
    End Sub

    Public Sub GetLatestGamePlayState()

        Dim ReceivedData As String = HttpApiRequests.GetPostRequests.Get($"https://{My.Settings.IPAddress}/wwtbam-state/GetMainGamePlayState.php")

        MessageQue = ReceivedData

    End Sub

    'Sub ProcessGamePlayData()
    '    Dim p As String
    '    While True And IsListenerAlive
    '        p = MessageQue
    '        If Not IsNothing(p) Then
    '            OnLineReceivedQuizOperator(p)
    '        End If
    '        Thread.Sleep(50)
    '    End While
    'End Sub

    ' UPDATE TEXT WHEN DATA IS RECEIVED
    'Private Sub OnLineReceivedQuizOperator(Data As String)
    '    'QuizOperatorDataToExecute = Data + vbCrLf

    '    Dim serializer As Xml.Serialization.XmlSerializer = New Xml.Serialization.XmlSerializer(GetType(Xml2CSharp.WWTBAMMAINGAMEPLAYSTATE))
    '    Dim WwtbamPlayState As Xml2CSharp.WWTBAMMAINGAMEPLAYSTATE

    '    Dim reader As System.IO.TextReader = New System.IO.StringReader(Data)
    '    Try
    '        WwtbamPlayState = serializer.Deserialize(reader)
    '    Catch ex As Exception
    '        Return
    '    End Try

    '    Dim TimeStampFromData As String = WwtbamPlayState.STATEID 'GetValueStringBetweenTags(QuizOperatorDataToExecute, "<STATEID>", "</STATEID>")
    '    If GamePlayStateId.Equals(TimeStampFromData) Then
    '        Exit Sub
    '    End If
    '    GamePlayStateId = TimeStampFromData

    '    UpdateExecuteCommandText(Me, WwtbamPlayState)
    'End Sub

    Private Sub OnLineReceivedProducer(Data As String)
        ProducerDataToExecute = Data + vbCrLf

        Dim TimeStampFromData As String = GetValueStringBetweenTags(ProducerDataToExecute, "<STATEID>", "</STATEID>")
        If GamePlayStateId = TimeStampFromData Then
            Exit Sub
        End If
        GamePlayStateId = TimeStampFromData

        'TODO PRODUCER
        'UpdateExecuteCommandText(Me, ProducerDataToExecute)
    End Sub

    Public Function GetValueStringBetweenTags(value As String, startTag As String, endTag As String) As String
        Try
            If value.ToUpper.Contains(startTag) And value.ToUpper.Contains(endTag) Then
                Dim Index As Integer = value.IndexOf(startTag) + startTag.Length
                Return value.Substring(Index, value.IndexOf(endTag) - Index)
            Else
                Return String.Empty
            End If
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function

#End Region

#Region "MAINGAMEPLAY"
    Sub ListenAndProcessLatestGamePlayData()
        Dim ReceivedData As String

        Dim serializerLastChange As Xml.Serialization.XmlSerializer = New Xml.Serialization.XmlSerializer(GetType(Xml2CSharp.WWTBAMLASTGAMEPLAYSTATECHANGE))
        Dim serializerPlayState As Xml.Serialization.XmlSerializer = New Xml.Serialization.XmlSerializer(GetType(Xml2CSharp.WWTBAMMAINGAMEPLAYSTATE))

        While True
            ReceivedData = HttpApiRequests.GetPostRequests.Get($"https://{My.Settings.IPAddress}/wwtbam-state/GetLastGamePlayStateChange.php")

            Dim WwtbamLastChangedPlayState As Xml2CSharp.WWTBAMLASTGAMEPLAYSTATECHANGE
            Dim readerChangedPlayState As System.IO.TextReader = New System.IO.StringReader(ReceivedData)
            Try
                WwtbamLastChangedPlayState = serializerLastChange.Deserialize(readerChangedPlayState)
            Catch ex As Exception
                Threading.Thread.Sleep(100)
                Continue While
            End Try

            Dim TimeStampFromData As String = WwtbamLastChangedPlayState.STATEID 'GetValueStringBetweenTags(QuizOperatorDataToExecute, "<STATEID>", "</STATEID>")
            If GamePlayStateId.Equals(TimeStampFromData) Then
                Threading.Thread.Sleep(100)
                Continue While
            Else
                GamePlayStateId = TimeStampFromData

                ReceivedData = HttpApiRequests.GetPostRequests.Get($"https://{My.Settings.IPAddress}/wwtbam-state/GetMainGamePlayState.php")
                Dim WwtbamPlayState As Xml2CSharp.WWTBAMMAINGAMEPLAYSTATE
                Dim readerPlayState As System.IO.TextReader = New System.IO.StringReader(ReceivedData)
                Try
                    WwtbamPlayState = serializerPlayState.Deserialize(readerPlayState)
                Catch ex As Exception
                    Threading.Thread.Sleep(100)
                    Continue While
                End Try

                ExecuteWwtbamPlayState(Me, WwtbamPlayState)
                Threading.Thread.Sleep(100)
            End If

        End While

    End Sub
    ' ALLOW THREAD TO COMMUNICATE WITH FORM CONTROL
    Private Delegate Sub ExecuteWwtbamPlayStateDelegate(TB As Form, wwtbamPlayState As Xml2CSharp.WWTBAMMAINGAMEPLAYSTATE)
    ' UPDATE TEXTBOX
    Private Sub ExecuteWwtbamPlayState(TB As Form, wwtbamPlayState As Xml2CSharp.WWTBAMMAINGAMEPLAYSTATE)
        If TB.InvokeRequired Then
            TB.Invoke(New ExecuteWwtbamPlayStateDelegate(AddressOf ExecuteWwtbamPlayState), New Object() {TB, wwtbamPlayState})
        Else
            If wwtbamPlayState IsNot Nothing Then
                'TB.AppendText(txt & vbCrLf)
                Try
                    ExecuteClientMessage(wwtbamPlayState)
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
            End If
        End If
    End Sub
    Private Sub ExecuteClientMessage(wwtbamPlayState As Xml2CSharp.WWTBAMMAINGAMEPLAYSTATE)

        'If wwtbamPlayState.Trim() = "" Then Exit Sub

        Dim QuestionAnswerState As String = wwtbamPlayState.QUESTIONANSWERSSTATE
        Dim DoubleDipState As String = wwtbamPlayState.DOUBLEDIPSTATE

        hostContestantData.QuestionLoad(wwtbamPlayState.QUESTIONTEXT, wwtbamPlayState.ANSWER1TEXT, wwtbamPlayState.ANSWER2TEXT, wwtbamPlayState.ANSWER3TEXT, wwtbamPlayState.ANSWER4TEXT, wwtbamPlayState.CORRECTANSWER, wwtbamPlayState.EXPLANATIONTEXT, wwtbamPlayState.PRONUNCIATIONTEXT)

        If String.Compare(hostContestantData.currentQAState, QuestionAnswerState, True) <> 0 Or String.Compare(hostContestantData.currentDoubleDipState, DoubleDipState, True) <> 0 Then
            If String.Compare("NONE", QuestionAnswerState, True) = 0 Then
                QuestionDissolve()
            ElseIf String.Compare("READQ", QuestionAnswerState, True) = 0 Then
                QuestionFire()
            ElseIf String.Compare("READQ1", QuestionAnswerState, True) = 0 Then
                QuestionFire()
                AnswerFire(1)
            ElseIf String.Compare("READQ12", QuestionAnswerState, True) = 0 Then
                QuestionFire()
                AnswerFire(1)
                AnswerFire(2)
            ElseIf String.Compare("READQ123", QuestionAnswerState, True) = 0 Then
                QuestionFire()
                AnswerFire(1)
                AnswerFire(2)
                AnswerFire(3)
            ElseIf String.Compare("READQ1234", QuestionAnswerState, True) = 0 Then
                QuestionFire()
                AnswerFire(1)
                AnswerFire(2)
                AnswerFire(3)
                AnswerFire(4)
            ElseIf String.Compare("FINALANSWERGIVEN", QuestionAnswerState, True) = 0 Then
                QuestionFire()
                AnswerFire(1)
                AnswerFire(2)
                AnswerFire(3)
                AnswerFire(4)
                hostContestantData.currentDoubleDipState = DoubleDipState
                FinalAnswer(wwtbamPlayState.FINALANSWER, String.Equals(DoubleDipState, "DoubleDipFirstFinal", StringComparison.OrdinalIgnoreCase))
                If String.Equals(DoubleDipState, "DoubleDipSecondFinal", StringComparison.OrdinalIgnoreCase) Then
                    DoubleDip(wwtbamPlayState.DOUBLEDIPFIRSTANSWER)
                End If
            ElseIf String.Compare("CORRECTANSWERREVEAL", QuestionAnswerState, True) = 0 Then
                QuestionFire()
                AnswerFire(1)
                AnswerFire(2)
                AnswerFire(3)
                AnswerFire(4)
                Dim FinalAnswerStr As String = wwtbamPlayState.FINALANSWER
                Dim isDoubleDipFirstFinal As Boolean = String.Equals(DoubleDipState, "DoubleDipFirstFinal", StringComparison.OrdinalIgnoreCase)
                FinalAnswer(FinalAnswerStr, isDoubleDipFirstFinal)

                Dim CorrectAnswerStr As String = wwtbamPlayState.CORRECTANSWER
                hostContestantData.currentDoubleDipState = DoubleDipState

                Dim isDoubleDipSecondFinal As Boolean = String.Equals(DoubleDipState, "DoubleDipSecondFinal", StringComparison.OrdinalIgnoreCase)
                If String.Equals(FinalAnswerStr, CorrectAnswerStr, StringComparison.OrdinalIgnoreCase) Then
                    If isDoubleDipSecondFinal Then
                        DoubleDip(wwtbamPlayState.DOUBLEDIPFIRSTANSWER)
                    End If
                    CorrectAnswerReveal(CorrectAnswerStr)
                Else
                    If isDoubleDipFirstFinal Then
                        DoubleDip(wwtbamPlayState.DOUBLEDIPFIRSTANSWER)
                    ElseIf isDoubleDipSecondFinal Then
                        DoubleDip(wwtbamPlayState.DOUBLEDIPFIRSTANSWER)
                        CorrectAnswerReveal(CorrectAnswerStr)
                    Else
                        CorrectAnswerReveal(CorrectAnswerStr)
                    End If
                End If
            ElseIf QuestionAnswerState.StartsWith("TOTALPRIZEWON", StringComparison.OrdinalIgnoreCase) Then
                TotalPrizeWonReveal(QuestionAnswerState)
            End If
        End If

        Dim ExplanationState As String = wwtbamPlayState.EXPLANATIONSTATE
        If String.Compare(hostContestantData.currentExplState, ExplanationState, True) = 0 Then
            If ExplanationState = "NONE" Then
                ExplanationDissolve()
            ElseIf ExplanationState = "FIRED" Then
                ExplanationFire()
            End If
        End If

        Dim SecondMilestoneAt As String = wwtbamPlayState.SECONDMILESTONEAT
        If String.Compare(SecondMilestoneAt, hostContestantData.currentSecondMilestone, True) <> 0 Then
            SetMilestone(SecondMilestoneAt)
        End If

        QLevelMove(Int(wwtbamPlayState.QLEVEL))
        Dim CurrentGameStatusData As String = wwtbamPlayState.BANKDROPIFCORRECTIFWRONG
        If String.Compare(hostContestantData.currentGameStatusData, CurrentGameStatusData, True) <> 0 Then
            hostContestantData.currentGameStatusData = CurrentGameStatusData
            CurrentGameStatus(CurrentGameStatusData)
        End If

        Dim ActiveLifelines As String = wwtbamPlayState.ACTIVELIFELINES
        If String.Compare(hostContestantData.activeLifelines, ActiveLifelines, True) <> 0 Then
            If String.Compare(ActiveLifelines, "4") = 0 Then
                AddFourthLifeline()
            Else
                RemoveFourthLifeline()
            End If
        End If

        Dim LifelinesState As String = wwtbamPlayState.LIFELINESSTATE
        LifelineStateSet(LifelinesState)

        Dim FiftyFifty As String = wwtbamPlayState.FIFTYFIFTY 'GetValueStringBetweenTags(wwtbamPlayState, "<FIFTYFIFTY>", "</FIFTYFIFTY>")
        If String.Compare(hostContestantData.fiftyFifty, FiftyFifty, True) <> 0 Then
            FiftyFiftyFire(FiftyFifty)
        End If

        Dim Paf As String = wwtbamPlayState.PAF 'GetValueStringBetweenTags(wwtbamPlayState, "<PAF>", "</PAF>")
        If String.Compare(hostContestantData.paf, Paf, True) <> 0 Then
            If String.Compare(Paf, "START", True) = 0 Then
                PAFFire()
            Else
                PAFAbort()
            End If
        End If

        Dim AtaState As String = wwtbamPlayState.ATASTATE 'GetValueStringBetweenTags(wwtbamPlayState, "<ATASTATE>", "</ATASTATE>")
        If String.Compare(hostContestantData.currentAtaState, AtaState, True) <> 0 Then
            If String.Compare(AtaState, "NONE", True) = 0 Then
                AskTheAudienceGraphHide()
            ElseIf String.Compare(AtaState, "CLEARDIAGRAM", True) = 0 Then
                AskTheAudienceReady()
            ElseIf String.Compare(AtaState, "DIAGRAMWITHPERCENTAGE", True) = 0 Then
                Dim AtaPercents As String = wwtbamPlayState.ATAPERCENTS 'GetValueStringBetweenTags(wwtbamPlayState, "<ATAPERCENTS>", "</ATAPERCENTS>")
                AskTheAudienceEndVote(AtaPercents)
            Else
                AskTheAudienceGraphHide()
            End If
        End If

        If hostContestantData.lastTimeContestantNameLoad < DateTime.Now.AddMinutes(-2) Then
            ContestantLoad(wwtbamPlayState.CONTESTANTNAMECITY, wwtbamPlayState.PARTNERNAME)
        End If

        UserInterfaceAdaptation()

        ''Dim PronunciationState As String
        ''Dim StqState As String

    End Sub

    Private Sub UserInterfaceAdaptation()
        If hostContestantData._isHost Then
            'sostojba publika
            If String.Equals(hostContestantData.currentAtaState, "CLEARDIAGRAM", StringComparison.OrdinalIgnoreCase) _
                OrElse String.Equals(hostContestantData.currentAtaState, "DIAGRAMWITHPERCENTAGE", StringComparison.OrdinalIgnoreCase) Then

                ExplanationDissolve()
                'ExplanationQuestion_TextBox.Visible = False
            End If
        End If
    End Sub

    Private Sub TotalPrizeWonReveal(QuestionAnswerState As String)
        Dim totalPrizeArray() As String = QuestionAnswerState.Split("|")
        If totalPrizeArray.Length = 2 Then
            hostContestantData._totalPrizeWonSume = totalPrizeArray.ElementAt(1)
            QuestionDissolve()
            Question_Label.Text = $"{hostContestantData._totalPrizeWonLocalizedMark}{vbCrLf}{hostContestantData._totalPrizeWonSume}"
        End If
        hostContestantData.currentQAState = "TOTALPRIZEWON"
    End Sub

#End Region

#Region "PRODUCER"
    Sub ListenAndProcessLatestProducerChatState()
        While True
            If Not hostContestantData._isHost Then
                Threading.Thread.Sleep(10000)
                Continue While
            End If
            Dim PlayState As String = HttpApiRequests.GetPostRequests.Get($"https://{My.Settings.IPAddress}/wwtbam-state/GetProducerChatState.php")


            ' Create an instance of the XmlSerializer.
            Dim serializer As New Xml.Serialization.XmlSerializer(GetType(Xml2CSharp.WWTBAMPRODUCERCHATSTATE))

            ' Declare an object variable of the type to be deserialized.
            Dim ProducerChatState As New Xml2CSharp.WWTBAMPRODUCERCHATSTATE

            Try
                Using reader As New IO.StringReader(PlayState)
                    ' Call the Deserialize method to restore the object's state.
                    ProducerChatState = CType(serializer.Deserialize(reader), Xml2CSharp.WWTBAMPRODUCERCHATSTATE)
                End Using
            Catch ex As Exception
                Threading.Thread.Sleep(250)
                Continue While
            End Try

            If IsNothing(ProducerChatState) Then Continue While
            If ProducerChatState.STATEID.Equals(ProducerChatStateId) Then
                Threading.Thread.Sleep(250)
                Continue While
            Else
                ProducerChatStateId = ProducerChatState.STATEID
                ProcessProducerChat(ProducerChatState)
                Threading.Thread.Sleep(250)
            End If

        End While
    End Sub
    Async Sub ProcessProducerChat(ProducerChatState As Xml2CSharp.WWTBAMPRODUCERCHATSTATE)

        Dim blinking As Integer = 0
        If ProducerChatState.PRODUCERCHATSTATE.StartsWith("BLINK", StringComparison.OrdinalIgnoreCase) Then
            If Not Integer.TryParse(ProducerChatState.PRODUCERCHATSTATE.ToUpper.Replace("BLINK", String.Empty), blinking) Then
                blinking = 1
            End If
        End If

        'Dim tekstToWrite As String = ProducerChatState.PRODUCERCHATTEXT
        'Dim arrayChat As String() = tekstToWrite.Split(New Char() {vbCr, vbCrLf})

        Dim output As New Text.StringBuilder
        If blinking > 0 Then
            For index As Short = 0 To blinking * 2
                If index Mod 2 = 1 Then
                    WriteToProducersChatTextBox("")
                Else
                    WriteToProducersChatTextBox(ProducerChatState.PRODUCERCHATTEXT)
                End If
                Await Threading.Tasks.Task.Delay(500)
            Next
            If blinking Mod 2 = 0 Then
                WriteToProducersChatTextBox(ProducerChatState.PRODUCERCHATTEXT)
            End If
        Else
            WriteToProducersChatTextBox(ProducerChatState.PRODUCERCHATTEXT)
        End If

    End Sub
    Public Sub WriteToProducersChatTextBox(ByVal value As String)
        If InvokeRequired Then
            Me.Invoke(New Action(Of String)(AddressOf WriteToProducersChatTextBox), New Object() {value})
            Return
        End If
        ProducersChat_TextBox.Text = value
    End Sub
#End Region

#Region "ONETIMEMESSAGE"
    Sub ListenAndProcessOneTimeMessages()
        'TODO+
        While True
            Dim PlayState As String = HttpApiRequests.GetPostRequests.Get($"https://{My.Settings.IPAddress}/wwtbam-state/GetOTMessageGamePlay.php")

            ' Create an instance of the XmlSerializer.
            Dim serializer As New Xml.Serialization.XmlSerializer(GetType(Xml2CSharp.WWTBAMONETIMEMESSAGESGAMEPLAY))

            ' Declare an object variable of the type to be deserialized.
            Dim OneTimeMessageGamePlay As New Xml2CSharp.WWTBAMONETIMEMESSAGESGAMEPLAY

            Try
                Using reader As New IO.StringReader(PlayState)
                    ' Call the Deserialize method to restore the object's state.
                    OneTimeMessageGamePlay = CType(serializer.Deserialize(reader), Xml2CSharp.WWTBAMONETIMEMESSAGESGAMEPLAY)
                End Using
            Catch ex As Exception
                Threading.Thread.Sleep(1000)
                Continue While
            End Try

            If IsNothing(OneTimeMessageGamePlay) Then Continue While
            If OneTimeMessageGamePlay.STATEID.Equals(OneTimeMessagesStateId) Then
                Threading.Thread.Sleep(1000)
                Continue While
            Else
                OneTimeMessagesStateId = OneTimeMessageGamePlay.STATEID
                ProcessOneTimeMessage(OneTimeMessageGamePlay)
                Threading.Thread.Sleep(1000)
            End If

        End While
    End Sub

    Dim OneTimeExplanationLastChange, OneTimeOtherLastChange As String

    Sub ProcessOneTimeMessage(OneTimeMessageGamePlay As Xml2CSharp.WWTBAMONETIMEMESSAGESGAMEPLAY) 'Async Sub? 
        'TODO
        If InvokeRequired Then
            Me.Invoke(New Action(Of Xml2CSharp.WWTBAMONETIMEMESSAGESGAMEPLAY)(AddressOf ProcessOneTimeMessage), New Xml2CSharp.WWTBAMONETIMEMESSAGESGAMEPLAY() {OneTimeMessageGamePlay})
            Return
        End If

        If hostContestantData._isHost Then
            If Not String.Equals(OneTimeMessageGamePlay.EXPLANATION.LASTCHANGE, OneTimeExplanationLastChange, StringComparison.OrdinalIgnoreCase) Then
                If String.Equals(OneTimeMessageGamePlay.EXPLANATION.EXECUTED, "NO", StringComparison.OrdinalIgnoreCase) Then
                    If String.Equals(OneTimeMessageGamePlay.EXPLANATION.STATE, "FIRED", StringComparison.OrdinalIgnoreCase) Then
                        ExplanationFire()
                    ElseIf String.Equals(OneTimeMessageGamePlay.EXPLANATION.STATE, "NONE", StringComparison.OrdinalIgnoreCase) Then
                        ExplanationDissolve()
                    End If
                    OneTimeExplanationLastChange = OneTimeMessageGamePlay.EXPLANATION.LASTCHANGE
                    HttpApiRequests.GetPostRequests.Get($"https://{My.Settings.IPAddress}/wwtbam-state/PostOTMessageExecution.php?MessageType=Explanation")
                End If
            End If
        End If

        If Not String.Equals(OneTimeMessageGamePlay.OTHER.LASTCHANGE, OneTimeOtherLastChange, StringComparison.OrdinalIgnoreCase) Then
            If String.Equals(OneTimeMessageGamePlay.OTHER.EXECUTED, "NO", StringComparison.OrdinalIgnoreCase) Then
                If String.Equals(OneTimeMessageGamePlay.OTHER.STATE, "CONFIGURATIONRESET", StringComparison.OrdinalIgnoreCase) Then
                    ConfigureLocalVersion(True)
                ElseIf String.Equals(OneTimeMessageGamePlay.OTHER.STATE, "TODO-SOMETHING2", StringComparison.OrdinalIgnoreCase) Then
                End If
                OneTimeOtherLastChange = OneTimeMessageGamePlay.OTHER.LASTCHANGE
                HttpApiRequests.GetPostRequests.Get($"https://{My.Settings.IPAddress}/wwtbam-state/PostOTMessageExecution.php?MessageType=Other")
            End If
        End If

    End Sub

#End Region

    Private Sub LifelineStateSet(LifelinesState As String, Optional ForceSet As Boolean = False)
        If (String.Compare(hostContestantData.lifelinesState, LifelinesState, True) <> 0) OrElse ForceSet Then
            Dim LifelineStateArray As String() = LifelinesState.Split(";")
            If LifelineStateArray.Count = 4 Then
                For i As Integer = 1 To LifelineStateArray.Length
                    If String.Compare(LifelineStateArray(i - 1), "UNUSED", True) = 0 Then
                        ResetLifeline(i)
                    ElseIf String.Compare(LifelineStateArray(i - 1), "INUSE", True) = 0 Then
                        InUsemarkLifeline(i)
                    ElseIf String.Compare(LifelineStateArray(i - 1), "USED", True) = 0 Then
                        XmarkLifeline(i)
                    ElseIf String.Compare(LifelineStateArray(i - 1), "DISABLED", True) = 0 Then
                        DisablemarkLifeline(i)
                    End If
                Next
            End If
        End If

    End Sub

    Private Sub ContestantLoad(ContestantName As String, PartnerName As String)
        hostContestantData._contestantNameCity = ContestantName
        hostContestantData._contestantPartner = PartnerName

        ContestantName_Label.Text = String.Format("{0} | {1}",
        hostContestantData._contestantNameCity, hostContestantData._contestantPartner)
    End Sub

    Private Sub CurrentGameStatus(currGameStatusData As String)
        Dim BankDropIfCorIfWrong As String() = currGameStatusData.Split(";")
        If BankDropIfCorIfWrong.Length = 4 Then
            sume.Text = BankDropIfCorIfWrong(0)
            fall.Text = BankDropIfCorIfWrong(1)
            qfor.Text = BankDropIfCorIfWrong(2)
            incorrect.Text = BankDropIfCorIfWrong(3)

            hostContestantData.currentGameStatusData = currGameStatusData
        End If
    End Sub

    Public Sub QuestionFire()

        QuestionDissolve()
        ExplanationDissolve()
        PronunciationFire()

        Question_Label.Text = hostContestantData._questionText
        Question_Label.Font = CreateSuitableQuestionFontSize(GUIDesignerPropertisContext.QuestionFontCutOff, hostContestantData._questionText)

        hostContestantData.currentQAState = "READQ"
    End Sub

    Function CreateSuitableQuestionFontSize(textsizelimit As Integer, teksttonarrow As String) As Font
        Dim myFont As Font = New Font("Arial", GUIDesignerPropertisContext.QuestionFontSize, FontStyle.Regular, GraphicsUnit.Point)
        'teksttonarrow = "------------------------------------------------------------------------------------------------"
        If teksttonarrow.Trim.Equals(String.Empty) Then Return myFont

        Dim textSize = TextRenderer.MeasureText(teksttonarrow, myFont)
        Dim narrowFont As Font

        Dim makeFontLower As Double = 0
        Dim isNarrow = ""
        If textSize.Width > textsizelimit Then
            Dim difference As Integer
            difference = textSize.Width - textsizelimit
            makeFontLower = difference / 50
            'isNarrow = " Narrow"
        End If

        makeFontLower = Math.Min(12, makeFontLower)
        narrowFont = New Font("Arial" + isNarrow, GUIDesignerPropertisContext.QuestionFontSize - makeFontLower, FontStyle.Regular, GraphicsUnit.Point)
        Return narrowFont
    End Function

    Function CreateSuitableAnswerFontSize(textsizelimit As Integer, teksttonarrow As String) As Font
        Dim myFont As Font = New Font("Arial", GUIDesignerPropertisContext.AnswerFontSize, FontStyle.Regular, GraphicsUnit.Point)

        If teksttonarrow.Trim.Equals(String.Empty) Then Return myFont

        Dim textSize = TextRenderer.MeasureText(teksttonarrow, myFont)
        Dim narrowFont As Font

        Dim makeFontLower As Double = 0
        Dim isNarrow = ""
        If textSize.Width > textsizelimit Then
            Dim difference As Integer
            difference = textSize.Width - textsizelimit
            makeFontLower = difference / 50
            isNarrow = " Narrow"
        End If
        'Arial Unicode MS
        makeFontLower = Math.Min(12, makeFontLower)
        narrowFont = New Font("Arial" + isNarrow, GUIDesignerPropertisContext.AnswerFontSize - makeFontLower, FontStyle.Regular, GraphicsUnit.Point)
        Return narrowFont
    End Function

    Friend Sub AnswerFire(v As Integer)
        If v > 0 And v < 5 Then
            If v = 1 Then
                AnswerA_Label.Text = hostContestantData._answer1Text
                AnswerA_Label.Font = CreateSuitableAnswerFontSize(GUIDesignerPropertisContext.AnswerFontCutOff, hostContestantData._answer1Text)
                hostContestantData.currentQAState = "READQ1"
            ElseIf v = 2 Then
                AnswerB_Label.Text = hostContestantData._answer2Text
                AnswerB_Label.Font = CreateSuitableAnswerFontSize(GUIDesignerPropertisContext.AnswerFontCutOff, hostContestantData._answer2Text)
                hostContestantData.currentQAState = "READQ12"
            ElseIf v = 3 Then
                AnswerC_Label.Text = hostContestantData._answer3Text
                AnswerC_Label.Font = CreateSuitableAnswerFontSize(GUIDesignerPropertisContext.AnswerFontCutOff, hostContestantData._answer3Text)
                hostContestantData.currentQAState = "READQ123"
            ElseIf v = 4 Then
                AnswerD_Label.Text = hostContestantData._answer4Text
                AnswerD_Label.Font = CreateSuitableAnswerFontSize(GUIDesignerPropertisContext.AnswerFontCutOff, hostContestantData._answer4Text)
                hostContestantData.currentQAState = "READQ1234"
            End If
        End If
    End Sub

    Friend Sub FinalAnswer(finAnswer As String, Optional IsDoubleDip As Boolean = False)
        If Not IsNumeric(finAnswer) Then Return
        Dim v = Int(finAnswer)

        If v > 0 And v < 5 Then
            hostContestantData._finalAnswer = v
            If v = 1 Then
                Answer1_PlaceGfx.BackgroundImage = My.Resources.ResourceManager.GetObject("FinalAC")
                AnswerA_Label.ForeColor = Color.Black
                MarkA_Label.ForeColor = Color.White
            ElseIf v = 2 Then
                Answer2_PlaceGfx.BackgroundImage = My.Resources.ResourceManager.GetObject("FinalBD")
                AnswerB_Label.ForeColor = Color.Black
                MarkB_Label.ForeColor = Color.White
            ElseIf v = 3 Then
                Answer3_PlaceGfx.BackgroundImage = My.Resources.ResourceManager.GetObject("FinalAC")
                AnswerC_Label.ForeColor = Color.Black
                MarkC_Label.ForeColor = Color.White
            ElseIf v = 4 Then
                Answer4_PlaceGfx.BackgroundImage = My.Resources.ResourceManager.GetObject("FinalBD")
                AnswerD_Label.ForeColor = Color.Black
                MarkD_Label.ForeColor = Color.White
            End If
            InfoAfterFinalAnswerFire(IsDoubleDip)
            hostContestantData.currentQAState = "FINALANSWERGIVEN"
        End If

    End Sub

    Friend Sub DoubleDip(finAnswer As String)
        If Not IsNumeric(finAnswer) Then Return
        Dim v = Int(finAnswer)
        If v > 0 And v < 5 Then
            If v = 1 Then
                Answer1_PlaceGfx.BackgroundImage = My.Resources.ResourceManager.GetObject("DoubleDipAC")
                AnswerA_Label.ForeColor = Color.White
                MarkA_Label.ForeColor = Color.Orange
            ElseIf v = 2 Then
                Answer2_PlaceGfx.BackgroundImage = My.Resources.ResourceManager.GetObject("DoubleDipBD")
                AnswerB_Label.ForeColor = Color.White
                MarkB_Label.ForeColor = Color.Orange
            ElseIf v = 3 Then
                Answer3_PlaceGfx.BackgroundImage = My.Resources.ResourceManager.GetObject("DoubleDipAC")
                AnswerC_Label.ForeColor = Color.White
                MarkC_Label.ForeColor = Color.Orange
            ElseIf v = 4 Then
                Answer4_PlaceGfx.BackgroundImage = My.Resources.ResourceManager.GetObject("DoubleDipBD")
                AnswerD_Label.ForeColor = Color.White
                MarkD_Label.ForeColor = Color.Orange
            End If
        End If
    End Sub

    Friend Sub CorrectAnswerReveal(corAnswer As String)
        If Not IsNumeric(corAnswer) Then Return
        Dim CorrectAnswer = Int(corAnswer)
        If (CorrectAnswer > 0 And CorrectAnswer < 5) Then
            If CorrectAnswer = 1 Then
                Answer1_PlaceGfx.BackgroundImage = My.Resources.ResourceManager.GetObject("CorrectAC")
                AnswerA_Label.ForeColor = Color.Black
                MarkA_Label.ForeColor = Color.White
            ElseIf CorrectAnswer = 2 Then
                Answer2_PlaceGfx.BackgroundImage = My.Resources.ResourceManager.GetObject("CorrectBD")
                AnswerB_Label.ForeColor = Color.Black
                MarkB_Label.ForeColor = Color.White
            ElseIf CorrectAnswer = 3 Then
                Answer3_PlaceGfx.BackgroundImage = My.Resources.ResourceManager.GetObject("CorrectAC")
                AnswerC_Label.ForeColor = Color.Black
                MarkC_Label.ForeColor = Color.White
            ElseIf CorrectAnswer = 4 Then
                Answer4_PlaceGfx.BackgroundImage = My.Resources.ResourceManager.GetObject("CorrectBD")
                AnswerD_Label.ForeColor = Color.Black
                MarkD_Label.ForeColor = Color.White
            End If
            hostContestantData.currentQAState = "CORRECTANSWERREVEAL"
        End If
    End Sub

    Friend Sub PronunciationFire()
        PronunciationHelp_Textbox.Text = hostContestantData._pronunciationQaText
    End Sub

    Private Sub PronunciationDissolve()
        PronunciationHelp_Textbox.Text = ""
    End Sub

    'Friend Sub DirectorChatFire(Blink As Int16, producerIMess As String)

    '    'Dim list As New List(Of String)
    '    'list.Add("New York")

    '    hostContestantData._producerMessages.Add(producerIMess)

    '    If hostContestantData._producerMessages.Count > 3 Then
    '        hostContestantData._producerMessages.RemoveAt(0)
    '    End If

    '    DirectorsChat_TextBox.Text = ""
    '    For value As Integer = 0 To hostContestantData._producerMessages.Count - 1
    '        Try
    '            DirectorsChat_TextBox.Text += hostContestantData._producerMessages.Item(value) + vbCrLf
    '        Catch ex As Exception
    '            Continue For
    '        End Try
    '    Next

    '    Timer_DirectMessageBlink.Stop()
    '    If Blink > 0 Then
    '        Timer_DirectMessageBlink.Start()
    '    End If

    'End Sub

    'Private Sub Timer_DirectMessageBlink_Tick(sender As Object, e As EventArgs) Handles Timer_DirectMessageBlink.Tick

    '    hostContestantData._producerMessageBlink -= 1
    '    DirectorsChat_TextBox.Text = ""
    '    For value As Integer = 0 To hostContestantData._producerMessages.Count - 2
    '        Try
    '            DirectorsChat_TextBox.Text += hostContestantData._producerMessages.Item(value) + vbCrLf
    '        Catch ex As Exception
    '            Continue For
    '        End Try
    '    Next
    '    Timer_DirectMessageUnblink.Start()
    '    Timer_DirectMessageBlink.Stop()
    'End Sub

    'Private Sub Timer_DirectMessageUnblink_Tick(sender As Object, e As EventArgs) Handles Timer_DirectMessageUnblink.Tick

    '    DirectorsChat_TextBox.Text = ""
    '    For value As Integer = 0 To hostContestantData._producerMessages.Count - 1
    '        Try
    '            DirectorsChat_TextBox.Text += hostContestantData._producerMessages.Item(value) + vbCrLf
    '        Catch ex As Exception
    '            Continue For
    '        End Try
    '    Next
    '    If hostContestantData._producerMessageBlink > 0 Then Timer_DirectMessageBlink.Start()
    '    Timer_DirectMessageUnblink.Stop()

    'End Sub

    'Private Async Sub Flash()
    '    For i = 0 To 10
    '        Await Task.Delay(100)
    '        Label1.Visible = Not Label1.Visible
    '    Next
    '    'set .Visible to True just to be sure
    '    Label1.Visible = True
    'End Sub

    Private Sub DirectorChatReset()
        hostContestantData._producerMessages.Clear()
        ProducersChat_TextBox.Text = ""

    End Sub

    Friend Sub InfoAfterFinalAnswerFire(Optional IsDoubleDip As Boolean = False)
        If hostContestantData._isHost Then Correct_Box.Visible = True
        If hostContestantData._finalAnswer = hostContestantData._correctAnswer Then
            Correct_Box.BackColor = GUIDesignerPropertisContext.GreenColorBox
        Else
            Correct_Box.BackColor = GUIDesignerPropertisContext.RedColorBox
        End If
        If IsDoubleDip And (hostContestantData._finalAnswer <> hostContestantData._correctAnswer) Then
            Correct_Box.Text = ""
        ElseIf hostContestantData._correctAnswer = 1 Then
            Correct_Box.Text = hostContestantData._mark1Label
        ElseIf hostContestantData._correctAnswer = 2 Then
            Correct_Box.Text = hostContestantData._mark2Label
        ElseIf hostContestantData._correctAnswer = 3 Then
            Correct_Box.Text = hostContestantData._mark3Label
        ElseIf hostContestantData._correctAnswer = 4 Then
            Correct_Box.Text = hostContestantData._mark4Label
        End If
        If Not IsDoubleDip Then
            ExplanationFire()
        End If

    End Sub

    Friend Sub QLevelMove(currentLevel As Integer)
        If hostContestantData._currentQuestionLevel = currentLevel Then
            Exit Sub
        ElseIf Math.Abs(currentLevel) < 0 Or (Math.Abs(currentLevel) > hostContestantData._numberOfGameQuestions) Then
            Exit Sub
        End If
        Dim Label As String
        ''transparent them
        For index = 1 To hostContestantData._numberOfGameQuestions
            Label = "Q_" + index.ToString
            For Each lb As Label In MoneyTreePanel.Controls.OfType(Of Label)()
                If String.Compare(Label, lb.Name, True) = 0 Then
                    lb.BackColor = Color.Transparent
                End If
            Next
        Next
        For index = 1 To hostContestantData._numberOfGameQuestions
            Label = "Lozenge_" + index.ToString
            For Each lb As Label In MoneyTreePanel.Controls.OfType(Of Label)()
                If String.Compare(Label, lb.Name, True) = 0 Then
                    lb.BackColor = Color.Transparent
                End If
            Next
        Next
        ''color them
        Label = "Q_" + currentLevel.ToString
        For Each lb As Label In MoneyTreePanel.Controls.OfType(Of Label)()
            If String.Compare(Label, lb.Name, True) = 0 Then
                lb.BackColor = Color.Chocolate
            End If
        Next
        Label = "Lozenge_" + currentLevel.ToString
        For Each lb As Label In MoneyTreePanel.Controls.OfType(Of Label)()
            If String.Compare(Label, lb.Name, True) = 0 Then
                lb.BackColor = Color.Chocolate
            End If
        Next
        For Each lb As Label In MoneyTreePanel.Controls.OfType(Of Label)()
            If lb.Name.StartsWith("Lozenge", StringComparison.OrdinalIgnoreCase) Then
                Dim IsAboveCurrentLevel As Integer
                Integer.TryParse(lb.Name.Replace("Lozenge_", ""), IsAboveCurrentLevel)
                If IsAboveCurrentLevel > currentLevel Then
                    lb.Visible = False
                Else
                    lb.Visible = True
                End If
            End If
        Next

        Away.Text = hostContestantData._numberOfGameQuestions - Math.Abs(currentLevel)

        hostContestantData._currentQuestionLevel = currentLevel
    End Sub

    Friend Sub QuestionDissolve()
        ResetQAGraphics()
        RemoveQATexts()
        InfoAfterFinalAnswerDissolve()
        AskTheAudienceGraphHide()
        PronunciationDissolve()
        hostContestantData.currentQAState = "NONE"
    End Sub

    Friend Sub InfoAfterFinalAnswerDissolve()
        'Correct_Box.Visible = False
        Correct_Box.BackColor = GUIDesignerPropertisContext.GrayColorBox
        Correct_Box.Text = ""
    End Sub

    Private Sub ExplanationFire()
        If hostContestantData._isHost Then
            If Not String.IsNullOrEmpty(hostContestantData._explanationAnswerText.Trim) Then
                ExplanationQuestion_TextBox.Visible = True 'pokazi samo ako ima tekst i za zasteda na performansi
                ExplanationQuestion_TextBox.Text = hostContestantData._explanationAnswerText
            End If
        Else
            ExplanationQuestion_TextBox.Text = String.Empty
        End If
    End Sub

    Friend Sub ExplanationDissolve()
        ExplanationQuestion_TextBox.Visible = False
        PronunciationDissolve()
    End Sub

    Friend Sub AddFourthLifeline()
        Lifeline4_PictureBox.Visible = True
        hostContestantData.activeLifelines = "4"
    End Sub

    Friend Sub RemoveFourthLifeline()
        Lifeline4_PictureBox.Visible = False
        hostContestantData.activeLifelines = "3"
    End Sub

    Friend Sub ResetLifeline(lifelinePosition As Integer)
        If lifelinePosition = 1 Then
            Lifeline1_PictureBox.BackgroundImage = My.Resources.ResourceManager.GetObject($"{hostContestantData.Lifeline1Name.ToUpper}_0")
            hostContestantData.Lifeline1State = "UNUSED"
        ElseIf lifelinePosition = 2 Then
            Lifeline2_PictureBox.BackgroundImage = My.Resources.ResourceManager.GetObject($"{hostContestantData.Lifeline2Name.ToUpper}_0")
            hostContestantData.Lifeline2State = "UNUSED"
        ElseIf lifelinePosition = 3 Then
            Lifeline3_PictureBox.BackgroundImage = My.Resources.ResourceManager.GetObject($"{hostContestantData.Lifeline3Name.ToUpper}_0")
            hostContestantData.Lifeline3State = "UNUSED"
        ElseIf lifelinePosition = 4 Then
            Lifeline4_PictureBox.BackgroundImage = My.Resources.ResourceManager.GetObject($"{hostContestantData.Lifeline4Name.ToUpper}_0")
            hostContestantData.Lifeline4State = "UNUSED"
        End If
    End Sub

    Private Sub InUsemarkLifeline(lifelinePosition As Integer)
        If lifelinePosition = 1 Then
            Lifeline1_PictureBox.BackgroundImage = My.Resources.ResourceManager.GetObject($"{hostContestantData.Lifeline1Name.ToUpper}_1")
            hostContestantData.Lifeline1State = "INUSE"
        ElseIf lifelinePosition = 2 Then
            Lifeline2_PictureBox.BackgroundImage = My.Resources.ResourceManager.GetObject($"{hostContestantData.Lifeline2Name.ToUpper}_1")
            hostContestantData.Lifeline2State = "INUSE"
        ElseIf lifelinePosition = 3 Then
            Lifeline3_PictureBox.BackgroundImage = My.Resources.ResourceManager.GetObject($"{hostContestantData.Lifeline3Name.ToUpper}_1")
            hostContestantData.Lifeline3State = "INUSE"
        ElseIf lifelinePosition = 4 Then
            Lifeline4_PictureBox.BackgroundImage = My.Resources.ResourceManager.GetObject($"{hostContestantData.Lifeline4Name.ToUpper}_1")
            hostContestantData.Lifeline4State = "INUSE"
        End If
    End Sub

    Friend Sub XmarkLifeline(lifelinePosition As Integer)
        If lifelinePosition = 1 Then
            Lifeline1_PictureBox.BackgroundImage = My.Resources.ResourceManager.GetObject($"{hostContestantData.Lifeline1Name.ToUpper}_X")
            hostContestantData.Lifeline1State = "USED"
        ElseIf lifelinePosition = 2 Then
            Lifeline2_PictureBox.BackgroundImage = My.Resources.ResourceManager.GetObject($"{hostContestantData.Lifeline2Name.ToUpper}_X")
            hostContestantData.Lifeline2State = "USED"
        ElseIf lifelinePosition = 3 Then
            Lifeline3_PictureBox.BackgroundImage = My.Resources.ResourceManager.GetObject($"{hostContestantData.Lifeline3Name.ToUpper}_X")
            hostContestantData.Lifeline3State = "USED"
        ElseIf lifelinePosition = 4 Then
            Lifeline4_PictureBox.BackgroundImage = My.Resources.ResourceManager.GetObject($"{hostContestantData.Lifeline4Name.ToUpper}_X")
            hostContestantData.Lifeline4State = "USED"
        End If
    End Sub

    Friend Sub DisablemarkLifeline(lifelinePosition As Integer)
        If lifelinePosition = 1 Then
            Lifeline1_PictureBox.BackgroundImage = My.Resources.ResourceManager.GetObject($"{hostContestantData.Lifeline1Name.ToUpper}_DSBL")
            hostContestantData.Lifeline1State = "DISABLED"
        ElseIf lifelinePosition = 2 Then
            Lifeline2_PictureBox.BackgroundImage = My.Resources.ResourceManager.GetObject($"{hostContestantData.Lifeline2Name.ToUpper}_DSBL")
            hostContestantData.Lifeline2State = "DISABLED"
        ElseIf lifelinePosition = 3 Then
            Lifeline3_PictureBox.BackgroundImage = My.Resources.ResourceManager.GetObject($"{hostContestantData.Lifeline3Name.ToUpper}_DSBL")
            hostContestantData.Lifeline3State = "DISABLED"
        ElseIf lifelinePosition = 4 Then
            Lifeline4_PictureBox.BackgroundImage = My.Resources.ResourceManager.GetObject($"{hostContestantData.Lifeline4Name.ToUpper}_DSBL")
            hostContestantData.Lifeline4State = "DISABLED"
        End If
    End Sub

    Friend Sub FiftyFiftyFire(RemoveAnswers As String)
        Dim Remove As Char() = RemoveAnswers.ToCharArray
        If Remove.Count = 2 Then
            If Remove(0) = "1" Or Remove(1) = "1" Then AnswerA_Label.Text = ""
            If Remove(0) = "2" Or Remove(1) = "2" Then AnswerB_Label.Text = ""
            If Remove(0) = "3" Or Remove(1) = "3" Then AnswerC_Label.Text = ""
            If Remove(0) = "4" Or Remove(1) = "4" Then AnswerD_Label.Text = ""
        End If
        hostContestantData.fiftyFifty = RemoveAnswers
    End Sub

    Private Sub AskTheAudienceGraphShow()
        AtaAns1_Textbox.Visible = True
        AtaAns2_Textbox.Visible = True
        AtaAns3_Textbox.Visible = True
        AtaAns4_Textbox.Visible = True
        AtaAns1percents_Textbox.Visible = True
        AtaAns2percents_Textbox.Visible = True
        AtaAns3percents_Textbox.Visible = True
        AtaAns4percents_Textbox.Visible = True
    End Sub

    Private Sub AskTheAudienceReady()
        AskTheAudienceGraphShow()
        AtaAns1_Textbox.BackColor = Color.Navy
        AtaAns2_Textbox.BackColor = Color.Navy
        AtaAns3_Textbox.BackColor = Color.Navy
        AtaAns4_Textbox.BackColor = Color.Navy
        AtaAns1percents_Textbox.BackColor = Color.Navy
        AtaAns2percents_Textbox.BackColor = Color.Navy
        AtaAns3percents_Textbox.BackColor = Color.Navy
        AtaAns4percents_Textbox.BackColor = Color.Navy
        AtaAns1_Textbox.Text = hostContestantData._mark1Label + ": " + AnswerA_Label.Text
        AtaAns2_Textbox.Text = hostContestantData._mark2Label + ": " + AnswerB_Label.Text
        AtaAns3_Textbox.Text = hostContestantData._mark3Label + ": " + AnswerC_Label.Text
        AtaAns4_Textbox.Text = hostContestantData._mark4Label + ": " + AnswerD_Label.Text
        AtaAns1percents_Textbox.Text = ""
        AtaAns2percents_Textbox.Text = ""
        AtaAns3percents_Textbox.Text = ""
        AtaAns4percents_Textbox.Text = ""
        hostContestantData.currentAtaState = "CLEARDIAGRAM"
    End Sub

    Private Sub AskTheAudienceEndVote(ataVotes As String)
        AskTheAudienceReady()
        Dim ataVotesArray As String() = ataVotes.Split(";")
        If ataVotesArray.Count = 4 Then
            AtaAns1percents_Textbox.Text = "" + ataVotesArray(0) + "%"
            AtaAns2percents_Textbox.Text = "" + ataVotesArray(1) + "%"
            AtaAns3percents_Textbox.Text = "" + ataVotesArray(2) + "%"
            AtaAns4percents_Textbox.Text = "" + ataVotesArray(3) + "%"

            Array.Sort(ataVotesArray)
            Array.Reverse(ataVotesArray)
            Dim maxVotePercentage As Double = 0
            If Not Double.TryParse(ataVotesArray.ElementAt(0), maxVotePercentage) Then
                Return
            End If

            'vrati gi nazad za da se zapazi redosled abcd na glasovi
            ataVotesArray = ataVotes.Split(";")

            If maxVotePercentage = 0 Then Return

            If maxVotePercentage = ataVotesArray(0) Then
                AtaAns1_Textbox.BackColor = Color.DarkOrange
                AtaAns1percents_Textbox.BackColor = Color.DarkOrange
            End If
            If maxVotePercentage = ataVotesArray(1) Then
                AtaAns2_Textbox.BackColor = Color.DarkOrange
                AtaAns2percents_Textbox.BackColor = Color.DarkOrange
            End If
            If maxVotePercentage = ataVotesArray(2) Then
                AtaAns3_Textbox.BackColor = Color.DarkOrange
                AtaAns3percents_Textbox.BackColor = Color.DarkOrange
            End If
            If maxVotePercentage = ataVotesArray(3) Then
                AtaAns4_Textbox.BackColor = Color.DarkOrange
                AtaAns4percents_Textbox.BackColor = Color.DarkOrange
            End If
        End If
        hostContestantData.currentAtaState = "DIAGRAMWITHPERCENTAGE"
    End Sub

    Private Sub AskTheAudienceGraphHide()
        AtaAns1_Textbox.Visible = False
        AtaAns2_Textbox.Visible = False
        AtaAns3_Textbox.Visible = False
        AtaAns4_Textbox.Visible = False
        AtaAns1percents_Textbox.Visible = False
        AtaAns2percents_Textbox.Visible = False
        AtaAns3percents_Textbox.Visible = False
        AtaAns4percents_Textbox.Visible = False
        hostContestantData.currentAtaState = "NONE"
    End Sub

    Private Sub SetMilestone(secondMilestone As String)
        Dim Label As String
        For index = 6 To hostContestantData._numberOfGameQuestions - 1
            Label = "Q_" + index.ToString
            For Each lb As Label In MoneyTreePanel.Controls.OfType(Of Label)()
                If String.Compare(Label, lb.Name, True) = 0 Then
                    lb.ForeColor = Color.DarkOrange
                End If
            Next
        Next
        hostContestantData.currentSecondMilestone = secondMilestone
        If Not IsNumeric(secondMilestone) Then Return
        If secondMilestone <= 0 Then Return
        Label = "Q_" + secondMilestone.ToString
        For Each lb As Label In MoneyTreePanel.Controls.OfType(Of Label)()
            If String.Compare(Label, lb.Name, True) = 0 Then
                lb.ForeColor = Color.White
            End If
        Next
    End Sub

    Private Sub DataSetValues(Configuration As Xml2CSharp.WWTBAMCONFIGURATIONDATA)

        'If Not Configuration.Contains("<WWTBAM-CONFIGURATION-DATA>") AndAlso Not Configuration.Contains("</WWTBAM-CONFIGURATION-DATA>") Then Exit Sub

        hostContestantData._numberOfGameQuestions = CInt(Configuration.NUMBEROFQUESTIONS)

        hostContestantData._moneyTree.Clear()
        'For index = 0 To hostContestantData._numberOfGameQuestions - 1
        '    hostContestantData._moneyTree.Add(GetValueStringBetweenTags(Configuration, "<Q" + (index + 1).ToString + ">", "</Q" + (index + 1).ToString + ">"))
        'Next
        hostContestantData._moneyTree.Add(Configuration.Q1)
        hostContestantData._moneyTree.Add(Configuration.Q2)
        hostContestantData._moneyTree.Add(Configuration.Q3)
        hostContestantData._moneyTree.Add(Configuration.Q4)
        hostContestantData._moneyTree.Add(Configuration.Q5)
        hostContestantData._moneyTree.Add(Configuration.Q6)
        hostContestantData._moneyTree.Add(Configuration.Q7)
        hostContestantData._moneyTree.Add(Configuration.Q8)
        hostContestantData._moneyTree.Add(Configuration.Q9)
        hostContestantData._moneyTree.Add(Configuration.Q10)
        hostContestantData._moneyTree.Add(Configuration.Q11)
        hostContestantData._moneyTree.Add(Configuration.Q12)
        hostContestantData._moneyTree.Add(Configuration.Q13)
        hostContestantData._moneyTree.Add(Configuration.Q14)
        hostContestantData._moneyTree.Add(Configuration.Q15)

        hostContestantData.Lifeline1Name = Configuration.LIFELINE1
        hostContestantData.Lifeline2Name = Configuration.LIFELINE2
        hostContestantData.Lifeline3Name = Configuration.LIFELINE3
        hostContestantData.Lifeline4Name = Configuration.LIFELINE4

        For index = 0 To hostContestantData._numberOfGameQuestions - 1
            For Each lb As Label In MoneyTreePanel.Controls.OfType(Of Label)()
                Dim Label As String = "Q_" + (Val(index + 1)).ToString
                If String.Compare(Label, lb.Name, True) = 0 Then
                    Dim prefixBlankSpace = "  "
                    If (index + 1) <= 9 Then
                        prefixBlankSpace = "    "
                    End If
                    lb.Text = prefixBlankSpace + (index + 1).ToString + "     " + hostContestantData._moneyTree.ElementAt(index) + "                             "
                End If
            Next
        Next

        Dim AnswerMarks As Char() = Configuration.ANSWERMARKS.ToCharArray

        hostContestantData._mark1Label = IIf(AnswerMarks(0) = Nothing, "A", AnswerMarks(0))
        hostContestantData._mark2Label = IIf(AnswerMarks(1) = Nothing, "B", AnswerMarks(1))
        hostContestantData._mark3Label = IIf(AnswerMarks(2) = Nothing, "C", AnswerMarks(2))
        hostContestantData._mark4Label = IIf(AnswerMarks(3) = Nothing, "D", AnswerMarks(3))

        MarkA_Label.Text = hostContestantData._mark1Label + ":"
        MarkB_Label.Text = hostContestantData._mark2Label + ":"
        MarkC_Label.Text = hostContestantData._mark3Label + ":"
        MarkD_Label.Text = hostContestantData._mark4Label + ":"

        hostContestantData._totalPrizeWonLocalizedMark = Configuration.TPWON

        hostContestantData.lastTimeSetMoneyTree = DateTime.Today
    End Sub

    Friend Sub ResetGame()
        hostContestantData.QuestionDataReset()
        QuestionDissolve()
        ExplanationDissolve()
        InfoAfterFinalAnswerDissolve()
        AskTheAudienceGraphHide()
        SetMilestone(0)
        QLevelMove(0)
        ResetLifeline(1)
        ResetLifeline(2)
        ResetLifeline(3)
        ResetLifeline(4)
    End Sub

    Public Sub RemoveQATexts()
        Question_Label.Text = ""
        AnswerA_Label.Text = ""
        AnswerB_Label.Text = ""
        AnswerC_Label.Text = ""
        AnswerD_Label.Text = ""
        AnswerA_Label.ForeColor = Color.White
        AnswerB_Label.ForeColor = Color.White
        AnswerC_Label.ForeColor = Color.White
        AnswerD_Label.ForeColor = Color.White
        MarkA_Label.ForeColor = Color.Orange
        MarkB_Label.ForeColor = Color.Orange
        MarkC_Label.ForeColor = Color.Orange
        MarkD_Label.ForeColor = Color.Orange
    End Sub

    Public Sub ResetQAGraphics()
        Answer1_PlaceGfx.BackgroundImage = My.Resources.ResourceManager.GetObject("AC")
        Answer2_PlaceGfx.BackgroundImage = My.Resources.ResourceManager.GetObject("BD")
        Answer3_PlaceGfx.BackgroundImage = My.Resources.ResourceManager.GetObject("AC")
        Answer4_PlaceGfx.BackgroundImage = My.Resources.ResourceManager.GetObject("BD")
    End Sub

    Private Sub PAFFire()
        PAFClock_Label.Text = "30"
        PAFClock_Label.Visible = True
        hostContestantData.paf = "START"
        Timer_ClockSecondDrop.Start()
    End Sub

    Private Sub PAFAbort()
        Timer_ClockSecondDrop.Stop()
        PAFClock_Label.Visible = False
        hostContestantData.paf = "ABORT"
    End Sub

    Private Sub PAFAutoEnd()
        PAFAbort()
        hostContestantData.paf = "END"
    End Sub

    Private Sub Timer_ClockSecondDrop_Tick(sender As Object, e As EventArgs) Handles Timer_ClockSecondDrop.Tick
        If Val(PAFClock_Label.Text) > 0 Then
            PAFClock_Label.Text = Val(PAFClock_Label.Text) - 1
        Else
            PAFAutoEnd()
        End If
    End Sub
    Public Sub EnableGUIIfHost()
        If hostContestantData._isHost Then
            ProducersChat_TextBox.Visible = True
            ExplanationQuestion_TextBox.Visible = True
            ContestantName_Panel.Visible = True
            incorrectSumePanel.Visible = True
            fallSumePanel.Visible = True
            correctSumePanel.Visible = True
            awayPanel.Visible = True
            sumePanel.Visible = True
            Correct_Box.Visible = True
            PronunciationHelp_Textbox.Visible = True
            Me.Text = "Host"
        Else
            ProducersChat_TextBox.Visible = False
            ExplanationQuestion_TextBox.Visible = False
            ContestantName_Panel.Visible = False
            incorrectSumePanel.Visible = False
            fallSumePanel.Visible = False
            correctSumePanel.Visible = False
            awayPanel.Visible = False
            sumePanel.Visible = False
            Correct_Box.Visible = False
            PronunciationHelp_Textbox.Visible = False
            Me.Text = "Contestant"
        End If
    End Sub

    Private Sub DirectorsChat_TextBox_TextChanged(sender As Object, e As EventArgs) Handles ProducersChat_TextBox.TextChanged
        If String.IsNullOrEmpty(ProducersChat_TextBox.Text) OrElse (Not hostContestantData._isHost) Then Return

        Dim myFont As Font = New Font("Arial", GUIDesignerPropertisContext.DirectorsChatFontSize, FontStyle.Bold, GraphicsUnit.Point)
        ProducersChat_TextBox.Font = myFont

        GUIHelpers.FitTextInsideControl(ProducersChat_TextBox, myFont)
    End Sub

    Private Sub ExplanationQuestion_TextBox_TextChanged(sender As Object, e As EventArgs) Handles ExplanationQuestion_TextBox.TextChanged
        If String.IsNullOrEmpty(ExplanationQuestion_TextBox.Text) OrElse (Not hostContestantData._isHost) Then Return

        Dim myFont As Font = New Font("Arial", GUIDesignerPropertisContext.ExplanationFontSize, FontStyle.Regular, GraphicsUnit.Point)
        ExplanationQuestion_TextBox.Font = myFont

        GUIHelpers.FitTextInsideControl(ExplanationQuestion_TextBox, myFont)
    End Sub

    Private Sub Answer1Final_Click(sender As Object, e As EventArgs) Handles AnswerA_Label.Click, MarkA_Label.Click, Answer1_PlaceGfx.Click
        ContestantFinalClick(1)
    End Sub
    Private Sub Answer2Final_Click(sender As Object, e As EventArgs) Handles AnswerB_Label.Click, MarkB_Label.Click, Answer2_PlaceGfx.Click
        ContestantFinalClick(2)
    End Sub
    Private Sub Answer3Final_Click(sender As Object, e As EventArgs) Handles AnswerC_Label.Click, MarkC_Label.Click, Answer3_PlaceGfx.Click
        ContestantFinalClick(3)
    End Sub
    Private Sub Answer4Final_Click(sender As Object, e As EventArgs) Handles AnswerD_Label.Click, MarkD_Label.Click, Answer4_PlaceGfx.Click
        ContestantFinalClick(4)
    End Sub

    Private Sub ContestantFinalClick(Answer As Integer)
        If (Not hostContestantData._isHost) Then
            Dim FinalAnswerStr As String = HttpApiRequests.GetPostRequests.Get($"https://{My.Settings.IPAddress}/wwtbam-state/PostContestantClickData.php?ClickType=FinalAnswer&ClickValue=" + Answer)
        End If
    End Sub

    Public Sub ConfigureLocalVersion(Optional ForceConfigure As Boolean = False)

        If (hostContestantData.lastTimeSetMoneyTree < DateTime.Today) OrElse ForceConfigure Then
            Dim Configuration As String = HttpApiRequests.GetPostRequests.Get($"https://{My.Settings.IPAddress}/wwtbam-state/GetConfigurationData.php")

            Dim configSerializer As Xml.Serialization.XmlSerializer = New Xml.Serialization.XmlSerializer(GetType(Xml2CSharp.WWTBAMCONFIGURATIONDATA))
            Dim WwtbamConfiguration As New Xml2CSharp.WWTBAMCONFIGURATIONDATA

            Try
                Dim reader As System.IO.TextReader = New System.IO.StringReader(Configuration)
                WwtbamConfiguration = configSerializer.Deserialize(reader)
            Catch ex As Exception
                MessageBox.Show($"Error occured during loading configuration.{vbCrLf}Check your connection with server and try restartng the screen.")
                Return
            End Try

            DataSetValues(WwtbamConfiguration)
            LifelineStateSet(hostContestantData.lifelinesState, ForceSet:=True)
        End If

    End Sub

    'Public Sub PutQuestionText(Question As String)

    '    Dim mystring As String = Question

    '    Dim myFont As Font = New Font("Arial", 40, FontStyle.Regular, GraphicsUnit.Pixel)
    '    Dim textSize = TextRenderer.MeasureText(mystring, myFont)
    '    Dim difference As Double = Math.Abs(textSize.Width - 1212)

    '    If textSize.Width > 1212 Then
    '        myFont = New Font("Arial", (Math.Abs(40 - (difference * 0.024))), FontStyle.Regular, GraphicsUnit.Pixel)
    '        textSize = TextRenderer.MeasureText(mystring, myFont)
    '    End If

    '    Dim gr As Graphics = Graphics.FromImage(Question_PictBox.BackgroundImage)

    '    Dim stringFormat As New StringFormat
    '    stringFormat.Alignment = StringAlignment.Center

    '    gr.DrawString(mystring, myFont, Brushes.White, Question_PictBox.BackgroundImage.Width / 2, (Question_PictBox.BackgroundImage.Height - textSize.Height) / 2, stringFormat)

    '    Question_PictBox.Invalidate()

    'End Sub

    'Public Sub PutAnswer1Text(Answer1 As String)

    '    Dim mystring As String = DefaultMark1text + ": " + Answer1

    '    Dim myFont As Font = New Font("Arial", 40, FontStyle.Regular, GraphicsUnit.Pixel)
    '    Dim textSize = TextRenderer.MeasureText(mystring, myFont)
    '    Dim difference As Double = Math.Abs(textSize.Width - 500)

    '    If textSize.Width > 500 Then
    '        myFont = New Font("Arial", (Math.Abs(40 - (difference * 0.03))), FontStyle.Regular, GraphicsUnit.Pixel)
    '        textSize = TextRenderer.MeasureText(mystring, myFont)
    '    End If

    '    Dim gr As Graphics = Graphics.FromImage(Answer1_PicBox.BackgroundImage)

    '    gr.DrawString(mystring, myFont, Brushes.White, 20, (Answer1_PicBox.BackgroundImage.Height - textSize.Height) / 2)

    '    Answer1_PicBox.Invalidate()

    'End Sub

    'Public Sub PutAnswer2Text(Answer2 As String)

    '    Dim mystring As String = DefaultMark2text + ": " + Answer2

    '    Dim myFont As Font = New Font("Arial", 40, FontStyle.Regular, GraphicsUnit.Pixel)
    '    Dim textSize = TextRenderer.MeasureText(mystring, myFont)
    '    Dim difference As Double = Math.Abs(textSize.Width - 500)

    '    If textSize.Width > 500 Then
    '        myFont = New Font("Arial", (Math.Abs(40 - (difference * 0.03))), FontStyle.Regular, GraphicsUnit.Pixel)
    '        textSize = TextRenderer.MeasureText(mystring, myFont)
    '    End If

    '    Dim gr As Graphics = Graphics.FromImage(Answer2_PicBox.BackgroundImage)

    '    gr.DrawString(mystring, myFont, Brushes.White, 20, (Answer2_PicBox.BackgroundImage.Height - textSize.Height) / 2)

    '    Answer2_PicBox.Invalidate()

    'End Sub

    'Public Sub PutAnswer3Text(Answer3 As String)

    '    Dim mystring As String = DefaultMark3text + ": " + Answer3

    '    Dim myFont As Font = New Font("Arial", 40, FontStyle.Regular, GraphicsUnit.Pixel)
    '    Dim textSize = TextRenderer.MeasureText(mystring, myFont)
    '    Dim difference As Double = Math.Abs(textSize.Width - 500)

    '    If textSize.Width > 500 Then
    '        myFont = New Font("Arial", (Math.Abs(40 - (difference * 0.03))), FontStyle.Regular, GraphicsUnit.Pixel)
    '        textSize = TextRenderer.MeasureText(mystring, myFont)
    '    End If

    '    Dim gr As Graphics = Graphics.FromImage(Answer3_PicBox.BackgroundImage)

    '    gr.DrawString(mystring, myFont, Brushes.White, 20, (Answer3_PicBox.BackgroundImage.Height - textSize.Height) / 2)

    '    Answer3_PicBox.Invalidate()

    'End Sub

    'Public Sub PutAnswer4Text(Answer4 As String)

    '    Dim mystring As String = DefaultMark4text + ": " + Answer4

    '    Dim myFont As Font = New Font("Arial", 40, FontStyle.Regular, GraphicsUnit.Pixel)
    '    Dim textSize = TextRenderer.MeasureText(mystring, myFont)
    '    Dim difference As Double = Math.Abs(textSize.Width - 500)

    '    If textSize.Width > 500 Then
    '        myFont = New Font("Arial", (Math.Abs(40 - (difference * 0.03))), FontStyle.Regular, GraphicsUnit.Pixel)
    '        textSize = TextRenderer.MeasureText(mystring, myFont)
    '    End If

    '    Dim gr As Graphics = Graphics.FromImage(Answer4_PicBox.BackgroundImage)

    '    gr.DrawString(mystring, myFont, Brushes.White, 20, (Answer4_PicBox.BackgroundImage.Height - textSize.Height) / 2)

    '    Answer4_PicBox.Invalidate()

    'End Sub

End Class
