Imports System.Runtime.InteropServices
Public Class frmToolChkChList

#Region "GetMimCH DLL Call"
    <System.Runtime.InteropServices.DllImport("GetMimCH", CharSet:=Runtime.InteropServices.CharSet.Ansi)> _
    Shared Function mainProc(pstrPath As String, pintCHNo As Integer) As IntPtr
    End Function
#End Region

#Region "変数"
    'チャンネルグループ情報 格納領域
    Private mudtChannelGroup As gTypChannelGroup
#End Region

#Region "画面"

    Private Sub frmToolChkChList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtCHNo.Text = ""

        'Ver2.0.2.4 ｺﾝﾊﾟｲﾙﾌｫﾙﾀﾞが無い場合はそのむね表記と実行禁止
        Dim strPath As String = System.IO.Path.Combine(gudtFileInfo.strFilePath, gudtFileInfo.strFileVersion)
        strPath = System.IO.Path.Combine(strPath, gCstFolderNameCompile)
        If System.IO.Directory.Exists(strPath) = False Then
            lblCompile.Text = "No Compile"
            cmdGo.Enabled = False
        Else
            lblCompile.Text = ""
            cmdGo.Enabled = True
        End If

        'Grid初期化
        Call subInitGrid()

        'Grid再表示
        Call subGridDisp()
    End Sub
    Private Sub frmToolChkChList_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'Ver2.0.1.5 ﾛｸﾞ結果ｳｨﾝﾄﾞｳ終了
        frmToolChkChListLog.Close()
    End Sub

    Private Sub frmToolChkChList_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        'Ver2.0.3.6 txtChNoが入力さているならばEnterキーで検索実行
        If e.KeyCode = Keys.Enter Then
            If txtCHNo.Text <> "" Then
                Call cmdGo_Click(sender, e)
            End If
        End If

    End Sub


    Private Sub btnAll_Click(sender As System.Object, e As System.EventArgs) Handles btnAll.Click
        '一覧の全部チェックON
        For i As Integer = 0 To grdChNo.RowCount - 1
            With grdChNo.Rows(i)
                .Cells(0).Value = True
            End With
        Next i
    End Sub

    Private Sub btnOFF_Click(sender As System.Object, e As System.EventArgs) Handles btnOFF.Click
        '一覧の全部チェックOFF
        For i As Integer = 0 To grdChNo.RowCount - 1
            With grdChNo.Rows(i)
                .Cells(0).Value = False
            End With
        Next i
    End Sub


    Private Sub cmdGo_Click(sender As Object, e As EventArgs) Handles cmdGo.Click
        Try
            '一覧のチェックがついているCHNoを格納する
            Dim strCHNoS As String = ""
            Dim strCHNo() As String
            For i As Integer = 0 To grdChNo.RowCount - 1
                With grdChNo.Rows(i)
                    If .Cells(0).Value = True Then
                        strCHNoS = strCHNoS & .Cells(2).Value & vbCrLf
                    End If
                End With
            Next i
            If txtCHNo.Text.Trim <> "" Then
                strCHNoS = strCHNoS & txtCHNo.Text.Trim & vbCrLf
            End If
            If strCHNoS.Trim = "" Then
                Call MessageBox.Show("CHNo Not Selected", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
            strCHNo = strCHNoS.Split(vbCrLf)




            '出力開始
            Call subOpen()

            Call subWrite("CHNo,Data")

            Dim intCHNo As Integer = 0
            For i As Integer = 0 To UBound(strCHNo) Step 1
                intCHNo = CInt(strCHNo(i))
                If intCHNo > 0 Then
                    Call subChMain(intCHNo)
                End If
            Next i

        Catch ex As Exception
        Finally
            '出力終了
            Call subClose()

            'Ver2.0.1.5 ﾛｸﾞ結果ｳｨﾝﾄﾞｳ表示
            frmToolChkChListLog.Show()
            Me.Focus()

            '終了ﾒｯｾｰｼﾞ
            Call MessageBox.Show("Has completed.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub


#End Region

#Region "関数"
    'Grid初期化
    Private Sub subInitGrid()
        Try

            Dim i As Integer
            Dim cellStyle As New DataGridViewCellStyle
            Dim Column0 As New DataGridViewCheckBoxColumn : Column0.Name = "chkSel"
            Dim Column1 As New DataGridViewTextBoxColumn : Column1.Name = "txtGrNo" : Column1.ReadOnly = True
            Dim Column2 As New DataGridViewTextBoxColumn : Column1.Name = "txtChNo" : Column2.ReadOnly = True
            Dim Column3 As New DataGridViewTextBoxColumn : Column2.Name = "txtChName" : Column3.ReadOnly = True
            Dim Column4 As New DataGridViewTextBoxColumn : Column3.Name = "txtChType" : Column4.ReadOnly = True

            'Column1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            With grdChNo

                ''列
                .Columns.Clear()
                .Columns.Add(Column0) : .Columns.Add(Column1) : .Columns.Add(Column2) : .Columns.Add(Column3) : .Columns.Add(Column4)
                .AllowUserToResizeColumns = False   ''列幅の変更不可
                .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing

                ''全ての列の並び替えを禁止
                For Each c As DataGridViewColumn In .Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next c

                ''列ヘッダー
                .Columns(0).HeaderText = "C" : .Columns(0).Width = 20
                .Columns(1).HeaderText = "GrNo" : .Columns(1).Width = 0 : .Columns(1).Visible = False
                .Columns(2).HeaderText = "CHNo" : .Columns(2).Width = 40
                .Columns(3).HeaderText = "Name" : .Columns(3).Width = 340
                .Columns(4).HeaderText = "ChType" : .Columns(4).Width = 150
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

                ''偶数行の背景色を変える
                'cellStyle.BackColor = gColorGridRowBack
                'For i = 0 To .Rows.Count - 1
                '	If i Mod 2 <> 0 Then
                '		.Rows(i).DefaultCellStyle = cellStyle
                '	End If
                'Next


                ''罫線
                .EnableHeadersVisualStyles = False
                .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
                .CellBorderStyle = DataGridViewCellBorderStyle.Single
                .GridColor = Color.Gray

                ''スクロールバー
                .ScrollBars = ScrollBars.Vertical

                '行選択
                '.SelectionMode = DataGridViewSelectionMode.FullRowSelect

            End With


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

    'Grid再表示
    Private Sub subGridDisp()
        Call gMakeChannelGroupData(gudt.SetChInfo, mudtChannelGroup)

        For i As Integer = 0 To UBound(mudtChannelGroup.udtGroup) Step 1
            For j As Integer = 0 To UBound(mudtChannelGroup.udtGroup(i).udtChannelData) Step 1
                If mudtChannelGroup.udtGroup(i).udtChannelData(j).udtChCommon.shtChno <> 0 Then
                    With grdChNo
                        'ChInfoデータを表示
                        .Rows.Add()
                        .Rows(.RowCount - 1).Cells(0).Value = False
                        .Rows(.RowCount - 1).Cells(1).Value = i + 1.ToString
                        .Rows(.RowCount - 1).Cells(2).Value = mudtChannelGroup.udtGroup(i).udtChannelData(j).udtChCommon.shtChno
                        .Rows(.RowCount - 1).Cells(3).Value = mudtChannelGroup.udtGroup(i).udtChannelData(j).udtChCommon.strChitem
                        .Rows(.RowCount - 1).Cells(4).Value = fnGetChTypeName(mudtChannelGroup.udtGroup(i).udtChannelData(j).udtChCommon.shtChType)
                    End With
                End If
            Next j
        Next i
        

    End Sub
    'CHﾀｲﾌﾟ名戻し
    Private Function fnGetChTypeName(pstrChType As Integer) As String
        Dim strRet As String = ""

        Select Case pstrChType
            Case 1
                strRet = "Analog"
            Case 2
                strRet = "Digital"
            Case 3
                strRet = "Motor"
            Case 4
                strRet = "Valve"
            Case 5
                strRet = "Composite"
            Case 6
                strRet = "Pulse"
            Case Else
                strRet = pstrChType.ToString
        End Select

        Return strRet
    End Function


#Region "CH番号検索関連"
    'CHNoを受け取り各ファイルを探す
    Private Sub subChMain(pintChNo As Integer)
        'CHNo→CHID
        Dim intChID As Integer = gConvChNoToChId(pintChNo)

        'コンポジット
        Call subComposite(pintChNo, intChID)
        '出力ﾁｬﾝﾈﾙ設定
        Call subChOutput(pintChNo, intChID)
        '論理出力設定
        Call subChAndOr(pintChNo, intChID)
        'リポーズ入力設定
        Call subChGroupRepose(pintChNo, intChID)
        '積算データ設定
        Call subChRunHour(pintChNo, intChID)
        '排ガス演算処理設定
        Call subChExhGus(pintChNo, intChID)
        'コントロール使用可／不可設定
        Call subChCtrlUse(pintChNo, intChID)
        'SIO設定
        Call subChSio(pintChNo, intChID)
        'SIO設定CH設定データ
        Call subChSioCh(pintChNo, intChID)
        'シーケンス設定
        Call subSeqSet(pintChNo, intChID)
        'OPSグラフ設定
        Call subOpsGraph(pintChNo, intChID)
        'ﾛｸﾞ設定印字ｵﾌﾟｼｮﾝ設定
        Call subOpsLogOption(pintChNo, intChID)
        'GWS CH設定
        Call subOpsGwsCh(pintChNo, intChID)


        'Mimic
        Call subGetMimic(pintChNo, intChID)


    End Sub

#Region "CHNo 検索ファイル群"
    '>>>ｺﾝﾎﾟｼﾞｯﾄ設定から探す
    Private Sub subComposite(pintCHNo As Integer, pintCHID As Integer)
        Dim i As Integer = 0

        With gudt.SetChComposite
            For i = 0 To UBound(.udtComposite) Step 1
                If .udtComposite(i).shtChid = pintCHNo Then
                    Call subWrite(pintCHNo & "," & "コンポジット設定　あり テーブル番号 " & (i + 1).ToString)
                End If
            Next i
        End With
    End Sub
    '>>>出力チャンネル設定から探す
    Private Sub subChOutput(pintCHNo As Integer, pintCHID As Integer)
        Dim i As Integer = 0

        With gudt.SetChOutput
            For i = 0 To UBound(.udtCHOutPut) Step 1
                If .udtCHOutPut(i).shtChid = pintCHNo Then
                    Call subWrite(pintCHNo & "," & "出力チャンネル設定　あり テーブル番号 " & (i + 1).ToString)
                End If
            Next i
        End With
    End Sub
    '>>>論理出力設定から探す
    Private Sub subChAndOr(pintCHNo As Integer, pintCHID As Integer)
        Dim i As Integer = 0
        Dim j As Integer = 0

        With gudt.SetChAndOr
            For i = 0 To UBound(.udtCHOut) Step 1
                For j = 0 To UBound(.udtCHOut(i).udtCHAndOr) Step 1
                    If .udtCHOut(i).udtCHAndOr(j).shtChid = pintCHNo Then
                        Call subWrite(pintCHNo & "," & "論理出力設定　あり テーブル番号 " & (i + 1).ToString)
                    End If
                Next j
            Next i
        End With
    End Sub
    '>>>リポーズ入力設定から探す
    Private Sub subChGroupRepose(pintCHNo As Integer, pintCHID As Integer)
        Dim i As Integer = 0
        Dim j As Integer = 0

        With gudt.SetChGroupRepose
            'MAIN
            For i = 0 To UBound(.udtRepose) Step 1
                If .udtRepose(i).shtChId = pintCHNo Then
                    Call subWrite(pintCHNo & "," & "リポーズ入力設定　あり テーブル番号 " & (i + 1).ToString)
                End If
            Next i
            'Detail
            For i = 0 To UBound(.udtRepose) Step 1
                For j = 0 To UBound(.udtRepose(i).udtReposeInf) Step 1
                    If .udtRepose(i).udtReposeInf(j).shtChId = pintCHNo Then
                        Call subWrite(pintCHNo & "," & "リポーズ入力設定詳細　あり テーブル番号 " & (i + 1).ToString)
                    End If
                Next j
            Next i
        End With
    End Sub
    '>>>積算データ設定から探す
    Private Sub subChRunHour(pintCHNo As Integer, pintCHID As Integer)
        Dim i As Integer = 0
        Dim j As Integer = 0

        With gudt.SetChRunHour
            'MAIN
            For i = 0 To UBound(.udtDetail) Step 1
                If .udtDetail(i).shtChid = pintCHNo Then
                    ''2018.12.14 倉重 積算とトリガを併記する
                    Call subWrite(pintCHNo & "," & "積算データ設定　積算CH　あり テーブル番号 " & (i + 1).ToString & "　積算 " & .udtDetail(i)._shtChid & "　トリガ " & .udtDetail(i)._shtTrgChid)
                End If
                If .udtDetail(i).shtTrgChid = pintCHNo Then
                    ''2018.12.14 倉重 積算とトリガを併記する
                    Call subWrite(pintCHNo & "," & "積算データ設定　トリガCH　あり テーブル番号 " & (i + 1).ToString & "　積算 " & .udtDetail(i)._shtChid & "　トリガ " & .udtDetail(i)._shtTrgChid)
                End If
            Next i
        End With
    End Sub
    '>>>排ガス演算処理設定から探す
    Private Sub subChExhGus(pintCHNo As Integer, pintCHID As Integer)
        Dim i As Integer = 0
        Dim j As Integer = 0

        With gudt.SetChExhGus
            'MAIN
            For i = 0 To UBound(.udtExhGusRec) Step 1
                If .udtExhGusRec(i).shtAveChid = pintCHNo Then
                    Call subWrite(pintCHNo & "," & "排ガス演算処理設定　平均値出力CH　あり テーブル番号 " & (i + 1).ToString)
                End If
                If .udtExhGusRec(i).shtRepChid = pintCHNo Then
                    Call subWrite(pintCHNo & "," & "排ガス演算処理設定　リポーズCH　あり テーブル番号 " & (i + 1).ToString)
                End If
            Next i
            'Detail
            For i = 0 To UBound(.udtExhGusRec) Step 1
                For j = 0 To UBound(.udtExhGusRec(i).udtExhGusCyl) Step 1
                    If .udtExhGusRec(i).udtExhGusCyl(j).shtChid = pintCHNo Then
                        Call subWrite(pintCHNo & "," & "排ガス演算処理設定　シリンダCH　あり テーブル番号 " & (i + 1).ToString)
                    End If
                Next j
                For j = 0 To UBound(.udtExhGusRec(i).udtExhGusDev) Step 1
                    If .udtExhGusRec(i).udtExhGusDev(j).shtChid = pintCHNo Then
                        Call subWrite(pintCHNo & "," & "排ガス演算処理設定　偏差CH　あり テーブル番号 " & (i + 1).ToString)
                    End If
                Next j
            Next i
        End With
    End Sub
    '>>>コントロール使用可／不可設定から探す
    Private Sub subChCtrlUse(pintCHNo As Integer, pintCHID As Integer)
        Dim i As Integer = 0
        Dim j As Integer = 0

        With gudt.SetChCtrlUseM
            For i = 0 To UBound(.udtCtrlUseNotuseRec) Step 1
                For j = 0 To UBound(.udtCtrlUseNotuseRec(i).udtUseNotuseDetails) Step 1
                    If .udtCtrlUseNotuseRec(i).udtUseNotuseDetails(j).shtChno = pintCHNo Then
                        Call subWrite(pintCHNo & "," & "コントロール使用可不可設定M　あり テーブル番号 " & (i + 1).ToString)
                    End If
                Next j
            Next i
        End With

        With gudt.SetChCtrlUseC
            For i = 0 To UBound(.udtCtrlUseNotuseRec) Step 1
                For j = 0 To UBound(.udtCtrlUseNotuseRec(i).udtUseNotuseDetails) Step 1
                    If .udtCtrlUseNotuseRec(i).udtUseNotuseDetails(j).shtChno = pintCHNo Then
                        Call subWrite(pintCHNo & "," & "コントロール使用可不可設定C　あり テーブル番号 " & (i + 1).ToString)
                    End If
                Next j
            Next i
        End With
    End Sub
    '>>>SIO設定から探す
    Private Sub subChSio(pintCHNo As Integer, pintCHID As Integer)
        Dim i As Integer = 0

        With gudt.SetChSio
            For i = 0 To UBound(.udtVdr) Step 1
                If .udtVdr(i).shtSendCH = pintCHNo Then
                    If i < 14 Then
                        Call subWrite(pintCHNo & "," & "SIO設定　あり COM" & (i + 1).ToString)
                    Else
                        Call subWrite(pintCHNo & "," & "EXL LAN設定　あり PORT" & ((i - 14) + 1).ToString)
                    End If
                End If
            Next i
        End With
    End Sub
    '>>>SIO設定CH設定データから探す
    Private Sub subChSioCh(pintCHNo As Integer, pintCHID As Integer)
        Dim i As Integer = 0
        Dim j As Integer = 0

        For i = 0 To UBound(gudt.SetChSioCh) Step 1
            With gudt.SetChSioCh(i)
                For j = 0 To UBound(.udtSioChRec) Step 1
                    If .udtSioChRec(j).shtChNo = pintCHNo Then
                        If i < 14 Then
                            Call subWrite(pintCHNo & "," & "SIO設定CH設定データ　あり COM" & (i + 1).ToString & " 送信順 " & (j + 1).ToString)
                        Else
                            Call subWrite(pintCHNo & "," & "EXT LAN設定CH設定データ　あり PORT" & ((i - 14) + 1).ToString & " 送信順 " & (j + 1).ToString)
                        End If
                    End If
                Next j
            End With
        Next i
    End Sub
    '>>>シーケンス設定から探す
    Private Sub subSeqSet(pintCHNo As Integer, pintCHID As Integer)
        Dim i As Integer = 0
        Dim j As Integer = 0

        With gudt.SetSeqSet
            'MAIN
            For i = 0 To UBound(.udtDetail) Step 1
                If .udtDetail(i).shtOutChid = pintCHNo Then
                    Call subWrite(pintCHNo & "," & "シーケンス設定　あり テーブル番号 " & (i + 1).ToString)
                End If
            Next i
            'Detail
            For i = 0 To UBound(.udtDetail) Step 1
                For j = 0 To UBound(.udtDetail(i).udtInput) Step 1
                    If .udtDetail(i).udtInput(j).shtChid = pintCHNo Then
                        Call subWrite(pintCHNo & "," & "シーケンス設定詳細　あり テーブル番号 " & (i + 1).ToString)
                    End If
                Next j


                'Ver2.0.7.W
                '>>>演算式テーブル等その他でCHNo探す
                Call ChkSeqSetProc(pintCHNo, i)
                '-

            Next i
        End With
    End Sub
    'Ver2.0.7.W
    Private Sub ChkSeqSetProc(ByVal pintCHNo As Integer, ByVal pintDtl As Integer)
        Dim i As Integer = 0
        Dim strMsg As String = ""
        strMsg = strMsg & pintCHNo & "," & "シーケンス設定演算等　あり"
        strMsg = strMsg & " シーケンステーブル番号" & (pintDtl + 1).ToString

        Dim strMsgtemp As String = ""

        Dim intRet As Integer


        With gudt.SetSeqSet.udtDetail(pintDtl)
            'ロジックの種類によってチェックポイントが異なるため分岐
            Select Case .shtLogicType
                Case 16, 28, 35, 49
                    '[16]calculate logic, [28]Integer Calculate Logic
                    '[35]methanol control, [49]IDUTSU1181 M/E PUMP CONTROL
                    '1項目目が演算式テーブル番号
                    intRet = ChkSeqCalcProc(pintCHNo, .shtLogicItem(0))
                    If intRet >= 0 Then
                        strMsg = strMsg & " 演算子テーブル " & .shtLogicItem(0).ToString & "-" & (intRet + 1).ToString
                        Call subWrite(strMsg)
                    End If
                Case 19, 22, 29, 30, 31, 32, 45, 48
                    '[19]logic pulse count, [22]Clear Running hour
                    '[29]Valve(AI-DO), [30]Valve(AI-AO), [31]Valve(DI-DO), [32]Motor(Input-Output)
                    '[45]PID CONTROLER, [48]N2086 MODE SET
                    '1項目目がCHNo
                    If pintCHNo = .shtLogicItem(0) Then
                        strMsg = strMsg & " Logic Set 1"
                        Call subWrite(strMsg)
                    End If
                Case 39, 50
                    '[39](NACKS NE231)Heel Control Seq
                    '[50]IDUTSU1181 AUTO BALLANCE
                    '1,2,3,4項目目が演算式テーブル番号
                    For i = 0 To 3 Step 1
                        intRet = ChkSeqCalcProc(pintCHNo, .shtLogicItem(i))
                        If intRet >= 0 Then
                            strMsgtemp = strMsgtemp & " 演算子テーブル " & .shtLogicItem(i).ToString & "-" & (intRet + 1).ToString
                        End If
                    Next i
                    If strMsgtemp <> "" Then
                        strMsg = strMsg & strMsgtemp
                        Call subWrite(strMsg)
                    End If

            End Select
        End With

    End Sub
    '演算式テーブルに該当CHNoがあるか探す Ver2.0.7.W
    Private Function ChkSeqCalcProc(ByVal ch_no As Integer, ByVal pintI As Integer) As Integer
        Dim intValue As Integer

        If pintI - 1 < 0 Then
            Return -1
        End If

        For j As Integer = 0 To UBound(gudt.SetSeqOpeExp.udtTables(pintI - 1).udtAryInf)
            With gudt.SetSeqOpeExp.udtTables(pintI - 1).udtAryInf(j)
                Select Case .shtType
                    'タイプが、CHNoを含む物だけ
                    Case gCstCodeSeqFixTypeChData _
                        , gCstCodeSeqFixTypeLowSet, gCstCodeSeqFixTypeHighSet _
                        , gCstCodeSeqFixTypeLLSet, gCstCodeSeqFixTypeHHSet

                        'ChNo格納
                        intValue = gConnect2Byte(.bytInfo(2), .bytInfo(3))
                        '比較
                        If ch_no = intValue Then
                            Return j
                        End If
                End Select
            End With
        Next

        Return -1
    End Function


    '>>>OPSグラフ設定から探す
    Private Sub subOpsGraph(pintCHNo As Integer, pintCHID As Integer)
        Dim i As Integer = 0
        Dim j As Integer = 0

        With gudt.SetOpsGraphM
            For i = 0 To UBound(.udtGraphExhaustRec) Step 1
                If .udtGraphExhaustRec(i).shtAveCh = pintCHNo Then
                    Call subWrite(pintCHNo & "," & "OPSグラフ設定。偏差グラフ平均CH　あり グラフ番号 " & (i + 1).ToString)
                End If
                For j = 0 To UBound(.udtGraphExhaustRec(i).udtCylinder) Step 1
                    If .udtGraphExhaustRec(i).udtCylinder(j).shtChCylinder = pintCHNo Then
                        Call subWrite(pintCHNo & "," & "OPSグラフ設定。偏差グラフシリンダCH　あり グラフ番号 " & (i + 1).ToString)
                    End If
                    If .udtGraphExhaustRec(i).udtCylinder(j).shtChDeviation = pintCHNo Then
                        Call subWrite(pintCHNo & "," & "OPSグラフ設定。偏差グラフ偏差CH　あり グラフ番号 " & (i + 1).ToString)
                    End If
                Next j
                For j = 0 To UBound(.udtGraphExhaustRec(i).udtTurboCharger) Step 1
                    If .udtGraphExhaustRec(i).udtTurboCharger(j).shtChTurboCharger = pintCHNo Then
                        Call subWrite(pintCHNo & "," & "OPSグラフ設定。偏差グラフTCCH　あり," & (i + 1).ToString)
                    End If
                Next j
            Next i
            For i = 0 To UBound(.udtGraphBarRec) Step 1
                For j = 0 To UBound(.udtGraphBarRec(i).udtCylinder) Step 1
                    If .udtGraphBarRec(i).udtCylinder(j).shtChCylinder = pintCHNo Then
                        Call subWrite(pintCHNo & "," & "OPSグラフ設定。バーグラフシリンダCH　あり グラフ番号 " & (i + 1).ToString)
                    End If
                Next j
            Next i
            For i = 0 To UBound(.udtGraphAnalogMeterRec) Step 1
                For j = 0 To UBound(.udtGraphAnalogMeterRec(i).udtDetail) Step 1
                    If .udtGraphAnalogMeterRec(i).udtDetail(j).shtChNo = pintCHNo Then
                        Call subWrite(pintCHNo & "," & "OPSグラフ設定。アナログメーターCH　あり グラフ番号 " & (i + 1).ToString)
                    End If
                Next j
            Next i
        End With
    End Sub
    '>>>ﾛｸﾞ設定印字ｵﾌﾟｼｮﾝ設定から探す
    Private Sub subOpsLogOption(pintCHNo As Integer, pintCHID As Integer)
        Dim i As Integer = 0

        With gudt.SetOpsLogOption
            For i = 0 To UBound(.udtLogOption) Step 1
                If .udtLogOption(i).shtCHNo = pintCHNo Then
                    Call subWrite(pintCHNo & "," & "ﾛｸﾞ設定印字ｵﾌﾟｼｮﾝ設定　あり テーブル番号 " & (i + 1).ToString)
                End If
            Next i
        End With
    End Sub
    '>>>GWS CH設定から探す
    Private Sub subOpsGwsCh(pintCHNo As Integer, pintCHID As Integer)
        Dim i As Integer = 0
        Dim j As Integer = 0

        With gudt.SetOpsGwsCh
            For i = 0 To UBound(.udtGwsFileRec) Step 1
                For j = 0 To UBound(.udtGwsFileRec(i).udtGwsChRec) Step 1
                    If .udtGwsFileRec(i).udtGwsChRec(j).shtChNo = pintCHNo Then
                        Call subWrite(pintCHNo & "," & "GWS CH設定　あり テーブル番号 " & (i + 1).ToString)
                    End If
                Next j
            Next i
        End With
    End Sub


#End Region

#Region "CHNo 検索Mimic群"
    'Mimicファイル検索
    Private Sub subGetMimic(pintCHNo As Integer, pintCHID As Integer)
        Dim i As Integer = 0
        Dim j As Integer = 0

        Dim strPathBase As String = ""
        'パス生成
        strPathBase = System.IO.Path.Combine(gudtFileInfo.strFilePath, gudtFileInfo.strFileVersion)
        strPathBase = System.IO.Path.Combine(strPathBase, gCstFolderNameSave)
        strPathBase = System.IO.Path.Combine(strPathBase, gCstFolderNameMimic)
        strPathBase = System.IO.Path.Combine(strPathBase, "Mimic1")

        'MimicファイルS02*.mim　を取得する
        Dim strMimicFiles As String() = System.IO.Directory.GetFiles(strPathBase, "S02*.mim", System.IO.SearchOption.AllDirectories)

        Dim pRet As IntPtr
        Dim strRet As String = ""
        Dim strPath As String = ""
        For i = 0 To UBound(strMimicFiles) Step 1
            strPath = strMimicFiles(i)
            Dim bytesData As Byte()
            'Shift JISとして文字列に変換
            bytesData = System.Text.Encoding.GetEncoding(932).GetBytes(strPath)
            strPath = System.Text.Encoding.GetEncoding(932).GetString(bytesData)
            'DLLコール
            'strRet = mainProc(strPath, pintCHNo)
            pRet = mainProc(strPath, pintCHNo)
            strRet = Marshal.PtrToStringAnsi(pRet)

            If strRet.Trim <> "" Then
                strRet = strRet.Replace(vbLf, "")
                Dim strRet2() As String = strRet.Split(vbCr)
                For j = 0 To UBound(strRet2) - 1 Step 1
                    Call subWrite(pintCHNo & "," & strRet2(j))
                Next j
            End If
        Next i
    End Sub
#End Region

#End Region

#Region "出力ファイル関連"
    Private sw As IO.StreamWriter
    'ファイルオープン
    Private Sub subOpen()
        Dim dt As DateTime = Now
        Dim strPathBase As String = ""
        strPathBase = System.IO.Path.Combine(gudtFileInfo.strFilePath, gudtFileInfo.strFileVersion)
        'Ver2.0.1.5 出力ﾌｧｲﾙは、一個だけ
        'strPathBase = System.IO.Path.Combine(strPathBase, "CHK" & dt.ToString("yyyyMMddHHmmss") & ".csv")
        strPathBase = System.IO.Path.Combine(strPathBase, gudtFileInfo.strFileName & "_CH_LIST_Search.csv")

        sw = Nothing
        Try
            sw = New IO.StreamWriter(strPathBase, False, System.Text.Encoding.GetEncoding("Shift-JIS"))

            'Ver2.0.1.5 結果をｸﾞﾛｰﾊﾞﾙ変数へ吐く
            gstrChSearchLog = ""
            'Ver2.0.1.5 ﾛｸﾞ結果ｳｨﾝﾄﾞｳ終了
            frmToolChkChListLog.Close()
        Catch ex As Exception
        End Try
    End Sub
    'データ書き込み
    Private Sub subWrite(pstrMsg As String)
        sw.WriteLine(pstrMsg)

        'Ver2.0.1.5 結果をｸﾞﾛｰﾊﾞﾙ変数へ吐く
        gstrChSearchLog = gstrChSearchLog & pstrMsg & vbCrLf
    End Sub
    'ファイルクローズ
    Private Sub subClose()
        If sw Is Nothing = False Then sw.Close()
    End Sub
#End Region

#End Region

End Class