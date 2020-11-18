Public Class frmChkFileCompSelectDetail

#Region "変数"
    Private pstrGridName_1() As String
    Private pstrGridName_2() As String
    Private pstrGridName_3() As String
    Private pstrGridName_4() As String
    Private pstrGridName_5() As String
    Private pstrGridName_6() As String
    Private pstrGridName_7() As String
    Private pstrGridName_8() As String
    Private pstrGridName_9() As String
    Private pstrGridName_10() As String
    Private pstrGridName_11() As String
    Private pstrGridName_12() As String
#End Region

#Region "画面"
    Private Sub frmChkFileCompSelectDetail_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Call subSetGridName()
        Call subInitGrid()
        Call subGridDisp()
    End Sub

    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click
        Dim i As Integer = 0
        'Save
        'チェックを格納 1～11
        For i = 0 To grdChk1.RowCount - 1
            With grdChk1.Rows(i)
                gCompareChk_1_Common(i) = .Cells(0).Value()
            End With
        Next i
        For i = 0 To grdChk2.RowCount - 1
            With grdChk2.Rows(i)
                gCompareChk_2_Analog(i) = .Cells(0).Value()
            End With
        Next i
        For i = 0 To grdChk3.RowCount - 1
            With grdChk3.Rows(i)
                gCompareChk_3_Digital(i) = .Cells(0).Value()
            End With
        Next i
        For i = 0 To grdChk4.RowCount - 1
            With grdChk4.Rows(i)
                gCompareChk_4_System(i) = .Cells(0).Value()
            End With
        Next i
        For i = 0 To grdChk5.RowCount - 1
            With grdChk5.Rows(i)
                gCompareChk_5_Motor(i) = .Cells(0).Value()
            End With
        Next i
        For i = 0 To grdChk6.RowCount - 1
            With grdChk6.Rows(i)
                gCompareChk_6_DIDO(i) = .Cells(0).Value()
            End With
        Next i
        For i = 0 To grdChk7.RowCount - 1
            With grdChk7.Rows(i)
                gCompareChk_7_AIDO(i) = .Cells(0).Value()
            End With
        Next i
        For i = 0 To grdChk8.RowCount - 1
            With grdChk8.Rows(i)
                gCompareChk_8_AIAO(i) = .Cells(0).Value()
            End With
        Next i
        For i = 0 To grdChk9.RowCount - 1
            With grdChk9.Rows(i)
                gCompareChk_9_Comp(i) = .Cells(0).Value()
            End With
        Next i
        For i = 0 To grdChk10.RowCount - 1
            With grdChk10.Rows(i)
                gCompareChk_10_Puls(i) = .Cells(0).Value()
            End With
        Next i
        For i = 0 To grdChk11.RowCount - 1
            With grdChk11.Rows(i)
                gCompareChk_11_RunHour(i) = .Cells(0).Value()
            End With
        Next i
        For i = 0 To grdChk12.RowCount - 1
            With grdChk12.Rows(i)
                gCompareChk_12_Pid(i) = .Cells(0).Value()
            End With
        Next i


        Me.Close()
    End Sub

    Private Sub btnEXIT_Click(sender As System.Object, e As System.EventArgs) Handles btnEXIT.Click
        'Exit
        Me.Close()
    End Sub

#End Region

