Imports System
Imports System.Xml.Serialization
Imports System.Collections.Generic

Namespace Xml2CSharp
    <XmlRoot(ElementName:="WWTBAM-MAIN-GAMEPLAY-STATE")>
    Public Class WWTBAMMAINGAMEPLAYSTATE
        <XmlElement(ElementName:="STATEID")>
        Public Property STATEID As String
        <XmlElement(ElementName:="QLEVEL")>
        Public Property QLEVEL As String
        <XmlElement(ElementName:="BANKDROPIFCORRECTIFWRONG")>
        Public Property BANKDROPIFCORRECTIFWRONG As String
        <XmlElement(ElementName:="SECONDMILESTONEAT")>
        Public Property SECONDMILESTONEAT As String
        <XmlElement(ElementName:="PRONUNCIATIONSTATE")>
        Public Property PRONUNCIATIONSTATE As String
        <XmlElement(ElementName:="PRONUNCIATIONTEXT")>
        Public Property PRONUNCIATIONTEXT As String
        <XmlElement(ElementName:="QUESTIONANSWERSSTATE")>
        Public Property QUESTIONANSWERSSTATE As String
        <XmlElement(ElementName:="QUESTIONTEXT")>
        Public Property QUESTIONTEXT As String
        <XmlElement(ElementName:="ANSWER1TEXT")>
        Public Property ANSWER1TEXT As String
        <XmlElement(ElementName:="ANSWER2TEXT")>
        Public Property ANSWER2TEXT As String
        <XmlElement(ElementName:="ANSWER3TEXT")>
        Public Property ANSWER3TEXT As String
        <XmlElement(ElementName:="ANSWER4TEXT")>
        Public Property ANSWER4TEXT As String
        <XmlElement(ElementName:="FINALANSWER")>
        Public Property FINALANSWER As String
        <XmlElement(ElementName:="CORRECTANSWER")>
        Public Property CORRECTANSWER As String
        <XmlElement(ElementName:="EXPLANATIONSTATE")>
        Public Property EXPLANATIONSTATE As String
        <XmlElement(ElementName:="EXPLANATIONTEXT")>
        Public Property EXPLANATIONTEXT As String
        <XmlElement(ElementName:="ACTIVELIFELINES")>
        Public Property ACTIVELIFELINES As String
        <XmlElement(ElementName:="LIFELINESSTATE")>
        Public Property LIFELINESSTATE As String
        <XmlElement(ElementName:="LIFELINEREMIND")>
        Public Property LIFELINEREMIND As String
        <XmlElement(ElementName:="FIFTYFIFTY")>
        Public Property FIFTYFIFTY As String
        <XmlElement(ElementName:="PAF")>
        Public Property PAF As String
        <XmlElement(ElementName:="ATASTATE")>
        Public Property ATASTATE As String
        <XmlElement(ElementName:="ATAPERCENTS")>
        Public Property ATAPERCENTS As String
        <XmlElement(ElementName:="STQSTATE")>
        Public Property STQSTATE As String
        <XmlElement(ElementName:="DOUBLEDIPSTATE")>
        Public Property DOUBLEDIPSTATE As String
        <XmlElement(ElementName:="DOUBLEDIPFIRSTANSWER")>
        Public Property DOUBLEDIPFIRSTANSWER As String
        <XmlElement(ElementName:="CONTESTANTNAMECITY")>
        Public Property CONTESTANTNAMECITY As String
        <XmlElement(ElementName:="PARTNERNAME")>
        Public Property PARTNERNAME As String
    End Class
End Namespace