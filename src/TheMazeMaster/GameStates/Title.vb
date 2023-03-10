Friend Module Title
    Friend Function Update() As String
        AnsiConsole.Clear()
        AnsiConsole.MarkUpLine("The Maze Master")
        AnsiConsole.MarkUpLine("By TheGrumpyGameDev")
        AnsiConsole.MarkUpLine("For Jam for All BASIC Dialects (#4)")
        AnsiConsole.MarkUpLine("(1) Start")
        Select Case Console.ReadKey(True).Key
            Case ConsoleKey.D1
                Game.Start()
                Return StateMachine.STATE_IN_PLAY
            Case Else
                Return STATE_TITLE
        End Select
    End Function
End Module
