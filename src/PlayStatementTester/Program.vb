Imports System

Module Program
    Sub Main(args As String())
        Dim song As String
        Do
            song = Console.ReadLine
            If String.IsNullOrWhiteSpace(song) Then
                Exit Do
            End If
            PlayStatement.Play(song)
        Loop
    End Sub
End Module
