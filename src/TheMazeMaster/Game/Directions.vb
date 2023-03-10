Friend Module Directions
    Friend Const DIRECTION_NORTH = 0
    Friend Const DIRECTION_EAST = 1
    Friend Const DIRECTION_SOUTH = 2
    Friend Const DIRECTION_WEST = 3
    Friend Const DIRECTION_FIRST = DIRECTION_NORTH
    Friend Const DIRECTION_LAST = DIRECTION_WEST
    Friend Const DIRECTION_COUNT = 4
    Friend Function OPPOSITE_DIRECTION(d As Integer) As Integer
        If d = DIRECTION_NORTH Then
            Return DIRECTION_SOUTH
        ElseIf d = DIRECTION_EAST Then
            Return DIRECTION_WEST
        ElseIf d = DIRECTION_SOUTH Then
            Return DIRECTION_NORTH
        ElseIf d = DIRECTION_WEST Then
            Return DIRECTION_EAST
        Else
            Throw New NotImplementedException
        End If
    End Function
    Function STEP_X(d As Integer, x As Integer, y As Integer) As Integer
        If d = DIRECTION_NORTH Then
            Return x
        ElseIf d = DIRECTION_EAST Then
            Return x + 1
        ElseIf d = DIRECTION_SOUTH Then
            Return x
        ElseIf d = DIRECTION_WEST Then
            Return x - 1
        Else
            Throw New NotImplementedException()
        End If
    End Function
    Function STEP_Y(d As Integer, x As Integer, y As Integer) As Integer
        If d = DIRECTION_NORTH Then
            Return y - 1
        ElseIf d = DIRECTION_EAST Then
            Return y
        ElseIf d = DIRECTION_SOUTH Then
            Return y + 1
        ElseIf d = DIRECTION_WEST Then
            Return y
        Else
            Throw New NotImplementedException()
        End If
    End Function
End Module
' DEF NEXT_DIRECTION(d)
'     IF d=DIRECTION_NORTH THEN
'         RETURN DIRECTION_EAST
'     ELSEIF d=DIRECTION_EAST THEN
'         RETURN DIRECTION_SOUTH
'     ELSEIF d=DIRECTION_SOUTH THEN
'         RETURN DIRECTION_WEST
'     ELSEIF d=DIRECTION_WEST THEN
'         RETURN DIRECTION_NORTH
'     ELSE
'         ASSERT(FALSE,"INVALID DIRECTION")
'     ENDIF    
' ENDDEF
' DEF PREVIOUS_DIRECTION(d)
'     IF d=DIRECTION_NORTH THEN
'         RETURN DIRECTION_WEST
'     ELSEIF d=DIRECTION_EAST THEN
'         RETURN DIRECTION_NORTH
'     ELSEIF d=DIRECTION_SOUTH THEN
'         RETURN DIRECTION_EAST
'     ELSEIF d=DIRECTION_WEST THEN
'         RETURN DIRECTION_SOUTH
'     ELSE
'         ASSERT(FALSE,"INVALID DIRECTION")
'     ENDIF    
' ENDDEF
