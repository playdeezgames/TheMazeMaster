﻿Friend Module ItemTypes
    Friend Const ITEMTYPE_NONE = ""
    Friend Const ITEMTYPE_FIST = "FIST"
    Friend Const ITEMTYPE_BITE = "BITE"
    Friend Const ITEMTYPE_RATTAIL = "RATTAIL"
End Module
' ALL_ITEMTYPES=LIST(ITEMTYPE_FIST,ITEMTYPE_BITE,ITEMTYPE_RATTAIL)
' ITEMTYPE_TILE_INDICES=DICT(ITEMTYPE_FIST,TILE_EMPTY,ITEMTYPE_BITE,TILE_EMPTY,ITEMTYPE_RATTAIL,TILE_RATTAIL)
' ITEMTYPE_ATTACK_VALUE=DICT(ITEMTYPE_FIST,1,ITEMTYPE_BITE,1)
' ITEMTYPE_ATTACK_MAXIMUM=DICT(ITEMTYPE_FIST,1,ITEMTYPE_BITE,1)
' ITEMTYPE_NAMES=DICT(ITEMTYPE_FIST,"FIST",ITEMTYPE_BITE,"BITE",ITEMTYPE_RATTAIL,"RAT TAIL")
' ITEMTYPE_STACKS=LIST(ITEMTYPE_RATTAIL)
' DEF ITEMTYPE_ROLL_ATTACK(IT)
'     D=0
'     IF EXISTS(ITEMTYPE_ATTACK_VALUE,IT) THEN
'         D=ITEMTYPE_ATTACK_VALUE(IT)
'     ENDIF
'     M=0
'     IF EXISTS(ITEMTYPE_ATTACK_MAXIMUM,IT) THEN
'         M=ITEMTYPE_ATTACK_MAXIMUM(IT)
'     ENDIF
'     RETURN ROLL_DICE(D,M)
' ENDDEF