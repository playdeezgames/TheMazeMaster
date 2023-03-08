Friend Module Driver
    Friend Sub UpdateWith(updater As Action)
        Do
            updater()
        Loop
    End Sub
End Module
