Friend Module Inventory
    Friend Function Update(world As World) As StateIdentifier
        AnsiConsole.Clear()
        AnsiConsole.MarkupLine("Inventory:")
        Dim hasNothing = True
        If world.character.ItemStacks.Any(Function(x) x.Value > 0) Then
            hasNothing = False
            For Each itemStack In world.character.ItemStacks.Where(Function(x) x.Value > 0)
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
                NoncombatUseItem(world)
                Return StateIdentifier.Inventory
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Private Sub NoncombatUseItem(world As World)
        Dim usableItemTypes As IEnumerable(Of ItemType) = world.character.NoncombatUsableItemTypes
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
                For Each text In world.character.NoncombatUseItemType(table(answer))
                    AnsiConsole.MarkupLine(text)
                Next
                OkPrompt()
        End Select
    End Sub
End Module
