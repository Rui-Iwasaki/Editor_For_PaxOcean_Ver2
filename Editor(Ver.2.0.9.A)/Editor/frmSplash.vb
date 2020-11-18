Public Class frmSplash
    Public Shared Sub ShowSplash(ByRef _thread As System.Threading.Thread)

        'スレッドの作成
        _thread = New System.Threading.Thread( _
            New System.Threading.ThreadStart(AddressOf StartThread))
        _thread.Name = "SplashForm"
        _thread.IsBackground = True
        _thread.SetApartmentState(System.Threading.ApartmentState.STA)
        'スレッドの開始
        _thread.Start()
    End Sub
    'スレッドで開始するメソッド
    Private Shared Sub StartThread()
        Application.Run(frmSplash)
    End Sub




    Private Sub frmSplash_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'ﾀｲﾏ起動
        tmProc.Enabled = True
    End Sub
    Private Sub tmProc_Tick(sender As System.Object, e As System.EventArgs) Handles tmProc.Tick
        '色変え処理
        With lblMSG
            Select Case .ForeColor
                Case Color.Black
                    .ForeColor = Color.White
                    Me.BackColor = Color.White
                Case Color.White
                    .ForeColor = Color.Black
                    Me.BackColor = Color.Blue
                Case Else
                    .ForeColor = Color.Black
                    Me.BackColor = Color.Blue
            End Select
        End With
        Application.DoEvents()
    End Sub
End Class