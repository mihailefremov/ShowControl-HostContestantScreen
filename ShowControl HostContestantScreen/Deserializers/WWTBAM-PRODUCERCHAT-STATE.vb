Imports System
Imports System.Xml.Serialization
Imports System.Collections.Generic

Namespace Xml2CSharp
    <XmlRoot(ElementName:="WWTBAM-PRODUCERCHAT-STATE")>
    Public Class WWTBAMPRODUCERCHATSTATE
        <XmlElement(ElementName:="STATEID")>
        Public Property STATEID As String
        <XmlElement(ElementName:="PRODUCERCHATSTATE")>
        Public Property PRODUCERCHATSTATE As String
        <XmlElement(ElementName:="PRODUCERCHATTEXT")>
        Public Property PRODUCERCHATTEXT As String
    End Class
End Namespace
