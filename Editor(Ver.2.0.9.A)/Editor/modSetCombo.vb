﻿Imports System.Runtime.InteropServices

Module modSetCombo

#Region "API定義"

    '指定のINIファイルから文字列を取得する(P989)
    <DllImport("KERNEL32.DLL", CharSet:=CharSet.Auto)> _
    Public Function GetPrivateProfileString( _
       ByVal lpAppName As String, _
       ByVal lpKeyName As String, _
       ByVal lpDefault As String, _
       ByVal lpReturnedString As System.Text.StringBuilder, _
       ByVal nSize As Integer, _
       ByVal lpFileName As String) As Integer
    End Function

    '指定のINIファイルの指定のキーの文字列を変更する(P994)
    <DllImport("KERNEL32.DLL")> _
    Public Function WritePrivateProfileString( _
       ByVal lpAppName As String, _
       ByVal lpKeyName As String, _
       ByVal lpString As String, _
       ByVal lpFileName As String) As Integer
    End Function

    '指定のINIファイルから整数値を取得する(P986)
    <DllImport("KERNEL32.DLL", CharSet:=CharSet.Auto)> _
    Public Function GetPrivateProfileInt( _
       ByVal lpAppName As String, _
       ByVal lpKeyName As String, _
       ByVal nDefault As Integer, _
       ByVal lpFileName As String) As Integer
    End Function


#End Region

#Region "列挙体定義"

#End Region

#Region "構造体定義"

    Public Structure gTypCodeName

        Dim shtCode As Short
        Dim strName As String
        Dim strOption1 As String
        Dim strOption2 As String
        Dim strOption3 As String
        Dim strOption4 As String
        Dim strOption5 As String

    End Structure

#End Region

