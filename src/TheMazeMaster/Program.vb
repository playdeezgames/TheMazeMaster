Module Program
    Sub Main(args As String())
        Chambers.Initialize()
        ChamberDoors.Initialize()
        Passageways.Initialize()
        PassagewayDoors.Initialize()
        Driver.UpdateWith(AddressOf StateMachine.Update)
    End Sub
End Module
