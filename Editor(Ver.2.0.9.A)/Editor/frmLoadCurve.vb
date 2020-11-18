Imports Microsoft.VisualBasic
Imports System.Runtime.InteropServices
Imports System.IO

Public Class frmLoadCurve

#Region "変数定義"

    '"T311.com"のパス
    Private strPathBase As String = ""

    '構造体のデータ数
    Private LoadCrvGrpDatLen As Integer = 96 - 1
    '全体のデータ数
    Private LoadCrvAllDatLen As Integer = 384 - 1

    'ファイル読み書き用の2次元配列(4グループ×96項目 = 384データ)     
    Private bytData(UBound(udtLoadCurve.CrvSet), LoadCrvGrpDatLen) As Byte

    'ファイル書き込み用1次元配列
    <VBFixedArray(383)> Private bytDataWrite() As Byte

    'T311.comの初期化用データ配列
    '負荷曲線設定用Excelファイルに基づく
    Private bytDataInit() As Byte = { _
        0, 0, 0, 0, 3, 0, 0, 0, 80, 0, 100, 0, 0, 0, 0, 0, _
        0, 0, 0, 0, 0, 0, 0, 0, 10, 5, 0, 0, 0, 0, 0, 0, _
        0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, _
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, _
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, _
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0} 'データ数：96個

    'テキストボックス、コンボボックスの配列
    Dim TxtBx1DialPos() As TextBox
    Dim TxtBxDrwRangeLow() As TextBox
    Dim TxtBxDrwRangeHigh() As TextBox
    Dim TxtBxVrtRangeLow() As TextBox
    Dim TxtBxVrtRangeHigh() As TextBox
    Dim TxtBxWidRangeLow() As TextBox
    Dim TxtBxWidRangeHigh() As TextBox
    Dim TxtBxVrtCH() As TextBox
    Dim TxtBxWidCH() As TextBox
    Dim TxtBx2DialPos() As TextBox
    Dim TxtBxPower() As TextBox
    Dim TxtBxCoef() As TextBox
    Dim TxtBxStbdRep() As TextBox
    Dim TxtBxPortRep() As TextBox
    Dim CmbBx1DispFlg() As ComboBox
    Dim CmbBx1LineColor() As ComboBox
    Dim CmbBx2DispFlg() As ComboBox
    Dim CmbBx2LineColor() As ComboBox

#End Region


