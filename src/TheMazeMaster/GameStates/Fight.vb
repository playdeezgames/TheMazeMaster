Friend Module Fight
    Friend FIGHT_CREATURE_INDEX As Integer = -1
    Friend Sub FIGHT_START(world As World)
        Dim DI = FIGHT_CREATURE_INDEX
        Dim AI = world.character.CreatureIndex
        AnsiConsole.MarkupLine($"FIGHTING {world.GetCreature(DI).Name}")
        FIGHT_PROMPT(world)
    End Sub
    Friend Function Update(world As World) As StateIdentifier
        Dim DI = FIGHT_CREATURE_INDEX
        Dim AI = world.character.CreatureIndex
        If world.GetCreature(AI).Alive And world.GetCreature(DI).Alive Then
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Now What?[/]"}
            prompt.AddChoices(AttackText, RunText, UseText)
            Select Case AnsiConsole.Prompt(prompt)
                Case RunText
                    Return StateIdentifier.InPlay
                Case AttackText
                    DoAttack(world)
                Case UseText
                    CombatUseItem(world)
            End Select
        ElseIf world.GetCreature(AI).Alive Then
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
    Private Sub CombatUseItem(world As World)
        Dim usableItemTypes As IEnumerable(Of ItemType) = world.character.CombatUsableItemTypes
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
                For Each text In world.character.CombatUseItemType(world, table(answer))
                    AnsiConsole.MarkupLine(text)
                Next
                OkPrompt()
        End Select
    End Sub
    Friend Sub FIGHT_PROMPT(world As World)
        Dim DI = FIGHT_CREATURE_INDEX
        Dim AI = world.character.CreatureIndex
        If world.GetCreature(AI).Alive AndAlso world.GetCreature(DI).Alive Then
            AnsiConsole.MarkupLine($"{world.GetCreature(AI).Name} has {world.GetCreature(AI).Health} HP")
            AnsiConsole.MarkupLine($"{world.GetCreature(DI).Name} has {world.GetCreature(DI).Health} HP")
        ElseIf world.GetCreature(AI).Alive Then
            world.character.AddXP(world.GetCreature(DI).XP)
        End If
    End Sub
    Friend Sub DoAttack(world As World)
        Dim DI = FIGHT_CREATURE_INDEX
        Dim AI = world.character.CreatureIndex
        RESOLVE_ATTACK(world, AI, DI, Sfx.PlayerHit, Sfx.PlayerMiss)
        If world.GetCreature(DI).Alive Then
            RESOLVE_ATTACK(world, DI, AI, Sfx.EnemyHit, Sfx.EnemyMiss)
        End If
        FIGHT_PROMPT(world)
    End Sub
    Friend Sub RESOLVE_ATTACK(world As World, AI As Integer, DI As Integer, hitSfx As Sfx, missSfx As Sfx)
        AnsiConsole.MarkupLine($"{world.GetCreature(AI).Name} attacks {world.GetCreature(DI).Name}")
        Dim AR = world.GetCreature(AI).RollAttack(world)
        Dim DR = world.GetCreature(DI).RollDefend
        AnsiConsole.MarkupLine($"{world.GetCreature(AI).Name} rolls {AR}")
        AnsiConsole.MarkupLine($"{world.GetCreature(DI).Name} rolls {DR}")
        If AR > DR Then
            Dim D = AR - DR
            world.GetCreature(DI).AddWounds(D)
            AnsiConsole.MarkupLine($"{world.GetCreature(AI).Name} hits for {D}")
            SfxHandler.HandleSfx(hitSfx)
            If Not world.GetCreature(DI).Alive Then
                AnsiConsole.MarkupLine($"{world.GetCreature(AI).Name} kills {world.GetCreature(DI).Name}")
                world.GetCreature(DI).Remove(world)
                world.GetCreature(DI).Drop(world)
            End If
        Else
            AnsiConsole.MarkupLine($"{world.GetCreature(AI).Name} misses")
            SfxHandler.HandleSfx(missSfx)
        End If
    End Sub
End Module
