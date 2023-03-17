Friend Module Inventory
    Friend Function Update() As StateIdentifier
        AnsiConsole.Clear()
        AnsiConsole.MarkupLine("Inventory:")
        Dim hasNothing = True
        If Worlds.world.character.ItemStacks.Any(Function(x) x.Value > 0) Then
            hasNothing = False
            For Each itemStack In Worlds.world.character.ItemStacks.Where(Function(x) x.Value > 0)
                AnsiConsole.MarkupLine($"{AllItemTypes(itemStack.Key).Name} x{itemStack.Value}")
            Next
        End If
        If hasNothing Then
            AnsiConsole.MarkupLine("You have nothing!")
        End If
        Dim prompt As New SelectionPrompt(Of String) With {.Title = ""}
        prompt.AddChoice(Constants.Ok)
        AnsiConsole.Prompt(prompt)
        Return StateIdentifier.GameMenu
    End Function
End Module
