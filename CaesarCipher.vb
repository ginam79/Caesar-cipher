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
        MsgBox("Created by ginam79 - MMXVI - Italian version")
    End Sub

    Public Function encrypt(ByVal key As Integer, ByVal input As String) As String
        key = key Mod 26
        input = Regex.Replace(input, "[^a-zA-Z]+", "")
        Return New String(input.Replace(" ", "").Replace(vbCr, "").Replace(vbLf, "").ToUpper().Select(Function(c) Chr(((Asc(c) + key - Asc("A"c)) Mod 26) + Asc("A"c))).ToArray())
    End Function

    Public Function calculate_frequencies(ByVal input As String) As Array
        input = Regex.Replace(input, "[^a-zA-Z]+", "")

        Dim c As Char
        Dim index As Integer = 1
        Dim freq(25) As Integer

        For Each c In input
            index = Asc(c) - Asc("A")
            freq(index) += 1
        Next

        Dim sum As Integer = 0
        Dim num As Integer = 0
        For Each num In freq
            sum += num
        Next

        Dim frequencies(25) As Double
        If sum <> 0 Then
            For index = 0 To 25
                frequencies(index) = freq(index) / sum
            Next
        End If

        Return frequencies

    End Function

    Public Function shift_text(ByVal input As String, ByVal index As Integer) As String
        input = Regex.Replace(input, "[^a-zA-Z]+", "")

        Return New String(input.ToUpper().Select(Function(c) Chr(((Asc(c) + 26 - index - Asc("A"c)) Mod 26) + Asc("A"c))).ToArray())

    End Function

    Public Function find_index(ByVal input As String) As Integer
        Dim it_freq(26) As Double

        it_freq = {0.0848, 0.0064, 0.043, 0.04, 0.1196, 0.0095, 0.0128, 0.0073, 0.1143, 0.0002, 0.0016, 0.0575, 0.0268, 0.0757, 0.0848, 0.0318, 0.03, 0.0731, 0.0667, 0.0643, 0.0333, 0.0157, 0.0088, 0.023, 0.14, 0.0153}

        Dim i, j As Integer
        Dim shifted_text As String
        Dim i_freq(25) As Double
        Dim chi(25, 25) As Double
        Dim chi_sum(25) As Double

        For i = 0 To 25
            shifted_text = shift_text(input, i)
            i_freq = calculate_frequencies(shifted_text)
            For j = 0 To 25
                chi(i, j) = ((i_freq(j) - it_freq(j)) ^ 2) / it_freq(j)
            Next
            For j = 0 To 25
                chi_sum(i) += chi(i, j)
            Next
        Next

        Dim shift As Integer
        shift = chi_sum.IndexOf(chi_sum, chi_sum.Min)

        If shift <> -1 Then
            Return shift
        Else
            Return 0
        End If
    End Function

    Public Function decrypt(ByVal input As String, ByVal shift As Integer) As String

        Return New String(input.ToUpper().Select(Function(c) Chr(((Asc(c) + 26 - shift - Asc("A"c)) Mod 26) + Asc("A"c))).ToArray())

    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim index As Integer

        index = find_index(TextBox2.Text)
        TextBox1.Text = decrypt(TextBox2.Text, index)
        TextBox3.Text = index
    End Sub
End Class
