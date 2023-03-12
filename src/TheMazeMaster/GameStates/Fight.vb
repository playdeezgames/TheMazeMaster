Friend Module Fight
    Friend FIGHT_CREATURE_INDEX As Integer = -1
    Friend FIGHT_MESSAGES As New List(Of String)
    Friend Sub FIGHT_START()
        FIGHT_MESSAGES.Clear()
        Dim DI = FIGHT_CREATURE_INDEX
        Dim AI = PLAYER_CREATURE_INDEX
        FIGHT_MESSAGES.Add($"FIGHTING {GET_CREATURE_NAME(DI)}")
        FIGHT_PROMPT()
    End Sub
    Friend Function Update() As String
        While FIGHT_MESSAGES.Count > 16
            FIGHT_MESSAGES.RemoveAt(0)
        End While
        For R = 0 To FIGHT_MESSAGES.Count - 1
            AnsiConsole.MarkUpLine(FIGHT_MESSAGES(R))
        Next R
        Dim DI = FIGHT_CREATURE_INDEX
        Dim AI = PLAYER_CREATURE_INDEX
        If AllCreatures(AI).Alive And AllCreatures(DI).Alive Then
            Select Case Console.ReadKey(True).Key
                Case ConsoleKey.R
                    Return STATE_IN_PLAY
                Case ConsoleKey.A
                    FIGHT_ATTACK()
                Case ConsoleKey.U
                    'USE ITEM
            End Select
        ElseIf AllCreatures(AI).Alive Then
            If Console.ReadKey(True).Key = ConsoleKey.V Then
                'CHECK FOR LEVELING!
                Return STATE_IN_PLAY
            End If
        Else
            If Console.ReadKey(True).Key = ConsoleKey.D Then
                Return STATE_TITLE
            End If
        End If
        Return STATE_FIGHT
    End Function
    Friend Sub FIGHT_PROMPT()
        Dim DI = FIGHT_CREATURE_INDEX
        Dim AI = PLAYER_CREATURE_INDEX
        If AllCreatures(AI).Alive AndAlso AllCreatures(DI).Alive Then
            FIGHT_MESSAGES.Add($"{GET_CREATURE_NAME(AI)} HAS {GET_CREATURE_HEALTH(AI)} HP")
            FIGHT_MESSAGES.Add($"{GET_CREATURE_NAME(DI)} HAS {GET_CREATURE_HEALTH(DI)} HP")
            FIGHT_MESSAGES.Add("(A)TTACK/(R)UN/(U)SE")
        ElseIf AllCreatures(AI).Alive Then
            PLAYER_ADD_XP(GET_CREATURE_XP(DI))
            FIGHT_MESSAGES.Add("(V)ICTORY!")
        Else
            FIGHT_MESSAGES.Add("YER (D)EAD")
        End If
    End Sub
    Friend Sub FIGHT_ATTACK()
        FIGHT_MESSAGES.Clear()
        Dim DI = FIGHT_CREATURE_INDEX
        Dim AI = PLAYER_CREATURE_INDEX
        RESOLVE_ATTACK(AI, DI)
        If AllCreatures(DI).Alive Then
            RESOLVE_ATTACK(DI, AI)
        End If
        FIGHT_PROMPT()
    End Sub
    Friend Sub RESOLVE_ATTACK(AI As Integer, DI As Integer)
        FIGHT_MESSAGES.Add($"{GET_CREATURE_NAME(AI)} ATTACKS {GET_CREATURE_NAME(DI)}")
        Dim AR = CREATURE_ROLL_ATTACK(AI)
        Dim DR = CREATURE_ROLL_DEFEND(DI)
        FIGHT_MESSAGES.Add($"{GET_CREATURE_NAME(AI)} ROLLS {AR}")
        FIGHT_MESSAGES.Add($"{GET_CREATURE_NAME(DI)} ROLLS {DR}")
        If AR > DR Then
            Dim D = AR - DR
            WOUND_CREATURE(DI, D)
            FIGHT_MESSAGES.Add($"{GET_CREATURE_NAME(AI)} HITS FOR {D}")
            If Not AllCreatures(DI).Alive Then
                FIGHT_MESSAGES.Add($"{GET_CREATURE_NAME(AI)} KILLS {GET_CREATURE_NAME(DI)}")
                REMOVE_CREATURE(DI)
                CREATURE_DROP_ITEM(DI)
            End If
        Else
                FIGHT_MESSAGES.Add($"{GET_CREATURE_NAME(AI)} MISSES")
        End If
    End Sub
End Module
