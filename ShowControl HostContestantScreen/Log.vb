Public Class Log
#Region "ERRORS"
    Public Shared Sub LogWrite(message As String, filename As String)
        Dim path As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\ShowControl-HostContestant-Logs"
        If Not IO.Directory.Exists(path) Then
            Dim di As IO.DirectoryInfo = IO.Directory.CreateDirectory(path)
        End If
        Dim file As String = path + "\" + filename + Date.Now.ToString("yyyy-MM-dd") + ".txt"

        Dim createText As String = String.Format(Date.Now.ToString("yyyy-MM-dd HH:mm:ss.ff") + " | {0} ", message)

        Using sw As IO.StreamWriter = New IO.StreamWriter(file, True)
            sw.WriteLine(createText)
        End Using
    End Sub
#End Region
End Class
