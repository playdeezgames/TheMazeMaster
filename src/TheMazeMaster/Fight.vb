﻿Friend Module Fight
    Friend Function Update() As String
        Throw New NotImplementedException()
    End Function
End Module
'FIGHT_CREATURE_INDEX = -1
' FIGHT_MESSAGES=LIST()
' DEF FIGHT_PROMPT()
'     DI=FIGHT_CREATURE_INDEX
'     AI=PLAYER_CREATURE_INDEX
'     IF CREATURE_ALIVE(AI) AND CREATURE_ALIVE(DI) THEN
'         PUSH(FIGHT_MESSAGES,GET_CREATURE_NAME(AI)+" HAS "+STR(GET_CREATURE_HEALTH(AI))+" HP")
'         PUSH(FIGHT_MESSAGES,GET_CREATURE_NAME(DI)+" HAS "+STR(GET_CREATURE_HEALTH(DI))+" HP")
'         PUSH(FIGHT_MESSAGES,"(A)TTACK/(R)UN/(U)SE")
'     ELSEIF CREATURE_ALIVE(AI) THEN
'         PLAYER_ADD_XP(GET_CREATURE_XP(DI))
'         PUSH(FIGHT_MESSAGES,"(V)ICTORY!")
'     ELSE
'         PUSH(FIGHT_MESSAGES,"YER (D)EAD")
'     ENDIF
' ENDDEF
' DEF FIGHT_START()
'     CLEAR(FIGHT_MESSAGES)
'     DI=FIGHT_CREATURE_INDEX
'     AI=PLAYER_CREATURE_INDEX
'     PUSH(FIGHT_MESSAGES,"FIGHTING "+GET_CREATURE_NAME(DI))
'     FIGHT_PROMPT()
' ENDDEF
' DEF RESOLVE_ATTACK(AI,DI)
'     PUSH(FIGHT_MESSAGES,GET_CREATURE_NAME(AI)+" ATTACKS "+GET_CREATURE_NAME(DI))
'     AR = CREATURE_ROLL_ATTACK(AI)
'     DR = CREATURE_ROLL_DEFEND(DI)
'     PUSH(FIGHT_MESSAGES,GET_CREATURE_NAME(AI)+" ROLLS "+STR(AR))
'     PUSH(FIGHT_MESSAGES,GET_CREATURE_NAME(DI)+" ROLLS "+STR(DR))
'     IF AR>DR THEN
'         D=AR-DR
'         WOUND_CREATURE(DI,D)
'         PUSH(FIGHT_MESSAGES,GET_CREATURE_NAME(AI)+" HITS FOR "+STR(D))
'         IF NOT CREATURE_ALIVE(DI) THEN
'             PUSH(FIGHT_MESSAGES,GET_CREATURE_NAME(AI)+" KILLS "+GET_CREATURE_NAME(DI))
'             REMOVE_CREATURE(DI)
'             CREATURE_DROP_ITEM(DI)
'         ENDIF
'     ELSE
'         PUSH(FIGHT_MESSAGES,GET_CREATURE_NAME(AI)+" MISSES")
'     ENDIF
' ENDDEF
' DEF FIGHT_ATTACK()
'     DI=FIGHT_CREATURE_INDEX
'     AI=PLAYER_CREATURE_INDEX
'     RESOLVE_ATTACK(AI,DI)
'     IF CREATURE_ALIVE(DI) THEN
'         RESOLVE_ATTACK(DI,AI)
'     ENDIF
'     FIGHT_PROMPT()
' ENDDEF
' DEF FIGHT_UPDATE(DELTA)
'     WHILE LEN(FIGHT_MESSAGES)>16
'         REMOVE(FIGHT_MESSAGES,0)
'     WEND
'     FOR R=0 TO LEN(FIGHT_MESSAGES)-1
'         TEXT 0,R*8,FIGHT_MESSAGES(R)
'     NEXT R
'     DI=FIGHT_CREATURE_INDEX
'     AI=PLAYER_CREATURE_INDEX
'     IF CREATURE_ALIVE(AI) AND CREATURE_ALIVE(DI) THEN
'         IF KEYP CODE_R THEN
'             RETURN STATE_IN_PLAY
'         ELSEIF KEYP CODE_A THEN
'             FIGHT_ATTACK()
'         ELSEIF KEYP CODE_U THEN
'             'USE ITEM
'         ENDIF
'     ELSEIF CREATURE_ALIVE(AI) THEN
'         IF KEYP CODE_V THEN
'             'CHECK FOR LEVELING!
'             RETURN STATE_IN_PLAY
'         ENDIF
'     ELSE
'         IF KEYP CODE_D THEN
'             RETURN STATE_TITLE
'         ENDIF
'     ENDIF
'     RETURN STATE_FIGHT
' ENDDEF