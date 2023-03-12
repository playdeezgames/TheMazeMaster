Friend Module Fight
    Friend FIGHT_CREATURE_INDEX As Integer = -1
    Friend Sub FIGHT_START()
        Dim DI = FIGHT_CREATURE_INDEX
        Dim AI = PLAYER_CREATURE_INDEX
        AnsiConsole.MarkupLine($"FIGHTING {AllCreatures(DI).Name}")
        FIGHT_PROMPT()
    End Sub
    Friend Function Update() As String
        Dim DI = FIGHT_CREATURE_INDEX
        Dim AI = PLAYER_CREATURE_INDEX
        If AllCreatures(AI).Alive And AllCreatures(DI).Alive Then
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now What?[/]"}
            Const AttackText = "Attack"
            Const RunText = "Run"
            prompt.AddChoices(AttackText, RunText)
            Select Case AnsiConsole.Prompt(prompt)
                Case RunText
                    Return STATE_IN_PLAY
                Case AttackText
                    FIGHT_ATTACK()
            End Select
        ElseIf AllCreatures(AI).Alive Then
            Dim prompt As New SelectionPrompt(Of String) With {.Title = ""}
            prompt.AddChoice("Victory!")
            AnsiConsole.Prompt(prompt)
            Return STATE_IN_PLAY
        Else
            Dim prompt As New SelectionPrompt(Of String) With {.Title = ""}
            prompt.AddChoice("Yer Dead!")
            AnsiConsole.Prompt(prompt)
            Return STATE_TITLE
        End If
        Return STATE_FIGHT
    End Function
    Friend Sub FIGHT_PROMPT()
        Dim DI = FIGHT_CREATURE_INDEX
        Dim AI = PLAYER_CREATURE_INDEX
        If AllCreatures(AI).Alive AndAlso AllCreatures(DI).Alive Then
            AnsiConsole.MarkupLine($"{AllCreatures(AI).Name} has {AllCreatures(AI).Health} HP")
            AnsiConsole.MarkupLine($"{AllCreatures(DI).Name} has {AllCreatures(DI).Health} HP")
        ElseIf AllCreatures(AI).Alive Then
            PLAYER_ADD_XP(AllCreatures(DI).XP)
        End If
    End Sub
    Friend Sub FIGHT_ATTACK()
        Dim DI = FIGHT_CREATURE_INDEX
        Dim AI = PLAYER_CREATURE_INDEX
        RESOLVE_ATTACK(AI, DI)
        If AllCreatures(DI).Alive Then
            RESOLVE_ATTACK(DI, AI)
        End If
        FIGHT_PROMPT()
    End Sub
    Friend Sub RESOLVE_ATTACK(AI As Integer, DI As Integer)
        AnsiConsole.MarkupLine($"{AllCreatures(AI).Name} attacks {AllCreatures(DI).Name}")
        Dim AR = AllCreatures(AI).RollAttack
        Dim DR = AllCreatures(DI).RollDefend
        AnsiConsole.MarkupLine($"{AllCreatures(AI).Name} rolls {AR}")
        AnsiConsole.MarkupLine($"{AllCreatures(DI).Name} rolls {DR}")
        If AR > DR Then
            Dim D = AR - DR
            AllCreatures(DI).AddWounds(D)
            AnsiConsole.MarkupLine($"{AllCreatures(AI).Name} hits for {D}")
            If Not AllCreatures(DI).Alive Then
                AnsiConsole.MarkupLine($"{AllCreatures(AI).Name} kills {AllCreatures(DI).Name}")
                AllCreatures(DI).Remove()
                AllCreatures(DI).Drop()
            End If
        Else
            AnsiConsole.MarkupLine($"{AllCreatures(AI).Name} misses")
        End If
    End Sub
End Module
