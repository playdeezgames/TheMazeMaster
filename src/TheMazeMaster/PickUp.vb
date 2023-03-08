Friend Module PickUp
    Friend Function Update() As String
        Throw New NotImplementedException()
    End Function
End Module
'PICKUP_ITEM_INDEX = -1
' DEF PICKUP_UPDATE(DELTA)
'     II=PICKUP_ITEM_INDEX
'     IT=ITEM_TYPES(II)
'     TEXT 0,0,"PICK UP "+ITEMTYPE_NAMES(IT)+"?"
'     TEXT 0,8,"(Y)ES/(N)O"
'     IF KEYP CODE_N THEN
'         RETURN STATE_IN_PLAY
'     ENDIF
'     IF KEYP CODE_Y THEN
'         REMOVE_ITEM(II)
'         PLAYER_TAKE_ITEM(II)
'         RETURN STATE_IN_PLAY
'     ENDIF
'     RETURN STATE_PICKUP
' ENDDEF