#Region "関数"
    'グリッド名称設定
    Private Sub subSetGridName()
        ReDim pstrGridName_1(UBound(gCompareChk_1_Common))
        ReDim pstrGridName_2(UBound(gCompareChk_2_Analog))
        ReDim pstrGridName_3(UBound(gCompareChk_3_Digital))
        ReDim pstrGridName_4(UBound(gCompareChk_4_System))
        ReDim pstrGridName_5(UBound(gCompareChk_5_Motor))
        ReDim pstrGridName_6(UBound(gCompareChk_6_DIDO))
        ReDim pstrGridName_7(UBound(gCompareChk_7_AIDO))
        ReDim pstrGridName_8(UBound(gCompareChk_8_AIAO))
        ReDim pstrGridName_9(UBound(gCompareChk_9_Comp))
        ReDim pstrGridName_10(UBound(gCompareChk_10_Puls))
        ReDim pstrGridName_11(UBound(gCompareChk_11_RunHour))
        ReDim pstrGridName_12(UBound(gCompareChk_12_Pid))


        pstrGridName_1(0) = "Group.No."
        pstrGridName_1(1) = "Disp.Pos"
        pstrGridName_1(2) = "SysNo."
        pstrGridName_1(3) = "ChNo."
        pstrGridName_1(4) = "CH Name"
        pstrGridName_1(5) = "Remarks"
        pstrGridName_1(6) = "EX"
        pstrGridName_1(7) = "DLY"
        pstrGridName_1(8) = "GR1"
        pstrGridName_1(9) = "GR2"
        pstrGridName_1(10) = "MR Status"
        pstrGridName_1(11) = "CH Type"
        pstrGridName_1(12) = "Data Type"
        pstrGridName_1(13) = "UNIT"
        pstrGridName_1(14) = "Dummy"
        pstrGridName_1(15) = "SC"
        pstrGridName_1(16) = "WK"
        pstrGridName_1(17) = "DLY UNIT"
        pstrGridName_1(18) = "PLC CH"
        pstrGridName_1(19) = "PWR FACTOR"
        pstrGridName_1(20) = "mmHg"
        pstrGridName_1(21) = "cmHg"
        pstrGridName_1(22) = "P/S Display"
        pstrGridName_1(23) = "RL"
        pstrGridName_1(24) = "AL"
        pstrGridName_1(25) = "EP"
        pstrGridName_1(26) = "AC"
        pstrGridName_1(27) = "LOCK"     '' Ver2.0.8.7 2018.08.10
        pstrGridName_1(28) = "MOTOR COLOR"  '' ver2.0.8.C 2018.11.14
        pstrGridName_1(29) = "STATUS"
        pstrGridName_1(30) = "FU Adress"
        pstrGridName_1(31) = "FU Count"
        pstrGridName_1(32) = "ECC Func"
        pstrGridName_1(33) = "SIO"
        pstrGridName_1(34) = "GWS"
        pstrGridName_1(35) = "UNIT Name"
        pstrGridName_1(36) = "Share CH Type"
        pstrGridName_1(37) = "Share CH No"
        pstrGridName_1(38) = "MR Set"
        pstrGridName_1(39) = "CHID"

        pstrGridName_2(0) = "Alarm HH Use"
        pstrGridName_2(1) = "Alarm HH DLY"
        pstrGridName_2(2) = "Alarm HH SET"
        pstrGridName_2(3) = "Alarm HH EX"
        pstrGridName_2(4) = "Alarm HH GR1"
        pstrGridName_2(5) = "Alarm HH GR2"
        pstrGridName_2(6) = "Alarm HH STATUS"
        pstrGridName_2(7) = "Alarm HH MR Status"
        pstrGridName_2(8) = "Alarm HH MR Set"
        pstrGridName_2(9) = "Alarm H Use"
        pstrGridName_2(10) = "Alarm H DLY"
        pstrGridName_2(11) = "Alarm H SET"
        pstrGridName_2(12) = "Alarm H EX"
        pstrGridName_2(13) = "Alarm H GR1"
        pstrGridName_2(14) = "Alarm H GR2"
        pstrGridName_2(15) = "Alarm H Status"
        pstrGridName_2(16) = "Alarm H MR Status"
        pstrGridName_2(17) = "Alarm H MR Set"
        pstrGridName_2(18) = "Alarm L Use"
        pstrGridName_2(19) = "Alarm L DLY"
        pstrGridName_2(20) = "Alarm L SET"
        pstrGridName_2(21) = "Alarm L EX"
        pstrGridName_2(22) = "Alarm L GR1"
        pstrGridName_2(23) = "Alarm L GR2"
        pstrGridName_2(24) = "Alarm L STATUS"
        pstrGridName_2(25) = "Alarm L MR Status"
        pstrGridName_2(26) = "Alarm L MR Set"
        pstrGridName_2(27) = "Alarm LL Use"
        pstrGridName_2(28) = "Alarm LL DLY"
        pstrGridName_2(29) = "Alarm LL SET"
        pstrGridName_2(30) = "Alarm LL EX"
        pstrGridName_2(31) = "Alarm LL GR1"
        pstrGridName_2(32) = "Alarm LL GR2"
        pstrGridName_2(33) = "Alarm LL STATUS"
        pstrGridName_2(34) = "Alarm LL MR Status"
        pstrGridName_2(35) = "Alarm LL MR Set"
        pstrGridName_2(36) = "Alarm S Use"
        pstrGridName_2(37) = "Alarm S DLY"
        pstrGridName_2(38) = "Alarm S SET"
        pstrGridName_2(39) = "Alarm S EX"
        pstrGridName_2(40) = "Alarm S GR1"
        pstrGridName_2(41) = "Alarm S GR2"
        pstrGridName_2(42) = "Alarm S STATUS"
        pstrGridName_2(43) = "Alarm S MR Status"
        pstrGridName_2(44) = "Alarm S MR Set"
        pstrGridName_2(45) = "RANGE"
        pstrGridName_2(46) = "NOR RANGE"
        pstrGridName_2(47) = "OFFSET"
        pstrGridName_2(48) = "String"
        pstrGridName_2(49) = "Decimal Point"
        pstrGridName_2(50) = "BAR GRAPH CENTER"
        pstrGridName_2(51) = "SENSOR ALM(UNDER)"
        pstrGridName_2(52) = "SENSOR ALM(OVER)"
        pstrGridName_2(53) = "PT Type"
        pstrGridName_2(54) = "TagNo."
        pstrGridName_2(55) = "LR Mode"
        pstrGridName_2(56) = "Dummy Setting"
        pstrGridName_2(57) = "Alarm MimicNo"


        pstrGridName_3(0) = "ALM Use"
        pstrGridName_3(1) = "DI Filter"
        pstrGridName_3(2) = "TagNo."
        pstrGridName_3(3) = "Dummy Setting"
        pstrGridName_3(4) = "Alarm MimicNo"


        pstrGridName_4(0) = "ALM Use"
        pstrGridName_4(1) = "SYS1 Use"
        pstrGridName_4(2) = "SYS1 Code"
        pstrGridName_4(3) = "SYS1 Status"
        pstrGridName_4(4) = "SYS2 Use"
        pstrGridName_4(5) = "SYS2 Code"
        pstrGridName_4(6) = "SYS2 Status"
        pstrGridName_4(7) = "SYS3 Use"
        pstrGridName_4(8) = "SYS3 Code"
        pstrGridName_4(9) = "SYS3 Status"
        pstrGridName_4(10) = "SYS4 Use"
        pstrGridName_4(11) = "SYS4 Code"
        pstrGridName_4(12) = "SYS4 Status"
        pstrGridName_4(13) = "SYS5 Use"
        pstrGridName_4(14) = "SYS5 Code"
        pstrGridName_4(15) = "SYS5 Status"
        pstrGridName_4(16) = "SYS6 Use"
        pstrGridName_4(17) = "SYS6 Code"
        pstrGridName_4(18) = "SYS6 Status"
        pstrGridName_4(19) = "SYS7 Use"
        pstrGridName_4(20) = "SYS7 Code"
        pstrGridName_4(21) = "SYS7 Status"
        pstrGridName_4(22) = "SYS8 Use"
        pstrGridName_4(23) = "SYS8 Code"
        pstrGridName_4(24) = "SYS8 Status"
        pstrGridName_4(25) = "SYS9 Use"
        pstrGridName_4(26) = "SYS9 Code"
        pstrGridName_4(27) = "SYS9 Status"
        pstrGridName_4(28) = "SYS10 Use"
        pstrGridName_4(29) = "SYS10 Code"
        pstrGridName_4(30) = "SYS10 Status"
        pstrGridName_4(31) = "SYS11 Use"
        pstrGridName_4(32) = "SYS11 Code"
        pstrGridName_4(33) = "SYS11 Status"
        pstrGridName_4(34) = "SYS12 Use"
        pstrGridName_4(35) = "SYS12 Code"
        pstrGridName_4(36) = "SYS12 Status"
        pstrGridName_4(37) = "SYS13 Use"
        pstrGridName_4(38) = "SYS13 Code"
        pstrGridName_4(39) = "SYS13 Status"
        pstrGridName_4(40) = "SYS14 Use"
        pstrGridName_4(41) = "SYS14 Code"
        pstrGridName_4(42) = "SYS14 Status"
        pstrGridName_4(43) = "SYS15 Use"
        pstrGridName_4(44) = "SYS15 Code"
        pstrGridName_4(45) = "SYS15 Status"
        pstrGridName_4(46) = "SYS16 Use"
        pstrGridName_4(47) = "SYS16 Code"
        pstrGridName_4(48) = "SYS16 Status"
        pstrGridName_4(49) = "TagNo."
        pstrGridName_4(50) = "Alarm MimicNo"

        pstrGridName_5(0) = "ALM Use"
        pstrGridName_5(1) = "Filter"
        pstrGridName_5(2) = "FB Timer"
        pstrGridName_5(3) = "OUT FU No"
        pstrGridName_5(4) = "OUT FU Slot"
        pstrGridName_5(5) = "OUT FU Pin"
        pstrGridName_5(6) = "Output Count"
        pstrGridName_5(7) = "Output Type"
        pstrGridName_5(8) = "Output width"
        pstrGridName_5(9) = "Output Status"
        pstrGridName_5(10) = "STATUS1"
        pstrGridName_5(11) = "STATUS2"
        pstrGridName_5(12) = "STATUS3"
        pstrGridName_5(13) = "STATUS4"
        pstrGridName_5(14) = "STATUS5"
        pstrGridName_5(15) = "STATUS6"
        pstrGridName_5(16) = "STATUS7"
        pstrGridName_5(17) = "STATUS8"
        pstrGridName_5(18) = "FB Use"
        pstrGridName_5(19) = "FB DLY"
        pstrGridName_5(20) = "FB SP1"
        pstrGridName_5(21) = "FB SP2"
        pstrGridName_5(22) = "FB hys1"
        pstrGridName_5(23) = "FB hys2"
        pstrGridName_5(24) = "FB Sampling"
        pstrGridName_5(25) = "FB Variation"
        pstrGridName_5(26) = "FB EX"
        pstrGridName_5(27) = "FB GR1"
        pstrGridName_5(28) = "FB GR2"
        pstrGridName_5(29) = "FB STATUS"
        pstrGridName_5(30) = "FB MR Status"
        pstrGridName_5(31) = "FB MR Set"
        pstrGridName_5(32) = "TagNo."
        pstrGridName_5(33) = "Dummy Setting"
        pstrGridName_5(34) = "Alarm MimicNo"


        pstrGridName_6(0) = "ALM Use"
        pstrGridName_6(1) = "Tbl Index"
        pstrGridName_6(2) = "FB Timer"
        pstrGridName_6(3) = "OUT FU No"
        pstrGridName_6(4) = "OUT FU Slot"
        pstrGridName_6(5) = "OUT FU Pin"
        pstrGridName_6(6) = "Output Count"
        pstrGridName_6(7) = "Output Type"
        pstrGridName_6(8) = "Output width"
        pstrGridName_6(9) = "Output Status"
        pstrGridName_6(10) = "STATUS1"
        pstrGridName_6(11) = "STATUS2"
        pstrGridName_6(12) = "STATUS3"
        pstrGridName_6(13) = "STATUS4"
        pstrGridName_6(14) = "STATUS5"
        pstrGridName_6(15) = "STATUS6"
        pstrGridName_6(16) = "STATUS7"
        pstrGridName_6(17) = "STATUS8"
        pstrGridName_6(18) = "FB Use"
        pstrGridName_6(19) = "FB DLY"
        pstrGridName_6(20) = "FB SP1"
        pstrGridName_6(21) = "FB SP2"
        pstrGridName_6(22) = "FB hys1"
        pstrGridName_6(23) = "FB hys2"
        pstrGridName_6(24) = "FB Sampling"
        pstrGridName_6(25) = "FB Variation"
        pstrGridName_6(26) = "FB EX"
        pstrGridName_6(27) = "FB GR1"
        pstrGridName_6(28) = "FB GR2"
        pstrGridName_6(29) = "FB STATUS"
        pstrGridName_6(30) = "FB MR Status"
        pstrGridName_6(31) = "FB MR Set"
        pstrGridName_6(32) = "TagNo."
        pstrGridName_6(33) = "Dummy Setting"
        pstrGridName_6(34) = "Alarm MimicNo"

        pstrGridName_7(0) = "ALM HH Use"
        pstrGridName_7(1) = "ALM HH DLY"
        pstrGridName_7(2) = "ALM HH SET"
        pstrGridName_7(3) = "ALM HH EX"
        pstrGridName_7(4) = "ALM HH GR1"
        pstrGridName_7(5) = "ALM HH GR2"
        pstrGridName_7(6) = "ALM HH STATUS"
        pstrGridName_7(7) = "ALM HH MR Status"
        pstrGridName_7(8) = "ALM HH MR Set"
        pstrGridName_7(9) = "ALM H Use"
        pstrGridName_7(10) = "ALM H DLY"
        pstrGridName_7(11) = "ALM H SET"
        pstrGridName_7(12) = "ALM H EX"
        pstrGridName_7(13) = "ALM H GR1"
        pstrGridName_7(14) = "ALM H GR2"
        pstrGridName_7(15) = "ALM H STATUS"
        pstrGridName_7(16) = "ALM H MR Status"
        pstrGridName_7(17) = "ALM H MR Set"
        pstrGridName_7(18) = "ALM L Use"
        pstrGridName_7(19) = "ALM L DLY"
        pstrGridName_7(20) = "ALM L SET"
        pstrGridName_7(21) = "ALM L EX"
        pstrGridName_7(22) = "ALM L GR1"
        pstrGridName_7(23) = "ALM L GR2"
        pstrGridName_7(24) = "ALM L STATUS"
        pstrGridName_7(25) = "ALM L MR Status"
        pstrGridName_7(26) = "ALM L MR Set"
        pstrGridName_7(27) = "ALM LL Use"
        pstrGridName_7(28) = "ALM LL DLY"
        pstrGridName_7(29) = "ALM LL Set"
        pstrGridName_7(30) = "ALM LL EX"
        pstrGridName_7(31) = "ALM LL GR1"
        pstrGridName_7(32) = "ALM LL GR2"
        pstrGridName_7(33) = "ALM LL STATUS"
        pstrGridName_7(34) = "ALM LL MR Status"
        pstrGridName_7(35) = "ALM LL MR Set"
        pstrGridName_7(36) = "ALM S Use"
        pstrGridName_7(37) = "ALM S DLY"
        pstrGridName_7(38) = "ALM S SET"
        pstrGridName_7(39) = "ALM S EX"
        pstrGridName_7(40) = "ALM S GR1"
        pstrGridName_7(41) = "ALM S GR2"
        pstrGridName_7(42) = "ALM S STATUS"
        pstrGridName_7(43) = "ALM S MR Status"
        pstrGridName_7(44) = "ALM S MR Set"
        pstrGridName_7(45) = "RANGE"
        pstrGridName_7(46) = "NOR RANGE"
        pstrGridName_7(47) = "OFFSET"
        pstrGridName_7(48) = "String"
        pstrGridName_7(49) = "Decimal Point"
        pstrGridName_7(50) = "BAR GRAPH CENTER"
        pstrGridName_7(51) = "SENSOR ALM(UNDER OVER)"
        pstrGridName_7(52) = "FB Timer"
        pstrGridName_7(53) = "OUT FU No"
        pstrGridName_7(54) = "OUT FU Slot"
        pstrGridName_7(55) = "OUT FU Pin"
        pstrGridName_7(56) = "Output Count"
        pstrGridName_7(57) = "Output Type"
        pstrGridName_7(58) = "Output Width"
        pstrGridName_7(59) = "Output Status"
        pstrGridName_7(60) = "STATUS1"
        pstrGridName_7(61) = "STATUS2"
        pstrGridName_7(62) = "STATUS3"
        pstrGridName_7(63) = "STATUS4"
        pstrGridName_7(64) = "STATUS5"
        pstrGridName_7(65) = "STATUS6"
        pstrGridName_7(66) = "STATUS7"
        pstrGridName_7(67) = "STATUS8"
        pstrGridName_7(68) = "FB USE"
        pstrGridName_7(69) = "FB DLY"
        pstrGridName_7(70) = "FB SP1"
        pstrGridName_7(71) = "FB SP2"
        pstrGridName_7(72) = "FB hys1"
        pstrGridName_7(73) = "FB hys2"
        pstrGridName_7(74) = "FB Sampling"
        pstrGridName_7(75) = "FB Variation"
        pstrGridName_7(76) = "FB EX"
        pstrGridName_7(77) = "FB GR1"
        pstrGridName_7(78) = "FB GR2"
        pstrGridName_7(79) = "FB STATUS"
        pstrGridName_7(80) = "FB MR Status"
        pstrGridName_7(81) = "FB MR Set"
        pstrGridName_7(82) = "TagNo."
        pstrGridName_7(83) = "Dummy Setting"
        pstrGridName_7(84) = "Alarm MimicNo"

        pstrGridName_8(0) = "ALM HH Use"
        pstrGridName_8(1) = "ALM HH DLY"
        pstrGridName_8(2) = "ALM HH SET"
        pstrGridName_8(3) = "ALM HH EX"
        pstrGridName_8(4) = "ALM HH GR1"
        pstrGridName_8(5) = "ALM HH GR2"
        pstrGridName_8(6) = "ALM HH STATUS"
        pstrGridName_8(7) = "ALM HH MR Status"
        pstrGridName_8(8) = "ALM HH MR Set"
        pstrGridName_8(9) = "ALM H Use"
        pstrGridName_8(10) = "ALM H DLY"
        pstrGridName_8(11) = "ALM H SET"
        pstrGridName_8(12) = "ALM H EX"
        pstrGridName_8(13) = "ALM H GR1"
        pstrGridName_8(14) = "ALM H GR2"
        pstrGridName_8(15) = "ALM H STATUS"
        pstrGridName_8(16) = "ALM H MR Status"
        pstrGridName_8(17) = "ALM H MR Set"
        pstrGridName_8(18) = "ALM L Use"
        pstrGridName_8(19) = "ALM L DLY"
        pstrGridName_8(20) = "ALM L SET"
        pstrGridName_8(21) = "ALM L EX"
        pstrGridName_8(22) = "ALM L GR1"
        pstrGridName_8(23) = "ALM L GR2"
        pstrGridName_8(24) = "ALM L STATUS"
        pstrGridName_8(25) = "ALM L MR Status"
        pstrGridName_8(26) = "ALM L MR Set"
        pstrGridName_8(27) = "ALM LL Use"
        pstrGridName_8(28) = "ALM LL DLY"
        pstrGridName_8(29) = "ALM LL SET"
        pstrGridName_8(30) = "ALM LL EX"
        pstrGridName_8(31) = "ALM LL GR1"
        pstrGridName_8(32) = "ALM LL GR2"
        pstrGridName_8(33) = "ALM LL STATUS"
        pstrGridName_8(34) = "ALM LL MR Status"
        pstrGridName_8(35) = "ALM LL MR Set"
        pstrGridName_8(36) = "ALM S Use"
        pstrGridName_8(37) = "ALM S DLY"
        pstrGridName_8(38) = "ALM S SET"
        pstrGridName_8(39) = "ALM S EX"
        pstrGridName_8(40) = "ALM S GR1"
        pstrGridName_8(41) = "ALM SF GR2"
        pstrGridName_8(42) = "ALM S STATUS"
        pstrGridName_8(43) = "ALM SF MR Status"
        pstrGridName_8(44) = "ALM SF MR Set"
        pstrGridName_8(45) = "RANGE"
        pstrGridName_8(46) = "NOR RANGE"
        pstrGridName_8(47) = "OFFSET"
        pstrGridName_8(48) = "String"
        pstrGridName_8(49) = "Decimal Point"
        pstrGridName_8(50) = "BAR GRAPH CENTER"
        pstrGridName_8(51) = "SENSOR ALM(UNDER OVER)"
        pstrGridName_8(52) = "FB Timer"
        pstrGridName_8(53) = "OUT FU No"
        pstrGridName_8(54) = "OUT FU Slot"
        pstrGridName_8(55) = "OUT FU Pin"
        pstrGridName_8(56) = "Output Count"
        pstrGridName_8(57) = "Output Status"
        pstrGridName_8(58) = "STATUS1"
        pstrGridName_8(59) = "FB Use"
        pstrGridName_8(60) = "FB DLY"
        pstrGridName_8(61) = "FB SP1"
        pstrGridName_8(62) = "FB SP2"
        pstrGridName_8(63) = "FB hys1"
        pstrGridName_8(64) = "FB hys2"
        pstrGridName_8(65) = "FB Sampling"
        pstrGridName_8(66) = "FB Variation"
        pstrGridName_8(67) = "FB EX"
        pstrGridName_8(68) = "FB GR1"
        pstrGridName_8(69) = "FB GR2"
        pstrGridName_8(70) = "FB STATUS"
        pstrGridName_8(71) = "FB MR Status"
        pstrGridName_8(72) = "FB MR Set"
        pstrGridName_8(73) = "TagNo."
        pstrGridName_8(74) = "Dummy Setting"
        pstrGridName_8(75) = "Alarm MimicNo"

        pstrGridName_9(0) = "Tbl Index"
        pstrGridName_9(1) = "TagNo."
        pstrGridName_9(2) = "Dummy Setting"
        pstrGridName_9(3) = "Alarm MimicNo"

        pstrGridName_10(0) = "ALM Use"
        pstrGridName_10(1) = "Filter"
        pstrGridName_10(2) = "ALM SET"
        pstrGridName_10(3) = "String"
        pstrGridName_10(4) = "Decimal Point"
        pstrGridName_10(5) = "TagNo."
        pstrGridName_10(6) = "Dummy Setting"
        pstrGridName_10(7) = "Alarm MimicNo"

        pstrGridName_11(0) = "ALM Use"
        pstrGridName_11(1) = "Filter"
        pstrGridName_11(2) = "ALM SET"
        pstrGridName_11(3) = "TRI SYSNo."
        pstrGridName_11(4) = "TRI CHNo."
        pstrGridName_11(5) = "String"
        pstrGridName_11(6) = "Decimal Point"
        pstrGridName_11(7) = "TagNo."
        pstrGridName_11(8) = "Alarm MimicNo"

        pstrGridName_12(0) = "Alarm HH Use"
        pstrGridName_12(1) = "Alarm HH DLY"
        pstrGridName_12(2) = "Alarm HH SET"
        pstrGridName_12(3) = "Alarm HH EX"
        pstrGridName_12(4) = "Alarm HH GR1"
        pstrGridName_12(5) = "Alarm HH GR2"
        pstrGridName_12(6) = "Alarm HH STATUS"
        pstrGridName_12(7) = "Alarm HH MR Status"
        pstrGridName_12(8) = "Alarm HH MR Set"
        pstrGridName_12(9) = "Alarm H Use"
        pstrGridName_12(10) = "Alarm H DLY"
        pstrGridName_12(11) = "Alarm H SET"
        pstrGridName_12(12) = "Alarm H EX"
        pstrGridName_12(13) = "Alarm H GR1"
        pstrGridName_12(14) = "Alarm H GR2"
        pstrGridName_12(15) = "Alarm H Status"
        pstrGridName_12(16) = "Alarm H MR Status"
        pstrGridName_12(17) = "Alarm H MR Set"
        pstrGridName_12(18) = "Alarm L Use"
        pstrGridName_12(19) = "Alarm L DLY"
        pstrGridName_12(20) = "Alarm L SET"
        pstrGridName_12(21) = "Alarm L EX"
        pstrGridName_12(22) = "Alarm L GR1"
        pstrGridName_12(23) = "Alarm L GR2"
        pstrGridName_12(24) = "Alarm L STATUS"
        pstrGridName_12(25) = "Alarm L MR Status"
        pstrGridName_12(26) = "Alarm L MR Set"
        pstrGridName_12(27) = "Alarm LL Use"
        pstrGridName_12(28) = "Alarm LL DLY"
        pstrGridName_12(29) = "Alarm LL SET"
        pstrGridName_12(30) = "Alarm LL EX"
        pstrGridName_12(31) = "Alarm LL GR1"
        pstrGridName_12(32) = "Alarm LL GR2"
        pstrGridName_12(33) = "Alarm LL STATUS"
        pstrGridName_12(34) = "Alarm LL MR Status"
        pstrGridName_12(35) = "Alarm LL MR Set"
        pstrGridName_12(36) = "Alarm S Use"
        pstrGridName_12(37) = "Alarm S DLY"
        pstrGridName_12(38) = "Alarm S SET"
        pstrGridName_12(39) = "Alarm S EX"
        pstrGridName_12(40) = "Alarm S GR1"
        pstrGridName_12(41) = "Alarm S GR2"
        pstrGridName_12(42) = "Alarm S STATUS"
        pstrGridName_12(43) = "Alarm S MR Status"
        pstrGridName_12(44) = "Alarm S MR Set"
        pstrGridName_12(45) = "RANGE"
        pstrGridName_12(46) = "NOR RANGE"
        pstrGridName_12(47) = "OFFSET"
        pstrGridName_12(48) = "String"
        pstrGridName_12(49) = "Decimal Point"
        pstrGridName_12(50) = "BAR GRAPH CENTER"
        pstrGridName_12(51) = "SENSOR ALM(UNDER)"
        pstrGridName_12(52) = "SENSOR ALM(OVER)"
        pstrGridName_12(53) = "PT Type"
        pstrGridName_12(54) = "TagNo."
        pstrGridName_12(55) = "LR Mode"
        pstrGridName_12(56) = "Dummy Setting"
        pstrGridName_12(57) = "Alarm MimicNo"
        pstrGridName_12(58) = "OUT FU ADRESS"
        pstrGridName_12(59) = "OUT PIN No"
        pstrGridName_12(60) = "PID Sp High"
        pstrGridName_12(61) = "PID Sp Low"
        pstrGridName_12(62) = "PID Mv High"
        pstrGridName_12(63) = "PID Mv Low"
        pstrGridName_12(64) = "PID PB"
        pstrGridName_12(65) = "PID TI"
        pstrGridName_12(66) = "PID TD"
        pstrGridName_12(67) = "PID GAP"
        pstrGridName_12(68) = "PID Para1"
        pstrGridName_12(69) = "PID Para High1"
        pstrGridName_12(70) = "PID Para Low1"
        pstrGridName_12(71) = "PID Para Name1"
        pstrGridName_12(72) = "PID Para Unit1"
        pstrGridName_12(73) = "PID Para2"
        pstrGridName_12(74) = "PID Para High2"
        pstrGridName_12(75) = "PID Para Low2"
        pstrGridName_12(76) = "PID Para Name2"
        pstrGridName_12(77) = "PID Para Unit2"
        pstrGridName_12(78) = "PID Para3"
        pstrGridName_12(79) = "PID Para High3"
        pstrGridName_12(80) = "PID Para Low3"
        pstrGridName_12(81) = "PID Para Name3"
        pstrGridName_12(82) = "PID Para Unit3"
        pstrGridName_12(83) = "PID Para4"
        pstrGridName_12(84) = "PID Para High4"
        pstrGridName_12(85) = "PID Para Low4"
        pstrGridName_12(86) = "PID Para Name4"
        pstrGridName_12(87) = "PID Para Unit4"
        pstrGridName_12(88) = "PID Out Mode"
        pstrGridName_12(89) = "PID Cas Mode"
        pstrGridName_12(90) = "PID Sp Tracking"
    End Sub

    'グリッド初期化
    Private Sub subInitGrid()
        Call subInitGrid_Logic(grdChk1)
        Call subInitGrid_Logic(grdChk2)
        Call subInitGrid_Logic(grdChk3)
        Call subInitGrid_Logic(grdChk4)
        Call subInitGrid_Logic(grdChk5)
        Call subInitGrid_Logic(grdChk6)
        Call subInitGrid_Logic(grdChk7)
        Call subInitGrid_Logic(grdChk8)
        Call subInitGrid_Logic(grdChk9)
        Call subInitGrid_Logic(grdChk10)
        Call subInitGrid_Logic(grdChk11)
        Call subInitGrid_Logic(grdChk12)
    End Sub
    Private Sub subInitGrid_Logic(ByRef grdData As clsDataGridViewPlus)
        Try

            Dim i As Integer
            Dim cellStyle As New DataGridViewCellStyle
            Dim Column0 As New DataGridViewCheckBoxColumn : Column0.Name = "chkSel"
            Dim Column1 As New DataGridViewTextBoxColumn : Column1.Name = "txtName" : Column1.ReadOnly = True


            With grdData

                ''列
                .Columns.Clear()
                .Columns.Add(Column0) : .Columns.Add(Column1)
                .AllowUserToResizeColumns = False   ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .Columns(0).HeaderText = "C" : .Columns(0).Width = 20
                .Columns(1).HeaderText = "Name" : .Columns(1).Width = 240
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter ''列ヘッダー　センタリング

                ''行
                .RowHeadersVisible = False
                '.RowCount = 257
                .AllowUserToAddRows = False         ''行の追加(新規行)を不可にする
                .AllowUserToResizeRows = False      ''行の高さの変更不可
                .AllowUserToDeleteRows = False      ''行の削除を不可にする

                ''行ヘッダー
                .RowHeadersWidth = 70
                For i = 1 To .Rows.Count
                    .Rows(i - 1).HeaderCell.Value = i.ToString
                Next
                .RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


                ''罫線
                .EnableHeadersVisualStyles = False
                .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .CellBorderStyle = DataGridViewCellBorderStyle.Single
                .GridColor = Color.Gray

                ''スクロールバー
                .ScrollBars = ScrollBars.Vertical
            End With


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    'Grid再表示
    Private Sub subGridDisp()
        Call subGridDisp_Logic(grdChk1, gCompareChk_1_Common, pstrGridName_1)
        Call subGridDisp_Logic(grdChk2, gCompareChk_2_Analog, pstrGridName_2)
        Call subGridDisp_Logic(grdChk3, gCompareChk_3_Digital, pstrGridName_3)
        Call subGridDisp_Logic(grdChk4, gCompareChk_4_System, pstrGridName_4)
        Call subGridDisp_Logic(grdChk5, gCompareChk_5_Motor, pstrGridName_5)
        Call subGridDisp_Logic(grdChk6, gCompareChk_6_DIDO, pstrGridName_6)
        Call subGridDisp_Logic(grdChk7, gCompareChk_7_AIDO, pstrGridName_7)
        Call subGridDisp_Logic(grdChk8, gCompareChk_8_AIAO, pstrGridName_8)
        Call subGridDisp_Logic(grdChk9, gCompareChk_9_Comp, pstrGridName_9)
        Call subGridDisp_Logic(grdChk10, gCompareChk_10_Puls, pstrGridName_10)
        Call subGridDisp_Logic(grdChk11, gCompareChk_11_RunHour, pstrGridName_11)
        Call subGridDisp_Logic(grdChk12, gCompareChk_12_Pid, pstrGridName_12)
    End Sub
    Private Sub subGridDisp_Logic(ByRef grdData As clsDataGridViewPlus, ByRef chk() As Boolean, ByRef grdName() As String)

        For i As Integer = 0 To UBound(chk) Step 1
            With grdData
                'Chkデータを表示
                .Rows.Add()
                .Rows(.RowCount - 1).Cells(0).Value = chk(i)
                .Rows(.RowCount - 1).Cells(1).Value = grdName(i)
            End With
        Next i


    End Sub
#End Region

End Class