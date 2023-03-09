Friend Module Title
    Friend Function Update() As String
        Console.Clear()
        Console.WriteLine("The Maze Master")
        Console.WriteLine("By TheGrumpyGameDev")
        Console.WriteLine("For Jam for All BASIC Dialects (#4)")
        Console.WriteLine("(1) Start")
        Select Case Console.ReadKey(True).Key
            Case ConsoleKey.D1
                Game.Start()
                Return StateMachine.STATE_IN_PLAY
            Case Else
                Return STATE_TITLE
        End Select
    End Function
End Module
