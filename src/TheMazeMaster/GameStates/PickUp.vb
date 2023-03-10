Friend Module PickUp
    Friend PICKUP_ITEM_INDEX As Integer = -1
    Friend Function Update() As String
        Dim II = PICKUP_ITEM_INDEX
        Dim IT = ITEM_TYPES(II)
        AnsiConsole.MarkUpLine($"PICK UP {ITEMTYPE_NAMES(IT)}?")
        AnsiConsole.MarkUpLine("(Y)ES/(N)O")
        Select Case Console.ReadKey(True).Key
            Case ConsoleKey.N
                Return STATE_IN_PLAY
            Case ConsoleKey.Y
                REMOVE_ITEM(II)
                PLAYER_TAKE_ITEM(II)
                Return STATE_IN_PLAY
        End Select
        Return STATE_PICKUP
    End Function
End Module
