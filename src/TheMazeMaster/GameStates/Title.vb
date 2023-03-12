Friend Module Title
    Friend Function Update() As String
        AnsiConsole.Clear()
        Dim figlet As New FigletText("The Maze Master") With {.Color = Color.Fuchsia, .Justification = Justify.Center}
        AnsiConsole.Write(figlet)
        AnsiConsole.MarkUpLine("By TheGrumpyGameDev")
        AnsiConsole.MarkupLine("For: Jam for All BASIC Dialects (#4)")
        Dim prompt As New SelectionPrompt(Of String) With {.Title = ""}
        prompt.AddChoice("Start")
        AnsiConsole.Prompt(prompt)
        Game.Start()
        Return StateMachine.STATE_IN_PLAY
    End Function
End Module
