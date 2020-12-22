Public Class frmFcuSelect

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 本画面を表示する
    ' 備考      : 
    '--------------------------------------------------------------------
    Friend Sub gShow()

        Try

            ''本画面表示
            Call gShowFormModelessForCloseWait1(Me)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        modFcuSelect.EditMenuCodeClear()
        Me.Close()
    End Sub

    Private Sub frmFcuSelect_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        modFcuSelect.EditMenuCodeClear()
    End Sub

    Private Sub btnOpen_Click(sender As Object, e As EventArgs) Handles btnOpen.Click

        Dim no As Integer = 0
        Dim nMenuCode As Integer = 0

        If rbtSelectFcu1.Checked = True And rbtSelectFcu2.Checked = False Then
            no = 1
        End If

        If rbtSelectFcu1.Checked = False And rbtSelectFcu2.Checked = True Then
            no = 2
        End If

        If modFcuSelect.SetFcuNumber(no) = True Then

            nMenuCode = modFcuSelect.nMenuCode


        Else

        End If

        Me.Close()

    End Sub

End Class