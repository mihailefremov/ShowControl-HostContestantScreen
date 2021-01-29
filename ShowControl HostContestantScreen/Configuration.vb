Public Class Configuration
    Private Sub Configuration_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        IPAddress_TextBox.Text = My.Settings.IPAddress
        QOperatorPort_TextBox.Text = My.Settings.IPPort

    End Sub

    Private Sub ConfigureAndStart_Button_Click(sender As Object, e As EventArgs) Handles ConfigureAndStart_Button.Click
        If HostContestant.ThreadGameStateListener.IsAlive Then
            Dim n As String = MsgBox("Do you really want to restart the listener?", MsgBoxStyle.YesNo, "ShowControl Screen")
            If Not n = vbYes Then
                Return
            End If
        End If
        If Application.OpenForms().OfType(Of HostContestant).Any Then
            HostContestant.Close()
        End If
        HostContestant.Show(HostScreenType_RadioButton.Checked, ScreenResoultion_ComboBox.Text)
    End Sub

    Private Sub SaveNetworkSettings_Button_Click(sender As Object, e As EventArgs) Handles SaveNetworkSettings_Button.Click
        My.Settings.IPAddress = IPAddress_TextBox.Text
        My.Settings.IPPort = QOperatorPort_TextBox.Text
        My.Settings.Save()
        'Port numbers range from 0 To 65535, but only port numbers 0 to 1023 are reserved for privileged services 
        'And designated as well-known ports. The following list of well-known port numbers specifies the port used 
        'by the server process as its contact port.
    End Sub

    Private Sub ResetNetworkSettings_Click(sender As Object, e As EventArgs) Handles ResetNetworkSettings_Button.Click
        My.Settings.Reset()
        IPAddress_TextBox.Text = My.Settings.IPAddress
        QOperatorPort_TextBox.Text = My.Settings.IPPort
    End Sub

    Private Sub Port_TextBox_TextChanged(sender As Object, e As EventArgs) Handles QOperatorPort_TextBox.TextChanged, IPAddress_TextBox.TextChanged
        If Val(QOperatorPort_TextBox.Text) > 65535 Then
            QOperatorPort_TextBox.Text = "65535"
        ElseIf Val(QOperatorPort_TextBox.Text) < 1 Then
            QOperatorPort_TextBox.Text = "0"
        End If
        If Val(IPAddress_TextBox.Text) > 65535 Then
            IPAddress_TextBox.Text = "65535"
        ElseIf Val(IPAddress_TextBox.Text) < 1 Then
            IPAddress_TextBox.Text = "0"
        End If
    End Sub

    Private Sub Configuration_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        HostContestant.Close()

    End Sub

    Private Sub WindowState_Label_Click(sender As Object, e As EventArgs) Handles WindowState_Label.Click
        If HostContestant.FormBorderStyle = FormBorderStyle.None Then
            HostContestant.FormBorderStyle = FormBorderStyle.FixedSingle
        Else
            HostContestant.FormBorderStyle = FormBorderStyle.None
        End If

    End Sub

    Private Sub RadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles Up_RadioButton.CheckedChanged, Down_RadioButton.CheckedChanged
        'Return
        'HostContestant.Panel1.Size = New System.Drawing.Size(398, 454)
        'HostContestant.PictureBox7.Size = New System.Drawing.Size(6, 480)
        'HostContestant.Q_4.Size = New System.Drawing.Size(40, 24)
        'HostContestant.Q_3.Size = New System.Drawing.Size(40, 24)
        'HostContestant.Q_2.Size = New System.Drawing.Size(40, 24)
        'HostContestant.Q_15.Size = New System.Drawing.Size(45, 24)
        'HostContestant.Q_14.Size = New System.Drawing.Size(45, 24)
        'HostContestant.Q_13.Size = New System.Drawing.Size(45, 24)
        'HostContestant.Q_12.Size = New System.Drawing.Size(45, 24)
        'HostContestant.Q_11.Size = New System.Drawing.Size(45, 24)
        'HostContestant.Q_10.Size = New System.Drawing.Size(45, 24)
        'HostContestant.Q_9.Size = New System.Drawing.Size(40, 24)
        'HostContestant.Q_8.Size = New System.Drawing.Size(40, 24)
        'HostContestant.Q_7.Size = New System.Drawing.Size(40, 24)
        'HostContestant.Q_6.Size = New System.Drawing.Size(40, 24)
        'HostContestant.Q_5.Size = New System.Drawing.Size(40, 24)
        'HostContestant.Q_1.Size = New System.Drawing.Size(40, 24)
        'HostContestant.Lifeline4_PictureBox.Size = New System.Drawing.Size(91, 64)
        'HostContestant.Lifeline3_PictureBox.Size = New System.Drawing.Size(91, 64)
        'HostContestant.Lifeline2_PictureBox.Size = New System.Drawing.Size(91, 64)
        'HostContestant.Lifeline1_PictureBox.Size = New System.Drawing.Size(91, 64)
        'HostContestant.Button2.Size = New System.Drawing.Size(75, 23)
        'HostContestant.ExplanationQuestion_TextBox.Size = New System.Drawing.Size(702, 190)
        'HostContestant.AtaAns3_Textbox.Size = New System.Drawing.Size(594, 39)
        'HostContestant.AtaAns4percents_Textbox.Size = New System.Drawing.Size(110, 39)
        'HostContestant.AtaAns4_Textbox.Size = New System.Drawing.Size(594, 39)
        'HostContestant.AtaAns3percents_Textbox.Size = New System.Drawing.Size(110, 39)
        'HostContestant.AtaAns2percents_Textbox.Size = New System.Drawing.Size(110, 39)
        'HostContestant.AtaAns2_Textbox.Size = New System.Drawing.Size(594, 39)
        'HostContestant.AtaAns1percents_Textbox.Size = New System.Drawing.Size(110, 39)
        'HostContestant.AtaAns1_Textbox.Size = New System.Drawing.Size(594, 39)
        'HostContestant.DirectorsChat_TextBox.Size = New System.Drawing.Size(702, 106)
        'HostContestant.PronunciationHelp_Textbox.Size = New System.Drawing.Size(111, 32)
        'HostContestant.Correct_Box.Size = New System.Drawing.Size(43, 40)
        'HostContestant.Label1.Size = New System.Drawing.Size(109, 61)
        'HostContestant.PAFClock_Label.Size = New System.Drawing.Size(118, 61)
        'HostContestant.ContestantName_Panel.Size = New System.Drawing.Size(697, 41)
        'HostContestant.ContestantName_Label.Size = New System.Drawing.Size(687, 31)
        'HostContestant.Panel2.Size = New System.Drawing.Size(116, 105)
        'HostContestant.GameLimitedClock_Label.Size = New System.Drawing.Size(116, 61)
        'HostContestant.Question_PlaceGfx.Size = New System.Drawing.Size(1257, 112)
        'HostContestant.Question_Label.Size = New System.Drawing.Size(1244, 95)
        'HostContestant.PictureBox4.Size = New System.Drawing.Size(1376, 4)
        'HostContestant.Answer2_PlaceGfx.Size = New System.Drawing.Size(592, 61)
        'HostContestant.AnswerB_Label.Size = New System.Drawing.Size(574, 41)
        'HostContestant.MarkB_Label.Size = New System.Drawing.Size(45, 36)
        'HostContestant.Answer1_PlaceGfx.Size = New System.Drawing.Size(592, 61)
        'HostContestant.AnswerA_Label.Size = New System.Drawing.Size(574, 41)
        'HostContestant.MarkA_Label.Size = New System.Drawing.Size(45, 36)
        'HostContestant.PictureBox3.Size = New System.Drawing.Size(1366, 4)
        'HostContestant.Answer4_PlaceGfx.Size = New System.Drawing.Size(592, 61)
        'HostContestant.AnswerD_Label.Size = New System.Drawing.Size(574, 41)
        'HostContestant.MarkD_Label.Size = New System.Drawing.Size(47, 36)
        'HostContestant.Answer3_PlaceGfx.Size = New System.Drawing.Size(592, 61)
        'HostContestant.AnswerC_Label.Size = New System.Drawing.Size(574, 41)
        'HostContestant.MarkC_Label.Size = New System.Drawing.Size(47, 36)
        'HostContestant.PictureBox2.Size = New System.Drawing.Size(1366, 4)
        'HostContestant.awayPanel.Size = New System.Drawing.Size(233, 55)
        'HostContestant.PictureBox9.Size = New System.Drawing.Size(51, 51)
        'HostContestant.Label6.Size = New System.Drawing.Size(81, 21)
        'HostContestant.Away.Size = New System.Drawing.Size(78, 31)
        'HostContestant.sumePanel.Size = New System.Drawing.Size(233, 55)
        'HostContestant.PictureBox1.Size = New System.Drawing.Size(53, 51)
        'HostContestant.correctSumePanel.Size = New System.Drawing.Size(233, 55)
        'HostContestant.PictureBox5.Size = New System.Drawing.Size(51, 51)
        'HostContestant.qfor.Size = New System.Drawing.Size(173, 31)
        'HostContestant.incorrectSumePanel.Size = New System.Drawing.Size(233, 55)
        'HostContestant.PictureBox8.Size = New System.Drawing.Size(51, 51)
        'HostContestant.incorrect.Size = New System.Drawing.Size(180, 31)
        'HostContestant.fallSumePanel.Size = New System.Drawing.Size(233, 55)
        'HostContestant.PictureBox17.Size = New System.Drawing.Size(51, 51)
        'HostContestant.fall.Size = New System.Drawing.Size(176, 31)
        'HostContestant.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        'HostContestant.ClientSize = New System.Drawing.Size(1366, 749)

        If Down_RadioButton.Checked = True Then
            HostContestant.Answer1_PlaceGfx.Location = New System.Drawing.Point(88, 613)
            HostContestant.Answer2_PlaceGfx.Location = New System.Drawing.Point(693, 613)
            HostContestant.Answer3_PlaceGfx.Location = New System.Drawing.Point(88, 681)
            HostContestant.Answer4_PlaceGfx.Location = New System.Drawing.Point(693, 681)
            HostContestant.AnswerA_Label.Location = New System.Drawing.Point(61, 9)
            HostContestant.AnswerB_Label.Location = New System.Drawing.Point(61, 9)
            HostContestant.AnswerC_Label.Location = New System.Drawing.Point(61, 9)
            HostContestant.AnswerD_Label.Location = New System.Drawing.Point(61, 9)
            HostContestant.AtaAns1percents_Textbox.Location = New System.Drawing.Point(630, 261)
            HostContestant.AtaAns1_Textbox.Location = New System.Drawing.Point(38, 261)
            HostContestant.AtaAns2percents_Textbox.Location = New System.Drawing.Point(630, 298)
            HostContestant.AtaAns2_Textbox.Location = New System.Drawing.Point(38, 298)
            HostContestant.AtaAns3percents_Textbox.Location = New System.Drawing.Point(630, 335)
            HostContestant.AtaAns3_Textbox.Location = New System.Drawing.Point(38, 335)
            HostContestant.AtaAns4percents_Textbox.Location = New System.Drawing.Point(630, 372)
            HostContestant.AtaAns4_Textbox.Location = New System.Drawing.Point(38, 372)
            HostContestant.Away.Location = New System.Drawing.Point(150, 18)
            HostContestant.awayPanel.Location = New System.Drawing.Point(37, 37)
            HostContestant.Button2.Location = New System.Drawing.Point(700, -55)
            HostContestant.ContestantName_Label.Location = New System.Drawing.Point(9, 1)
            HostContestant.ContestantName_Panel.Location = New System.Drawing.Point(40, 451)
            HostContestant.ContestantName_Panel.Visible = False
            HostContestant.correctSumePanel.Location = New System.Drawing.Point(37, 90)
            HostContestant.Correct_Box.Location = New System.Drawing.Point(833, 385)
            HostContestant.ProducersChat_TextBox.Location = New System.Drawing.Point(38, 149)
            HostContestant.ExplanationQuestion_TextBox.Location = New System.Drawing.Point(38, 259)
            HostContestant.fall.Location = New System.Drawing.Point(54, 15)
            HostContestant.fallSumePanel.Location = New System.Drawing.Point(502, 90)
            HostContestant.GameLimitedClock_Label.Location = New System.Drawing.Point(3, 16)
            HostContestant.incorrect.Location = New System.Drawing.Point(49, 15)
            HostContestant.incorrectSumePanel.Location = New System.Drawing.Point(270, 90)
            HostContestant.Label1.Location = New System.Drawing.Point(5, 16)
            HostContestant.Label6.Location = New System.Drawing.Point(63, 22)
            HostContestant.Lifeline1_PictureBox.Location = New System.Drawing.Point(10, 4)
            HostContestant.Lifeline2_PictureBox.Location = New System.Drawing.Point(104, 4)
            HostContestant.Lifeline3_PictureBox.Location = New System.Drawing.Point(198, 4)
            HostContestant.Lifeline4_PictureBox.Location = New System.Drawing.Point(292, 4)
            HostContestant.MarkA_Label.Location = New System.Drawing.Point(23, 13)
            HostContestant.MarkB_Label.Location = New System.Drawing.Point(23, 13)
            HostContestant.MarkC_Label.Location = New System.Drawing.Point(23, 13)
            HostContestant.MarkD_Label.Location = New System.Drawing.Point(23, 13)
            HostContestant.PAFClock_Label.Location = New System.Drawing.Point(801, 284)
            HostContestant.MoneyTreePanel.Location = New System.Drawing.Point(966, 12)
            HostContestant.Panel2.Location = New System.Drawing.Point(803, 149)
            HostContestant.PictureBox1.Location = New System.Drawing.Point(3, 2)
            HostContestant.PictureBox5.Location = New System.Drawing.Point(4, 2)
            HostContestant.PictureBox7.Location = New System.Drawing.Point(0, 0)
            HostContestant.PictureBox8.Location = New System.Drawing.Point(4, 2)
            HostContestant.PictureBox9.Location = New System.Drawing.Point(3, 2)
            HostContestant.PictureBox17.Location = New System.Drawing.Point(3, 2)
            HostContestant.PronunciationHelp_Textbox.Location = New System.Drawing.Point(43, 4)
            HostContestant.qfor.Location = New System.Drawing.Point(55, 14)
            HostContestant.Question_Label.Location = New System.Drawing.Point(5, 5)
            HostContestant.Question_PlaceGfx.Location = New System.Drawing.Point(58, 500)
            HostContestant.Q_1.Location = New System.Drawing.Point(10, 423)
            HostContestant.Q_2.Location = New System.Drawing.Point(10, 398)
            HostContestant.Q_3.Location = New System.Drawing.Point(10, 373)
            HostContestant.Q_4.Location = New System.Drawing.Point(10, 348)
            HostContestant.Q_5.Location = New System.Drawing.Point(10, 323)
            HostContestant.Q_6.Location = New System.Drawing.Point(10, 298)
            HostContestant.Q_7.Location = New System.Drawing.Point(10, 273)
            HostContestant.Q_8.Location = New System.Drawing.Point(10, 248)
            HostContestant.Q_9.Location = New System.Drawing.Point(10, 223)
            HostContestant.Q_10.Location = New System.Drawing.Point(10, 198)
            HostContestant.Q_11.Location = New System.Drawing.Point(10, 173)
            HostContestant.Q_12.Location = New System.Drawing.Point(10, 148)
            HostContestant.Q_13.Location = New System.Drawing.Point(10, 123)
            HostContestant.Q_14.Location = New System.Drawing.Point(10, 98)
            HostContestant.Q_15.Location = New System.Drawing.Point(10, 73)
            HostContestant.sumePanel.Location = New System.Drawing.Point(269, 37)
        Else
            'HostContestant.Answer1_PlaceGfx.Location = New System.Drawing.Point(90, 122)
            'HostContestant.Answer2_PlaceGfx.Location = New System.Drawing.Point(695, 122)
            'HostContestant.Answer3_PlaceGfx.Location = New System.Drawing.Point(90, 190)
            'HostContestant.Answer4_PlaceGfx.Location = New System.Drawing.Point(695, 190)
            'HostContestant.AnswerA_Label.Location = New System.Drawing.Point(61, 9)
            'HostContestant.AnswerB_Label.Location = New System.Drawing.Point(61, 9)
            'HostContestant.AnswerC_Label.Location = New System.Drawing.Point(61, 9)
            'HostContestant.AnswerD_Label.Location = New System.Drawing.Point(61, 9)
            'HostContestant.AtaAns1percents_Textbox.Location = New System.Drawing.Point(610, 515)
            'HostContestant.AtaAns1_Textbox.Location = New System.Drawing.Point(18, 515)
            'HostContestant.AtaAns2percents_Textbox.Location = New System.Drawing.Point(610, 553)
            'HostContestant.AtaAns2_Textbox.Location = New System.Drawing.Point(18, 553)
            'HostContestant.AtaAns3percents_Textbox.Location = New System.Drawing.Point(610, 591)
            'HostContestant.AtaAns3_Textbox.Location = New System.Drawing.Point(18, 591)
            'HostContestant.AtaAns4percents_Textbox.Location = New System.Drawing.Point(610, 629)
            'HostContestant.AtaAns4_Textbox.Location = New System.Drawing.Point(18, 629)
            'HostContestant.Away.Location = New System.Drawing.Point(150, 18)
            'HostContestant.awayPanel.Location = New System.Drawing.Point(18, 293)
            'HostContestant.Button2.Location = New System.Drawing.Point(700, -55)
            'HostContestant.ContestantName_Label.Location = New System.Drawing.Point(8, 0)
            'HostContestant.ContestantName_Panel.Location = New System.Drawing.Point(19, 715)
            'HostContestant.ContestantName_Panel.Visible = True
            'HostContestant.correctSumePanel.Location = New System.Drawing.Point(18, 346)
            'HostContestant.Correct_Box.Location = New System.Drawing.Point(575, 293)
            'HostContestant.DirectorsChat_TextBox.Location = New System.Drawing.Point(14, 406)
            'HostContestant.ExplanationQuestion_TextBox.Location = New System.Drawing.Point(14, 518)
            'HostContestant.fall.Location = New System.Drawing.Point(54, 15)
            'HostContestant.fallSumePanel.Location = New System.Drawing.Point(482, 346)
            'HostContestant.GameLimitedClock_Label.Location = New System.Drawing.Point(3, 16)
            'HostContestant.incorrect.Location = New System.Drawing.Point(49, 15)
            'HostContestant.incorrectSumePanel.Location = New System.Drawing.Point(251, 346)
            'HostContestant.Label1.Location = New System.Drawing.Point(5, 16)
            'HostContestant.Label6.Location = New System.Drawing.Point(63, 22)
            'HostContestant.Lifeline1_PictureBox.Location = New System.Drawing.Point(10, 4)
            'HostContestant.Lifeline2_PictureBox.Location = New System.Drawing.Point(104, 4)
            'HostContestant.Lifeline3_PictureBox.Location = New System.Drawing.Point(198, 4)
            'HostContestant.Lifeline4_PictureBox.Location = New System.Drawing.Point(292, 4)
            'HostContestant.MarkA_Label.Location = New System.Drawing.Point(23, 13)
            'HostContestant.MarkB_Label.Location = New System.Drawing.Point(23, 13)
            'HostContestant.MarkC_Label.Location = New System.Drawing.Point(23, 13)
            'HostContestant.MarkD_Label.Location = New System.Drawing.Point(23, 13)
            'HostContestant.PAFClock_Label.Location = New System.Drawing.Point(839, 450)
            'HostContestant.MoneyTreePanel.Location = New System.Drawing.Point(976, 292)
            'HostContestant.Panel2.Location = New System.Drawing.Point(837, 291)
            'HostContestant.PictureBox1.Location = New System.Drawing.Point(3, 2)
            'HostContestant.PictureBox2.Location = New System.Drawing.Point(0, 219)
            'HostContestant.PictureBox3.Location = New System.Drawing.Point(0, 150)
            'HostContestant.PictureBox4.Location = New System.Drawing.Point(0, 61)
            'HostContestant.PictureBox5.Location = New System.Drawing.Point(4, 2)
            'HostContestant.PictureBox7.Location = New System.Drawing.Point(0, 0)
            'HostContestant.PictureBox8.Location = New System.Drawing.Point(4, 2)
            'HostContestant.PictureBox9.Location = New System.Drawing.Point(3, 2)
            'HostContestant.PictureBox17.Location = New System.Drawing.Point(2, 2)
            'HostContestant.PronunciationHelp_Textbox.Location = New System.Drawing.Point(17, 257)
            'HostContestant.qfor.Location = New System.Drawing.Point(55, 14)
            'HostContestant.Question_Label.Location = New System.Drawing.Point(5, 5)
            'HostContestant.Question_PlaceGfx.Location = New System.Drawing.Point(60, 9)
            'HostContestant.Q_1.Location = New System.Drawing.Point(10, 423)
            'HostContestant.Q_2.Location = New System.Drawing.Point(10, 398)
            'HostContestant.Q_3.Location = New System.Drawing.Point(10, 373)
            'HostContestant.Q_4.Location = New System.Drawing.Point(10, 348)
            'HostContestant.Q_5.Location = New System.Drawing.Point(10, 323)
            'HostContestant.Q_6.Location = New System.Drawing.Point(10, 298)
            'HostContestant.Q_7.Location = New System.Drawing.Point(10, 273)
            'HostContestant.Q_8.Location = New System.Drawing.Point(10, 248)
            'HostContestant.Q_9.Location = New System.Drawing.Point(10, 223)
            'HostContestant.Q_10.Location = New System.Drawing.Point(10, 198)
            'HostContestant.Q_11.Location = New System.Drawing.Point(10, 173)
            'HostContestant.Q_12.Location = New System.Drawing.Point(10, 148)
            'HostContestant.Q_13.Location = New System.Drawing.Point(10, 123)
            'HostContestant.Q_14.Location = New System.Drawing.Point(10, 98)
            'HostContestant.Q_15.Location = New System.Drawing.Point(10, 73)
            'HostContestant.sumePanel.Location = New System.Drawing.Point(250, 293)
        End If
    End Sub

    Private Sub HostScreenType_RadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles HostScreenType_RadioButton.CheckedChanged, ContestantScreenType_RadioButton.CheckedChanged

        HostContestant.hostContestantData._isHost = HostScreenType_RadioButton.Checked
        HostContestant.EnableGUIIfHost()
    End Sub

    Private Sub CheckServerStatus_LinkLabel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles CheckServerStatus_LinkLabel.LinkClicked
        System.Diagnostics.Process.Start($"https://{My.Settings.IPAddress}/wwtbam-state/GetMainGamePlayState.php")

    End Sub

    Private Sub ScreenResoultion_ComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ScreenResoultion_ComboBox.SelectedIndexChanged
        Select Case ScreenResoultion_ComboBox.Text.ToUpper.Trim
            Case "1080", "1080P", "FULLHD", "1920X1080"
                GUIDesignerPropertisContext.SetDesignerData(GUIDesignerPropertisContext.Resolution.FullHD)
            Case "768", "720P", "HDREADY", "1366X768", "1280X720"
                GUIDesignerPropertisContext.SetDesignerData(GUIDesignerPropertisContext.Resolution.HDReady)
        End Select

    End Sub

End Class