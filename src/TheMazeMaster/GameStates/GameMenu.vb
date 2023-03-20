Friend Module GameMenu
    Friend Function Update(world As World) As StateIdentifier
        AnsiConsole.Clear()
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now What?[/]"}
        prompt.AddChoice(Constants.BackToGame)
        prompt.AddChoice(Constants.Inventory)
        prompt.AddChoice(Constants.Status)
        Select Case AnsiConsole.Prompt(prompt)
            Case BackToGame
                Return StateIdentifier.InPlay
            Case Constants.Inventory
                Return StateIdentifier.Inventory
            Case Constants.Status
                Return StateIdentifier.Status
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Module
