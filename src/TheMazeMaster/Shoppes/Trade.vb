Friend Class Trade
    ReadOnly Property Input As (ItemTypeIdentifier, Integer)
    ReadOnly Property Output As (ItemTypeIdentifier, Integer)
    Sub New(input As (ItemTypeIdentifier, Integer), output As (ItemTypeIdentifier, Integer))
        Me.Input = input
        Me.Output = output
    End Sub
    Public Overrides Function ToString() As String
        Return $"{AllItemTypes(Input.Item1).Name}x{Input.Item2} -> {AllItemTypes(Output.Item1).Name}x{Output.Item2}"
    End Function
End Class
