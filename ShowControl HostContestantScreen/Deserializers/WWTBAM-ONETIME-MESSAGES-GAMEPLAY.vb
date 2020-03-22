Imports System
Imports System.Xml.Serialization
Imports System.Collections.Generic

Namespace Xml2CSharp
    <XmlRoot(ElementName:="WWTBAM-ONETIME-MESSAGES-GAMEPLAY")>
    Public Class WWTBAMONETIMEMESSAGESGAMEPLAY
        <XmlElement(ElementName:="STATEID")>
        Public Property STATEID As String
        <XmlElement(ElementName:="EXPLANATION")>
        Public Property EXPLANATION As EXPLANATION
        <XmlElement(ElementName:="OTHER")>
        Public Property OTHER As OTHER
    End Class

    <XmlRoot(ElementName:="EXPLANATION")>
    Public Class EXPLANATION
        <XmlElement(ElementName:="LASTCHANGE")>
        Public Property LASTCHANGE As String
        <XmlElement(ElementName:="STATE")>
        Public Property STATE As String
        <XmlElement(ElementName:="EXECUTED")>
        Public Property EXECUTED As String
    End Class

    <XmlRoot(ElementName:="OTHER")>
    Public Class OTHER
        <XmlElement(ElementName:="LASTCHANGE")>
        Public Property LASTCHANGE As String
        <XmlElement(ElementName:="STATE")>
        Public Property STATE As String
        <XmlElement(ElementName:="EXECUTED")>
        Public Property EXECUTED As String
    End Class

End Namespace