#Region "画面イベント"

    '--------------------------------------------------------------------
    ' 機能      : フォームロード
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 画面表示初期処理を行う
    '--------------------------------------------------------------------
    Private Sub frmLoadCurve_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            strPathBase = System.IO.Path.Combine(gudtFileInfo.strFilePath, gudtFileInfo.strFileVersion) '選択中のファイル情報
            strPathBase = System.IO.Path.Combine(strPathBase, gCstFolderNameSave)                       'Saveパス
            strPathBase = System.IO.Path.Combine(strPathBase, gCstFolderNameMimic)                      'mimicパス
            strPathBase = System.IO.Path.Combine(strPathBase, "Mimic1\")                                'Mimic1パス


            'T311.comが存在しない場合(初回)、構造体に初期値を格納する
            If System.IO.File.Exists(strPathBase & "T311.com") = False Then
                Call InitStructure(udtLoadCurve)

            Else
                'T311.comが存在する場合、T311.comを読み取りで開く
                Dim FRead As Stream = File.OpenRead(strPathBase & "T311.com")
                Using Reader As New BinaryReader(FRead)
                    'ファイル読み込み結果を二次元配列"bytData"へ退避
                    For i = 0 To UBound(udtLoadCurve.CrvSet)
                        For j = 0 To LoadCrvGrpDatLen
                            bytData(i, j) = Reader.ReadByte()
                        Next
                    Next
                End Using
                FRead.Close()

                'ファイル読み込み結果を構造体へ格納
                For i = 0 To UBound(udtLoadCurve.CrvSet)
                    With udtLoadCurve.CrvSet(i)
                        .AlmNoStbd01 = bytData(i, 0) + (bytData(i, 1) * 256)  '2バイト分のデータは足して1つのデータと置き換える
                        .CrvFlg01 = bytData(i, 2)
                        .CrvColr01 = bytData(i, 3)
                        .CrvWdh01 = bytData(i, 4)
                        .Spare11 = bytData(i, 5)
                        .ChNoDialPos01 = bytData(i, 6) + (bytData(i, 7) * 256)
                        .CrvRangeMin = bytData(i, 8) + (bytData(i, 9) * 256)
                        .CrvRangeMax = bytData(i, 10) + (bytData(i, 11) * 256)
                        .CrvDrawMin = bytData(i, 12) + (bytData(i, 13) * 256)
                        .CrvDrawMax = bytData(i, 14) + (bytData(i, 15) * 256)
                        .CrvVertRangeHigh = bytData(i, 16) + (bytData(i, 17) * 256)
                        .CrvVertRangeLow = bytData(i, 18) + (bytData(i, 19) * 256)
                        .CrvWidhRangeHigh = bytData(i, 20) + (bytData(i, 21) * 256)
                        .CrvWidhRangeLow = bytData(i, 22) + (bytData(i, 23) * 256)
                        .RangeSplit = bytData(i, 24)
                        .Spare12 = bytData(i, 25)
                        .CrvDispFlg02 = bytData(i, 26)
                        .Spare21 = bytData(i, 27)
                        .OutputChNoPower02 = bytData(i, 28) + (bytData(i, 29) * 256)
                        .OutputChNoSpeed02 = bytData(i, 30) + (bytData(i, 31) * 256)
                        .OutputNrmlNo02 = bytData(i, 32) + (bytData(i, 33) * 256)
                        .OutputAlmNo02 = bytData(i, 34) + (bytData(i, 35) * 256)
                        .OutputNrmlColr02 = bytData(i, 36)
                        .OutputAlmColr02 = bytData(i, 37)
                        .CrvFlg02 = bytData(i, 38)
                        .CrvColr02 = bytData(i, 39)
                        .CrvWdh02 = bytData(i, 40)
                        .Spare22 = bytData(i, 41)
                        .ChNoDialPos02 = bytData(i, 42) + (bytData(i, 43) * 256)
                        .Power = bytData(i, 44) + (bytData(i, 45) * 256)
                        .Coeff = bytData(i, 46) + (bytData(i, 47) * 256)
                        .Spare23(0) = bytData(i, 48)
                        .Spare23(1) = bytData(i, 49)
                        .CrvDispFlg03 = bytData(i, 50)
                        .Spare31 = bytData(i, 51)
                        .OutputChNoPower03 = bytData(i, 52) + (bytData(i, 53) * 256)
                        .OutputChNoSpeed03 = bytData(i, 54) + (bytData(i, 55) * 256)
                        .OutputNrmlNo03 = bytData(i, 56) + (bytData(i, 57) * 256)
                        .OutputAlmNo03 = bytData(i, 58) + (bytData(i, 59) * 256)
                        .OutputNrmlColr03 = bytData(i, 60)
                        .OutputAlmColr03 = bytData(i, 61)
                        .Spare32(0) = bytData(i, 62)
                        .Spare32(1) = bytData(i, 63)
                        .CrvDispFlg04 = bytData(i, 64)
                        .Spare41 = bytData(i, 65)
                        .OutputChNoPower04 = bytData(i, 66) + (bytData(i, 67) * 256)
                        .OutputChNoSpeed04 = bytData(i, 68) + (bytData(i, 69) * 256)
                        .OutputNrmlNo04 = bytData(i, 70) + (bytData(i, 71) * 256)
                        .OutputAlmNo04 = bytData(i, 72) + (bytData(i, 73) * 256)
                        .OutputNrmlColr04 = bytData(i, 74)
                        .OutputAlmColr04 = bytData(i, 75)
                        .RepsChNoStbd = bytData(i, 76) + (bytData(i, 77) * 256)
                        .RepsChNoPort = bytData(i, 78) + (bytData(i, 79) * 256)
                        For j = 0 To UBound(udtLoadCurve.CrvSet(i).Spare)
                            .Spare(j) = bytData(i, 80 + j)
                        Next
                    End With
                Next
            End If

            'テキストボックスの配列定義
            Me.TxtBx1DialPos = New TextBox() {Me.TxtBx1DialPos1, Me.TxtBx1DialPos2, Me.TxtBx1DialPos3, Me.TxtBx1DialPos4}
            Me.TxtBxDrwRangeLow = New TextBox() {Me.TxtBxDrwRangeLow1, Me.TxtBxDrwRangeLow2, Me.TxtBxDrwRangeLow3, Me.TxtBxDrwRangeLow4}
            Me.TxtBxDrwRangeHigh = New TextBox() {Me.TxtBxDrwRangeHigh1, Me.TxtBxDrwRangeHigh2, Me.TxtBxDrwRangeHigh3, Me.TxtBxDrwRangeHigh4}
            Me.TxtBxVrtRangeLow = New TextBox() {Me.TxtBxVrtRangeLow1, Me.TxtBxVrtRangeLow2, Me.TxtBxVrtRangeLow3, Me.TxtBxVrtRangeLow4}
            Me.TxtBxVrtRangeHigh = New TextBox() {Me.TxtBxVrtRangeHigh1, Me.TxtBxVrtRangeHigh2, Me.TxtBxVrtRangeHigh3, Me.TxtBxVrtRangeHigh4}
            Me.TxtBxWidRangeLow = New TextBox() {Me.TxtBxWidRangeLow1, Me.TxtBxWidRangeLow2, Me.TxtBxWidRangeLow3, Me.TxtBxWidRangeLow4}
            Me.TxtBxWidRangeHigh = New TextBox() {Me.TxtBxWidRangeHigh1, Me.TxtBxWidRangeHigh2, Me.TxtBxWidRangeHigh3, Me.TxtBxWidRangeHigh4}
            Me.TxtBxVrtCH = New TextBox() {Me.TxtBxVrtCH1, Me.TxtBxVrtCH2, Me.TxtBxVrtCH3, Me.TxtBxVrtCH4}
            Me.TxtBxWidCH = New TextBox() {Me.TxtBxWidCH1, Me.TxtBxWidCH2, Me.TxtBxWidCH3, Me.TxtBxWidCH4}
            Me.TxtBx2DialPos = New TextBox() {Me.TxtBx2DialPos1, Me.TxtBx2DialPos2, Me.TxtBx2DialPos3, Me.TxtBx2DialPos4}
            Me.TxtBxPower = New TextBox() {Me.TxtBxPower1, Me.TxtBxPower2, Me.TxtBxPower3, Me.TxtBxPower4}
            Me.TxtBxCoef = New TextBox() {Me.TxtBxCoef1, Me.TxtBxCoef2, Me.TxtBxCoef3, Me.TxtBxCoef4}
            Me.TxtBxStbdRep = New TextBox() {Me.TxtBxStbdRep1, Me.TxtBxStbdRep2, Me.TxtBxStbdRep3, Me.TxtBxStbdRep4}
            Me.TxtBxPortRep = New TextBox() {Me.TxtBxPortRep1, Me.TxtBxPortRep2, Me.TxtBxPortRep3, Me.TxtBxPortRep4}
            Me.CmbBx1DispFlg = New ComboBox() {Me.CmbBx1DispFlg1, Me.CmbBx1DispFlg2, Me.CmbBx1DispFlg3, Me.CmbBx1DispFlg4}
            Me.CmbBx1LineColor = New ComboBox() {Me.CmbBx1LineColor1, Me.CmbBx1LineColor2, Me.CmbBx1LineColor3, Me.CmbBx1LineColor4}
            Me.CmbBx2DispFlg = New ComboBox() {Me.CmbBx2DispFlg1, Me.CmbBx2DispFlg2, Me.CmbBx2DispFlg3, Me.CmbBx2DispFlg4}
            Me.CmbBx2LineColor = New ComboBox() {Me.CmbBx2LineColor1, Me.CmbBx2LineColor2, Me.CmbBx2LineColor3, Me.CmbBx2LineColor4}

            'テキストボックス表示値設定
            For i = 0 To UBound(udtLoadCurve.CrvSet)
                With udtLoadCurve.CrvSet(i)
                    TxtBx1DialPos(i).Text = .ChNoDialPos01
                    TxtBxDrwRangeLow(i).Text = .CrvDrawMin
                    TxtBxDrwRangeHigh(i).Text = .CrvDrawMax
                    TxtBxVrtRangeLow(i).Text = .CrvVertRangeLow
                    TxtBxVrtRangeHigh(i).Text = .CrvVertRangeHigh
                    TxtBxWidRangeLow(i).Text = .CrvWidhRangeLow
                    TxtBxWidRangeHigh(i).Text = .CrvWidhRangeHigh
                    TxtBxVrtCH(i).Text = .OutputChNoPower02
                    TxtBxWidCH(i).Text = .OutputChNoSpeed02
                    TxtBx2DialPos(i).Text = .ChNoDialPos02
                    TxtBxPower(i).Text = .Power
                    TxtBxCoef(i).Text = .Coeff
                    TxtBxStbdRep(i).Text = .RepsChNoStbd
                    TxtBxPortRep(i).Text = .RepsChNoPort
                    CmbBx1DispFlg(i).SelectedIndex = .CrvFlg01
                    CmbBx2DispFlg(i).SelectedIndex = .CrvFlg02
                    Select Case .CrvColr01
                        Case 5  '5=マゼンタ
                            CmbBx1LineColor(i).SelectedIndex = 1
                        Case 2  '1=グリーン
                            CmbBx1LineColor(i).SelectedIndex = 2
                        Case Else
                            CmbBx1LineColor(i).SelectedIndex = 0
                    End Select

                    Select Case .CrvColr02
                        Case 5  'マゼンタ
                            CmbBx2LineColor(i).SelectedIndex = 1
                        Case 2  'グリーン
                            CmbBx2LineColor(i).SelectedIndex = 2
                        Case Else
                            CmbBx2LineColor(i).SelectedIndex = 0
                    End Select

                End With
            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : テキストボックス入力チェック
    ' 返り値    : True:入力OK、False:入力NG
    ' 引き数    : なし
    ' 機能説明  : 入力チェックを行う
    '--------------------------------------------------------------------
    Private Function mChkInput() As Boolean

        Try

            For i = 0 To UBound(udtLoadCurve.CrvSet)
                If Not gChkInputNumLdCv(TxtBx1DialPos(i), 0, 9999, "Set CH (M/E Load Dial Position)1", i + 1) Then Return False
                If Not gChkInputNumLdCv(TxtBxDrwRangeLow(i), -30000, 30000, "Drawing Range - Low", i + 1) Then Return False
                If Not gChkInputNumLdCv(TxtBxDrwRangeHigh(i), -30000, 30000, "Drawing Range - High", i + 1) Then Return False
                If Not gChkInputNumLdCv(TxtBxVrtRangeLow(i), -30000, 30000, "Vertical Range - Low", i + 1) Then Return False
                If Not gChkInputNumLdCv(TxtBxVrtRangeHigh(i), -30000, 30000, "Vertical Range - High", i + 1) Then Return False
                If Not gChkInputNumLdCv(TxtBxWidRangeLow(i), -30000, 30000, "Width Range - Low", i + 1) Then Return False
                If Not gChkInputNumLdCv(TxtBxWidRangeHigh(i), -30000, 30000, "Width Range - High", i + 1) Then Return False
                If Not gChkInputNumLdCv(TxtBxVrtCH(i), 0, 9999, "Vertical CH (M/E Power)", i + 1) Then Return False
                If Not gChkInputNumLdCv(TxtBxWidCH(i), 0, 9999, "Width CH (M/E Speed)", i + 1) Then Return False
                If Not gChkInputNumLdCv(TxtBx2DialPos(i), 0, 9999, "Set CH (M/E Load Dial Position)2", i + 1) Then Return False
                If Not gChkInputNumLdCv(TxtBxPower(i), -30000, 30000, "Power (Rated Value[kW])", i + 1) Then Return False
                If Not gChkInputNumLdCv(TxtBxCoef(i), -30000, 30000, "Coefficient", i + 1) Then Return False
                If Not gChkInputNumLdCv(TxtBxStbdRep(i), 0, 9999, "Curve 1 (STBD) Repose", i + 1) Then Return False
                If Not gChkInputNumLdCv(TxtBxPortRep(i), 0, 9999, "Curve 2 (PORT) Repose", i + 1) Then Return False
            Next

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 共通入力チェック数値テキスト（テキストボックス用）
    ' 返り値    : True:入力OK、False:入力NG
    ' 引き数    : ARG1 - (I ) 入力テキスト
    ' 　　　    : ARG2 - (I ) 最小値
    ' 　　　    : ARG3 - (I ) 最大値
    ' 　　　    : ARG4 - (I ) 項目名称
    ' 　　　    : ARG5 - (I ) タブ番号
    ' 機能説明  : テキストボックスに入力されている数値の各種チェック処理を行う。
    '           　フォームにタブ機能を使用しているため、独自に関数を作成
    '--------------------------------------------------------------------
    Private Function gChkInputNumLdCv(ByVal txtInput As TextBox, _
                                      ByVal intMin As Integer, _
                                      ByVal intMax As Integer, _
                                      ByVal strName As String, _
                                      ByVal count As Integer) As Boolean
        Try

            ''入力が空文字の場合
            If Trim(txtInput.Text) = "" Then
                Call MessageBox.Show("Please input value. " & vbNewLine & vbNewLine & "Setting[" & count & "] [" & strName & "]", "Input error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Call txtInput.Focus()
                Return False
            End If

            ''数値入力チェック
            If Not IsNumeric(Trim(txtInput.Text)) Then
                Call MessageBox.Show("Please input the numerical value." & vbNewLine & vbNewLine & "Setting[" & count & "] [" & strName & "]", "Input error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Call txtInput.Focus()
                Return False
            End If

            ''数値が範囲内か
            If gChkTextNumSpan(intMin, intMax, Trim(txtInput.Text), True, "Setting[" & count & "] [" & strName & "]") Then
                Call txtInput.Focus()
                Return False
            End If

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : Saveボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 保存処理を行う
    '--------------------------------------------------------------------
    Private Sub cmdSave_Click_1(sender As System.Object, e As System.EventArgs) Handles cmdSave.Click

        Try
            '入力チェック
            If Not mChkInput() Then Return

            '元の構造体の値を、比較用構造体にコピー
            Call mCopyStructure(udtLoadCurve, mudtSetLoadCurveNew)

            'テキストボックスの設定値を比較用構造体に格納
            Call mSetStructure(mudtSetLoadCurveNew)

            'データが変更されているかチェック
            If Not mChkStructureEquals(udtLoadCurve, mudtSetLoadCurveNew) Then

                '変更された場合は設定を更新する
                Call mCopyStructure(mudtSetLoadCurveNew, udtLoadCurve)

                '構造体を2次元配列へ格納
                mSaveStructureToArray(udtLoadCurve)

                'ファイル書き込み用の1次元配列へデータを格納
                ReDim bytDataWrite(383)
                Dim k = 0
                For Each bytArrayData As Byte In bytData
                    bytDataWrite(k) = bytArrayData
                    k = k + 1
                Next

                'T311ファイルを更新
                Dim FSave As New System.IO.FileStream(strPathBase & "T311.com", System.IO.FileMode.Create, System.IO.FileAccess.Write)
                FSave.Write(bytDataWrite, 0, bytDataWrite.Length)
                FSave.Close()

                'メッセージ表示
                Call MessageBox.Show("It saved.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                '更新フラグ設定
                gblnUpdateAll = True
                gudt.SetEditorUpdateInfo.udtSave.bytSystem = 1
                gudt.SetEditorUpdateInfo.udtCompile.bytSystem = 1
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ：Exitボタン
    ' 引数      ：なし
    ' 戻値      ：なし
    ' 機能説明　：フォームを閉じる
    '----------------------------------------------------------------------------
    Private Sub cmdExit_Click_1(sender As System.Object, e As System.EventArgs) Handles cmdExit.Click

        Try

            Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : フォームクローズ中
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 設定が変更されている場合は確認メッセージを表示する
    '--------------------------------------------------------------------
    Private Sub frmLoadCurve_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

        Try

            '元の構造体の値を、比較用構造体にコピー
            Call mCopyStructure(udtLoadCurve, mudtSetLoadCurveNew)

            'テキストボックスの設定値を比較用構造体に格納
            Call mSetStructure(mudtSetLoadCurveNew)

            'データが変更されているかチェック
            If Not mChkStructureEquals(udtLoadCurve, mudtSetLoadCurveNew) Then

                ''変更されている場合はメッセージ表示
                Select Case MessageBox.Show("Setting has been changed." & vbNewLine & _
                                            "Do you save the changes?", Me.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

                    Case Windows.Forms.DialogResult.Yes

                        ''入力チェック
                        If Not mChkInput() Then
                            e.Cancel = True
                            Return
                        End If

                        '変更された場合は設定を更新する
                        Call mCopyStructure(mudtSetLoadCurveNew, udtLoadCurve)

                        '構造体を2次元配列へ格納
                        mSaveStructureToArray(udtLoadCurve)

                        'ファイル書き込み用の1次元配列へデータを格納
                        ReDim bytDataWrite(383)
                        Dim k = 0
                        For Each bytArrayData As Byte In bytData
                            bytDataWrite(k) = bytArrayData
                            k = k + 1
                        Next
                        'T311ファイルへ書き込み
                        Dim FSave As New System.IO.FileStream(strPathBase & "T311.com", System.IO.FileMode.Create, System.IO.FileAccess.Write)
                        FSave.Write(bytDataWrite, 0, bytDataWrite.Length)
                        FSave.Close()

                        ''更新フラグ設定
                        gblnUpdateAll = True
                        gudt.SetEditorUpdateInfo.udtSave.bytSystem = 1
                        gudt.SetEditorUpdateInfo.udtCompile.bytSystem = 1

                    Case Windows.Forms.DialogResult.No
                        ''何もしない

                    Case Windows.Forms.DialogResult.Cancel
                        ''画面を閉じない
                        e.Cancel = True

                End Select

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ：フォームクローズ
    ' 引数      ：なし
    ' 戻値      ：なし
    ' 機能説明　：フォームのインスタンスを破棄する
    '----------------------------------------------------------------------------
    Private Sub frmLoadCurve_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try

            Me.Dispose()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 負荷曲線初期設定値を構造体へ格納
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) システム設定構造体
    ' 機能説明  : 構造体に初期設定値を格納する
    '--------------------------------------------------------------------
    Private Sub InitStructure(ByRef udtInit As gTypLoadCurve)

        Try

            '読み込み結果を構造体へ格納
            For i = 0 To UBound(udtInit.CrvSet)
                With udtInit.CrvSet(i)
                    .AlmNoStbd01 = bytDataInit(0) + (bytDataInit(1) * 256)  '2バイト分のデータは足して1つのデータと置き換える
                    .CrvFlg01 = bytDataInit(2)
                    .CrvColr01 = bytDataInit(3)
                    .CrvWdh01 = bytDataInit(4)
                    .Spare11 = bytDataInit(5)
                    .ChNoDialPos01 = bytDataInit(6) + (bytDataInit(7) * 256)
                    .CrvRangeMin = bytDataInit(8) + (bytDataInit(9) * 256)
                    .CrvRangeMax = bytDataInit(10) + (bytDataInit(11) * 256)
                    .CrvDrawMin = bytDataInit(12) + (bytDataInit(13) * 256)
                    .CrvDrawMax = bytDataInit(14) + (bytDataInit(15) * 256)
                    .CrvVertRangeHigh = bytDataInit(16) + (bytDataInit(17) * 256)
                    .CrvVertRangeLow = bytDataInit(18) + (bytDataInit(19) * 256)
                    .CrvWidhRangeHigh = bytDataInit(20) + (bytDataInit(21) * 256)
                    .CrvWidhRangeLow = bytDataInit(22) + (bytDataInit(23) * 256)
                    .RangeSplit = bytDataInit(24)
                    .Spare12 = bytDataInit(25)
                    .CrvDispFlg02 = bytDataInit(26)
                    .Spare21 = bytDataInit(27)
                    .OutputChNoPower02 = bytDataInit(28) + (bytDataInit(29) * 256)
                    .OutputChNoSpeed02 = bytDataInit(30) + (bytDataInit(31) * 256)
                    .OutputNrmlNo02 = bytDataInit(32) + (bytDataInit(33) * 256)
                    .OutputAlmNo02 = bytDataInit(34) + (bytDataInit(35) * 256)
                    .OutputNrmlColr02 = bytDataInit(36)
                    .OutputAlmColr02 = bytDataInit(37)
                    .CrvFlg02 = bytDataInit(38)
                    .CrvColr02 = bytDataInit(39)
                    .CrvWdh02 = bytDataInit(40)
                    .Spare22 = bytDataInit(41)
                    .ChNoDialPos02 = bytDataInit(42) + (bytDataInit(43) * 256)
                    .Power = bytDataInit(44) + (bytDataInit(45) * 256)
                    .Coeff = bytDataInit(46) + (bytDataInit(47) * 256)
                    .Spare23(0) = bytDataInit(48)
                    .Spare23(1) = bytDataInit(49)
                    .CrvDispFlg03 = bytDataInit(50)
                    .Spare31 = bytDataInit(51)
                    .OutputChNoPower03 = bytDataInit(52) + (bytDataInit(53) * 256)
                    .OutputChNoSpeed03 = bytDataInit(54) + (bytDataInit(55) * 256)
                    .OutputNrmlNo03 = bytDataInit(56) + (bytDataInit(57) * 256)
                    .OutputAlmNo03 = bytDataInit(58) + (bytDataInit(59) * 256)
                    .OutputNrmlColr03 = bytDataInit(60)
                    .OutputAlmColr03 = bytDataInit(61)
                    .Spare32(0) = bytDataInit(62)
                    .Spare32(1) = bytDataInit(63)
                    .CrvDispFlg04 = bytDataInit(64)
                    .Spare41 = bytDataInit(65)
                    .OutputChNoPower04 = bytDataInit(66) + (bytDataInit(67) * 256)
                    .OutputChNoSpeed04 = bytDataInit(68) + (bytDataInit(69) * 256)
                    .OutputNrmlNo04 = bytDataInit(70) + (bytDataInit(71) * 256)
                    .OutputAlmNo04 = bytDataInit(72) + (bytDataInit(73) * 256)
                    .OutputNrmlColr04 = bytDataInit(74)
                    .OutputAlmColr04 = bytDataInit(75)
                    .RepsChNoStbd = bytDataInit(76) + (bytDataInit(77) * 256)
                    .RepsChNoPort = bytDataInit(78) + (bytDataInit(79) * 256)
                    For j = 0 To UBound(udtInit.CrvSet(i).Spare)
                        .Spare(j) = bytDataInit(80 + j)
                    Next
                End With
            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub
    '--------------------------------------------------------------------
    ' 機能      : テキストボックスの設定値を構造体へ格納
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) システム設定構造体
    ' 機能説明  : 構造体に設定を格納する
    '--------------------------------------------------------------------
    Private Sub mSetStructure(ByRef udtSet As gTypLoadCurve)

        Try

            For i = 0 To UBound(udtSet.CrvSet)
                With udtSet.CrvSet(i)
                    .ChNoDialPos01 = TxtBx1DialPos(i).Text
                    .CrvDrawMin = TxtBxDrwRangeLow(i).Text
                    .CrvDrawMax = TxtBxDrwRangeHigh(i).Text
                    .CrvVertRangeLow = TxtBxVrtRangeLow(i).Text
                    .CrvVertRangeHigh = TxtBxVrtRangeHigh(i).Text
                    .CrvWidhRangeLow = TxtBxWidRangeLow(i).Text
                    .CrvWidhRangeHigh = TxtBxWidRangeHigh(i).Text
                    .OutputChNoPower02 = TxtBxVrtCH(i).Text
                    .OutputChNoSpeed02 = TxtBxWidCH(i).Text
                    .ChNoDialPos02 = TxtBx2DialPos(i).Text
                    .Power = TxtBxPower(i).Text
                    .Coeff = TxtBxCoef(i).Text
                    .RepsChNoStbd = TxtBxStbdRep(i).Text
                    .RepsChNoPort = TxtBxPortRep(i).Text
                    .CrvFlg01 = CmbBx1DispFlg(i).SelectedIndex
                    .CrvFlg02 = CmbBx2DispFlg(i).SelectedIndex
                    Select Case CmbBx1LineColor(i).SelectedIndex
                        Case 1  'マゼンタ
                            .CrvColr01 = 5
                        Case 2  'グリーン
                            .CrvColr01 = 2
                        Case Else
                            .CrvColr01 = 0
                    End Select

                    Select Case CmbBx2LineColor(i).SelectedIndex
                        Case 1  'マゼンタ
                            .CrvColr02 = 5
                        Case 2 'グリーン
                            .CrvColr02 = 2
                        Case Else
                            .CrvColr02 = 0
                    End Select

                End With
            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 構造体比較
    ' 返り値    : True:相違なし、False:相違あり
    ' 引き数    : ARG1 - (I ) 構造体１
    ' 　　　    : ARG1 - (I ) 構造体２
    ' 機能説明  : 構造体の設定値を比較する
    ' 備考　　  : 構造体メンバの中に構造体配列がいると Equals メソッドで正しい結果が得られないため関数を用意
    ' 　　　　  : 構造体メンバの中に構造体配列がいない場合は、 Equals メソッドで処理しても良いが一応これを使うこと
    ' 　　　　  : String文字列の比較には gCompareString を使用すること（単純な = だとNULL文字の有り無しで結果が変わってしまう）
    '--------------------------------------------------------------------
    Private Function mChkStructureEquals(ByVal udt1 As gTypLoadCurve, _
                                         ByVal udt2 As gTypLoadCurve) As Boolean

        Try

            For i As Integer = 0 To UBound(udt1.CrvSet)
                If udt1.CrvSet(i).AlmNoStbd01 <> udt2.CrvSet(i).AlmNoStbd01 Then Return False
                If udt1.CrvSet(i).CrvFlg01 <> udt2.CrvSet(i).CrvFlg01 Then Return False
                If udt1.CrvSet(i).CrvColr01 <> udt2.CrvSet(i).CrvColr01 Then Return False
                If udt1.CrvSet(i).CrvWdh01 <> udt2.CrvSet(i).CrvWdh01 Then Return False
                If udt1.CrvSet(i).Spare11 <> udt2.CrvSet(i).Spare11 Then Return False
                If udt1.CrvSet(i).ChNoDialPos01 <> udt2.CrvSet(i).ChNoDialPos01 Then Return False
                If udt1.CrvSet(i).CrvRangeMin <> udt2.CrvSet(i).CrvRangeMin Then Return False
                If udt1.CrvSet(i).CrvRangeMax <> udt2.CrvSet(i).CrvRangeMax Then Return False
                If udt1.CrvSet(i).CrvDrawMin <> udt2.CrvSet(i).CrvDrawMin Then Return False
                If udt1.CrvSet(i).CrvDrawMax <> udt2.CrvSet(i).CrvDrawMax Then Return False
                If udt1.CrvSet(i).CrvVertRangeHigh <> udt2.CrvSet(i).CrvVertRangeHigh Then Return False
                If udt1.CrvSet(i).CrvVertRangeLow <> udt2.CrvSet(i).CrvVertRangeLow Then Return False
                If udt1.CrvSet(i).CrvWidhRangeHigh <> udt2.CrvSet(i).CrvWidhRangeHigh Then Return False
                If udt1.CrvSet(i).CrvWidhRangeLow <> udt2.CrvSet(i).CrvWidhRangeLow Then Return False
                If udt1.CrvSet(i).RangeSplit <> udt2.CrvSet(i).RangeSplit Then Return False
                If udt1.CrvSet(i).Spare12 <> udt2.CrvSet(i).Spare12 Then Return False
                If udt1.CrvSet(i).CrvDispFlg02 <> udt2.CrvSet(i).CrvDispFlg02 Then Return False
                If udt1.CrvSet(i).Spare21 <> udt2.CrvSet(i).Spare21 Then Return False
                If udt1.CrvSet(i).OutputChNoPower02 <> udt2.CrvSet(i).OutputChNoPower02 Then Return False
                If udt1.CrvSet(i).OutputChNoSpeed02 <> udt2.CrvSet(i).OutputChNoSpeed02 Then Return False
                If udt1.CrvSet(i).OutputNrmlNo02 <> udt2.CrvSet(i).OutputNrmlNo02 Then Return False
                If udt1.CrvSet(i).OutputAlmNo02 <> udt2.CrvSet(i).OutputAlmNo02 Then Return False
                If udt1.CrvSet(i).OutputNrmlColr02 <> udt2.CrvSet(i).OutputNrmlColr02 Then Return False
                If udt1.CrvSet(i).OutputAlmColr02 <> udt2.CrvSet(i).OutputAlmColr02 Then Return False
                If udt1.CrvSet(i).CrvFlg02 <> udt2.CrvSet(i).CrvFlg02 Then Return False
                If udt1.CrvSet(i).CrvColr02 <> udt2.CrvSet(i).CrvColr02 Then Return False
                If udt1.CrvSet(i).CrvWdh02 <> udt2.CrvSet(i).CrvWdh02 Then Return False
                If udt1.CrvSet(i).Spare22 <> udt2.CrvSet(i).Spare22 Then Return False
                If udt1.CrvSet(i).ChNoDialPos02 <> udt2.CrvSet(i).ChNoDialPos02 Then Return False
                If udt1.CrvSet(i).Power <> udt2.CrvSet(i).Power Then Return False
                If udt1.CrvSet(i).Coeff <> udt2.CrvSet(i).Coeff Then Return False
                If udt1.CrvSet(i).Spare23(0) <> udt2.CrvSet(i).Spare23(0) Then Return False
                If udt1.CrvSet(i).Spare23(1) <> udt2.CrvSet(i).Spare23(1) Then Return False
                If udt1.CrvSet(i).CrvDispFlg03 <> udt2.CrvSet(i).CrvDispFlg03 Then Return False
                If udt1.CrvSet(i).Spare31 <> udt2.CrvSet(i).Spare31 Then Return False
                If udt1.CrvSet(i).OutputChNoPower03 <> udt2.CrvSet(i).OutputChNoPower03 Then Return False
                If udt1.CrvSet(i).OutputChNoSpeed03 <> udt2.CrvSet(i).OutputChNoSpeed03 Then Return False
                If udt1.CrvSet(i).OutputNrmlNo03 <> udt2.CrvSet(i).OutputNrmlNo03 Then Return False
                If udt1.CrvSet(i).OutputAlmNo03 <> udt2.CrvSet(i).OutputAlmNo03 Then Return False
                If udt1.CrvSet(i).OutputNrmlColr03 <> udt2.CrvSet(i).OutputNrmlColr03 Then Return False
                If udt1.CrvSet(i).OutputAlmColr03 <> udt2.CrvSet(i).OutputAlmColr03 Then Return False
                If udt1.CrvSet(i).Spare32(0) <> udt2.CrvSet(i).Spare32(0) Then Return False
                If udt1.CrvSet(i).Spare32(1) <> udt2.CrvSet(i).Spare32(1) Then Return False
                If udt1.CrvSet(i).CrvDispFlg04 <> udt2.CrvSet(i).CrvDispFlg04 Then Return False
                If udt1.CrvSet(i).Spare41 <> udt2.CrvSet(i).Spare41 Then Return False
                If udt1.CrvSet(i).OutputChNoPower04 <> udt2.CrvSet(i).OutputChNoPower04 Then Return False
                If udt1.CrvSet(i).OutputChNoSpeed04 <> udt2.CrvSet(i).OutputChNoSpeed04 Then Return False
                If udt1.CrvSet(i).OutputNrmlNo04 <> udt2.CrvSet(i).OutputNrmlNo04 Then Return False
                If udt1.CrvSet(i).OutputAlmNo04 <> udt2.CrvSet(i).OutputAlmNo04 Then Return False
                If udt1.CrvSet(i).OutputNrmlColr04 <> udt2.CrvSet(i).OutputNrmlColr04 Then Return False
                If udt1.CrvSet(i).OutputAlmColr04 <> udt2.CrvSet(i).OutputAlmColr04 Then Return False
                If udt1.CrvSet(i).RepsChNoStbd <> udt2.CrvSet(i).RepsChNoStbd Then Return False
                If udt1.CrvSet(i).RepsChNoPort <> udt2.CrvSet(i).RepsChNoPort Then Return False
                For j As Integer = 0 To UBound(udt1.CrvSet(i).Spare) 'Spare 0～15
                    If udt1.CrvSet(i).Spare(j) <> udt2.CrvSet(i).Spare(j) Then Return False
                Next
            Next

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 構造体の複製
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 複製元構造体
    ' 機能説明  : 構造体を複製する
    ' 備考　　  : 構造体メンバの中に構造体配列がいると単純に = では複製できないため関数を用意
    ' 　　　　  : ↑ = でやると配列部分が参照渡しになり（？）値更新時に両方更新されてしまう
    ' 　　　　  : 構造体メンバの中に構造体配列がいない場合は、この関数を使わずに = で処理しても良い
    '--------------------------------------------------------------------
    Private Sub mCopyStructure(ByVal udtSource As gTypLoadCurve, ByRef udtTarget As gTypLoadCurve)

        Try

            For i = 0 To UBound(udtLoadCurve.CrvSet)
                With udtLoadCurve.CrvSet(i)
                    udtTarget.CrvSet(i).AlmNoStbd01 = udtSource.CrvSet(i).AlmNoStbd01
                    udtTarget.CrvSet(i).CrvFlg01 = udtSource.CrvSet(i).CrvFlg01
                    udtTarget.CrvSet(i).CrvColr01 = udtSource.CrvSet(i).CrvColr01
                    udtTarget.CrvSet(i).CrvWdh01 = udtSource.CrvSet(i).CrvWdh01
                    udtTarget.CrvSet(i).Spare11 = udtSource.CrvSet(i).Spare11
                    udtTarget.CrvSet(i).ChNoDialPos01 = udtSource.CrvSet(i).ChNoDialPos01
                    udtTarget.CrvSet(i).CrvRangeMin = udtSource.CrvSet(i).CrvRangeMin
                    udtTarget.CrvSet(i).CrvRangeMax = udtSource.CrvSet(i).CrvRangeMax
                    udtTarget.CrvSet(i).CrvDrawMin = udtSource.CrvSet(i).CrvDrawMin
                    udtTarget.CrvSet(i).CrvDrawMax = udtSource.CrvSet(i).CrvDrawMax
                    udtTarget.CrvSet(i).CrvVertRangeHigh = udtSource.CrvSet(i).CrvVertRangeHigh
                    udtTarget.CrvSet(i).CrvVertRangeLow = udtSource.CrvSet(i).CrvVertRangeLow
                    udtTarget.CrvSet(i).CrvWidhRangeHigh = udtSource.CrvSet(i).CrvWidhRangeHigh
                    udtTarget.CrvSet(i).CrvWidhRangeLow = udtSource.CrvSet(i).CrvWidhRangeLow
                    udtTarget.CrvSet(i).RangeSplit = udtSource.CrvSet(i).RangeSplit
                    udtTarget.CrvSet(i).Spare12 = udtSource.CrvSet(i).Spare12
                    udtTarget.CrvSet(i).CrvDispFlg02 = udtSource.CrvSet(i).CrvDispFlg02
                    udtTarget.CrvSet(i).Spare21 = udtSource.CrvSet(i).Spare21
                    udtTarget.CrvSet(i).OutputChNoPower02 = udtSource.CrvSet(i).OutputChNoPower02
                    udtTarget.CrvSet(i).OutputChNoSpeed02 = udtSource.CrvSet(i).OutputChNoSpeed02
                    udtTarget.CrvSet(i).OutputNrmlNo02 = udtSource.CrvSet(i).OutputNrmlNo02
                    udtTarget.CrvSet(i).OutputAlmNo02 = udtSource.CrvSet(i).OutputAlmNo02
                    udtTarget.CrvSet(i).OutputNrmlColr02 = udtSource.CrvSet(i).OutputNrmlColr02
                    udtTarget.CrvSet(i).OutputAlmColr02 = udtSource.CrvSet(i).OutputAlmColr02
                    udtTarget.CrvSet(i).CrvFlg02 = udtSource.CrvSet(i).CrvFlg02
                    udtTarget.CrvSet(i).CrvColr02 = udtSource.CrvSet(i).CrvColr02
                    udtTarget.CrvSet(i).CrvWdh02 = udtSource.CrvSet(i).CrvWdh02
                    udtTarget.CrvSet(i).Spare22 = udtSource.CrvSet(i).Spare22
                    udtTarget.CrvSet(i).ChNoDialPos02 = udtSource.CrvSet(i).ChNoDialPos02
                    udtTarget.CrvSet(i).Power = udtSource.CrvSet(i).Power
                    udtTarget.CrvSet(i).Coeff = udtSource.CrvSet(i).Coeff
                    udtTarget.CrvSet(i).Spare23(0) = udtSource.CrvSet(i).Spare23(0)
                    udtTarget.CrvSet(i).Spare23(1) = udtSource.CrvSet(i).Spare23(1)
                    udtTarget.CrvSet(i).CrvDispFlg03 = udtSource.CrvSet(i).CrvDispFlg03
                    udtTarget.CrvSet(i).Spare31 = udtSource.CrvSet(i).Spare31
                    udtTarget.CrvSet(i).OutputChNoPower03 = udtSource.CrvSet(i).OutputChNoPower03
                    udtTarget.CrvSet(i).OutputChNoSpeed03 = udtSource.CrvSet(i).OutputChNoSpeed03
                    udtTarget.CrvSet(i).OutputNrmlNo03 = udtSource.CrvSet(i).OutputNrmlNo03
                    udtTarget.CrvSet(i).OutputAlmNo03 = udtSource.CrvSet(i).OutputAlmNo03
                    udtTarget.CrvSet(i).OutputNrmlColr03 = udtSource.CrvSet(i).OutputNrmlColr03
                    udtTarget.CrvSet(i).OutputAlmColr03 = udtSource.CrvSet(i).OutputAlmColr03
                    udtTarget.CrvSet(i).Spare32(0) = udtSource.CrvSet(i).Spare32(0)
                    udtTarget.CrvSet(i).Spare32(1) = udtSource.CrvSet(i).Spare32(1)
                    udtTarget.CrvSet(i).CrvDispFlg04 = udtSource.CrvSet(i).CrvDispFlg04
                    udtTarget.CrvSet(i).Spare41 = udtSource.CrvSet(i).Spare41
                    udtTarget.CrvSet(i).OutputChNoPower04 = udtSource.CrvSet(i).OutputChNoPower04
                    udtTarget.CrvSet(i).OutputChNoSpeed04 = udtSource.CrvSet(i).OutputChNoSpeed04
                    udtTarget.CrvSet(i).OutputNrmlNo04 = udtSource.CrvSet(i).OutputNrmlNo04
                    udtTarget.CrvSet(i).OutputAlmNo04 = udtSource.CrvSet(i).OutputAlmNo04
                    udtTarget.CrvSet(i).OutputNrmlColr04 = udtSource.CrvSet(i).OutputNrmlColr04
                    udtTarget.CrvSet(i).OutputAlmColr04 = udtSource.CrvSet(i).OutputAlmColr04
                    udtTarget.CrvSet(i).RepsChNoStbd = udtSource.CrvSet(i).RepsChNoStbd
                    udtTarget.CrvSet(i).RepsChNoPort = udtSource.CrvSet(i).RepsChNoPort
                    For j = 0 To UBound(udtLoadCurve.CrvSet(i).Spare)
                        bytData(i, 80 + j) = .Spare(j)
                    Next
                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 構造体の2次元配列への複製
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 複製元構造体
    ' 機能説明  : ファイル書き込みのために、構造体を2次元配列へ複製する
    ' 備考　　  : 構造体メンバの中に構造体配列がいると単純に = では複製できないため関数を用意
    ' 　　　　  : ↑ = でやると配列部分が参照渡しになり（？）値更新時に両方更新されてしまう
    ' 　　　　  : 構造体メンバの中に構造体配列がいない場合は、この関数を使わずに = で処理しても良い
    '--------------------------------------------------------------------
    Private Sub mSaveStructureToArray(ByVal udtSource As gTypLoadCurve)

        Try

            '二次元配列へ格納
            For i = 0 To UBound(udtLoadCurve.CrvSet)
                With udtLoadCurve.CrvSet(i)
                    bytData(i, 0) = mTrimHighByte(.AlmNoStbd01)
                    bytData(i, 1) = mTrimLowByte(.AlmNoStbd01)
                    bytData(i, 2) = .CrvFlg01
                    bytData(i, 3) = .CrvColr01
                    bytData(i, 4) = .CrvWdh01
                    bytData(i, 5) = .Spare11
                    bytData(i, 6) = mTrimHighByte(.ChNoDialPos01)
                    bytData(i, 7) = mTrimLowByte(.ChNoDialPos01)
                    bytData(i, 8) = mTrimHighByte(.CrvRangeMin)
                    bytData(i, 9) = mTrimLowByte(.CrvRangeMin)
                    bytData(i, 10) = mTrimHighByte(.CrvRangeMax)
                    bytData(i, 11) = mTrimLowByte(.CrvRangeMax)
                    bytData(i, 12) = mTrimHighByte(.CrvDrawMin)
                    bytData(i, 13) = mTrimLowByte(.CrvDrawMin)
                    bytData(i, 14) = mTrimHighByte(.CrvDrawMax)
                    bytData(i, 15) = mTrimLowByte(.CrvDrawMax)
                    bytData(i, 16) = mTrimHighByte(.CrvVertRangeHigh)
                    bytData(i, 17) = mTrimLowByte(.CrvVertRangeHigh)
                    bytData(i, 18) = mTrimHighByte(.CrvVertRangeLow)
                    bytData(i, 19) = mTrimLowByte(.CrvVertRangeLow)
                    bytData(i, 20) = mTrimHighByte(.CrvWidhRangeHigh)
                    bytData(i, 21) = mTrimLowByte(.CrvWidhRangeHigh)
                    bytData(i, 22) = mTrimHighByte(.CrvWidhRangeLow)
                    bytData(i, 23) = mTrimLowByte(.CrvWidhRangeLow)
                    bytData(i, 24) = .RangeSplit
                    bytData(i, 25) = .Spare12
                    bytData(i, 26) = .CrvDispFlg02
                    bytData(i, 27) = .Spare21
                    bytData(i, 28) = mTrimHighByte(.OutputChNoPower02)
                    bytData(i, 29) = mTrimLowByte(.OutputChNoPower02)
                    bytData(i, 30) = mTrimHighByte(.OutputChNoSpeed02)
                    bytData(i, 31) = mTrimLowByte(.OutputChNoSpeed02)
                    bytData(i, 32) = mTrimHighByte(.OutputNrmlNo02)
                    bytData(i, 33) = mTrimLowByte(.OutputNrmlNo02)
                    bytData(i, 34) = mTrimHighByte(.OutputAlmNo02)
                    bytData(i, 35) = mTrimLowByte(.OutputAlmNo02)
                    bytData(i, 36) = .OutputNrmlColr02
                    bytData(i, 37) = .OutputAlmColr02
                    bytData(i, 38) = .CrvFlg02
                    bytData(i, 39) = .CrvColr02
                    bytData(i, 40) = .CrvWdh02
                    bytData(i, 41) = .Spare22
                    bytData(i, 42) = mTrimHighByte(.ChNoDialPos02)
                    bytData(i, 43) = mTrimLowByte(.ChNoDialPos02)
                    bytData(i, 44) = mTrimHighByte(.Power)
                    bytData(i, 45) = mTrimLowByte(.Power)
                    bytData(i, 46) = mTrimHighByte(.Coeff)
                    bytData(i, 47) = mTrimLowByte(.Coeff)
                    bytData(i, 48) = .Spare23(0)
                    bytData(i, 49) = .Spare23(1)
                    bytData(i, 50) = .CrvDispFlg03
                    bytData(i, 51) = .Spare31
                    bytData(i, 52) = mTrimHighByte(.OutputChNoPower03)
                    bytData(i, 53) = mTrimLowByte(.OutputChNoPower03)
                    bytData(i, 54) = mTrimHighByte(.OutputChNoSpeed03)
                    bytData(i, 55) = mTrimLowByte(.OutputChNoSpeed03)
                    bytData(i, 56) = mTrimHighByte(.OutputNrmlNo03)
                    bytData(i, 57) = mTrimLowByte(.OutputNrmlNo03)
                    bytData(i, 58) = mTrimHighByte(.OutputAlmNo03)
                    bytData(i, 59) = mTrimLowByte(.OutputAlmNo03)
                    bytData(i, 60) = .OutputNrmlColr03
                    bytData(i, 61) = .OutputAlmColr03
                    bytData(i, 62) = .Spare32(0)
                    bytData(i, 63) = .Spare32(1)
                    bytData(i, 64) = .CrvDispFlg04
                    bytData(i, 65) = .Spare41
                    bytData(i, 66) = mTrimHighByte(.OutputChNoPower04)
                    bytData(i, 67) = mTrimLowByte(.OutputChNoPower04)
                    bytData(i, 68) = mTrimHighByte(.OutputChNoSpeed04)
                    bytData(i, 69) = mTrimLowByte(.OutputChNoSpeed04)
                    bytData(i, 70) = mTrimHighByte(.OutputNrmlNo04)
                    bytData(i, 71) = mTrimLowByte(.OutputNrmlNo04)
                    bytData(i, 72) = mTrimHighByte(.OutputAlmNo04)
                    bytData(i, 73) = mTrimLowByte(.OutputAlmNo04)
                    bytData(i, 74) = mTrimHighByte(.OutputNrmlColr04)
                    bytData(i, 75) = mTrimLowByte(.OutputAlmColr04)
                    bytData(i, 76) = mTrimHighByte(.RepsChNoStbd)
                    bytData(i, 77) = mTrimLowByte(.RepsChNoStbd)
                    bytData(i, 78) = mTrimHighByte(.RepsChNoPort)
                    bytData(i, 79) = mTrimLowByte(.RepsChNoPort)

                    For j = 0 To UBound(udtLoadCurve.CrvSet(i).Spare)
                        bytData(i, 80 + j) = .Spare(j)
                    Next
                End With
            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region


#Region "画面表示関数"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 本画面を表示する
    '--------------------------------------------------------------------
    Friend Sub gShow()

        Try

            ''本画面表示
            Call gShowFormModelessForCloseWait1(Me)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "バイナリ変換"
    '--------------------------------------------------------------------
    ' 機能      : バイナリ値のエンディアン表示方式の転換(上桁)
    ' 返り値    : T311.com用の1バイト数字(引数をリトルエンディアンに変換した後の上の桁)
    ' 引き数    : 構造体の中身の2バイト数字(ビッグエンディアン)
    ' 機能説明  : リトルエンディアン方式で構成されているT311.comに対して書き込みを行うための変換
    '           　(例)引数：構造体の中身の2バイト数字 &D100(=&H0064)→ 返り値：T311.com用の1バイト数字 &H64
    '--------------------------------------------------------------------
    Private Function mTrimHighByte(ByVal Source As UShort) As Byte

        Try
            Dim Target As UShort        'ビットシフト後の数値
            Target = Source And &HFF    '下位ビットの抽出

            Return Target

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    ''--------------------------------------------------------------------
    '' 機能      : バイナリ値のエンディアン表示方式の転換(下桁)
    '' 返り値    : T311.com用の1バイト数字(引数をリトルエンディアンに変換した後の下の桁)
    '' 引き数    : 構造体の中身の2バイト数字(ビッグエンディアン)
    '' 機能説明  : リトルエンディアン方式で構成されているT311.comに対して書き込みを行うための変換
    ''           　(例)引数：構造体の中身の2バイト数字 &D100(=&H0064)→ 返り値：T311.com用の1バイト数字 &H00
    ''--------------------------------------------------------------------
    Private Function mTrimLowByte(ByVal Source As UShort) As UShort

        Try

            Dim Target As UShort                'ビットシフト後の数値
            Target = (Source And &HFF00) >> 8   '上位ビットの抽出
            Return Target

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

End Class