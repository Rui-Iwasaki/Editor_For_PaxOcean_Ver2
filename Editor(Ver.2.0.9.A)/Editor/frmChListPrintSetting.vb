Public Class frmChListPrintSetting

#Region "変数定義"

    ''保存フラグ
    Private mintSaveFlag As Integer

    Private mDrawNo As String
    Private mComment As String
    Private mShip As String

#End Region

#Region "画面イベント"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : 0:OK  <> 0:キャンセル
    ' 引き数    : ARG1 - (O) DrawNo
    '           : ARG2 - (O) Comment
    '           : ARG3 - (O) Ship
    ' 機能説明  : 
    ' 備考      : 
    '--------------------------------------------------------------------
    Friend Function gShow(ByRef hDrawNo As String, _
                          ByRef hComment As String, _
                          ByRef hShip As String, _
                          ByRef frmOwner As Form) As Integer

        Try

            Dim intAns As Integer = 1

            mDrawNo = hDrawNo
            mComment = hComment
            mShip = hShip

            Call gShowFormModelessForCloseWait2(Me, frmOwner)

            If mintSaveFlag = 1 Then

                hDrawNo = mDrawNo
                hComment = mComment
                hShip = mShip

                intAns = 0

            End If

            gShow = intAns

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
    Private Sub frmChListPrintSetting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''参照モードの設定
            Call gSetChListDispOnly(Me, cmdOK)

            txtDrawNo.Text = mDrawNo.Trim
            txtComment.Text = mComment.Trim
            txtShipName.Text = mShip.Trim

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： フォームクローズ
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub frmChListPrintSetting_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

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

            Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： OKボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click

        Try

            ''共通テキスト入力チェック
            If Not gChkInputText(txtDrawNo, "Draw No", True, True) Then Exit Sub
            If Not gChkInputText(txtComment, "Comment", True, True) Then Exit Sub
            If Not gChkInputText(txtShipName, "Ship No", True, True) Then Exit Sub

            mDrawNo = txtDrawNo.Text
            mComment = txtComment.Text
            mShip = txtShipName.Text

            mintSaveFlag = 1

            Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： KeyPressイベント
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtDrawNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDrawNo.KeyPress

        Try

            e.Handled = gCheckTextInput(8, sender, e.KeyChar, False)


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtShipName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtShipName.KeyPress

        Try

            e.Handled = gCheckTextInput(40, sender, e.KeyChar, False)


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub txtComment_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtComment.KeyPress

        Try

            e.Handled = gCheckTextInput(32, sender, e.KeyChar, False)


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region
 
End Class
