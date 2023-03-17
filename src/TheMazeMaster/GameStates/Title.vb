Friend Module Title
    Friend Function Update() As StateIdentifier
        AnsiConsole.Clear()
        Dim figlet As New FigletText("The Maze Master") With {.Color = Color.Fuchsia, .Justification = Justify.Center}
        AnsiConsole.Write(figlet)
        AnsiConsole.MarkupLine("By TheGrumpyGameDev")
        AnsiConsole.MarkupLine("With Music by Graham Weldon")
        AnsiConsole.MarkupLine("For: Jam for All BASIC Dialects (#4)")
        SfxHandler.HandleSfx(Sfx.Title)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = ""}
        prompt.AddChoice("Start")
        AnsiConsole.Prompt(prompt)
        Worlds.Start()
        Return StateIdentifier.InPlay
    End Function
End Module
