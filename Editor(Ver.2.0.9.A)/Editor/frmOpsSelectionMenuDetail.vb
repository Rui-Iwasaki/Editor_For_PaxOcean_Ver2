Public Class frmOpsSelectionMenuDetail

#Region "定数定義"

    Private Const mCstFuncNoEnter As Integer = 1
    Private Const mCstFuncNoCancel As Integer = 2
    Private Const mCstFuncNoNext As Integer = 3
    Private Const mCstFuncNoPrevious As Integer = 4

#End Region

#Region "変数定義"

    ''セレクションメニューレコード構造体
    Private mudtSetOpsSelectionMenuRecEdit As gTypSetOpsSelectionMenuEdit = Nothing

    'Private mudtSetOpsSelectionOffSetRecEdit As gTypSetOpsSelectionMenuOffSetRecEdit = Nothing
    Private mudtSetOpsSelectionSetViewRecEdit() As gTypSetOpsSelectionMenuSetViewRecEdit = Nothing
    'Private mudtSetOpsSelectionMenuNameKeyRecEdit As gTypSetOpsSelectionMenuNameKeyRecEdit = Nothing

    ' ''セレクションメニューのデフォルト状態
    'Private mudtSetOpsSelectionMenuRecDefault As gTypSetOpsSelectionMenuRec = Nothing

    ''ボタン機能構造体
    Private mudtIniFunctionList() As gTypCodeName = Nothing

    ''設定可能ボタンリスト
    Private mintIniFunctionSet() As Integer = Nothing

    ''画面番号
    Private mstrScreenNo As String

    ''本画面戻り値
    Private mintRtn As Integer

#End Region

