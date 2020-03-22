Imports System
Imports System.Xml.Serialization
Imports System.Collections.Generic

Namespace Xml2CSharp
    <XmlRoot(ElementName:="WWTBAM-LAST-GAMEPLAYSTATE-CHANGE")>
    Public Class WWTBAMLASTGAMEPLAYSTATECHANGE
        <XmlElement(ElementName:="STATEID")>
        Public Property STATEID As String
    End Class
End Namespace