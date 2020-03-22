<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Configuration
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Configuration))
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ConfigureAndStart_Button = New System.Windows.Forms.Button()
        Me.CheckServerStatus_LinkLabel = New System.Windows.Forms.LinkLabel()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Down_RadioButton = New System.Windows.Forms.RadioButton()
        Me.Up_RadioButton = New System.Windows.Forms.RadioButton()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.WindowState_Label = New System.Windows.Forms.Label()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ScreenResoultion_ComboBox = New System.Windows.Forms.ComboBox()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.ResetNetworkSettings_Button = New System.Windows.Forms.Label()
        Me.SaveNetworkSettings_Button = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ContestantScreenType_RadioButton = New System.Windows.Forms.RadioButton()
        Me.HostScreenType_RadioButton = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.IPAddress_TextBox = New System.Windows.Forms.TextBox()
        Me.QOperatorPort_TextBox = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Navy
        Me.Label6.Location = New System.Drawing.Point(12, 9)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(409, 25)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Host/Contestant Configuration Wizard"
        '
        'ConfigureAndStart_Button
        '
        Me.ConfigureAndStart_Button.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ConfigureAndStart_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ConfigureAndStart_Button.ForeColor = System.Drawing.Color.Green
        Me.ConfigureAndStart_Button.Location = New System.Drawing.Point(10, 298)
        Me.ConfigureAndStart_Button.Name = "ConfigureAndStart_Button"
        Me.ConfigureAndStart_Button.Size = New System.Drawing.Size(148, 36)
        Me.ConfigureAndStart_Button.TabIndex = 23
        Me.ConfigureAndStart_Button.Text = "Start"
        Me.ConfigureAndStart_Button.UseVisualStyleBackColor = True
        '
        'CheckServerStatus_LinkLabel
        '
        Me.CheckServerStatus_LinkLabel.AutoSize = True
        Me.CheckServerStatus_LinkLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.CheckServerStatus_LinkLabel.LinkColor = System.Drawing.Color.DarkGray
        Me.CheckServerStatus_LinkLabel.Location = New System.Drawing.Point(525, 306)
        Me.CheckServerStatus_LinkLabel.Name = "CheckServerStatus_LinkLabel"
        Me.CheckServerStatus_LinkLabel.Size = New System.Drawing.Size(155, 20)
        Me.CheckServerStatus_LinkLabel.TabIndex = 24
        Me.CheckServerStatus_LinkLabel.TabStop = True
        Me.CheckServerStatus_LinkLabel.Text = "Check Server Status"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.GroupBox2)
        Me.TabPage2.Controls.Add(Me.Label7)
        Me.TabPage2.Controls.Add(Me.WindowState_Label)
        Me.TabPage2.Controls.Add(Me.ComboBox1)
        Me.TabPage2.Controls.Add(Me.Label4)
        Me.TabPage2.Controls.Add(Me.ScreenResoultion_ComboBox)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(670, 220)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Screen Settings"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Down_RadioButton)
        Me.GroupBox2.Controls.Add(Me.Up_RadioButton)
        Me.GroupBox2.Location = New System.Drawing.Point(152, 132)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(149, 76)
        Me.GroupBox2.TabIndex = 29
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Question Position"
        '
        'Down_RadioButton
        '
        Me.Down_RadioButton.AutoSize = True
        Me.Down_RadioButton.Location = New System.Drawing.Point(6, 46)
        Me.Down_RadioButton.Name = "Down_RadioButton"
        Me.Down_RadioButton.Size = New System.Drawing.Size(60, 17)
        Me.Down_RadioButton.TabIndex = 1
        Me.Down_RadioButton.Text = "DOWN"
        Me.Down_RadioButton.UseVisualStyleBackColor = True
        '
        'Up_RadioButton
        '
        Me.Up_RadioButton.AutoSize = True
        Me.Up_RadioButton.Checked = True
        Me.Up_RadioButton.Location = New System.Drawing.Point(7, 23)
        Me.Up_RadioButton.Name = "Up_RadioButton"
        Me.Up_RadioButton.Size = New System.Drawing.Size(40, 17)
        Me.Up_RadioButton.TabIndex = 0
        Me.Up_RadioButton.TabStop = True
        Me.Up_RadioButton.Text = "UP"
        Me.Up_RadioButton.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(13, 132)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(133, 20)
        Me.Label7.TabIndex = 28
        Me.Label7.Text = "Question Position"
        '
        'WindowState_Label
        '
        Me.WindowState_Label.AutoSize = True
        Me.WindowState_Label.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.WindowState_Label.Location = New System.Drawing.Point(13, 58)
        Me.WindowState_Label.Name = "WindowState_Label"
        Me.WindowState_Label.Size = New System.Drawing.Size(108, 20)
        Me.WindowState_Label.TabIndex = 26
        Me.WindowState_Label.Text = "Window State"
        '
        'ComboBox1
        '
        Me.ComboBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"None", "Window Border"})
        Me.ComboBox1.Location = New System.Drawing.Point(134, 55)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(149, 28)
        Me.ComboBox1.TabIndex = 25
        Me.ComboBox1.Text = "None"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(43, 19)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(85, 20)
        Me.Label4.TabIndex = 24
        Me.Label4.Text = "Resolution"
        '
        'ScreenResoultion_ComboBox
        '
        Me.ScreenResoultion_ComboBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ScreenResoultion_ComboBox.FormattingEnabled = True
        Me.ScreenResoultion_ComboBox.Items.AddRange(New Object() {"1366x768", "1920x1080"})
        Me.ScreenResoultion_ComboBox.Location = New System.Drawing.Point(134, 16)
        Me.ScreenResoultion_ComboBox.Name = "ScreenResoultion_ComboBox"
        Me.ScreenResoultion_ComboBox.Size = New System.Drawing.Size(149, 28)
        Me.ScreenResoultion_ComboBox.TabIndex = 23
        Me.ScreenResoultion_ComboBox.Text = "1366x768"
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.ResetNetworkSettings_Button)
        Me.TabPage1.Controls.Add(Me.SaveNetworkSettings_Button)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.IPAddress_TextBox)
        Me.TabPage1.Controls.Add(Me.QOperatorPort_TextBox)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(670, 220)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Network Settings"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'ResetNetworkSettings_Button
        '
        Me.ResetNetworkSettings_Button.AutoSize = True
        Me.ResetNetworkSettings_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ResetNetworkSettings_Button.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ResetNetworkSettings_Button.Location = New System.Drawing.Point(448, 52)
        Me.ResetNetworkSettings_Button.Name = "ResetNetworkSettings_Button"
        Me.ResetNetworkSettings_Button.Size = New System.Drawing.Size(52, 20)
        Me.ResetNetworkSettings_Button.TabIndex = 25
        Me.ResetNetworkSettings_Button.Text = "Reset"
        '
        'SaveNetworkSettings_Button
        '
        Me.SaveNetworkSettings_Button.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.SaveNetworkSettings_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SaveNetworkSettings_Button.ForeColor = System.Drawing.Color.Goldenrod
        Me.SaveNetworkSettings_Button.Location = New System.Drawing.Point(368, 44)
        Me.SaveNetworkSettings_Button.Name = "SaveNetworkSettings_Button"
        Me.SaveNetworkSettings_Button.Size = New System.Drawing.Size(64, 36)
        Me.SaveNetworkSettings_Button.TabIndex = 24
        Me.SaveNetworkSettings_Button.Text = "Save"
        Me.SaveNetworkSettings_Button.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.ContestantScreenType_RadioButton)
        Me.GroupBox1.Controls.Add(Me.HostScreenType_RadioButton)
        Me.GroupBox1.Location = New System.Drawing.Point(173, 102)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(149, 100)
        Me.GroupBox1.TabIndex = 19
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Screen Info Type"
        '
        'ContestantScreenType_RadioButton
        '
        Me.ContestantScreenType_RadioButton.AutoSize = True
        Me.ContestantScreenType_RadioButton.Location = New System.Drawing.Point(7, 62)
        Me.ContestantScreenType_RadioButton.Name = "ContestantScreenType_RadioButton"
        Me.ContestantScreenType_RadioButton.Size = New System.Drawing.Size(98, 17)
        Me.ContestantScreenType_RadioButton.TabIndex = 1
        Me.ContestantScreenType_RadioButton.Text = "CONTESTANT"
        Me.ContestantScreenType_RadioButton.UseVisualStyleBackColor = True
        '
        'HostScreenType_RadioButton
        '
        Me.HostScreenType_RadioButton.AutoSize = True
        Me.HostScreenType_RadioButton.Checked = True
        Me.HostScreenType_RadioButton.Location = New System.Drawing.Point(7, 28)
        Me.HostScreenType_RadioButton.Name = "HostScreenType_RadioButton"
        Me.HostScreenType_RadioButton.Size = New System.Drawing.Size(55, 17)
        Me.HostScreenType_RadioButton.TabIndex = 0
        Me.HostScreenType_RadioButton.TabStop = True
        Me.HostScreenType_RadioButton.Text = "HOST"
        Me.HostScreenType_RadioButton.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(80, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(87, 20)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "IP Address"
        '
        'IPAddress_TextBox
        '
        Me.IPAddress_TextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IPAddress_TextBox.Location = New System.Drawing.Point(173, 17)
        Me.IPAddress_TextBox.Name = "IPAddress_TextBox"
        Me.IPAddress_TextBox.Size = New System.Drawing.Size(149, 26)
        Me.IPAddress_TextBox.TabIndex = 13
        Me.IPAddress_TextBox.Text = "127.0.0.1"
        Me.IPAddress_TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'QOperatorPort_TextBox
        '
        Me.QOperatorPort_TextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.QOperatorPort_TextBox.Location = New System.Drawing.Point(173, 54)
        Me.QOperatorPort_TextBox.Name = "QOperatorPort_TextBox"
        Me.QOperatorPort_TextBox.Size = New System.Drawing.Size(149, 26)
        Me.QOperatorPort_TextBox.TabIndex = 15
        Me.QOperatorPort_TextBox.Text = "443"
        Me.QOperatorPort_TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(129, 57)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 20)
        Me.Label2.TabIndex = 16
        Me.Label2.Text = "Port"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(6, 44)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(678, 246)
        Me.TabControl1.TabIndex = 12
        '
        'Configuration
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(690, 350)
        Me.Controls.Add(Me.CheckServerStatus_LinkLabel)
        Me.Controls.Add(Me.ConfigureAndStart_Button)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Label6)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Configuration"
        Me.Text = "HCConfiguration"
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label6 As Label
    Friend WithEvents ConfigureAndStart_Button As Button
    Friend WithEvents CheckServerStatus_LinkLabel As LinkLabel
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Down_RadioButton As RadioButton
    Friend WithEvents Up_RadioButton As RadioButton
    Friend WithEvents Label7 As Label
    Friend WithEvents WindowState_Label As Label
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents ScreenResoultion_ComboBox As ComboBox
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents ResetNetworkSettings_Button As Label
    Friend WithEvents SaveNetworkSettings_Button As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents ContestantScreenType_RadioButton As RadioButton
    Friend WithEvents HostScreenType_RadioButton As RadioButton
    Friend WithEvents Label1 As Label
    Friend WithEvents IPAddress_TextBox As TextBox
    Friend WithEvents QOperatorPort_TextBox As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TabControl1 As TabControl
End Class
