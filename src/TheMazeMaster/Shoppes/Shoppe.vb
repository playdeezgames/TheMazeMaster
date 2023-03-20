Friend Module Shoppe
    Friend ShoppeTypeIdentifier As ShoppeTypeIdentifier

    Friend Function Update() As StateIdentifier
        AnsiConsole.Clear()
        AnsiConsole.MarkupLine("Yer in a shoppe.")
        OkPrompt()
        Return StateIdentifier.InPlay
    End Function
End Module
