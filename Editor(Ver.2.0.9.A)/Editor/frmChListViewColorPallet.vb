Public Class frmChListViewColorPallet

#Region "変数定義"

    Private mboolCancel As Boolean  ''true : Cancelボタンクリック時

    Private mColorNo As Integer

#End Region

#Region "画面イベント"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : 0～8:色  -1:キャンセル
    ' 引き数    : 現表示色
    ' 機能説明  : 表示色の選択をする
    ' 備考      : 
    '--------------------------------------------------------------------

    Friend Function gShow(ByVal hNowColorNo As Integer, _
                          ByRef hColor As Color, _
                          ByRef frmOwner As Form) As Integer

        Try

            Dim selectColor As Color
            Dim selectColorNo As Integer

            mColorNo = hNowColorNo

            Call gShowFormModelessForCloseWait2(Me, frmOwner)

            If mboolCancel = False Then

                If optColor1.Checked Then
                    selectColor = lblColor1.BackColor
                    selectColorNo = 0

                ElseIf optColor2.Checked Then
                    selectColor = lblColor2.BackColor
                    selectColorNo = 1

                ElseIf optColor3.Checked Then
                    selectColor = lblColor3.BackColor
                    selectColorNo = 2

                ElseIf optColor4.Checked Then
                    selectColor = lblColor4.BackColor
                    selectColorNo = 3

                ElseIf optColor5.Checked Then
                    selectColor = lblColor5.BackColor
                    selectColorNo = 4

                ElseIf optColor6.Checked Then
                    selectColor = lblColor6.BackColor
                    selectColorNo = 5

                    'ElseIf optColor6.Checked Then
                    '    selectColor = lblColor6.BackColor
                    '    selectColorNo = 6

                    'ElseIf optColor7.Checked Then
                    '    selectColor = lblColor7.BackColor
                    '    selectColorNo = 7

                    'ElseIf optColor8.Checked Then
                    '    selectColor = lblColor8.BackColor
                    '    selectColorNo = 8

                End If

            Else
                'selectColor = Color.White   ''キャンセル時は白をセット
                selectColor = Color.Black   ''キャンセル時は黒をセット
                selectColorNo = -1

            End If

            hColor = selectColor
            gShow = selectColorNo

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
    Private Sub frmChListViewColorPallet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''参照モードの設定
            Call gSetChListDispOnly(Me, cmdOK)

            Select Case mColorNo
                Case 0
                    optColor1.Checked = True
                Case 1
                    optColor2.Checked = True
                Case 2
                    optColor3.Checked = True
                Case 3
                    optColor4.Checked = True
                Case 4
                    optColor5.Checked = True
                Case 5
                    optColor6.Checked = True
                    'Case 7
                    '    optColor7.Checked = True
                    'Case 8
                    '    optColor8.Checked = True
                    'Case 0
                    '    optColor0.Checked = True
            End Select

            mboolCancel = False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： フォームクローズ
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub frmChListViewColorPallet_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try

            Me.Dispose()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Cancelボタン クリック時処理
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click

        Try

            mboolCancel = True
            Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： OKボタン クリック時処理
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click

        Try

            Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

End Class
