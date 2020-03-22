Public Class GUIHelpers
    Public Shared Sub FitTextInsideControl(ByRef FormControl As System.Windows.Forms.Control, defaultFont As System.Drawing.Font)
        If String.IsNullOrEmpty(FormControl.Text) Then Return
        If IsShowingEllipsis(FormControl) Then
            FormControl.Font = New Font(FormControl.Font.FontFamily, FormControl.Font.Size - 0.1F, FormControl.Font.Style)
            FitTextInsideControl(FormControl, defaultFont)
        End If
    End Sub

    Public Shared Function IsShowingEllipsis(ByVal FormControl As System.Windows.Forms.Control) As Boolean
        Dim sz As Size = TextRenderer.MeasureText(FormControl.Text, FormControl.Font, FormControl.Size, TextFormatFlags.WordBreak)
        Return (sz.Width > FormControl.Size.Width OrElse sz.Height > FormControl.Size.Height)

    End Function
End Class
