Public Class frmChListSelectType

#Region "変数定義"

    ''チャンネルタイプ
    Private mChType As Integer

#End Region

#Region "画面イベント"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : チャンネルタイプ
    ' 引き数    : 
    ' 機能説明  : 
    ' 備考      : 
    '--------------------------------------------------------------------
    Friend Function gShow(ByRef frmOwner As Form) As Integer

        Try

            Call gShowFormModelessForCloseWait2(Me, frmOwner)

            gShow = mChType   ''チャンネルタイプ

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
    Private Sub frmChListSelectType_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            mChType = 0

            '■外販
            '外販の場合、PID CH選択ﾎﾞﾀﾝは消す
            If gintNaiGai = 1 Then
                cmdPID.Visible = False
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub frmChListSelectType_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try
            Me.Dispose()
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub cmdAnalog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnalog.Click

        Try

            'frmChListAnalog.ShowDialog()
            mChType = 1
            Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub cmdDigital_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDigital.Click

        Try

            'frmChListDigital.ShowDialog()
            mChType = 2
            Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub cmdMotor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMotor.Click

        Try

            'frmChListMotor.ShowDialog()
            mChType = 3
            Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub cmdValve_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdValve.Click

        Try

            'frmChListValve.ShowDialog()
            mChType = 4
            Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub cmdDigitalComposite_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDigitalComposite.Click

        Try

            'frmChListComposite.ShowDialog()
            mChType = 5
            Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub cmdPulseRevolution_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPulseRevolution.Click

        Try

            'frmChListPulse.ShowDialog()
            mChType = 6
            Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub cmdPID_Click(sender As System.Object, e As System.EventArgs) Handles cmdPID.Click
        Try
            mChType = 7
            Me.Close()
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

#End Region


End Class

