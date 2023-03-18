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
        prompt.AddChoice(Constants.BackToGame)
        prompt.AddChoice(Constants.UseText)
        Select Case AnsiConsole.Prompt(prompt)
            Case BackToGame
                Return StateIdentifier.GameMenu
            Case UseText
                NoncombatUseItem()
                Return StateIdentifier.Inventory
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Private Sub NoncombatUseItem()
        Dim usableItemTypes As IEnumerable(Of ItemType) = Worlds.world.character.NoncombatUsableItemTypes
        Dim prompt As SelectionPrompt(Of String)
        Dim table = usableItemTypes.ToDictionary(Function(x) x.Name, Function(x) x)
        prompt = New SelectionPrompt(Of String) With {.Title = "[olive]Use What?[/]"}
        prompt.AddChoice(NeverMind)
        prompt.AddChoices(table.Keys)
        Dim answer = AnsiConsole.Prompt(prompt)
        Select Case answer
            Case NeverMind
                Return
            Case Else
                For Each text In Worlds.world.character.NoncombatUseItemType(table(answer))
                    AnsiConsole.MarkupLine(text)
                Next
                OkPrompt()
        End Select
    End Sub
End Module
