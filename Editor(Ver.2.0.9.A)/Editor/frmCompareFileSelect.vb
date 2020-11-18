Public Class frmCompareFileSelect

#Region "変数定義"

    Private mintRtn As Integer
    Private mudtFileMode As gEnmFileMode
    Private mudtFileInfoMain As gTypCompareFileInfo
    Private mudtFileInfoTemp As gTypCompareFileInfo
    Private mudtSource As clsStructure
    Private mudtTarget As clsStructure

    Private blnCFRead As Boolean
    Private blnSaveRead As Boolean
    Private blnCompileRead As Boolean

#End Region

#Region "画面表示関数"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : 0:キャンセル
    ' 　　　    : 1:処理成功
    ' 　　　    :-1:処理失敗（失敗あり）
    ' 引き数    : ARG1 - (I ) ファイルモード
    ' 　　　    : ARG1 - ( O) ファイル情報構造体
    ' 機能説明  : 画面表示を行い戻り値を返す
    '--------------------------------------------------------------------
    Friend Function gShow(ByVal udtFileMode As gEnmFileMode, _
                          ByRef udtFileInfo As gTypCompareFileInfo, _
                          ByRef udtSource As clsStructure, _
                          ByRef udtTarget As clsStructure, _
                          ByVal CFRead As Boolean, _
                          ByVal SaveRead As Boolean, _
                          ByVal CompileRead As Boolean) As Integer

        Try

            ''戻り値初期化
            mintRtn = 0

            ''引数の保存
            mudtFileMode = udtFileMode
            mudtFileInfoMain = udtFileInfo
            mudtFileInfoTemp = udtFileInfo
            mudtSource = udtSource
            mudtTarget = udtTarget
            blnCFRead = CFRead
            blnSaveRead = SaveRead
            blnCompileRead = CompileRead

            ''画面表示
            Call Me.ShowDialog()

            ''戻り値設定
            If mintRtn <> 0 Then

                ''決定された場合はファイル情報を更新する
                udtFileInfo = mudtFileInfoTemp

                If udtFileMode = gEnmFileMode.fmEdit Then
                    udtSource = mudtSource
                    udtTarget = mudtTarget
                End If

            Else

                ''キャンセルされた場合はファイル情報を元に戻す
                udtFileInfo = mudtFileInfoMain

            End If

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
    Private Sub frmFileSelect_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            With mudtFileInfoTemp

                Me.Text = "File Select"
                txtFilePath.Text = .strFilePath
                txtFileName.Text = ""

                cmdRef.Enabled = True
                txtFilePath.Enabled = True

                'CFカード読込みの場合は[File Name]欄非表示
                If blnCFRead = True Then
                    txtFileName.Enabled = False
                Else
                    txtFileName.Enabled = True
                End If

                Call txtFileName.Focus()
                Call cmdOK.Focus()

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : フォルダ参照ボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : フォルダ選択ダイアログを表示する
    '--------------------------------------------------------------------
    Private Sub cmdRef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRef.Click

        Try

            Dim strFilePath As String = ""
            Dim strFileName As String = ""
            Dim strVersions() As String = Nothing
            Dim blnExistVersion As Boolean

            With mudtFileInfoTemp

                ''初期フォルダ設定
                If System.IO.Directory.Exists(txtFilePath.Text.Trim()) Then
                    fdgFolder.SelectedPath = txtFilePath.Text.Trim()
                Else
                    fdgFolder.SelectedPath = "C:\"
                End If

                ''フォルダ選択ダイアログ表示
                If fdgFolder.ShowDialog = Windows.Forms.DialogResult.OK Then

                    ''保存フォルダとして正しいフォルダが選択されたかチェック
                    If mChkSelectFolder(fdgFolder.SelectedPath, blnExistVersion, blnCFRead) Then

                        Select Case mudtFileMode

                            Case gEnmFileMode.fmEdit

                                ''ファイル情報表示
                                Call mGetPathAndFileName(fdgFolder.SelectedPath, .strFilePath, .strFileName, .strFileOrgPath)
                                txtFilePath.Text = .strFilePath
                                txtFileName.Text = .strFileName
                               
                        End Select

                    End If

                End If

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : OKボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : モード別の処理を行う
    '--------------------------------------------------------------------
    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click

        Try

            Dim intRtn As Integer
            Dim strPathNew As String = ""
            Dim strPathOld As String = ""


            With mudtFileInfoTemp

                ''入力チェック
                If Not mChkInput() Then Return

                ''画面設定値を構造体に格納
                .strFilePath = txtFilePath.Text
                .strFileName = txtFileName.Text

                ''モード毎に処理を分岐
                Select Case mudtFileMode

                    Case gEnmFileMode.fmEdit

                        ''各設定ファイルから設定値を読み込み構造体に設定
                        intRtn = frmCompareFileAccess.gShow(mudtFileInfoTemp, blnCFRead, blnSaveRead, blnCompileRead, False, mudtSource)

                        ''読み込み成功の場合は 1 、読み込み失敗の場合は -1 を戻り値に設定する
                        intRtn = IIf(intRtn = 0, 1, -1)

                        If intRtn <> -1 Then
                            'CFカードの場合はorg内も読込み
                            If blnCFRead = True Then
                                ''各設定ファイルから設定値を読み込み構造体に設定
                                intRtn = frmCompareFileAccess.gShow(mudtFileInfoTemp, blnCFRead, blnSaveRead, blnCompileRead, True, mudtTarget)

                                ''読み込み成功の場合は 1 、読み込み失敗の場合は -1 を戻り値に設定する
                                intRtn = IIf(intRtn = 0, 1, -1)
                            End If
                        End If

                End Select

            End With

            mintRtn = intRtn
            Call Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : ファイル名テキストフォーカス取得
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : テキストを選択する
    '--------------------------------------------------------------------
    Private Sub txtFileName_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFileName.Enter

        Try


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : ファイルパステキストキープレス
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 値を表示する
    '--------------------------------------------------------------------
    Private Sub txtFilePath_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFilePath.KeyPress

        Try

            e.Handled = gCheckTextInput(255, sender, e.KeyChar, False, , , True)

            If e.KeyChar = "?"c Then e.Handled = True
            If e.KeyChar = "/"c Then e.Handled = True
            If e.KeyChar = "*"c Then e.Handled = True
            If e.KeyChar = """"c Then e.Handled = True
            If e.KeyChar = "<"c Then e.Handled = True
            If e.KeyChar = ">"c Then e.Handled = True
            If e.KeyChar = "|"c Then e.Handled = True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : ファイルパステキスト検証
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 値を表示する
    '--------------------------------------------------------------------
    Private Sub txtFilePath_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtFilePath.Validating

        Try

            txtFileName.Text = txtFileName.Text.Replace("?", "")
            txtFileName.Text = txtFileName.Text.Replace("/", "")
            txtFileName.Text = txtFileName.Text.Replace("*", "")
            txtFileName.Text = txtFileName.Text.Replace("""", "")
            txtFileName.Text = txtFileName.Text.Replace(">", "")
            txtFileName.Text = txtFileName.Text.Replace("<", "")
            txtFileName.Text = txtFileName.Text.Replace("|", "")

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : ファイル名テキストキープレス
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 値を表示する
    '--------------------------------------------------------------------
    Private Sub txtFileName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFileName.KeyPress

        Try

            e.Handled = gCheckTextInput(16, sender, e.KeyChar, False)

            ''半角のアンダーバーとハイフン以外、記号の使用不可
            If e.KeyChar = "\"c Then e.Handled = True
            If e.KeyChar = "?"c Then e.Handled = True
            If e.KeyChar = "/"c Then e.Handled = True
            If e.KeyChar = ":"c Then e.Handled = True
            If e.KeyChar = "*"c Then e.Handled = True
            If e.KeyChar = """"c Then e.Handled = True
            If e.KeyChar = "<"c Then e.Handled = True
            If e.KeyChar = ">"c Then e.Handled = True
            If e.KeyChar = "|"c Then e.Handled = True

            If e.KeyChar = "!"c Then e.Handled = True
            If e.KeyChar = "#"c Then e.Handled = True
            If e.KeyChar = "$"c Then e.Handled = True
            If e.KeyChar = "%"c Then e.Handled = True
            If e.KeyChar = "&"c Then e.Handled = True
            If e.KeyChar = "'"c Then e.Handled = True
            If e.KeyChar = "("c Then e.Handled = True
            If e.KeyChar = ")"c Then e.Handled = True
            If e.KeyChar = "="c Then e.Handled = True
            If e.KeyChar = "~"c Then e.Handled = True
            If e.KeyChar = "{"c Then e.Handled = True
            If e.KeyChar = "}"c Then e.Handled = True
            If e.KeyChar = "`"c Then e.Handled = True
            If e.KeyChar = "+"c Then e.Handled = True
            If e.KeyChar = "."c Then e.Handled = True
            If e.KeyChar = ","c Then e.Handled = True
            If e.KeyChar = ";"c Then e.Handled = True
            If e.KeyChar = "["c Then e.Handled = True
            If e.KeyChar = "]"c Then e.Handled = True
            If e.KeyChar = "@"c Then e.Handled = True
            If e.KeyChar = "^"c Then e.Handled = True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : ファイル名テキスト検証
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 値を表示する
    '--------------------------------------------------------------------
    Private Sub txtFileName_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtFileName.Validating

        Try

            ''半角のアンダーバーとハイフン以外、記号の使用不可
            txtFileName.Text = txtFileName.Text.Replace("\", "")
            txtFileName.Text = txtFileName.Text.Replace("?", "")
            txtFileName.Text = txtFileName.Text.Replace("/", "")
            txtFileName.Text = txtFileName.Text.Replace(":", "")
            txtFileName.Text = txtFileName.Text.Replace("*", "")
            txtFileName.Text = txtFileName.Text.Replace("""", "")
            txtFileName.Text = txtFileName.Text.Replace(">", "")
            txtFileName.Text = txtFileName.Text.Replace("<", "")
            txtFileName.Text = txtFileName.Text.Replace("|", "")

            txtFileName.Text = txtFileName.Text.Replace("!", "")
            txtFileName.Text = txtFileName.Text.Replace("#", "")
            txtFileName.Text = txtFileName.Text.Replace("$", "")
            txtFileName.Text = txtFileName.Text.Replace("%", "")
            txtFileName.Text = txtFileName.Text.Replace("&", "")
            txtFileName.Text = txtFileName.Text.Replace("'", "")
            txtFileName.Text = txtFileName.Text.Replace("(", "")
            txtFileName.Text = txtFileName.Text.Replace(")", "")
            txtFileName.Text = txtFileName.Text.Replace("=", "")
            txtFileName.Text = txtFileName.Text.Replace("~", "")
            txtFileName.Text = txtFileName.Text.Replace("{", "")
            txtFileName.Text = txtFileName.Text.Replace("}", "")
            txtFileName.Text = txtFileName.Text.Replace("`", "")
            txtFileName.Text = txtFileName.Text.Replace("+", "")
            txtFileName.Text = txtFileName.Text.Replace(",", "")
            txtFileName.Text = txtFileName.Text.Replace(".", "")
            txtFileName.Text = txtFileName.Text.Replace(";", "")
            txtFileName.Text = txtFileName.Text.Replace("]", "")
            txtFileName.Text = txtFileName.Text.Replace("[", "")
            txtFileName.Text = txtFileName.Text.Replace("@", "")
            txtFileName.Text = txtFileName.Text.Replace("^", "")

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : キャンセルボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 画面を閉じる
    '--------------------------------------------------------------------
    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click

        Try

            mintRtn = 0
            Call Me.Close()

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

    '--------------------------------------------------------------------
    ' 機能      : 入力チェック
    ' 返り値    : True:入力OK、False:入力NG
    ' 引き数    : なし
    ' 機能説明  : 入力チェックを行う
    '--------------------------------------------------------------------
    Private Function mChkInput() As Boolean

        Try

            Dim strDataPath As String = ""

            ''パスが入力されていない場合
            If txtFilePath.Text.Trim() = "" Then
                Call MessageBox.Show("Please input the [Saving Place].", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Call txtFilePath.Focus()
                Return False
            End If

            ''パスが存在しない場合
            If Not System.IO.Directory.Exists(txtFilePath.Text.Trim()) Then
                Call MessageBox.Show("The folder set to [Saving Place] doesn't exist. ", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Call txtFilePath.Focus()
                Return False
            End If

            '選択肢が「1」で固定だが、予備としてSelect文は残す
            Select Case mudtFileMode

                Case gEnmFileMode.fmEdit

                    If blnCFRead Then
                        strDataPath = txtFilePath.Text
                    ElseIf blnSaveRead Then
                        strDataPath = System.IO.Path.Combine(txtFilePath.Text, txtFileName.Text)
                        strDataPath = System.IO.Path.Combine(strDataPath, gCstFolderNameSave)
                    Else
                        strDataPath = System.IO.Path.Combine(txtFilePath.Text, txtFileName.Text)
                        strDataPath = System.IO.Path.Combine(strDataPath, gCstFolderNameCompile)
                    End If

                    ''バージョン番号までのパスが存在しない場合
                    If Not System.IO.Directory.Exists(strDataPath) Then
                        Call MessageBox.Show("The Order file folder doesn't exist. " & vbNewLine & vbNewLine & _
                                             strDataPath, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        'フォーカスする場所を変える
                        If blnCFRead Then
                            txtFilePath.Focus()
                        Else
                            Call txtFileName.Focus()
                        End If

                        Return False
                    End If

            End Select

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 選択パスチェック
    ' 返り値    : True:OK、False:NG
    ' 引き数    : ARG1 - (I ) 選択パス
    ' 機能説明  : 選択パスが正しいかチェックする
    '--------------------------------------------------------------------
    Private Function mChkSelectFolder(ByVal strSelectPath As String, _
                                      ByRef blnExistVersion As Boolean, _
                                      ByRef blnCFCardRead As Boolean) As Boolean

        Try

            ''更新の場合
            If mudtFileMode = gEnmFileMode.fmEdit Then

                '2014/5/14 コンペアの場合でCFｶｰﾄﾞから読込む場合はバージョン確認を行わない。 T.Ueki

                ''選択したフォルダ配下にバージョンがあるかチェック
                If Not gExistFolderVersion(fdgFolder.SelectedPath, blnCFCardRead) Then

                    Call MessageBox.Show("Under this Folder, there is no '" & gCstVersionPrefix & "xxx' folder." & vbNewLine & _
                                         "Try to select folder again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return False

                End If

            End If

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : ファイルパス、ファイル名取得
    ' 返り値    : 0:OK、-1:選択パス不正
    ' 引き数    : ARG1 - (I ) 選択パス
    ' 　　　    : ARG2 - ( O) ファイルパス
    ' 　　　    : ARG3 - ( O) ファイル名
    ' 機能説明  : 選択パスからファイルパスとファイル名を取得する
    '--------------------------------------------------------------------
    Private Function mGetPathAndFileName(ByVal strSelectPath As String, _
                                         ByRef strFilePath As String, _
                                         ByRef strFileName As String, _
                                         ByRef strFileOrgPath As String) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim strwk() As String = Nothing

            strFilePath = ""
            strFileName = ""

            strwk = strSelectPath.Split("\")

            If strwk Is Nothing Then
                intRtn = -1
            Else

                If blnCFRead = False Then
                    For i As Integer = LBound(strwk) To UBound(strwk)

                        If i <> UBound(strwk) Then
                            strFilePath &= strwk(i) & "\"
                        Else
                            strFileName = strwk(i)
                        End If

                    Next

                    strFileOrgPath = ""

                Else
                    'CFカードの場合はファイルパスはそのまま
                    strFilePath = strSelectPath
                    strFileName = ""
                    strFileOrgPath = strSelectPath & "\org"
                End If

            End If

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

End Class