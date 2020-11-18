Public Class frmChListRemoteIO

#Region "変数定義"

    ''キャンセルフラグ
    Private mintCancelFlag As Integer

    Private strFu As String
    Private intPortNo As Integer
    Private intPin As Integer
    Private intDataType As Integer
    Private intSysNo As Integer
    Private intChNo As Integer
    Private intGroupNo As Integer
    Private intDispIndex As Integer
    Private intFunction As Integer
    Private strFunction As String
    Private strRemarks As String

#End Region

#Region "画面イベント"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : 0:OK  <> 0:キャンセル
    ' 引き数    : ARG1 - (I ) FU(FCU or A ～ T)
    '           : ARG2 - (I ) FUポート番号
    '           : ARG3 - (I ) FU計測点番号
    '           : ARG4 - (IO) Data Type (1:DI 2:DO)
    '           : ARG5 - (IO) Sys No
    '           : ARG6 - (IO) CH No
    '           : ARG7 - (IO) Group No
    '           : ARG8 - (IO) Function No
    '           : ARG9 - (IO) Function Name
    '           : ARG10 -(IO) Remarks
    ' 機能説明  : 
    ' 備考      : 
    '--------------------------------------------------------------------
    Friend Function gShow(ByVal hFu As String, ByVal hPortNo As Integer, _
                          ByVal hPin As Integer, ByRef hDataType As Integer, _
                          ByRef hSysNo As Integer, ByRef hChNo As Integer, ByRef hGroupNo As Integer, _
                          ByRef hDispNo As Integer, _
                          ByRef hFunctionNo As Integer, ByRef hFunctionName As String, _
                          ByRef hRemarks As String, ByRef frmOwner As Form) As Integer

        Try

            strFu = hFu
            intPortNo = hPortNo
            intPin = hPin
            intDataType = hDataType
            intSysNo = hSysNo
            intChNo = hChNo
            intGroupNo = hGroupNo
            intDispIndex = hDispNo
            intFunction = hFunctionNo
            strFunction = hFunctionName
            strRemarks = hRemarks

            Call gShowFormModelessForCloseWait2(Me, frmOwner)

            If mintCancelFlag = 0 Then

                hDataType = intDataType
                hSysNo = intSysNo
                hChNo = intChNo
                hGroupNo = intGroupNo
                hDispNo = intDispIndex
                hFunctionNo = intFunction
                hFunctionName = strFunction
                hRemarks = strRemarks

            End If

            gShow = mintCancelFlag

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : フォームロード
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 画面表示初期処理を行う
    '--------------------------------------------------------------------
    Private Sub frmChListDigital_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''コンボボックス初期設定
            Call gSetComboBox(cmbSysNo, gEnmComboType.ctChListChannelListSysNo)

            If intDataType = 1 Then
                cmbDataType.Items.Add("DI-Function")
                cmbDataType.SelectedIndex = 0

                Call gSetComboBox(cmbFunction, gEnmComboType.ctChTerminalFunctionFuncDI)

            ElseIf intDataType = 2 Then
                cmbDataType.Items.Add("DO-Function")
                cmbDataType.SelectedIndex = 0

                Call gSetComboBox(cmbFunction, gEnmComboType.ctChTerminalFunctionFuncDO)

            End If

            ''画面初期化
            txtFuNo.Text = strFu
            txtPortNo.Text = intPortNo
            txtPin.Text = intPin.ToString("00")

            cmbSysNo.SelectedValue = intSysNo

            If intChNo > 0 Then
                txtChNo.Text = intChNo.ToString("0000")
                Call txtChNo_Validated(Me, New EventArgs)
            End If

            If intGroupNo > 0 Then txtGroupNo.Text = intGroupNo.ToString
            If intDispIndex > 0 Then txtDispIndex.Text = intDispIndex.ToString

            cmbFunction.Text = strFunction
            If cmbFunction.SelectedIndex < 0 Then cmbFunction.SelectedIndex = 0

            txtRemarks.Text = strRemarks

            mintCancelFlag = 0

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： フォームクローズ
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub frmChListDigital_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try

            Me.Dispose()

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

            mintCancelFlag = 1
            Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : OKボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 
    '--------------------------------------------------------------------
    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click

        Try

            ''入力チェック
            If Not mChkInput() Then Return

            intSysNo = cmbSysNo.SelectedValue

            intChNo = CCInt(txtChNo.Text)
            intGroupNo = CCInt(txtGroupNo.Text)
            intDispIndex = CCInt(txtDispIndex.Text)

            intFunction = cmbFunction.SelectedValue
            strFunction = cmbFunction.Text

            strRemarks = txtRemarks.Text

            Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： CH No.入力時の処理
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtChNo_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtChNo.Validated

        Try
            txtDispIndex.Text = ""
            txtGroupNo.Text = ""
            txtRemarks.Text = ""
            cmbFunction.SelectedValue = -1

            If IsNumeric(txtChNo.Text) Then

                txtChNo.Text = Integer.Parse(txtChNo.Text).ToString("0000")

                ''登録済のCHの場合、行番号等を表示する。
                For i As Integer = LBound(gudt.SetChInfo.udtChannel) To UBound(gudt.SetChInfo.udtChannel)

                    With gudt.SetChInfo.udtChannel(i).udtChCommon

                        ''チャンネルが一致
                        If gGet2Byte(.shtChno) = Integer.Parse(txtChNo.Text) Then

                            txtDispIndex.Text = .shtDispPos.ToString
                            txtGroupNo.Text = .shtGroupNo.ToString
                            txtRemarks.Text = .strRemark
                            cmbFunction.SelectedValue = .shtEccFunc

                            Exit For

                        End If

                    End With

                Next

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： KeyPressイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtChNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtChNo.KeyPress

        Try

            e.Handled = gCheckTextInput(5, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtGroupNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtGroupNo.KeyPress

        Try

            e.Handled = gCheckTextInput(2, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtDispIndex_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDispIndex.KeyPress

        Try

            e.Handled = gCheckTextInput(3, sender, e.KeyChar)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtRemarks_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRemarks.KeyPress

        Try
            e.Handled = gCheckTextInput(16, sender, e.KeyChar, False)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "内部関数"

    '--------------------------------------------------------------------
    ' 機能      : 入力チェック
    ' 返り値    : True:入力OK、False:入力NG
    ' 引き数    : なし
    ' 機能説明  : 入力チェックを行う
    '--------------------------------------------------------------------
    Private Function mChkInput() As Boolean

        Try

            ''共通数値入力チェック
            If Not gChkInputNum(txtChNo, 1, 65535, "CH No", False, True) Then Return False
            If Not gChkInputNum(txtGroupNo, 0, 36, "Group No", False, True) Then Return False ' '' K.Tanigawa 2012/01/12 グループNo.1-36 を 0 -36 に変更。0はグループ無し。0xFF
            If Not gChkInputNum(txtDispIndex, 1, 100, "Disp Index", False, True) Then Return False

            ''共通テキスト入力チェック
            If Not gChkInputText(txtRemarks, "Remarks", True, True) Then Return False

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

End Class