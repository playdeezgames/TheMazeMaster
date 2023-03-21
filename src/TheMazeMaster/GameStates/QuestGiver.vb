Friend Module QuestGiver
    Friend Function Update(world As World) As StateIdentifier
        If world.character.GetItemCount(ItemTypeIdentifier.MacGuffin) > 0 Then
            Return StateIdentifier.Win
        End If
        AnsiConsole.Clear()
        AnsiConsole.MarkupLine("Please get the MacGuffin from the dungeon!")
        OkPrompt()
        Return StateIdentifier.InPlay
    End Function
End Module
