Module modFcuSelect

    Public Enum MenuCode
        MENU_CODE_START = 1

        TERMINAL_INPUT
        GROUP_REPOSE
        RUN_HOUR
        CYLINDER_DEV
        CONTROL_USE
        SIO
        DATA_STRAGE_TABLE
        DATA_FORWARD
        EXT_LAN
        CONTROL_SEQ
        CHANNEL_LIST_PRINT
        TERMINAL_PRINT

        MENU_CODE_END
    End Enum


    Public nMenuCode As Integer
    Public nFcuNo As Integer

    Public Function EditMenuCodeSet(code As Integer) As Boolean

        If code <= MenuCode.MENU_CODE_START Or code >= MenuCode.MENU_CODE_END Then
            Return False
        End If

        nMenuCode = code

        Return True

    End Function

    Public Sub EditMenuCodeClear()
        nMenuCode = 0
    End Sub

    Public Function SetFcuNumber(no As Integer) As Boolean
        If no = 1 Or no = 2 Then
            nFcuNo = no
            Return True
        End If

        Return False
    End Function

End Module