#Region "画面表示関数"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : 0：OK  <> 0：キャンセル
    ' 引き数    : ARG1 - (IO) コントロール使用可／不可設定構造体
    '           : ARG2 - (I ) 行Index
    ' 機能説明  : 本画面を表示する
    ' 備考      : 
    '--------------------------------------------------------------------
    Friend Function gShow(ByVal udtOpsSelectionSetViewRecEdit() As gTypSetOpsSelectionMenuSetViewRecEdit, _
                          ByVal udtIniFunctionList() As gTypCodeName, ByVal RowNo As Integer, ByRef frmOwner As Form) As Integer

        Try

            ''ボタン選択フラグ初期化
            mintRtn = 1

            ''引数保存
            mudtSetOpsSelectionSetViewRecEdit = udtOpsSelectionSetViewRecEdit
            mstrScreenNo = RowNo
            mudtIniFunctionList = udtIniFunctionList

            ''本画面表示
            Call gShowFormModelessForCloseWait2(Me, frmOwner)

            'OKで閉じる場合は戻り値設定
            If mintRtn = 0 Then
                udtOpsSelectionSetViewRecEdit = mudtSetOpsSelectionSetViewRecEdit
            End If

            Return mintRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "画面イベント"

    '----------------------------------------------------------------------------
    ' 機能説明  ： フォームロード
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub frmChControlUseNotuseDetail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''コンボボックス初期設定
            Call mSetComboBox()

            ''画面設定
            Call mSetDisplay(mudtSetOpsSelectionSetViewRecEdit, mstrScreenNo)

            CLabel.Text = "##10501: Page UP ##10503: Page Down ##10505: Group UP ##10507: Group Down  ##10509: UP  ##10511: Down"

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： デフォルトボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdDefault_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDefault.Click

        'Call mSetDisplay(mudtSetOpsSelectionMenuRecDefault)

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： コンボチェンジ
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmbButtonFunc1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbButtonFunc1.SelectedIndexChanged, _
                                                                                                                        cmbButtonFunc2.SelectedIndexChanged, _
                                                                                                                        cmbButtonFunc3.SelectedIndexChanged, _
                                                                                                                        cmbButtonFunc4.SelectedIndexChanged, _
                                                                                                                        cmbButtonFunc5.SelectedIndexChanged, _
                                                                                                                        cmbButtonFunc6.SelectedIndexChanged, _
                                                                                                                        cmbButtonFunc7.SelectedIndexChanged, _
                                                                                                                        cmbButtonFunc8.SelectedIndexChanged, _
                                                                                                                        cmbButtonFunc9.SelectedIndexChanged, _
                                                                                                                        cmbButtonFunc10.SelectedIndexChanged
        ''画面設定
        Call mRefreshBottonName()

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： フォームクローズ
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub frmChControlUseNotuseDetail_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try

            Me.Dispose()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : OKボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 保存処理を行う
    '--------------------------------------------------------------------
    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click

        Try

            ''入力チェック
            If Not mChkInput() Then Return

            '設定値の保存
            Call mSetStructure(mudtSetOpsSelectionSetViewRecEdit(mstrScreenNo), mstrScreenNo)

            mintRtn = 0
            Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Cancelボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし 
    '----------------------------------------------------------------------------
    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click

        Try

            Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "内部関数"

    Private Sub mSetComboBox()

        Dim intCnt As Integer = 1

        Dim strCode(0) As String
        Dim strName(0) As String
        

        For i As Integer = 0 To UBound(mudtIniFunctionList)

            ReDim Preserve strCode(intCnt)
            ReDim Preserve strName(intCnt)

            strCode(intCnt) = mudtIniFunctionList(i).shtCode
            strName(intCnt) = mudtIniFunctionList(i).strName

            intCnt += 1
        Next

        strCode(0) = 0
        strName(0) = ""

        Call gMakeDataMapCombo(cmbButtonFunc1, strCode, strName)
        Call gMakeDataMapCombo(cmbButtonFunc2, strCode, strName)
        Call gMakeDataMapCombo(cmbButtonFunc3, strCode, strName)
        Call gMakeDataMapCombo(cmbButtonFunc4, strCode, strName)
        Call gMakeDataMapCombo(cmbButtonFunc5, strCode, strName)
        Call gMakeDataMapCombo(cmbButtonFunc6, strCode, strName)
        Call gMakeDataMapCombo(cmbButtonFunc7, strCode, strName)
        Call gMakeDataMapCombo(cmbButtonFunc8, strCode, strName)
        Call gMakeDataMapCombo(cmbButtonFunc9, strCode, strName)
        Call gMakeDataMapCombo(cmbButtonFunc10, strCode, strName)

    End Sub


    Private Function mGetFunctionName(ByVal intFunctionNo As Integer) As String

        Dim strRtn As String = "???"

        If intFunctionNo = 0 Then Return ""

        For i As Integer = 0 To UBound(mudtIniFunctionList)

            If intFunctionNo = mudtIniFunctionList(i).shtCode Then

                strRtn = mudtIniFunctionList(i).strName
                Exit For

            End If

        Next

        Return strRtn

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 入力チェック
    ' 返り値    : True:入力OK、False:入力NG
    ' 引き数    : なし
    ' 機能説明  : 入力チェックを行う
    '--------------------------------------------------------------------
    Private Function mChkInput() As Boolean

        Try

            ''テキストボックス入力値チェック   条件範囲修正　2014.12.09
            If Not gChkInputNum(TextBox1, 0, 255, "View Postion No.1", False, True) Then Return False
            If Not gChkInputNum(TextBox2, 0, 255, "View Postion No.2", False, True) Then Return False
            If Not gChkInputNum(TextBox3, 0, 255, "View Postion No.3", False, True) Then Return False
            If Not gChkInputNum(TextBox4, 0, 255, "View Postion No.4", False, True) Then Return False
            If Not gChkInputNum(TextBox5, 0, 9999, "View Postion SCREEN NUMBER", False, True) Then Return False
            If Not gChkInputNum(TextBox6, 0, 255, "View Postion No.1", False, True) Then Return False
            If Not gChkInputNum(TextBox7, 0, 255, "View Postion No.2", False, True) Then Return False
            If Not gChkInputNum(TextBox8, 0, 255, "View Postion No.3", False, True) Then Return False
            If Not gChkInputNum(TextBox9, 0, 255, "View Postion No.4", False, True) Then Return False
            If Not gChkInputNum(TextBox10, 0, 9999, "View Postion SCREEN NUMBER", False, True) Then Return False
            If Not gChkInputNum(TextBox11, 0, 255, "View Postion No.1", False, True) Then Return False
            If Not gChkInputNum(TextBox12, 0, 255, "View Postion No.2", False, True) Then Return False
            If Not gChkInputNum(TextBox13, 0, 255, "View Postion No.3", False, True) Then Return False
            If Not gChkInputNum(TextBox14, 0, 255, "View Postion No.4", False, True) Then Return False
            If Not gChkInputNum(TextBox15, 0, 9999, "View Postion SCREEN NUMBER", False, True) Then Return False
            If Not gChkInputNum(TextBox16, 0, 255, "View Postion No.1", False, True) Then Return False
            If Not gChkInputNum(TextBox17, 0, 255, "View Postion No.2", False, True) Then Return False
            If Not gChkInputNum(TextBox18, 0, 255, "View Postion No.3", False, True) Then Return False
            If Not gChkInputNum(TextBox19, 0, 255, "View Postion No.4", False, True) Then Return False
            If Not gChkInputNum(TextBox20, 0, 9999, "View Postion SCREEN NUMBER", False, True) Then Return False
            If Not gChkInputNum(TextBox21, 0, 255, "View Postion No.1", False, True) Then Return False
            If Not gChkInputNum(TextBox22, 0, 255, "View Postion No.2", False, True) Then Return False
            If Not gChkInputNum(TextBox23, 0, 255, "View Postion No.3", False, True) Then Return False
            If Not gChkInputNum(TextBox24, 0, 255, "View Postion No.4", False, True) Then Return False
            If Not gChkInputNum(TextBox25, 0, 9999, "View Postion SCREEN NUMBER", False, True) Then Return False
            If Not gChkInputNum(TextBox26, 0, 255, "View Postion No.1", False, True) Then Return False
            If Not gChkInputNum(TextBox27, 0, 255, "View Postion No.2", False, True) Then Return False
            If Not gChkInputNum(TextBox28, 0, 255, "View Postion No.3", False, True) Then Return False
            If Not gChkInputNum(TextBox29, 0, 255, "View Postion No.4", False, True) Then Return False
            If Not gChkInputNum(TextBox30, 0, 9999, "View Postion SCREEN NUMBER", False, True) Then Return False
            If Not gChkInputNum(TextBox31, 0, 255, "View Postion No.1", False, True) Then Return False
            If Not gChkInputNum(TextBox32, 0, 255, "View Postion No.2", False, True) Then Return False
            If Not gChkInputNum(TextBox33, 0, 255, "View Postion No.3", False, True) Then Return False
            If Not gChkInputNum(TextBox34, 0, 255, "View Postion No.4", False, True) Then Return False
            If Not gChkInputNum(TextBox35, 0, 9999, "View Postion SCREEN NUMBER", False, True) Then Return False
            If Not gChkInputNum(TextBox36, 0, 255, "View Postion No.1", False, True) Then Return False
            If Not gChkInputNum(TextBox37, 0, 255, "View Postion No.2", False, True) Then Return False
            If Not gChkInputNum(TextBox38, 0, 255, "View Postion No.3", False, True) Then Return False
            If Not gChkInputNum(TextBox39, 0, 255, "View Postion No.4", False, True) Then Return False
            If Not gChkInputNum(TextBox40, 0, 9999, "View Postion SCREEN NUMBER", False, True) Then Return False
            If Not gChkInputNum(TextBox41, 0, 255, "View Postion No.1", False, True) Then Return False
            If Not gChkInputNum(TextBox42, 0, 255, "View Postion No.2", False, True) Then Return False
            If Not gChkInputNum(TextBox43, 0, 255, "View Postion No.3", False, True) Then Return False
            If Not gChkInputNum(TextBox44, 0, 255, "View Postion No.4", False, True) Then Return False
            If Not gChkInputNum(TextBox45, 0, 9999, "View Postion SCREEN NUMBER", False, True) Then Return False
            If Not gChkInputNum(TextBox46, 0, 255, "View Postion No.1", False, True) Then Return False
            If Not gChkInputNum(TextBox47, 0, 255, "View Postion No.2", False, True) Then Return False
            If Not gChkInputNum(TextBox48, 0, 255, "View Postion No.3", False, True) Then Return False
            If Not gChkInputNum(TextBox49, 0, 255, "View Postion No.4", False, True) Then Return False
            If Not gChkInputNum(TextBox50, 0, 9999, "View Postion SCREEN NUMBER", False, True) Then Return False

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '----------------------------------------------------------------------------
    ' 機能説明  ： KeyPressイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtgroupBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
              Handles TextBox1.KeyPress, TextBox2.KeyPress, TextBox3.KeyPress, TextBox4.KeyPress, TextBox6.KeyPress, TextBox7.KeyPress, TextBox8.KeyPress, TextBox9.KeyPress, _
              TextBox11.KeyPress, TextBox12.KeyPress, TextBox13.KeyPress, TextBox14.KeyPress, TextBox16.KeyPress, TextBox17.KeyPress, TextBox18.KeyPress, TextBox19.KeyPress, _
              TextBox21.KeyPress, TextBox22.KeyPress, TextBox23.KeyPress, TextBox24.KeyPress, TextBox26.KeyPress, TextBox27.KeyPress, TextBox28.KeyPress, TextBox29.KeyPress, _
              TextBox31.KeyPress, TextBox32.KeyPress, TextBox33.KeyPress, TextBox34.KeyPress, TextBox36.KeyPress, TextBox37.KeyPress, TextBox38.KeyPress, TextBox39.KeyPress, _
              TextBox41.KeyPress, TextBox42.KeyPress, TextBox43.KeyPress, TextBox44.KeyPress, TextBox46.KeyPress, TextBox47.KeyPress, TextBox48.KeyPress, TextBox49.KeyPress

        Try

            e.Handled = gCheckTextInput(3, sender, e.KeyChar, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub


    '----------------------------------------------------------------------------
    ' 機能説明  ： KeyPressイベント  2014.12.09
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtScreenNumber_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
              Handles TextBox5.KeyPress, TextBox10.KeyPress, TextBox15.KeyPress, TextBox20.KeyPress, TextBox25.KeyPress, _
                      TextBox30.KeyPress, TextBox35.KeyPress, TextBox40.KeyPress, TextBox45.KeyPress, TextBox50.KeyPress

        Try

            e.Handled = gCheckTextInput(4, sender, e.KeyChar, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 設定値格納
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) コントロール使用可／不可設定構造体
    ' 機能説明  : 構造体に設定を格納する
    '--------------------------------------------------------------------
    Private Sub mSetStructure(ByRef udtSet As gTypSetOpsSelectionMenuSetViewRecEdit, ByVal ScreenNo As Integer)

        Try

            ''名称保存
            udtSet.EditSelectName = SCREENTXT.Text

            ''処理項目保存
            udtSet.EditudtKey(0).EditBytNameType1 = TextBox1.Text
            udtSet.EditudtKey(0).EditBytNameType2 = TextBox2.Text
            udtSet.EditudtKey(0).EditBytNameType3 = TextBox3.Text
            udtSet.EditudtKey(0).EditBytNameType4 = TextBox4.Text
            udtSet.EditudtKey(0).EditBytSelectName = DataExchange2(Val(TextBox5.Text))
            udtSet.EditudtKey(0).EditNameCode = cmbButtonFunc1.SelectedIndex

            udtSet.EditudtKey(1).EditBytNameType1 = TextBox6.Text
            udtSet.EditudtKey(1).EditBytNameType2 = TextBox7.Text
            udtSet.EditudtKey(1).EditBytNameType3 = TextBox8.Text
            udtSet.EditudtKey(1).EditBytNameType4 = TextBox9.Text
            udtSet.EditudtKey(1).EditBytSelectName = DataExchange2(Val(TextBox10.Text))
            udtSet.EditudtKey(1).EditNameCode = cmbButtonFunc2.SelectedIndex

            udtSet.EditudtKey(2).EditBytNameType1 = TextBox11.Text
            udtSet.EditudtKey(2).EditBytNameType2 = TextBox12.Text
            udtSet.EditudtKey(2).EditBytNameType3 = TextBox13.Text
            udtSet.EditudtKey(2).EditBytNameType4 = TextBox14.Text
            udtSet.EditudtKey(2).EditBytSelectName = DataExchange2(Val(TextBox15.Text))
            udtSet.EditudtKey(2).EditNameCode = cmbButtonFunc3.SelectedIndex

            udtSet.EditudtKey(3).EditBytNameType1 = TextBox16.Text
            udtSet.EditudtKey(3).EditBytNameType2 = TextBox17.Text
            udtSet.EditudtKey(3).EditBytNameType3 = TextBox18.Text
            udtSet.EditudtKey(3).EditBytNameType4 = TextBox19.Text
            udtSet.EditudtKey(3).EditBytSelectName = DataExchange2(Val(TextBox20.Text))
            udtSet.EditudtKey(3).EditNameCode = cmbButtonFunc4.SelectedIndex

            udtSet.EditudtKey(4).EditBytNameType1 = TextBox21.Text
            udtSet.EditudtKey(4).EditBytNameType2 = TextBox22.Text
            udtSet.EditudtKey(4).EditBytNameType3 = TextBox23.Text
            udtSet.EditudtKey(4).EditBytNameType4 = TextBox24.Text
            udtSet.EditudtKey(4).EditBytSelectName = DataExchange2(Val(TextBox25.Text))
            udtSet.EditudtKey(4).EditNameCode = cmbButtonFunc5.SelectedIndex

            udtSet.EditudtKey(5).EditBytNameType1 = TextBox26.Text
            udtSet.EditudtKey(5).EditBytNameType2 = TextBox27.Text
            udtSet.EditudtKey(5).EditBytNameType3 = TextBox28.Text
            udtSet.EditudtKey(5).EditBytNameType4 = TextBox29.Text
            udtSet.EditudtKey(5).EditBytSelectName = DataExchange2(Val(TextBox30.Text))
            udtSet.EditudtKey(5).EditNameCode = cmbButtonFunc6.SelectedIndex

            udtSet.EditudtKey(6).EditBytNameType1 = TextBox31.Text
            udtSet.EditudtKey(6).EditBytNameType2 = TextBox32.Text
            udtSet.EditudtKey(6).EditBytNameType3 = TextBox33.Text
            udtSet.EditudtKey(6).EditBytNameType4 = TextBox34.Text
            udtSet.EditudtKey(6).EditBytSelectName = DataExchange2(Val(TextBox35.Text))
            udtSet.EditudtKey(6).EditNameCode = cmbButtonFunc7.SelectedIndex

            udtSet.EditudtKey(7).EditBytNameType1 = TextBox36.Text
            udtSet.EditudtKey(7).EditBytNameType2 = TextBox37.Text
            udtSet.EditudtKey(7).EditBytNameType3 = TextBox38.Text
            udtSet.EditudtKey(7).EditBytNameType4 = TextBox39.Text
            udtSet.EditudtKey(7).EditBytSelectName = DataExchange2(Val(TextBox40.Text))
            udtSet.EditudtKey(7).EditNameCode = cmbButtonFunc8.SelectedIndex

            udtSet.EditudtKey(8).EditBytNameType1 = TextBox41.Text
            udtSet.EditudtKey(8).EditBytNameType2 = TextBox42.Text
            udtSet.EditudtKey(8).EditBytNameType3 = TextBox43.Text
            udtSet.EditudtKey(8).EditBytNameType4 = TextBox44.Text
            udtSet.EditudtKey(8).EditBytSelectName = DataExchange2(Val(TextBox45.Text))
            udtSet.EditudtKey(8).EditNameCode = cmbButtonFunc9.SelectedIndex

            udtSet.EditudtKey(9).EditBytNameType1 = TextBox46.Text
            udtSet.EditudtKey(9).EditBytNameType2 = TextBox47.Text
            udtSet.EditudtKey(9).EditBytNameType3 = TextBox48.Text
            udtSet.EditudtKey(9).EditBytNameType4 = TextBox49.Text
            udtSet.EditudtKey(9).EditBytSelectName = DataExchange2(Val(TextBox50.Text))
            udtSet.EditudtKey(9).EditNameCode = cmbButtonFunc10.SelectedIndex


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 設定値表示
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) コントロール使用可／不可設定構造体
    ' 機能説明  : 構造体の設定を画面に表示する
    '--------------------------------------------------------------------
    Private Sub mSetDisplay(ByRef udtSetView() As gTypSetOpsSelectionMenuSetViewRecEdit, ByVal RowNo As Integer)

        Try

            ''画面番号
            lblScreenNo.Text = Str(RowNo + 1)

            ''画面名称
            SCREENTXT.Text = udtSetView(RowNo).EditSelectName

            ''ボタン機能
            cmbButtonFunc1.SelectedValue = udtSetView(RowNo).EditudtKey(0).EditNameCode
            cmbButtonFunc2.SelectedValue = udtSetView(RowNo).EditudtKey(1).EditNameCode
            cmbButtonFunc3.SelectedValue = udtSetView(RowNo).EditudtKey(2).EditNameCode
            cmbButtonFunc4.SelectedValue = udtSetView(RowNo).EditudtKey(3).EditNameCode
            cmbButtonFunc5.SelectedValue = udtSetView(RowNo).EditudtKey(4).EditNameCode
            cmbButtonFunc6.SelectedValue = udtSetView(RowNo).EditudtKey(5).EditNameCode
            cmbButtonFunc7.SelectedValue = udtSetView(RowNo).EditudtKey(6).EditNameCode
            cmbButtonFunc8.SelectedValue = udtSetView(RowNo).EditudtKey(7).EditNameCode
            cmbButtonFunc9.SelectedValue = udtSetView(RowNo).EditudtKey(8).EditNameCode
            cmbButtonFunc10.SelectedValue = udtSetView(RowNo).EditudtKey(9).EditNameCode

            Call mRefreshBottonName()

            ''処理項目表示
            TextBox1.Text = udtSetView(RowNo).EditudtKey(0).EditBytNameType1
            TextBox2.Text = udtSetView(RowNo).EditudtKey(0).EditBytNameType2
            TextBox3.Text = udtSetView(RowNo).EditudtKey(0).EditBytNameType3
            TextBox4.Text = udtSetView(RowNo).EditudtKey(0).EditBytNameType4
            TextBox5.Text = DataExchange(gGet2Byte(udtSetView(RowNo).EditudtKey(0).EditBytSelectName))

            TextBox6.Text = udtSetView(RowNo).EditudtKey(1).EditBytNameType1
            TextBox7.Text = udtSetView(RowNo).EditudtKey(1).EditBytNameType2
            TextBox8.Text = udtSetView(RowNo).EditudtKey(1).EditBytNameType3
            TextBox9.Text = udtSetView(RowNo).EditudtKey(1).EditBytNameType4
            TextBox10.Text = DataExchange(gGet2Byte(udtSetView(RowNo).EditudtKey(1).EditBytSelectName))

            TextBox11.Text = udtSetView(RowNo).EditudtKey(2).EditBytNameType1
            TextBox12.Text = udtSetView(RowNo).EditudtKey(2).EditBytNameType2
            TextBox13.Text = udtSetView(RowNo).EditudtKey(2).EditBytNameType3
            TextBox14.Text = udtSetView(RowNo).EditudtKey(2).EditBytNameType4
            TextBox15.Text = DataExchange(gGet2Byte(udtSetView(RowNo).EditudtKey(2).EditBytSelectName))

            TextBox16.Text = udtSetView(RowNo).EditudtKey(3).EditBytNameType1
            TextBox17.Text = udtSetView(RowNo).EditudtKey(3).EditBytNameType2
            TextBox18.Text = udtSetView(RowNo).EditudtKey(3).EditBytNameType3
            TextBox19.Text = udtSetView(RowNo).EditudtKey(3).EditBytNameType4
            TextBox20.Text = DataExchange(gGet2Byte(udtSetView(RowNo).EditudtKey(3).EditBytSelectName))

            TextBox21.Text = udtSetView(RowNo).EditudtKey(4).EditBytNameType1
            TextBox22.Text = udtSetView(RowNo).EditudtKey(4).EditBytNameType2
            TextBox23.Text = udtSetView(RowNo).EditudtKey(4).EditBytNameType3
            TextBox24.Text = udtSetView(RowNo).EditudtKey(4).EditBytNameType4
            TextBox25.Text = DataExchange(gGet2Byte(udtSetView(RowNo).EditudtKey(4).EditBytSelectName))

            TextBox26.Text = udtSetView(RowNo).EditudtKey(5).EditBytNameType1
            TextBox27.Text = udtSetView(RowNo).EditudtKey(5).EditBytNameType2
            TextBox28.Text = udtSetView(RowNo).EditudtKey(5).EditBytNameType3
            TextBox29.Text = udtSetView(RowNo).EditudtKey(5).EditBytNameType4
            TextBox30.Text = DataExchange(gGet2Byte(udtSetView(RowNo).EditudtKey(5).EditBytSelectName))

            TextBox31.Text = udtSetView(RowNo).EditudtKey(6).EditBytNameType1
            TextBox32.Text = udtSetView(RowNo).EditudtKey(6).EditBytNameType2
            TextBox33.Text = udtSetView(RowNo).EditudtKey(6).EditBytNameType3
            TextBox34.Text = udtSetView(RowNo).EditudtKey(6).EditBytNameType4
            TextBox35.Text = DataExchange(gGet2Byte(udtSetView(RowNo).EditudtKey(6).EditBytSelectName))

            TextBox36.Text = udtSetView(RowNo).EditudtKey(7).EditBytNameType1
            TextBox37.Text = udtSetView(RowNo).EditudtKey(7).EditBytNameType2
            TextBox38.Text = udtSetView(RowNo).EditudtKey(7).EditBytNameType3
            TextBox39.Text = udtSetView(RowNo).EditudtKey(7).EditBytNameType4
            TextBox40.Text = DataExchange(gGet2Byte(udtSetView(RowNo).EditudtKey(7).EditBytSelectName))

            TextBox41.Text = udtSetView(RowNo).EditudtKey(8).EditBytNameType1
            TextBox42.Text = udtSetView(RowNo).EditudtKey(8).EditBytNameType2
            TextBox43.Text = udtSetView(RowNo).EditudtKey(8).EditBytNameType3
            TextBox44.Text = udtSetView(RowNo).EditudtKey(8).EditBytNameType4
            TextBox45.Text = DataExchange(gGet2Byte(udtSetView(RowNo).EditudtKey(8).EditBytSelectName))

            TextBox46.Text = udtSetView(RowNo).EditudtKey(9).EditBytNameType1
            TextBox47.Text = udtSetView(RowNo).EditudtKey(9).EditBytNameType2
            TextBox48.Text = udtSetView(RowNo).EditudtKey(9).EditBytNameType3
            TextBox49.Text = udtSetView(RowNo).EditudtKey(9).EditBytNameType4
            TextBox50.Text = DataExchange(gGet2Byte(udtSetView(RowNo).EditudtKey(9).EditBytSelectName))


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub mRefreshBottonName()

        lblButtonFunc1.Text = cmbButtonFunc1.Text
        lblButtonFunc2.Text = cmbButtonFunc2.Text
        lblButtonFunc3.Text = cmbButtonFunc3.Text
        lblButtonFunc4.Text = cmbButtonFunc4.Text
        lblButtonFunc5.Text = cmbButtonFunc5.Text
        lblButtonFunc6.Text = cmbButtonFunc6.Text
        lblButtonFunc7.Text = cmbButtonFunc7.Text
        lblButtonFunc8.Text = cmbButtonFunc8.Text
        lblButtonFunc9.Text = cmbButtonFunc9.Text
        lblButtonFunc10.Text = cmbButtonFunc10.Text

    End Sub

    '--------------------------------------------------------------------
    '画面番号変更
    '--------------------------------------------------------------------
    Private Function DataExchange(ByVal BytSelectName As Integer) As Integer

        Try

            Dim bytValue1, bytValue2 As Byte
            Dim bytArray(3) As Byte

            Call gSeparat2Byte(BytSelectName, bytValue1, bytValue2)

            bytArray(0) = bytValue2
            bytArray(1) = bytValue1

            DataExchange = BitConverter.ToInt32(bytArray, 0)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    '画面番号変更
    '--------------------------------------------------------------------
    Private Function DataExchange2(ByVal BytSelectName As Integer) As Short

        Try

            Dim bytValue1, bytValue2 As Byte
            Dim bytArray(1) As Byte

            Call gSeparat2Byte(BytSelectName, bytValue1, bytValue2)

            bytArray(0) = bytValue2
            bytArray(1) = bytValue1

            DataExchange2 = BitConverter.ToInt16(bytArray, 0)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function


#End Region

End Class