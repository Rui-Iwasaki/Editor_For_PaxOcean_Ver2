Public Class frmFileVersion

#Region "変数定義"

    Private mintRtn As Integer
    Private mudtFileMode As gEnmFileMode
    Private mudtFileInfo As gTypFileInfo
    Private mblnExit As Boolean

#End Region

#Region "画面表示関数"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : 0:キャンセル
    ' 　　　    : 1:処理成功
    ' 　　　    :-1:処理失敗（失敗あり）
    ' 引き数    : ARG1 - (I ) ファイルモード
    ' 　　　    : ARG1 - ( O) ファイル情報構造体
    ' 　　　    : ARG1 - (I ) 終了フラグ
    ' 機能説明  : 画面表示を行い戻り値を返す
    '--------------------------------------------------------------------
    Friend Function gShow(ByVal udtFileMode As gEnmFileMode, _
                          ByRef udtFileInfo As gTypFileInfo, _
                          ByVal blnExit As Boolean) As Integer

        Try

            ''戻り値初期化
            mintRtn = 0

            ''引数保存
            mudtFileMode = udtFileMode
            mudtFileInfo = udtFileInfo
            mblnExit = blnExit

            ''画面表示
            Call Me.ShowDialog()

            ''戻り値設定
            udtFileInfo = mudtFileInfo
            Return mintRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "画面イベント"

    '--------------------------------------------------------------------
    ' 機能      : フォームロード
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 画面表示初期処理を行う
    '--------------------------------------------------------------------
    Private Sub frmFileVersion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            Dim strVerNums() As String = Nothing
            'Dim strNewestVersion As String = ""


            If mudtFileMode = gEnmFileMode.fmNew Then
                '■外販
                '外販の場合、Excelチェックは消す
                If gintNaiGai = 1 Then
                    chkExcelOUT.Checked = False
                    chkExcelOUT.Visible = False
                End If
                'JSH版の場合Excelチェックはデフォルトoff
                If gintNaiGai = 2 Then
                    chkExcelOUT.Checked = False
                End If

                '' 新規作成時の処理変更   2013.11.29
                SaveChkLabel.Text = "Do you want to be store the data in" + " " + mudtFileInfo.strFileVersion + " " + "folder ?"

                ''コンボクリア
                'Call lstVersion.Items.Clear()

            Else
                'Ver2.0.4.2
                '保存時はExcelOUTﾁｪｯｸﾎﾞｯｸｽを見せる
                chkExcelOUT.Visible = True

                '■外販
                '外販の場合、Excelチェックは消す
                If gintNaiGai = 1 Then
                    chkExcelOUT.Checked = False
                    chkExcelOUT.Visible = False
                End If
                'JSH版の場合Excelチェックはデフォルトoff
                If gintNaiGai = 2 Then
                    chkExcelOUT.Checked = False
                End If


                SaveChkLabel.Text = "Do you want to be store the data in" + " " + mudtFileInfo.strFileVersion + " " + "folder ?"


                ' ''コンボクリア
                'Call lstVersion.Items.Clear()

                'T.Ueki ファイル管理仕様変更
                ' ''バージョン番号を取得
                'Select Case gGetVerNums(mudtFileInfo, strVerNums)
                '    Case 0

                '        ''Updateコンボに既存のバージョンをセット
                '        For i As Integer = LBound(strVerNums) To UBound(strVerNums)
                '            lstVersion.Items.Add(CInt(strVerNums(i)))
                '        Next
                '        lstVersion.SelectedIndex = lstVersion.FindString(mudtFileInfo.strFileVersion)

                '        'numVersion.Value = strVerNums(UBound(strVerNums))                                                  ''最大バージョン
                '        'numVersion.Value = IIf(strVerNums(UBound(strVerNums)) = 999, 999, strVerNums(UBound(strVerNums)))  ''最大バージョン+1

                '    Case 1
                '        ''フォルダ自体がが存在しない
                '    Case 2
                '        ''バージョンフォルダが存在しない
                'End Select

                ' ''初期表示バージョンをセット
                'numVersion.Value = mudtFileInfo.strFileVersion                                                      ''現バージョン

            End If


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    'バージョン表示削除 T.Ueki
    ''--------------------------------------------------------------------
    '' 機能      : バージョンリストチェンジ
    '' 返り値    : なし
    '' 引き数    : なし
    '' 機能説明  : バージョンテキストを更新する
    ''--------------------------------------------------------------------
    'Private Sub lstVersion_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstVersion.SelectedIndexChanged

    '    Try

    '        If IsNumeric(lstVersion.Text) Then
    '            numVersion.Value = lstVersion.Text
    '        End If

    '    Catch ex As Exception
    '        Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '    End Try

    'End Sub


    ''--------------------------------------------------------------------
    '' 機能      : バージョンテキスト値変更
    '' 返り値    : なし
    '' 引き数    : なし
    '' 機能説明  : バージョンリストを更新する
    ''--------------------------------------------------------------------
    'Private Sub numVersion_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numVersion.ValueChanged

    '    Try

    '        lstVersion.SelectedIndex = lstVersion.FindString(numVersion.Value)

    '    Catch ex As Exception
    '        Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '    End Try

    'End Sub

    ''--------------------------------------------------------------------
    '' 機能      : バージョンテキストロストフォーカス
    '' 返り値    : なし
    '' 引き数    : なし
    '' 機能説明  : 値を表示する
    ''--------------------------------------------------------------------
    'Private Sub numVersion_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles numVersion.Leave

    '    Try

    '        numVersion.Text = numVersion.Value

    '    Catch ex As Exception
    '        Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '    End Try

    'End Sub

    '--------------------------------------------------------------------
    ' 機能      : OKボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : ファイル保存画面を表示する
    '--------------------------------------------------------------------
    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click

        Try
            'Ver2.0.4.2
            'ExcelOutフラグ格納
            gblExcelOut = chkExcelOUT.Checked


            ' ''バージョンをセット
            'mudtFileInfo.strFileVersion = numVersion.Text

            ''ファイル保存画面表示
            If frmFileAccess.gShow(mudtFileInfo, gEnmAccessMode.amSave, False, mblnExit, mudtFileInfo.blnVersionUP, gudt, gudt2, False, False) <> 0 Then
                mintRtn = -1
            Else

                'バージョンアップさせる予定の場合 ファイル管理仕様変更 T.Ueki
                If mudtFileInfo.blnVersionUP = True Then

                    'アップ後のファイルに変更
                    mudtFileInfo.strFileName = mudtFileInfo.strFileVersion

                    ''設定値保存
                    My.Settings.SelectPath = mudtFileInfo.strFilePath
                    My.Settings.SelectFile = mudtFileInfo.strFileName
                    My.Settings.SelectVersion = mudtFileInfo.strFileVersion

                Else
                    ''設定値保存
                    My.Settings.SelectPath = mudtFileInfo.strFilePath
                    My.Settings.SelectFile = mudtFileInfo.strFileName
                    My.Settings.SelectVersion = mudtFileInfo.strFileVersion

                End If

                Call My.Settings.Save()

                ' ''設定値保存
                'My.Settings.SelectPath = mudtFileInfo.strFilePath
                'My.Settings.SelectFile = mudtFileInfo.strFileName
                'My.Settings.SelectVersion = mudtFileInfo.strFileVersion
                'Call My.Settings.Save()

                mudtFileInfo.blnVersionUP = False
                mudtFileInfo.strFileVersionPrev = 0
                mintRtn = 1

            End If

            Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : Cancelボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 画面を閉じる
    '--------------------------------------------------------------------
    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click

        Try

            mintRtn = 0
            Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : フォームクローズ
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : フォームのインスタンスを破棄する
    '--------------------------------------------------------------------
    Private Sub frmSysSystem_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try

            Me.Dispose()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "内部関数"

#End Region

End Class