#Region "コンボ設定"

    '----------------------------------------------------------------------------
    ' 機能説明  ： コンボ設定（システム設定）
    ' 戻値      ： 0:成功
    ' 　　      ： 1:セクション無し
    ' 　　      ：-1:エラー
    ' 引数      ： ARG1 ( O) コンボボックスオブジェクト
    ' 　　      ： ARG2 (I ) コンボボックス種類
    ' 　　      ： ARG3 (I ) サブコード
    '----------------------------------------------------------------------------
    Public Function gSetComboBox(ByRef cmbCombo As ComboBox, _
                                 ByVal udtComboType As gEnmComboType, _
                        Optional ByVal strSub As String = "", _
                        Optional ByVal strAddCode As String = "", _
                        Optional ByVal strAddName As String = "", _
                        Optional ByVal blnNoSectionError As Boolean = True) As Integer

        Try

            Dim intCnt As Integer
            Dim strIniFilePath As String = ""
            Dim strIniFileName As String = ""
            Dim strSectionName As String = ""
            Dim strwk() As String = Nothing
            Dim strCode() As String = Nothing
            Dim strName() As String = Nothing

            ''iniファイル名取得
            strIniFileName = mGetIniFileName(udtComboType)

            ''iniファイルパス作成
            strIniFilePath = System.IO.Path.Combine(gGetDirNameIniFile, strIniFileName)

            ''ファイル存在確認
            If Not System.IO.File.Exists(strIniFilePath) Then
                Call MessageBox.Show("Under '" & gCstIniFileDir & "' Folder, There is no '" & strIniFileName & "' File.", _
                                     "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return Not 0
            End If

            ''セクション名取得
            strSectionName = mGetSectionName(udtComboType, strSub)

            Dim strBuffer As New System.Text.StringBuilder
            strBuffer.Capacity = 256   'バッファのサイズを指定

            intCnt = 1
            Do
                ''iniファイルから値取得
                If intCnt < 100 Then
                    Call GetPrivateProfileString(strSectionName, "Item" & intCnt.ToString("00"), "", strBuffer, strBuffer.Capacity, strIniFilePath)
                Else
                    Call GetPrivateProfileString(strSectionName, "Item" & intCnt.ToString, "", strBuffer, strBuffer.Capacity, strIniFilePath)
                End If

                ''値が取得出来なかった場合は処理を抜ける
                If strBuffer.ToString() = "" Then Exit Do

                ''「,」区切りの文字列取得
                Erase strwk
                strwk = strBuffer.ToString.Split(",")

                ''配列再定義
                ReDim Preserve strCode(intCnt - 1)
                ReDim Preserve strName(intCnt - 1)

                ''配列に格納
                strCode(intCnt - 1) = strwk(0)
                strName(intCnt - 1) = strwk(1)

                ''カウントアップ
                intCnt += 1

            Loop

            ''項目が取得できなかった場合
            If intCnt = 1 Then

                If blnNoSectionError Then
                    Return 1
                Else
                    Call MessageBox.Show("Under '" & strIniFileName & "' File, There is no '" & strSectionName & "' Section.", _
                                         "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Not 0
                End If

            End If

            ''追加コードがある場合
            If strAddCode <> "" And strAddName <> "" Then

                ''配列再定義
                ReDim Preserve strCode(UBound(strCode) + 1)
                ReDim Preserve strName(UBound(strCode) + 1)

                ''配列に格納
                strCode(UBound(strCode)) = strAddCode
                strName(UBound(strCode)) = strAddName

            End If

            ''データマップコンボ作成
            If gMakeDataMapCombo(cmbCombo, strCode, strName) <> 0 Then
                Return Not 0
            End If

            Return 0

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '----------------------------------------------------------------------------
    ' 機能説明  ： コンボ設定（システム設定）
    ' 戻値      ： 0:成功
    ' 　　      ： 1:セクション無し
    ' 　　      ：-1:エラー
    ' 引数      ： ARG1 ( O) コンボボックスオブジェクト
    ' 　　      ： ARG2 (I ) コンボボックス種類
    '----------------------------------------------------------------------------
    Public Function gSetComboBox(ByRef cmbCombo As DataGridViewComboBoxColumn, _
                                 ByVal udtComboType As gEnmComboType, _
                        Optional ByVal strSub As String = "", _
                        Optional ByVal strAddCode As String = "", _
                        Optional ByVal strAddName As String = "", _
                        Optional ByVal blnNoSectionError As Boolean = True) As Integer

        Try

            Dim intCnt As Integer
            Dim strIniFilePath As String = ""
            Dim strIniFileName As String = ""
            Dim strSectionName As String = ""
            Dim strwk() As String = Nothing
            Dim strCode() As String = Nothing
            Dim strName() As String = Nothing

            ''iniファイル名取得
            strIniFileName = mGetIniFileName(udtComboType)

            ''iniファイルパス作成
            strIniFilePath = System.IO.Path.Combine(gGetDirNameIniFile, strIniFileName)

            ''ファイル存在確認
            If Not System.IO.File.Exists(strIniFilePath) Then
                Call MessageBox.Show("Under '" & gCstIniFileDir & "' Folder, There is no '" & strIniFileName & "' File.", _
                                     "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return Not 0
            End If

            ''セクション名取得
            strSectionName = mGetSectionName(udtComboType, strSub)

            Dim strBuffer As New System.Text.StringBuilder
            strBuffer.Capacity = 256   'バッファのサイズを指定

            intCnt = 1
            Do
                ''iniファイルから値取得
                If intCnt < 100 Then
                    Call GetPrivateProfileString(strSectionName, "Item" & intCnt.ToString("00"), "", strBuffer, strBuffer.Capacity, strIniFilePath)
                Else
                    Call GetPrivateProfileString(strSectionName, "Item" & intCnt.ToString, "", strBuffer, strBuffer.Capacity, strIniFilePath)
                End If

                ''値が取得出来なかった場合は処理を抜ける
                If strBuffer.ToString() = "" Then Exit Do

                ''「,」区切りの文字列取得
                Erase strwk
                strwk = strBuffer.ToString.Split(",")

                '■外販
                '外販の場合、PID CH選択は消す
                If gintNaiGai = 1 Then
                    'CH TYPEｺﾝﾎﾞでなおかつ、PIDの場合は処理スルー
                    If udtComboType = gEnmComboType.ctChListChannelListChType And strSectionName = "ChType" Then
                        If strwk(0) = "7" Then
                            intCnt += 1
                            Continue Do
                        End If
                    End If
                End If


                ''配列再定義
                ReDim Preserve strCode(intCnt - 1)
                ReDim Preserve strName(intCnt - 1)

                ''配列に格納
                strCode(intCnt - 1) = strwk(0)
                strName(intCnt - 1) = strwk(1)

                ''カウントアップ
                intCnt += 1

            Loop

            ''項目が取得できなかった場合
            If intCnt = 1 Then

                If blnNoSectionError Then
                    Return 1
                Else
                    Call MessageBox.Show("Under '" & strIniFileName & "' File, There is no '" & strSectionName & "' Section.", _
                                         "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Not 0
                End If

            End If

            ''追加コードがある場合
            If strAddCode <> "" And strAddName <> "" Then

                ''配列再定義
                ReDim Preserve strCode(UBound(strCode) + 1)
                ReDim Preserve strName(UBound(strCode) + 1)

                ''配列に格納
                strCode(UBound(strCode)) = strAddCode
                strName(UBound(strCode)) = strAddName

            End If

            ''データマップコンボ作成
            If gMakeDataMapCombo(cmbCombo, strCode, strName) <> 0 Then
                Return Not 0
            End If

            Return 0

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '----------------------------------------------------------------------------
    ' 機能説明  ： コンボ設定（システム設定）
    ' 引数      ： ARG1 ( O) コンボボックスオブジェクト
    ' 　　      ： ARG2 (I ) コンボボックス種類
    ' 戻値      ： 0:成功、<>0:失敗
    '----------------------------------------------------------------------------
    Public Function gSetComboBox(ByRef cmbCombo As DataGridViewComboBoxCell, _
                                 ByVal udtComboType As gEnmComboType, _
                        Optional ByVal strSub As String = "", _
                        Optional ByVal strAddCode As String = "", _
                        Optional ByVal strAddName As String = "") As Integer

        Try

            Dim intCnt As Integer
            Dim strIniFilePath As String = ""
            Dim strIniFileName As String = ""
            Dim strSectionName As String = ""
            Dim strwk() As String = Nothing
            Dim strCode() As String = Nothing
            Dim strName() As String = Nothing

            ''iniファイル名取得
            strIniFileName = mGetIniFileName(udtComboType)

            ''iniファイルパス作成
            strIniFilePath = System.IO.Path.Combine(gGetDirNameIniFile, strIniFileName)

            ''ファイル存在確認
            If Not System.IO.File.Exists(strIniFilePath) Then
                Call MessageBox.Show("Under '" & gCstIniFileDir & "' Folder, There is no '" & strIniFileName & "' File.", _
                                     "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return Not 0
            End If

            ''セクション名取得
            strSectionName = mGetSectionName(udtComboType, strSub)

            Dim strBuffer As New System.Text.StringBuilder
            strBuffer.Capacity = 256   'バッファのサイズを指定

            intCnt = 1
            Do
                ''iniファイルから値取得
                If intCnt < 100 Then
                    Call GetPrivateProfileString(strSectionName, "Item" & intCnt.ToString("00"), "", strBuffer, strBuffer.Capacity, strIniFilePath)
                Else
                    Call GetPrivateProfileString(strSectionName, "Item" & intCnt.ToString, "", strBuffer, strBuffer.Capacity, strIniFilePath)
                End If


                ''値が取得出来なかった場合は処理を抜ける
                If strBuffer.ToString() = "" Then Exit Do

                ''「,」区切りの文字列取得
                Erase strwk
                strwk = strBuffer.ToString.Split(",")

                ''配列再定義
                ReDim Preserve strCode(intCnt - 1)
                ReDim Preserve strName(intCnt - 1)

                ''配列に格納
                strCode(intCnt - 1) = strwk(0)
                strName(intCnt - 1) = strwk(1)

                ''カウントアップ
                intCnt += 1

            Loop

            ''項目が取得できなかった場合
            If intCnt = 1 Then
                Call MessageBox.Show("Under '" & strIniFileName & "' File, There is no '" & strSectionName & "' Section.", _
                                     "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return Not 0
            End If

            ''追加コードがある場合
            If strAddCode <> "" And strAddName <> "" Then

                ''配列再定義
                ReDim Preserve strCode(UBound(strCode) + 1)
                ReDim Preserve strName(UBound(strCode) + 1)

                ''配列に格納
                strCode(UBound(strCode)) = strAddCode
                strName(UBound(strCode)) = strAddName

            End If

            ''データマップコンボ作成
            If gMakeDataMapCombo(cmbCombo, strCode, strName) <> 0 Then
                Return Not 0
            End If

            Return 0

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '----------------------------------------------------------------------------
    ' 機能説明  ： コンボ設定（FCU/FU種類用）
    ' 引数      ： ARG1 ( O) コンボボックスオブジェクト
    ' 　　      ： ARG2 (I ) コンボボックス種類
    ' 　　      ： ARG3 ( O) サブコード
    ' 戻値      ： 0:成功、<>0:失敗
    '----------------------------------------------------------------------------
    Public Function gSetComboBoxPlus(ByRef cmbCombo As DataGridViewComboBoxColumn, _
                                     ByVal udtComboType As gEnmComboType, _
                                     ByRef intSubCode() As Integer) As Integer
        Try

            Dim intCnt As Integer
            Dim strIniFilePath As String = ""
            Dim strIniFileName As String = ""
            Dim strSectionName As String = ""
            Dim strwk() As String = Nothing
            Dim strCode() As String = Nothing
            Dim strName() As String = Nothing
            Dim strSub As String = ""

            ''iniファイル名取得
            strIniFileName = mGetIniFileName(udtComboType)

            ''iniファイルパス作成
            strIniFilePath = System.IO.Path.Combine(gGetDirNameIniFile, strIniFileName)

            ''ファイル存在確認
            If Not System.IO.File.Exists(strIniFilePath) Then
                Call MessageBox.Show("Under '" & gCstIniFileDir & "' Folder, There is no '" & strIniFileName & "' File.", _
                                     "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return Not 0
            End If

            ''セクション名取得
            strSectionName = mGetSectionName(udtComboType, strSub)

            Dim strBuffer As New System.Text.StringBuilder
            strBuffer.Capacity = 256   'バッファのサイズを指定

            intCnt = 1
            Do
                ''iniファイルから値取得
                If intCnt < 100 Then
                    Call GetPrivateProfileString(strSectionName, "Item" & intCnt.ToString("00"), "", strBuffer, strBuffer.Capacity, strIniFilePath)
                Else
                    Call GetPrivateProfileString(strSectionName, "Item" & intCnt.ToString, "", strBuffer, strBuffer.Capacity, strIniFilePath)
                End If

                ''値が取得出来なかった場合は処理を抜ける
                If strBuffer.ToString() = "" Then Exit Do

                ''「,」区切りの文字列取得
                Erase strwk
                strwk = strBuffer.ToString.Split(",")

                ''配列再定義
                ReDim Preserve strCode(intCnt - 1)
                ReDim Preserve strName(intCnt - 1)
                ReDim Preserve intSubCode((intCnt - 1))

                ''配列に格納
                strCode(intCnt - 1) = strwk(0)
                strName(intCnt - 1) = strwk(1)
                intSubCode(intCnt - 1) = CCInt(strwk(2))

                ''カウントアップ
                intCnt += 1

            Loop

            ''項目が取得できなかった場合
            If intCnt = 1 Then
                Call MessageBox.Show("Under '" & strIniFileName & "' File, There is no '" & strSectionName & "' Section.", _
                                     "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return Not 0
            End If

            ''データマップコンボ作成
            If gMakeDataMapCombo(cmbCombo, strCode, strName) <> 0 Then
                Return Not 0
            End If

            Return 0

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '----------------------------------------------------------------------------
    ' 機能説明  ： コンボ設定（FCU/FU種類用）
    ' 引数      ： ARG1 ( O) コンボボックスオブジェクト
    ' 　　      ： ARG2 (I ) コンボボックス種類
    ' 　　      ： ARG3 (I ) サブコード
    ' 戻値      ： 0:成功、<>0:失敗
    '----------------------------------------------------------------------------
    Public Function gSetComboBoxPlus(ByRef cmbCombo As ComboBox, _
                                 ByVal udtComboType As gEnmComboType, _
                              ByRef intSubCode() As Integer) As Integer

        Try

            Dim intCnt As Integer
            Dim strIniFilePath As String = ""
            Dim strIniFileName As String = ""
            Dim strSectionName As String = ""
            Dim strwk() As String = Nothing
            Dim strCode() As String = Nothing
            Dim strName() As String = Nothing
            Dim strSub As String = ""

            ''iniファイル名取得
            strIniFileName = mGetIniFileName(udtComboType)

            ''iniファイルパス作成
            strIniFilePath = System.IO.Path.Combine(gGetDirNameIniFile, strIniFileName)

            ''ファイル存在確認
            If Not System.IO.File.Exists(strIniFilePath) Then
                Call MessageBox.Show("Under '" & gCstIniFileDir & "' Folder, There is no '" & strIniFileName & "' File.", _
                                     "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return Not 0
            End If

            ''セクション名取得
            strSectionName = mGetSectionName(udtComboType, strSub)

            Dim strBuffer As New System.Text.StringBuilder
            strBuffer.Capacity = 256   'バッファのサイズを指定

            intCnt = 1
            Do
                ''iniファイルから値取得
                If intCnt < 100 Then
                    Call GetPrivateProfileString(strSectionName, "Item" & intCnt.ToString("00"), "", strBuffer, strBuffer.Capacity, strIniFilePath)
                Else
                    Call GetPrivateProfileString(strSectionName, "Item" & intCnt.ToString, "", strBuffer, strBuffer.Capacity, strIniFilePath)
                End If

                ''値が取得出来なかった場合は処理を抜ける
                If strBuffer.ToString() = "" Then Exit Do

                ''「,」区切りの文字列取得
                Erase strwk
                strwk = strBuffer.ToString.Split(",")

                ''配列再定義
                ReDim Preserve strCode(intCnt - 1)
                ReDim Preserve strName(intCnt - 1)
                ReDim Preserve intSubCode((intCnt - 1))

                ''配列に格納
                strCode(intCnt - 1) = strwk(0)
                strName(intCnt - 1) = strwk(1)
                intSubCode(intCnt - 1) = CCInt(strwk(2))

                ''カウントアップ
                intCnt += 1

            Loop

            ''項目が取得できなかった場合
            If intCnt = 1 Then
                Call MessageBox.Show("Under '" & strIniFileName & "' File, There is no '" & strSectionName & "' Section.", _
                                     "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return Not 0
            End If

            ''データマップコンボ作成
            If gMakeDataMapCombo(cmbCombo, strCode, strName) <> 0 Then
                Return Not 0
            End If

            Return 0

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "セクション名取得"

    '----------------------------------------------------------------------------
    ' 機能説明  ： セクション名取得
    ' 引数      ： ARG1 (I ) 画面タイプ
    ' 　　      ： ARG2 (I ) コンボタイプ
    ' 　　      ： ARG3 (I ) サブコード
    ' 戻値      ： セクション名
    '----------------------------------------------------------------------------
    Private Function mGetSectionName(ByVal udtComboType As gEnmComboType, ByVal strSub As String) As String

        Try

            Dim strRtn As String = ""

            Select Case udtComboType

                '========================
                ' システム設定
                '========================
                Case gEnmComboType.ctSysSystemSystemClock : strRtn = "SystemClock"
                Case gEnmComboType.ctSysSystemDataFormat : strRtn = "DataFormat"
                Case gEnmComboType.ctSysSystemLanguage : strRtn = "Language"
                Case gEnmComboType.ctSysSystemManual : strRtn = "Manual"    '' 2015.02.05
                Case gEnmComboType.ctSysSystemCombine : strRtn = "Combine"
                Case gEnmComboType.ctSysSystemEthernetLine : strRtn = "EthernetLine"

                Case gEnmComboType.ctSysSystemGL_SPEC : strRtn = "GL_Spec"    '' 2015.05.27

                Case gEnmComboType.ctSysFcuFcuCount : strRtn = "FcuCount"
                Case gEnmComboType.ctSysFcuPartSet : strRtn = "FcuPart"         '' Ver1.11.8.2 2016.11.01 FCU2台仕様 Cargo/Hull選択

                Case gEnmComboType.ctSysOpsChSetup : strRtn = "ChSetup"
                Case gEnmComboType.ctSysOpsChEdit : strRtn = "ChEdit"
                Case gEnmComboType.ctSysOpsSystemAlarm : strRtn = "SystemAlarm"
                Case gEnmComboType.ctSysOpsDutySetting : strRtn = "DutySetting"
                Case gEnmComboType.ctSysOpsAlarmDisplay : strRtn = "AlarmDisplay"
                Case gEnmComboType.ctSysOpsResolution : strRtn = "Resolution"
                Case gEnmComboType.ctSysOpsAutoAlarmOrder : strRtn = "AutoAlarmOrder"
                Case gEnmComboType.ctSysOpsLcdType : strRtn = "LCDType"     '' Ver1.11.8.2 2016.11.01  LCDType追加

                Case gEnmComboType.ctSysGws1Type : strRtn = "Gws1Type"  '' GWS1タイプ      2014.02.03
                Case gEnmComboType.ctSysGws2Type : strRtn = "Gws2Type"  '' GWS2タイプ
                Case gEnmComboType.ctSysGwsFile : strRtn = "GwsFile"    '' GWS FILE種類
                Case gEnmComboType.ctChGwsDetailCH : strRtn = "FileNo"  '' GWS CH FILE番号

                Case gEnmComboType.ctSysPrinterLogPrinter1 : strRtn = "LogPrinter1"
                Case gEnmComboType.ctSysPrinterLogPrinter2 : strRtn = "LogPrinter2"
                Case gEnmComboType.ctSysPrinterAlarmPrinter1 : strRtn = "AlarmPrinter1"
                Case gEnmComboType.ctSysPrinterAlarmPrinter2 : strRtn = "AlarmPrinter2"
                Case gEnmComboType.ctSysPrinterPrinterType : strRtn = "PrinterType"
                Case gEnmComboType.ctSysPrinterHcPrinter : strRtn = "HcPrinter"
                Case gEnmComboType.ctSysPrinterPrinterName : strRtn = "PrinterName" 'Ver2.0.6.5

                    '' Ver1.11.8.6 2016.11.10  ｼｽﾃﾑ設定追加
                Case gEnmComboType.ctSysSystemTag : strRtn = "Tag"
                Case gEnmComboType.ctSysSystemAlmLevel : strRtn = "AlmLevel"

                    '========================
                    ' チャンネル設定画面
                    '========================
                Case gEnmComboType.ctChListChannelListChType : strRtn = "ChType"
                Case gEnmComboType.ctChListChannelListSysNo : strRtn = "SysNo"
                Case gEnmComboType.ctChListChannelListSF : strRtn = "SF"
                Case gEnmComboType.ctChListChannelListUnit : strRtn = "Unit"
                Case gEnmComboType.ctChListChannelListShareType : strRtn = "ShareType"
                Case gEnmComboType.ctChListChannelListTime : strRtn = "DelayTime"
                Case gEnmComboType.ctChListChannelListOutputControlType : strRtn = "OutputControlType"
                Case gEnmComboType.ctChListChannelListAlmLevel : strRtn = "AlarmLevel"      '' 2015.11.12 Ver1.7.8 ﾛｲﾄﾞ対応

                Case gEnmComboType.ctChListChannelListDataTypeAnalog : strRtn = "DataTypeAnalog"
                Case gEnmComboType.ctChListChannelListDataTypeDigital : strRtn = "DataTypeDigital"
                Case gEnmComboType.ctChListChannelListDataTypeMotor : strRtn = "DataTypeMotor"
                Case gEnmComboType.ctChListChannelListDataTypeValve : strRtn = "DataTypeValve"
                Case gEnmComboType.ctChListChannelListDataTypeComposite : strRtn = "DataTypeComposite"
                Case gEnmComboType.ctChListChannelListDataTypePulse : strRtn = "DataTypePulse"
                Case gEnmComboType.ctChListChannelListDataTypeSystem : strRtn = "DataTypeSystem"
                Case gEnmComboType.ctChListChannelListDataTypePID : strRtn = "DataTypePID"

                    'Case gEnmComboType.ctChListChannelListCompExp : strRtn = "CompExp"
                Case gEnmComboType.ctChListChannelListRangeType1 : strRtn = "RangeType1"
                Case gEnmComboType.ctChListChannelListRangeType2 : strRtn = "RangeType2"

                Case gEnmComboType.ctChListChannelListDeviceStatus
                    strRtn = "Kiki"
                    If strSub <> "" Then strRtn = strRtn & strSub

                Case gEnmComboType.ctChListChannelListStatusAnalog : strRtn = "StatusAnalog"
                Case gEnmComboType.ctChListChannelListStatusDigital : strRtn = "StatusDigital"
                Case gEnmComboType.ctChListChannelListStatusMotor : strRtn = "StatusMotor"
                Case gEnmComboType.ctChListChannelListStatusValve : strRtn = "StatusValve"
                Case gEnmComboType.ctChListChannelListStatusPulse : strRtn = "StatusPulse"

                Case gEnmComboType.ctChListChannelListOutStatusMotor : strRtn = "BitMotor"

                Case gEnmComboType.ctChListAnalogExtDeviceJACOM22DI : strRtn = "ExtDevice(JACOM-22) DI"
                Case gEnmComboType.ctChListAnalogExtDeviceJACOM22AI : strRtn = "ExtDevice(JACOM-22) AI"
                Case gEnmComboType.ctChListAnalogExtDeviceJACOM22DO : strRtn = "ExtDevice(JACOM-22) DO"
                Case gEnmComboType.ctChListAnalogExtDeviceMODBUSDI : strRtn = "ExtDevice(MODBUS) DI"
                Case gEnmComboType.ctChListAnalogExtDeviceMODBUSAI : strRtn = "ExtDevice(MODBUS) AI"


                Case gEnmComboType.ctChTerminalListColumn3 : strRtn = "Type"
                Case gEnmComboType.ctChTerminalListColumn : strRtn = "Kiki"
                Case gEnmComboType.ctChTerminalListTerminalNo : strRtn = "TerminalNo"

                Case gEnmComboType.ctChTerminalFunctionFuncName : strRtn = "FuncName"
                Case gEnmComboType.ctChTerminalFunctionFuncDI : strRtn = "DI"
                Case gEnmComboType.ctChTerminalFunctionFuncDO : strRtn = "DO"

                Case gEnmComboType.ctChOutputDoChOutType : strRtn = "ChOutType"
                Case gEnmComboType.ctChOutputDoStatus : strRtn = "Status"

                Case gEnmComboType.ctChOutputDoTermType1 : strRtn = "ChOutDOTermType1" 'Ver.2.0.8.P DO端子選択用追加
                Case gEnmComboType.ctChOutputDoTermType2 : strRtn = "ChOutDOTermType2" 'Ver.2.0.8.P DO端子選択用追加

                Case gEnmComboType.ctChGroupReposeListColumn2 : strRtn = "Type"

                Case gEnmComboType.ctChRunHourColumn3 : strRtn = "Type"

                Case gEnmComboType.ctChExhGusGroupcmbNo : strRtn = "NO"

                Case gEnmComboType.ctChCtrlUseNotuseDetail : strRtn = "Flg"
                Case gEnmComboType.ctChCtrlUseNotuseDetailgrd : strRtn = "Type"

                Case gEnmComboType.ctChDataForwardTableListColumn1 : strRtn = "DataCode"
                    'Case gEnmComboType.ctChDataForwardTableListColumn7 : strRtn = "DataSave"

                Case gEnmComboType.ctChDataSaveTableListColumn3 : strRtn = "DefaultSet"


                Case gEnmComboType.ctChSioDetailcmbPort : strRtn = "Port"
                Case gEnmComboType.ctChSioDetailcmbPriority : strRtn = "Priority"
                Case gEnmComboType.ctChSioDetailcmbMC : strRtn = "MC"
                Case gEnmComboType.ctChSioDetailcmbCom : strRtn = "Com"
                Case gEnmComboType.ctChSioDetailcmbParityBit : strRtn = "ParityBit"
                Case gEnmComboType.ctChSioDetailcmbDataBit : strRtn = "DataBit"
                Case gEnmComboType.ctChSioDetailcmbStopBit : strRtn = "StopBit"
                Case gEnmComboType.ctChSioDetailcmbDuplet : strRtn = "Duplet"
                Case gEnmComboType.ctChSioDetailcmbCommType1 : strRtn = "CommType1"
                Case gEnmComboType.ctChSioDetailcmbCommType2 : strRtn = "CommType2" & strSub
                Case gEnmComboType.ctChSioDetailcmbTransmisionCh : strRtn = "TransmissionCH"


                    '========================
                    ' 延長警報設定
                    '========================
                Case gEnmComboType.ctExtPnlSystem : strRtn = "GroupOutputPattern"

                Case gEnmComboType.ctExtPnlListWh : strRtn = "WH"
                Case gEnmComboType.ctExtPnlListPr : strRtn = "PR"
                Case gEnmComboType.ctExtPnlListCe : strRtn = "CE"
                Case gEnmComboType.ctExtPnlListEngineerCall : strRtn = "EngineerCall"
                Case gEnmComboType.ctExtPnlListNo : strRtn = "No"
                Case gEnmComboType.ctExtPnlListDutyNo : strRtn = "DutyNo"
                Case gEnmComboType.ctExtPnlListReAlm : strRtn = "ReAlm"
                Case gEnmComboType.ctExtPnlListBzCut : strRtn = "BzCut"
                Case gEnmComboType.ctExtPnlListFree : strRtn = "Free"
                Case gEnmComboType.ctExtPnlListLedLcd : strRtn = "LedLcd"

                Case gEnmComboType.ctExtPnlLedEngNo : strRtn = "EngineerNo"
                Case gEnmComboType.ctExtPnlLedDutyNo : strRtn = "DutyNo"
                Case gEnmComboType.ctExtPnlLedWatchLED : strRtn = "WatchLED"

                Case gEnmComboType.ctExtPnlLcdGroupGrpNo : strRtn = "GroupNo"
                Case gEnmComboType.ctExtPnlLcdGroupMarkNo : strRtn = "MarkNo"

                    'Case gEnmComboType.ctExtTermTestBz : strRtn = "BzTest"
                    'Case gEnmComboType.ctExtTermTestLamp : strRtn = "LampTest"
                    'Case gEnmComboType.ctExtTermTestFunc : strRtn = "FuncTest"

                Case gEnmComboType.ctExtTimerPart : strRtn = "Part"         '' パート追加　ver.1.4.4 2012.05.08
                Case gEnmComboType.ctExtTimerType : strRtn = "Type"
                Case gEnmComboType.ctExtTimerTimeDisp : strRtn = "TimeDisp"


                    '========================
                    ' シーケンス設定
                    '========================
                Case gEnmComboType.ctSeqOpeTableNo : strRtn = "NO"
                Case gEnmComboType.ctSeqLineTableNo : strRtn = "NO"
                Case gEnmComboType.ctSeqOpeFixedType : strRtn = "FixedType"

                Case gEnmComboType.ctSeqSetDetailStatus : strRtn = "Status"
                Case gEnmComboType.ctSeqSetDetailDataType : strRtn = "DataType"
                Case gEnmComboType.ctSeqSetDetailOutputType : strRtn = "OutputType"
                Case gEnmComboType.ctSeqSetDetailFucFu : strRtn = "FcuFu"
                Case gEnmComboType.ctSeqSetDetailLogic : strRtn = "SeqLogic" & strSub


                    '========================
                    ' OPSSET
                    '========================
                Case gEnmComboType.ctOpsScreenTitle : strRtn = "ScreenTitle"
                Case gEnmComboType.ctOpsSelectionFunctionList : strRtn = "FunctionList"
                Case gEnmComboType.ctOpsSelectionFunctionSet : strRtn = "FunctionSet"
                Case gEnmComboType.ctOpsSelectionSelectionDefault : strRtn = "SelectionDefault"
                Case gEnmComboType.ctOpsSelectionSelectionDisableList : strRtn = "SelectionDisableList"
                Case gEnmComboType.ctOpsPulldownColumn1 : strRtn = "Main"
                Case gEnmComboType.ctOpsPulldownColumn2 : strRtn = "Type"
                Case gEnmComboType.ctOpsGraphListColumn2 : strRtn = "Type"
                Case gEnmComboType.ctOpsGraphAnalogDispType : strRtn = "DispType"
                    'Case gEnmComboType.ctOpsGraphExhaustLimit : strRtn = "Limit"
                Case gEnmComboType.ctOpsGraphAnalogDetailChNameDispPoint : strRtn = "ChNameDispPoint"
                Case gEnmComboType.ctOpsGraphAnalogDetailMarkNumValue : strRtn = "MarkNumValue"
                Case gEnmComboType.ctOpsGraphAnalogDetailPointerFrame : strRtn = "PointerFrame"
                Case gEnmComboType.ctOpsGraphAnalogDetailPointerColor : strRtn = "PointerColor"
                Case gEnmComboType.ctOpsGraphAnalogDetailSideColor : strRtn = "SideColor"

                Case gEnmComboType.ctOpsLogFormatListColumn1Type : strRtn = "SelectItem"
                Case gEnmComboType.ctOpsLogFormatListColumn2Type : strRtn = "SelectItem"
                Case gEnmComboType.ctOpsLogFormatListSaveStrings : strRtn = "SaveFormatStrings"

                    '========================
                    ' print
                    '========================
                    'Case gEnmComboType.ctPrtLocalCanbus : strRtn = "Type"

            End Select

            Return strRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return ""
        End Try

    End Function

#End Region

#Region "iniファイル名取得"

    '----------------------------------------------------------------------------
    ' 機能説明  ： iniファイル名取得
    ' 引数      ： ARG1 (I ) 画面タイプ
    ' 戻値      ： iniファイル名
    '----------------------------------------------------------------------------
    Private Function mGetIniFileName(ByVal udtComboType As gEnmComboType) As String

        Try

            Dim strRtn As String = ""

            '========================
            ' システム設定画面
            '========================
            ''System Set    2015.02.05 Manual追加
            If udtComboType = gEnmComboType.ctSysSystemSystemClock _
                Or udtComboType = gEnmComboType.ctSysSystemDataFormat _
                Or udtComboType = gEnmComboType.ctSysSystemLanguage _
                Or udtComboType = gEnmComboType.ctSysSystemManual _
                Or udtComboType = gEnmComboType.ctSysSystemCombine _
                Or udtComboType = gEnmComboType.ctSysSystemEthernetLine _
                Or udtComboType = gEnmComboType.ctSysSystemGL_Spec Then
                Return gCstIniFileNameComboSysSystem
            End If

            ''FCU Set
            '' Ver1.11.8.2 2016.11.01 FCU2台仕様 Cargo/Hull選択 追加
            If udtComboType = gEnmComboType.ctSysFcuFcuCount Or udtComboType = gEnmComboType.ctSysFcuPartSet Then
                Return gCstIniFileNameComboSysFcu
            End If

            ''OPS Set
            '' Ver1.11.8.2 2016.11.01  LCDType追加
            If udtComboType = gEnmComboType.ctSysOpsChSetup _
                Or udtComboType = gEnmComboType.ctSysOpsChEdit _
                Or udtComboType = gEnmComboType.ctSysOpsSystemAlarm _
                Or udtComboType = gEnmComboType.ctSysOpsDutySetting _
                Or udtComboType = gEnmComboType.ctSysOpsAlarmDisplay _
                Or udtComboType = gEnmComboType.ctSysOpsResolution _
                Or udtComboType = gEnmComboType.ctSysOpsAutoAlarmOrder _
                Or udtComboType = gEnmComboType.ctSysOpsLcdType Then
                Return gCstIniFileNameComboSysOps
            End If

            ''GWS Set   GWS処理追加 2014.02.03
            If udtComboType = gEnmComboType.ctSysGws1Type _
                Or udtComboType = gEnmComboType.ctSysGws2Type _
                Or udtComboType = gEnmComboType.ctSysGwsFile _
                Or udtComboType = gEnmComboType.ctChGwsDetailCH Then

                Return gCstIniFileNameComboSysGws
            End If

            ''Printer Set
            If udtComboType = gEnmComboType.ctSysPrinterAlarmPrinter1 _
                Or udtComboType = gEnmComboType.ctSysPrinterAlarmPrinter2 _
                Or udtComboType = gEnmComboType.ctSysPrinterLogPrinter1 _
                Or udtComboType = gEnmComboType.ctSysPrinterLogPrinter2 _
                Or udtComboType = gEnmComboType.ctSysPrinterPrinterType _
                Or udtComboType = gEnmComboType.ctSysPrinterHcPrinter _
                Or udtComboType = gEnmComboType.ctSysPrinterPrinterName Then
                Return gCstIniFileNameComboSysPrinter
            End If

            ''ｼｽﾃﾑ設定    Ver1.11.8.6 2016.11.10 追加
            If udtComboType = gEnmComboType.ctSysSystemTag _
                Or udtComboType = gEnmComboType.ctSysSystemAlmLevel Then
                Return gCstIniFileNameComboSystem
            End If

            '========================
            ' チャンネル設定画面
            '========================
            '' 2015.11.12 Ver1.7.8 ﾛｲﾄﾞ対応　　AlarmLevel追加
            If udtComboType = gEnmComboType.ctChListChannelListChType _
               Or udtComboType = gEnmComboType.ctChListChannelListSysNo _
               Or udtComboType = gEnmComboType.ctChListChannelListSF _
               Or udtComboType = gEnmComboType.ctChListChannelListDataTypeAnalog _
               Or udtComboType = gEnmComboType.ctChListChannelListDataTypeDigital _
               Or udtComboType = gEnmComboType.ctChListChannelListDataTypeMotor _
               Or udtComboType = gEnmComboType.ctChListChannelListDataTypeValve _
               Or udtComboType = gEnmComboType.ctChListChannelListDataTypeComposite _
               Or udtComboType = gEnmComboType.ctChListChannelListDataTypePulse _
               Or udtComboType = gEnmComboType.ctChListChannelListDataTypeSystem _
               Or udtComboType = gEnmComboType.ctChListChannelListDataTypePID _
               Or udtComboType = gEnmComboType.ctChListChannelListOutputControlType _
               Or udtComboType = gEnmComboType.ctChListChannelListRangeType1 _
               Or udtComboType = gEnmComboType.ctChListChannelListRangeType2 _
               Or udtComboType = gEnmComboType.ctChListChannelListStatusAnalog _
               Or udtComboType = gEnmComboType.ctChListChannelListStatusDigital _
               Or udtComboType = gEnmComboType.ctChListChannelListStatusMotor _
               Or udtComboType = gEnmComboType.ctChListChannelListStatusValve _
               Or udtComboType = gEnmComboType.ctChListChannelListStatusPulse _
               Or udtComboType = gEnmComboType.ctChListChannelListUnit _
               Or udtComboType = gEnmComboType.ctChListChannelListTime _
               Or udtComboType = gEnmComboType.ctChListChannelListShareType _
               Or udtComboType = gEnmComboType.ctChListChannelListAlmLevel _
               Or udtComboType = gEnmComboType.ctChListAnalogExtDeviceJACOM22DI _
               Or udtComboType = gEnmComboType.ctChListAnalogExtDeviceJACOM22AI _
               Or udtComboType = gEnmComboType.ctChListAnalogExtDeviceJACOM22DO _
               Or udtComboType = gEnmComboType.ctChListAnalogExtDeviceMODBUSDI _
               Or udtComboType = gEnmComboType.ctChListAnalogExtDeviceMODBUSAI Then

                If gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then     '和文仕様 20200219 hori
                    Return gCstIniFileNameComboChListJpn
                Else
                    Return gCstIniFileNameComboChList
                End If
            End If

            If udtComboType = gEnmComboType.ctChListChannelListDeviceStatus Then
                If gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then '全和文仕様 20200603 hori
                    Return gCstIniFileNameListChSystemJpn
                Else
                    Return gCstIniFileNameListChSystem
                End If
            End If

                If udtComboType = gEnmComboType.ctChTerminalListColumn _
                    Or udtComboType = gEnmComboType.ctChTerminalListColumn3 _
                    Or udtComboType = gEnmComboType.ctChTerminalListTerminalNo Then
                    Return gCstIniFileNameComboChTerminalList
                End If

                'Ver.2.0.8.P追加
                If udtComboType = gEnmComboType.ctChOutputDoTermType1 _
                    Or udtComboType = gEnmComboType.ctChOutputDoTermType2 Then
                    Return gCstIniFileNameComboChTerminalList
                End If

                If udtComboType = gEnmComboType.ctChTerminalFunctionFuncName _
                    Or udtComboType = gEnmComboType.ctChTerminalFunctionFuncDI _
                    Or udtComboType = gEnmComboType.ctChTerminalFunctionFuncDO Then
                    Return gCstIniFileNameComboChTerminalFunction
                End If

                If udtComboType = gEnmComboType.ctChOutputDoChOutType _
                    Or udtComboType = gEnmComboType.ctChOutputDoStatus Then
                    Return gCstIniFileNameComboChOutputDo
                End If

                If udtComboType = gEnmComboType.ctChGroupReposeListColumn2 Then
                    Return gCstIniFileNameComboChGroupReposeList
                End If


                If udtComboType = gEnmComboType.ctChRunHourColumn3 Then
                    Return gCstIniFileNameComboChRunHour
                End If

                If udtComboType = gEnmComboType.ctChExhGusGroupcmbNo Then
                    Return gCstIniFileNameComboChExhGusGroup
                End If

                If udtComboType = gEnmComboType.ctChCtrlUseNotuseDetail _
                    Or udtComboType = gEnmComboType.ctChCtrlUseNotuseDetailgrd Then
                    Return gCstIniFileNameComboChCtrlUseNotuseDetail
                End If

                If udtComboType = gEnmComboType.ctChDataForwardTableListColumn1 Then
                    Return gCstIniFileNameComboChDataForwardTableList
                End If
                'If udtComboType = gEnmComboType.ctChDataForwardTableListColumn7 Then
                '    Return gCstIniFileNameComboChDataForwardTableList
                'End If

                If udtComboType = gEnmComboType.ctChDataSaveTableListColumn3 Then
                    Return gCstIniFileNameComboChDataSaveTableList
                End If

                If udtComboType = gEnmComboType.ctChSioDetailcmbCom _
                    Or udtComboType = gEnmComboType.ctChSioDetailcmbCommType1 _
                    Or udtComboType = gEnmComboType.ctChSioDetailcmbCommType2 _
                    Or udtComboType = gEnmComboType.ctChSioDetailcmbDataBit _
                    Or udtComboType = gEnmComboType.ctChSioDetailcmbDuplet _
                    Or udtComboType = gEnmComboType.ctChSioDetailcmbMC _
                    Or udtComboType = gEnmComboType.ctChSioDetailcmbParityBit _
                    Or udtComboType = gEnmComboType.ctChSioDetailcmbPort _
                    Or udtComboType = gEnmComboType.ctChSioDetailcmbPriority _
                    Or udtComboType = gEnmComboType.ctChSioDetailcmbStopBit _
                    Or udtComboType = gEnmComboType.ctChSioDetailcmbTransmisionCh Then
                    Return gCstIniFileNameComboChSioDetail
                End If


                '========================
                ' 延長警報設定画面
                '========================
                ''Ext：System Set
                If udtComboType = gEnmComboType.ctExtPnlSystem Then
                    Return gCstIniFileNameComboExtPnlSystem
                End If

                ''Ext：EXT Panel Set
                If udtComboType = gEnmComboType.ctExtPnlListWh _
                    Or udtComboType = gEnmComboType.ctExtPnlListPr _
                    Or udtComboType = gEnmComboType.ctExtPnlListCe _
                    Or udtComboType = gEnmComboType.ctExtPnlListEngineerCall _
                    Or udtComboType = gEnmComboType.ctExtPnlListNo _
                    Or udtComboType = gEnmComboType.ctExtPnlListDutyNo _
                    Or udtComboType = gEnmComboType.ctExtPnlListReAlm _
                    Or udtComboType = gEnmComboType.ctExtPnlListBzCut _
                    Or udtComboType = gEnmComboType.ctExtPnlListFree _
                    Or udtComboType = gEnmComboType.ctExtPnlListLedLcd Then
                    Return gCstIniFileNameComboExtPnlList
                End If

                ''Ext：LED
                If udtComboType = gEnmComboType.ctExtPnlLedEngNo _
                    Or udtComboType = gEnmComboType.ctExtPnlLedDutyNo _
                    Or udtComboType = gEnmComboType.ctExtPnlLedWatchLED Then
                    Return gCstIniFileNameComboExtPnlLed
                End If

                ''Ext：LCD EXT Group
                If udtComboType = gEnmComboType.ctExtPnlLcdGroupGrpNo _
                    Or udtComboType = gEnmComboType.ctExtPnlLcdGroupMarkNo Then
                    Return gCstIniFileNameComboExtPnlLcdGroup
                End If

                ''Term：Test Output Set
                'If udtComboType = gEnmComboType.ctExtTermTestBz _
                '    Or udtComboType = gEnmComboType.ctExtTermTestLamp _
                '    Or udtComboType = gEnmComboType.ctExtTermTestFunc Then
                '    Return gCstIniFileNameComboExtTermTest
                'End If

                ''Timer
                If udtComboType = gEnmComboType.ctExtTimerPart _
                    Or udtComboType = gEnmComboType.ctExtTimerType _
                    Or udtComboType = gEnmComboType.ctExtTimerTimeDisp Then     '' パート追加　ver.1.4.4 2012.05.08
                    Return gCstIniFileNameComboExtTimer
                End If


                '========================
                ' シーケンス設定
                '========================

                If udtComboType = gEnmComboType.ctSeqLineTableNo Then
                    Return gCstIniFileNameComboSeqLine
                End If

                If udtComboType = gEnmComboType.ctSeqOpeTableNo _
                Or udtComboType = gEnmComboType.ctSeqOpeFixedType Then
                    Return gCstIniFileNameComboSeqOpe
                End If

                ''シーケンス設定詳細画面
                If udtComboType = gEnmComboType.ctSeqSetDetailStatus _
                Or udtComboType = gEnmComboType.ctSeqSetDetailDataType _
                Or udtComboType = gEnmComboType.ctSeqSetDetailOutputType _
                Or udtComboType = gEnmComboType.ctSeqSetDetailFucFu Then
                    Return gCstIniFileNameComboSeqSetDetail
                End If

                If udtComboType = gEnmComboType.ctSeqSetDetailLogic Then
                    Return gCstIniFileNameListSeqLogic
                End If

                '========================
                ' OPSSET
                '========================

                If udtComboType = gEnmComboType.ctOpsScreenTitle Then
                    If gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then '全和文仕様 20200603 hori
                        Return gCstIniFileNameListOpsScreenTitleJpn
                    Else
                        Return gCstIniFileNameListOpsScreenTitle
                    End If
                End If

                If udtComboType = gEnmComboType.ctOpsSelectionFunctionList _
                    Or udtComboType = gEnmComboType.ctOpsSelectionFunctionSet _
                    Or udtComboType = gEnmComboType.ctOpsSelectionSelectionDefault _
                    Or udtComboType = gEnmComboType.ctOpsSelectionSelectionDisableList Then

                    If gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then '全和文仕様 20200603 hori
                        Return gCstIniFileNameListOpsSelectionMenuJpn
                    Else
                        Return gCstIniFileNameListOpsSelectionMenu
                    End If
                End If


                If udtComboType = gEnmComboType.ctOpsPulldownColumn1 _
                Or udtComboType = gEnmComboType.ctOpsPulldownColumn2 Then
                    If gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then '全和文仕様 20200603 hori
                        Return gCstIniFileNameComboOpsPulldownJpn
                    Else
                        Return gCstIniFileNameComboOpsPulldown
                    End If
                End If

                If udtComboType = gEnmComboType.ctOpsGraphListColumn2 Then
                    Return gCstIniFileNameComboOpsGraphList
                End If

                ''偏差グラフ（排ガス）
                'If udtComboType = gEnmComboType.ctOpsGraphExhaustLimit Then
                '    Return gCstIniFileNameComboOpsGraphExhaust
                'End If

                ''アナログメーター
                If udtComboType = gEnmComboType.ctOpsGraphAnalogDispType Then
                    Return gCstIniFileNameComboOpsGraphAnalogMeter
                End If

                ''アナログメーター設定（詳細画面）
                If udtComboType = gEnmComboType.ctOpsGraphAnalogDetailChNameDispPoint _
                Or udtComboType = gEnmComboType.ctOpsGraphAnalogDetailMarkNumValue _
                Or udtComboType = gEnmComboType.ctOpsGraphAnalogDetailPointerFrame _
                Or udtComboType = gEnmComboType.ctOpsGraphAnalogDetailPointerColor _
                Or udtComboType = gEnmComboType.ctOpsGraphAnalogDetailSideColor Then
                    Return gCstIniFileNameComboOpsGraphAnalogMeterDetail
                End If

                '========================
                ' Log Format
                '========================
                If udtComboType = gEnmComboType.ctOpsLogFormatListColumn1Type _
                Or udtComboType = gEnmComboType.ctOpsLogFormatListColumn2Type _
                Or udtComboType = gEnmComboType.ctOpsLogFormatListSaveStrings Then
                    Return gCstIniFileNameComboOpsLogFormat
                End If

                '========================
                ' Print
                '========================
                'If udtComboType = gEnmComboType.ctPrtLocalCanbus Then
                '    Return gCstIniFileNameComboPrtLocalUnit
                'End If

                Return strRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return ""
        End Try

    End Function

#End Region

#Region "コード＆名称取得"

    '----------------------------------------------------------------------------
    ' 機能説明  ： コンボ名称取得
    ' 引数      ： ARG1 (I ) コンボコード
    ' 　　      ： ARG2 (I ) コード＆名称構造体
    ' 戻値      ： コンボ名称
    '----------------------------------------------------------------------------
    Public Function gGetComboItemName(ByVal intCode As Integer, _
                                      ByVal udtComboType As gEnmComboType, _
                             Optional ByVal strSub As String = "", _
                             Optional ByVal blnShowMassage As Boolean = True) As String

        Try

            Dim udtCodeName() As gTypCodeName = Nothing

            ''コンボアイテム取得
            If gGetComboCodeName(udtCodeName, udtComboType, strSub, blnShowMassage) <> 0 Then
                Return ""
            End If

            ''構造体がない場合
            If udtCodeName Is Nothing Then
                Return ""
            End If

            For i As Integer = LBound(udtCodeName) To UBound(udtCodeName)

                With udtCodeName(i)

                    If .shtCode = intCode Then
                        Return .strName
                    End If

                End With

            Next

            Return ""

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return ""
        End Try

    End Function

    '----------------------------------------------------------------------------
    ' 機能説明  ： コンボコード取得
    ' 引数      ： ARG1 (I ) コンボ名称
    ' 　　      ： ARG2 (I ) コード＆名称構造体
    ' 戻値      ： シーケンスロジックコード
    '----------------------------------------------------------------------------
    Public Function gGetComboItemCode(ByVal strName As String, _
                                      ByVal udtComboType As gEnmComboType, _
                             Optional ByVal strSub As String = "", _
                             Optional ByVal blnShowMassage As Boolean = True) As String

        Try

            Dim udtCodeName() As gTypCodeName = Nothing

            ''コンボアイテム取得
            If gGetComboCodeName(udtCodeName, udtComboType, strSub, blnShowMassage) <> 0 Then
                Return ""
            End If

            ''構造体がない場合
            If udtCodeName Is Nothing Then
                Return ""
            End If

            For i As Integer = LBound(udtCodeName) To UBound(udtCodeName)

                With udtCodeName(i)

                    If .strName = strName Then
                        Return .shtCode
                    End If

                End With

            Next

            Return ""

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return ""
        End Try

    End Function

    '----------------------------------------------------------------------------
    ' 機能説明  ： コード＆名称取得
    ' 戻り値      ： 0:成功、<>0:失敗
    ' 引き数      ： ARG1 - ( O) コード＆名称構造体
    ' 機能詳細    ： 
    '----------------------------------------------------------------------------
    Public Function gGetComboCodeName(ByRef udtCodeName() As gTypCodeName, _
                                      ByVal udtComboType As gEnmComboType, _
                             Optional ByVal strSub As String = "", _
                             Optional ByVal blnShowMassage As Boolean = True) As Integer

        Try

            Dim intCnt As Integer
            Dim strIniFilePath As String = ""
            Dim strIniFileName As String = ""
            Dim strSectionName As String = ""
            Dim strwk() As String = Nothing

            ''iniファイル名取得
            strIniFileName = mGetIniFileName(udtComboType)

            ''iniファイルパス作成
            strIniFilePath = System.IO.Path.Combine(gGetDirNameIniFile, strIniFileName)

            ''ファイル存在確認
            If Not System.IO.File.Exists(strIniFilePath) Then
                Call MessageBox.Show("Under '" & gCstIniFileDir & "' Folder, There is no '" & strIniFileName & "' File.", _
                                     "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return Not 0
            End If

            ''セクション名取得
            strSectionName = mGetSectionName(udtComboType, strSub)

            Dim strBuffer As New System.Text.StringBuilder
            strBuffer.Capacity = 256   'バッファのサイズを指定


            intCnt = 1
            Erase udtCodeName
            Do
                ''iniファイルから値取得
                If intCnt < 100 Then
                    Call GetPrivateProfileString(strSectionName, "Item" & intCnt.ToString("00"), "", strBuffer, strBuffer.Capacity, strIniFilePath)
                Else
                    Call GetPrivateProfileString(strSectionName, "Item" & intCnt.ToString, "", strBuffer, strBuffer.Capacity, strIniFilePath)
                End If

                ''値が取得出来なかった場合は処理を抜ける
                If strBuffer.ToString() = "" Then Exit Do

                ''「,」区切りの文字列取得
                Erase strwk
                strwk = strBuffer.ToString.Split(",")

                ''配列再定義
                ReDim Preserve udtCodeName(intCnt - 1)

                ''配列に格納
                udtCodeName(intCnt - 1).shtCode = strwk(0)
                udtCodeName(intCnt - 1).strName = strwk(1)

                ''オプション設定がある場合
                Select Case UBound(strwk)
                    Case 2
                        udtCodeName(intCnt - 1).strOption1 = strwk(2)
                    Case 3
                        udtCodeName(intCnt - 1).strOption1 = strwk(2)
                        udtCodeName(intCnt - 1).strOption2 = strwk(3)
                    Case 4
                        udtCodeName(intCnt - 1).strOption1 = strwk(2)
                        udtCodeName(intCnt - 1).strOption2 = strwk(3)
                        udtCodeName(intCnt - 1).strOption3 = strwk(4)
                    Case 5
                        udtCodeName(intCnt - 1).strOption1 = strwk(2)
                        udtCodeName(intCnt - 1).strOption2 = strwk(3)
                        udtCodeName(intCnt - 1).strOption3 = strwk(4)
                        udtCodeName(intCnt - 1).strOption4 = strwk(5)
                    Case 6
                        udtCodeName(intCnt - 1).strOption1 = strwk(2)
                        udtCodeName(intCnt - 1).strOption2 = strwk(3)
                        udtCodeName(intCnt - 1).strOption3 = strwk(4)
                        udtCodeName(intCnt - 1).strOption4 = strwk(5)
                        udtCodeName(intCnt - 1).strOption5 = strwk(6)
                End Select

                ''カウントアップ
                intCnt += 1

            Loop

            ''項目が取得できなかった場合
            If intCnt = 1 Then
                If blnShowMassage Then
                    Call MessageBox.Show("Under '" & strIniFileName & "' File, There is no '" & strSectionName & "' Section.", _
                                         "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
                Return Not 0
            End If

            Return 0

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    ''----------------------------------------------------------------------------
    '' 機能説明  ： シーケンスロジック取得
    '' 引数      ： ARG1 ( O) シーケンスロジック構造体
    '' 戻値      ： 0:成功、<>0:失敗
    ''----------------------------------------------------------------------------
    'Public Function gGetSecLogic(ByRef udtCodeName() As gTypCodeName) As Integer

    '    Dim intCnt As Integer
    '    Dim strIniFilePath As String = ""
    '    Dim strIniFileName As String = ""
    '    Dim strSectionName As String = ""
    '    Dim strwk() As String = Nothing

    '    ''iniファイル名取得
    '    strIniFileName = gCstIniFileNameListSeqLogic

    '    ''iniファイルパス作成
    '    strIniFilePath = System.IO.Path.Combine(mGetIniFileDir, strIniFileName)

    '    ''ファイル存在確認
    '    If Not System.IO.File.Exists(strIniFilePath) Then
    '        Call MessageBox.Show("Under '" & gCstIniFileDir & "' Folder, There is no '" & strIniFileName & "' File.", _
    '                             "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Return Not 0
    '    End If

    '    ''セクション名取得
    '    strSectionName = gCstIniFileNameListSeqLogicSection

    '    Dim strBuffer As New System.Text.StringBuilder
    '    strBuffer.Capacity = 256   'バッファのサイズを指定

    '    intCnt = 1
    '    Erase udtCodeName
    '    Do
    '        ''iniファイルから値取得
    '        If intCnt < 100 Then
    '            Call GetPrivateProfileString(strSectionName, "Item" & intCnt.ToString("00"), "", strBuffer, strBuffer.Capacity, strIniFilePath)
    '        Else
    '            Call GetPrivateProfileString(strSectionName, "Item" & intCnt.ToString, "", strBuffer, strBuffer.Capacity, strIniFilePath)
    '        End If

    '        ''値が取得出来なかった場合は処理を抜ける
    '        If strBuffer.ToString() = "" Then Exit Do

    '        ''「,」区切りの文字列取得
    '        Erase strwk
    '        strwk = strBuffer.ToString.Split(",")

    '        ''配列再定義
    '        ReDim Preserve udtCodeName(intCnt - 1)

    '        ''配列に格納
    '        udtCodeName(intCnt - 1).shtCode = strwk(0)
    '        udtCodeName(intCnt - 1).strName = strwk(1)

    '        ''カウントアップ
    '        intCnt += 1

    '    Loop

    '    ''項目が取得できなかった場合
    '    If intCnt = 1 Then
    '        Call MessageBox.Show("Under '" & strIniFileName & "' File, There is no '" & strSectionName & "' Section.", _
    '                             "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Return Not 0
    '    End If

    '    Return 0

    'End Function

    ''----------------------------------------------------------------------------
    '' 機能説明  ： シーケンスロジック名称取得
    '' 引数      ： ARG1 (I ) シーケンスロジックコード
    '' 　　      ： ARG2 (I ) シーケンスロジック構造体
    '' 戻値      ： シーケンスロジック名称
    ''----------------------------------------------------------------------------
    'Public Function gGetSecLogicName(ByVal intSeqLogicCode As Integer, _
    '                                 ByVal udtSeqLogic() As gTypCodeName) As String

    '    ''シーケンスロジック構造体がない場合
    '    If udtSeqLogic Is Nothing Then
    '        Return ""
    '    End If

    '    For i As Integer = LBound(udtSeqLogic) To UBound(udtSeqLogic)

    '        With udtSeqLogic(i)

    '            If .shtCode = intSeqLogicCode Then
    '                Return .strName
    '            End If

    '        End With

    '    Next

    '    Return ""

    'End Function

    ''----------------------------------------------------------------------------
    '' 機能説明  ： シーケンスロジックコード取得
    '' 引数      ： ARG1 (I ) シーケンスロジック名称
    '' 　　      ： ARG2 (I ) シーケンスロジック構造体
    '' 戻値      ： シーケンスロジックコード
    ''----------------------------------------------------------------------------
    'Public Function gGetSecLogicCode(ByVal strSeqLogicName As String, _
    '                                 ByVal udtSeqLogic() As gTypCodeName) As String

    '    ''シーケンスロジック構造体がない場合
    '    If udtSeqLogic Is Nothing Then
    '        Return ""
    '    End If

    '    For i As Integer = LBound(udtSeqLogic) To UBound(udtSeqLogic)

    '        With udtSeqLogic(i)

    '            If .strName = strSeqLogicName Then
    '                Return .shtCode
    '            End If

    '        End With

    '    Next

    '    Return ""

    'End Function

#End Region

#Region "iniファイルライン情報取得"

    '--------------------------------------------------------------------
    ' 機能      : iniファイルライン情報取得
    ' 返り値    : 0:成功、<>0:失敗
    ' 引き数    : ARG1 - ( O) iniファイルライン情報
    ' 　　　    : ARG2 - (I ) コンボボックスタイプ
    ' 　　　    : ARG3 - (I ) サブコード
    ' 　　　    : ARG4 - (I ) メッセージ表示フラグ
    ' 機能説明  : iniファイルのライン情報を取得する
    '--------------------------------------------------------------------
    Public Function gGetIniFileLine(ByRef strLine() As String, _
                                    ByVal udtComboType As gEnmComboType, _
                           Optional ByVal strSub As String = "", _
                           Optional ByVal blnShowMassage As Boolean = True) As Integer

        Try

            Dim intCnt As Integer
            Dim strIniFilePath As String = ""
            Dim strIniFileName As String = ""
            Dim strSectionName As String = ""
            Dim strwk() As String = Nothing

            ''iniファイル名取得
            strIniFileName = mGetIniFileName(udtComboType)

            ''iniファイルパス作成
            strIniFilePath = System.IO.Path.Combine(gGetDirNameIniFile, strIniFileName)

            ''ファイル存在確認
            If Not System.IO.File.Exists(strIniFilePath) Then
                Call MessageBox.Show("Under '" & gCstIniFileDir & "' Folder, There is no '" & strIniFileName & "' File.", _
                                     "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return Not 0
            End If

            ''セクション名取得
            strSectionName = mGetSectionName(udtComboType, strSub)

            Dim strBuffer As New System.Text.StringBuilder
            strBuffer.Capacity = 256   'バッファのサイズを指定

            intCnt = 1
            Erase strLine
            Do
                ''iniファイルから値取得
                If intCnt < 100 Then
                    Call GetPrivateProfileString(strSectionName, "Item" & intCnt.ToString("00"), "", strBuffer, strBuffer.Capacity, strIniFilePath)
                Else
                    Call GetPrivateProfileString(strSectionName, "Item" & intCnt.ToString, "", strBuffer, strBuffer.Capacity, strIniFilePath)
                End If

                ''値が取得出来なかった場合は処理を抜ける
                If strBuffer.ToString() = "" Then Exit Do

                ''配列再定義
                ReDim Preserve strLine(intCnt - 1)

                strLine(intCnt - 1) = strBuffer.ToString

                ''カウントアップ
                intCnt += 1

            Loop

            ''項目が取得できなかった場合
            If intCnt = 1 Then
                If blnShowMassage Then
                    Call MessageBox.Show("Under '" & strIniFileName & "' File, There is no '" & strSectionName & "' Section.", _
                                         "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
                Return Not 0
            End If

            Return 0

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "OPSスクリーンタイトル"

    ''----------------------------------------------------------------------------
    '' 機能説明  ： OPSスクリーンタイトル取得
    '' 引数      ： ARG1 ( O) OPSスクリーンタイトル構造体
    '' 戻値      ： 0:成功、<>0:失敗
    ''----------------------------------------------------------------------------
    'Public Function gGetOpsScreenTitle(ByRef udtCodeName() As gTypCodeName) As Integer

    '    Dim intCnt As Integer
    '    Dim strIniFilePath As String = ""
    '    Dim strIniFileName As String = ""
    '    Dim strSectionName As String = ""
    '    Dim strwk() As String = Nothing

    '    ''iniファイル名取得
    '    strIniFileName = gCstIniFileNameListOpsScreenTitle

    '    ''iniファイルパス作成
    '    strIniFilePath = System.IO.Path.Combine(mGetIniFileDir, strIniFileName)

    '    ''ファイル存在確認
    '    If Not System.IO.File.Exists(strIniFilePath) Then
    '        Call MessageBox.Show("Under '" & gCstIniFileDir & "' Folder, There is no '" & strIniFileName & "' File.", _
    '                             "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Return Not 0
    '    End If

    '    ''セクション名取得
    '    strSectionName = gCstIniFileNameListOpsScreenTitleSection

    '    Dim strBuffer As New System.Text.StringBuilder
    '    strBuffer.Capacity = 256   'バッファのサイズを指定

    '    intCnt = 1
    '    Erase udtCodeName
    '    Do
    '        ''iniファイルから値取得
    '        If intCnt < 100 Then
    '            Call GetPrivateProfileString(strSectionName, "Item" & intCnt.ToString("00"), "", strBuffer, strBuffer.Capacity, strIniFilePath)
    '        Else
    '            Call GetPrivateProfileString(strSectionName, "Item" & intCnt.ToString, "", strBuffer, strBuffer.Capacity, strIniFilePath)
    '        End If

    '        ''値が取得出来なかった場合は処理を抜ける
    '        If strBuffer.ToString() = "" Then Exit Do

    '        ''「,」区切りの文字列取得
    '        Erase strwk
    '        strwk = strBuffer.ToString.Split(",")

    '        ''配列再定義
    '        ReDim Preserve udtCodeName(intCnt - 1)

    '        ''配列に格納
    '        udtCodeName(intCnt - 1).shtCode = strwk(0)
    '        udtCodeName(intCnt - 1).strName = strwk(1)

    '        ''カウントアップ
    '        intCnt += 1

    '    Loop

    '    ''項目が取得できなかった場合
    '    If intCnt = 1 Then
    '        Call MessageBox.Show("Under '" & strIniFileName & "' File, There is no '" & strSectionName & "' Section.", _
    '                             "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Return Not 0
    '    End If

    '    Return 0

    'End Function

#End Region

End Module
