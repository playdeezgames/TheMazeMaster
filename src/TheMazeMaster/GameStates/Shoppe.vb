Friend Module Shoppe
    Friend ShoppeTypeIdentifier As ShoppeTypeIdentifier

    Friend Function Update(world As World) As StateIdentifier
        AnsiConsole.Clear()
        Dim shoppeType = AllShoppeTypes(ShoppeTypeIdentifier)
        AnsiConsole.MarkupLine($"Yer in {shoppeType.Name}.")
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now What[/]"}
        prompt.AddChoice(Leave)
        If shoppeType.CanSell Then
            prompt.AddChoice(Constants.Trade)
        End If
        Select Case AnsiConsole.Prompt(prompt)
            Case Leave
                Return StateIdentifier.InPlay
            Case Constants.Trade
                DoTrade(world, shoppeType)
            Case Else
                Throw New NotImplementedException
        End Select
        Return StateIdentifier.Shoppe
    End Function
    Private Sub DoTrade(world As World, shoppeType As ShoppeType)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]What would you like to trade?[/]"}
        prompt.AddChoice(NeverMind)
        Dim trades = shoppeType.Offers.Where(AddressOf world.character.CanTrade)
        Dim table = trades.ToDictionary(Function(x) x.ToString(), Function(x) x)
        prompt.AddChoices(table.Keys)
        Dim answer = AnsiConsole.Prompt(prompt)
        If answer = NeverMind Then
            Return
        End If
        world.character.MakeTrade(table(answer))
    End Sub
End Module
