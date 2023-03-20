Friend Module Status
    Friend Function Update(world As World) As StateIdentifier
        AnsiConsole.Clear()
        Dim character = world.character
        AnsiConsole.MarkupLine("Status:")
        AnsiConsole.MarkupLine($"HP {character.Creature.Health}/{character.Creature.MaximumHitPoints}")
        OkPrompt()
        Return StateIdentifier.GameMenu
    End Function
End Module
