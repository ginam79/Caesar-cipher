Imports System.Text.RegularExpressions

Public Class CaesarCipher

    Public Shared Property freq As Double() = {0.0848, 0.0064, 0.043, 0.04, 0.1196, 0.0095, 0.0128, 0.0073, 0.1143, 0.0002, 0.0016, 0.0575, 0.0268, 0.0757, 0.0848, 0.0318, 0.03, 0.0731, 0.0667, 0.0643, 0.0333, 0.0157, 0.0088, 0.023, 0.14, 1, 53}

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim key As Integer

        key = Convert.ToInt16(TextBox3.Text)

        TextBox2.Text = encrypt(key, TextBox1.Text)
    End Sub

    Private Sub EsciToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EsciToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        MsgBox("Created by ginam79 - MMXVI")
    End Sub

    Public Function encrypt(ByVal key As Integer, ByVal input As String) As String
        key = key Mod 26
        input = Regex.Replace(input, "[^a-zA-Z]+", "")
        Return New String(input.Replace(" ", "").Replace(vbCr, "").Replace(vbLf, "").ToUpper().Select(Function(c) Chr(((Asc(c) + key - Asc("A"c)) Mod 26) + Asc("A"c))).ToArray())
    End Function

    Public Function decrypt(ByVal input As String) As Integer
        Dim key As Integer = 1
        input = Regex.Replace(input, "[^a-zA-Z]+", "")

    End Function

End Class
