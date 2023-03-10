Module Program
    Sub Main(args As String())
        Console.Title = "The Maze Master"
#Disable Warning CA1416 ' Validate platform compatibility
        Console.WindowWidth = 60
        Console.BufferWidth = 60
#Enable Warning CA1416 ' Validate platform compatibility
        Chambers.Initialize()
        ChamberDoors.Initialize()
        Passageways.Initialize()
        PassagewayDoors.Initialize()
        Driver.UpdateWith(AddressOf StateMachine.Update)
    End Sub
End Module
