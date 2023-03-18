Friend Module Fight
    Friend FIGHT_CREATURE_INDEX As Integer = -1
    Friend Sub FIGHT_START()
        Dim DI = FIGHT_CREATURE_INDEX
        Dim AI = Worlds.world.character.CreatureIndex
        AnsiConsole.MarkupLine($"FIGHTING {Worlds.world.GetCreature(DI).Name}")
        FIGHT_PROMPT()
    End Sub
    Friend Function Update() As StateIdentifier
        Dim DI = FIGHT_CREATURE_INDEX
        Dim AI = Worlds.world.character.CreatureIndex
        If Worlds.world.GetCreature(AI).Alive And Worlds.world.GetCreature(DI).Alive Then
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now What?[/]"}
            Const AttackText = "Attack"
            Const RunText = "Run"
            Const UseText = "Use..."
            prompt.AddChoices(AttackText, RunText, UseText)
            Select Case AnsiConsole.Prompt(prompt)
                Case RunText
                    Return StateIdentifier.InPlay
                Case AttackText
                    DoAttack()
                Case UseText
                    UseItem()
            End Select
        ElseIf Worlds.world.GetCreature(AI).Alive Then
            SfxHandler.HandleSfx(Sfx.KillEnemy)
            Dim prompt As New SelectionPrompt(Of String) With {.Title = ""}
            prompt.AddChoice("Victory!")
            AnsiConsole.Prompt(prompt)
            Return StateIdentifier.InPlay
        Else
            SfxHandler.HandleSfx(Sfx.Death)
            Dim prompt As New SelectionPrompt(Of String) With {.Title = ""}
            prompt.AddChoice("Yer Dead!")
            AnsiConsole.Prompt(prompt)
            Return StateIdentifier.Title
        End If
        Return StateIdentifier.Fight
    End Function
    Private Sub UseItem()
        Dim usableItemTypes As IEnumerable(Of ItemType) = Worlds.world.character.UsableItemTypes
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
                For Each text In Worlds.world.character.UseItemType(table(answer))
                    AnsiConsole.MarkupLine(text)
                Next
                OkPrompt()
        End Select
    End Sub
    Friend Sub FIGHT_PROMPT()
        Dim DI = FIGHT_CREATURE_INDEX
        Dim AI = Worlds.world.character.CreatureIndex
        If Worlds.world.GetCreature(AI).Alive AndAlso Worlds.world.GetCreature(DI).Alive Then
            AnsiConsole.MarkupLine($"{Worlds.world.GetCreature(AI).Name} has {Worlds.world.GetCreature(AI).Health} HP")
            AnsiConsole.MarkupLine($"{Worlds.world.GetCreature(DI).Name} has {Worlds.world.GetCreature(DI).Health} HP")
        ElseIf Worlds.world.GetCreature(AI).Alive Then
            Worlds.world.character.AddXP(Worlds.world.GetCreature(DI).XP)
        End If
    End Sub
    Friend Sub DoAttack()
        Dim DI = FIGHT_CREATURE_INDEX
        Dim AI = Worlds.world.character.CreatureIndex
        RESOLVE_ATTACK(AI, DI, Sfx.PlayerHit, Sfx.PlayerMiss)
        If Worlds.world.GetCreature(DI).Alive Then
            RESOLVE_ATTACK(DI, AI, Sfx.EnemyHit, Sfx.EnemyMiss)
        End If
        FIGHT_PROMPT()
    End Sub
    Friend Sub RESOLVE_ATTACK(AI As Integer, DI As Integer, hitSfx As Sfx, missSfx As Sfx)
        AnsiConsole.MarkupLine($"{Worlds.world.GetCreature(AI).Name} attacks {Worlds.world.GetCreature(DI).Name}")
        Dim AR = Worlds.world.GetCreature(AI).RollAttack
        Dim DR = Worlds.world.GetCreature(DI).RollDefend
        AnsiConsole.MarkupLine($"{Worlds.world.GetCreature(AI).Name} rolls {AR}")
        AnsiConsole.MarkupLine($"{Worlds.world.GetCreature(DI).Name} rolls {DR}")
        If AR > DR Then
            Dim D = AR - DR
            Worlds.world.GetCreature(DI).AddWounds(D)
            AnsiConsole.MarkupLine($"{Worlds.world.GetCreature(AI).Name} hits for {D}")
            SfxHandler.HandleSfx(hitSfx)
            If Not Worlds.world.GetCreature(DI).Alive Then
                AnsiConsole.MarkupLine($"{Worlds.world.GetCreature(AI).Name} kills {Worlds.world.GetCreature(DI).Name}")
                Worlds.world.GetCreature(DI).Remove()
                Worlds.world.GetCreature(DI).Drop()
            End If
        Else
            AnsiConsole.MarkupLine($"{Worlds.world.GetCreature(AI).Name} misses")
            SfxHandler.HandleSfx(missSfx)
        End If
    End Sub
End Module
