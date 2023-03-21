Friend Module Win
    Friend Function Update(arg As World) As StateIdentifier
        AnsiConsole.Clear()
        AnsiConsole.MarkupLine("You win!")
        SfxHandler.HandleSfx(Sfx.Win)
        OkPrompt()
        Return StateIdentifier.Title
    End Function
End Module
