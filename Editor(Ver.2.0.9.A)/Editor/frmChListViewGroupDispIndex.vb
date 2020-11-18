Public Class frmChListViewGroupDispIndex

#Region "変数定義"

    Private mintCancelFlag As Integer
    Private mintEventCancelFlag As Integer

    ''色情報
    Private mColorInfo() As Color = {Color.WhiteSmoke, Color.Silver, Color.GreenYellow, _
                                     Color.MediumPurple, Color.Yellow, Color.CornflowerBlue, _
                                     Color.PaleTurquoise, Color.Red, Color.Gold}


    ''グループ枠配列
    Private mFrame() As System.Windows.Forms.Label

    ''グループ表示配列
    Private mCmbGroupNo() As System.Windows.Forms.ComboBox

    ''グループ情報格納
    Private Structure GroupInfo
        Public intGroupNo As Integer    ''グループ番号
        Public intColorNo As Integer    ''グループ枠色番号：0～8
        Public intDispIndex As Integer  ''グループ表示位置
    End Structure
    Private mGroupInfo(gCstChannelGroupMax - 1) As GroupInfo

#End Region

#Region "画面イベント"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : 0:OK  <> 0:キャンセル
    ' 引き数    : ARG1 - (I ) 色情報
    '           : ARG2 - (I ) グループ番号
    '           : ARG3 - (IO) 表示位置情報
    ' 機能説明  : 
    ' 備考      : 
    '--------------------------------------------------------------------
    Friend Function gShow(ByRef hGroupInfo() As frmChListViewGroup.GroupInfo, _
                          ByRef frmOwner As Form) As Integer

        Try

            For i As Integer = 0 To 35
                mGroupInfo(i).intGroupNo = hGroupInfo(i).intGroupNo
                mGroupInfo(i).intColorNo = hGroupInfo(i).intColorNo
                mGroupInfo(i).intDispIndex = hGroupInfo(i).intDispIndex
            Next i

            Call gShowFormModelessForCloseWait2(Me, frmOwner)

            If mintCancelFlag = 0 Then

                For i As Integer = 0 To 35
                    hGroupInfo(i).intDispIndex = mGroupInfo(i).intDispIndex
                Next

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
    Private Sub frmChListViewGroupDispIndex_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''参照モードの設定
            Call gSetChListDispOnly(Me, cmdOK)

            ''グループ背景色用　インスタンス作成
            mFrame = New System.Windows.Forms.Label(35) { _
                Frame01, Frame02, Frame03, Frame04, Frame05, Frame06, _
                Frame07, Frame08, Frame09, Frame10, Frame11, Frame12, _
                Frame13, Frame14, Frame15, Frame16, Frame17, Frame18, _
                Frame19, Frame20, Frame21, Frame22, Frame23, Frame24, _
                Frame25, Frame26, Frame27, Frame28, Frame29, Frame30, _
                Frame31, Frame32, Frame33, Frame34, Frame35, Frame36}

            ''グループ番号用　インスタンス作成
            mCmbGroupNo = New System.Windows.Forms.ComboBox(35) { _
                cmbGroupNo01, cmbGroupNo02, cmbGroupNo03, cmbGroupNo04, cmbGroupNo05, cmbGroupNo06, cmbGroupNo07, cmbGroupNo08, cmbGroupNo09, cmbGroupNo10, cmbGroupNo11, cmbGroupNo12, _
                cmbGroupNo13, cmbGroupNo14, cmbGroupNo15, cmbGroupNo16, cmbGroupNo17, cmbGroupNo18, cmbGroupNo19, cmbGroupNo20, cmbGroupNo21, cmbGroupNo22, cmbGroupNo23, cmbGroupNo24, _
                cmbGroupNo25, cmbGroupNo26, cmbGroupNo27, cmbGroupNo28, cmbGroupNo29, cmbGroupNo30, cmbGroupNo31, cmbGroupNo32, cmbGroupNo33, cmbGroupNo34, cmbGroupNo35, cmbGroupNo36}

            ''イベントハンドラに関連付け
            For i = 0 To gCstChannelGroupMax - 1
                AddHandler mCmbGroupNo(i).SelectedIndexChanged, AddressOf Me.cmbGroupNo_SelectedIndexChanged
            Next

            ''グループ番号コンボ初期設定
            Dim dstTBL As New DataSet

            dstTBL.Tables.Add("Tbl")
            dstTBL.Tables(0).Columns.Add("GroupNo")

            dstTBL.Tables(0).Rows.Add("")
            For i As Integer = LBound(mGroupInfo) To UBound(mGroupInfo)
                ' '' ↓↓↓ K.Tanigawa 2012/01/12 グループ無し時の処理追加。グループ番号'0'はグループ無し（データは0xFFFF)
                If mGroupInfo(i).intGroupNo = &HFFFF Then
                    dstTBL.Tables(0).Rows.Add("00")     ' '' (0xFFFF)は、表示時は"00"
                Else
                    dstTBL.Tables(0).Rows.Add(mGroupInfo(i).intGroupNo.ToString("00"))
                End If

                ' ''If mGroupInfo(i).intGroupNo = &HFFFF Then
                ' ''    dstTBL.Tables(0).Rows.Add("00" + ":TBL" + (i + 1).ToString("00"))     ' '' (0xFFFF)は、表示時は"00"
                ' ''Else
                ' ''    dstTBL.Tables(0).Rows.Add(mGroupInfo(i).intGroupNo.ToString("00") + ":TBL" + (i + 1).ToString("00"))
                ' ''End If
            Next

            For i As Integer = 0 To gCstChannelGroupMax - 1

                mCmbGroupNo(i).ValueMember = dstTBL.Tables(0).Columns(0).ColumnName
                mCmbGroupNo(i).DisplayMember = dstTBL.Tables(0).Columns(0).ColumnName
                mCmbGroupNo(i).DataSource = dstTBL.Tables(0).Copy

            Next

            ''画面設定
            Call mSetDisplay()

            mintCancelFlag = 0

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : OKボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 設定内容を保存する
    '--------------------------------------------------------------------
    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click

        Try

            ''入力チェック
            If Not mChkInput() Then Return

            Dim intGroupNo As Integer = 0
            Dim intIndex As Integer = 0

            ''クリア
            For i As Integer = 0 To gCstChannelGroupMax - 1
                mGroupInfo(i).intDispIndex = gCstCodeChGroupDisplayPositionNothing
            Next

            For i As Integer = 0 To gCstChannelGroupMax - 1

                If mCmbGroupNo(i).Text <> "" Then

                    ''グループ番号から構造体インデックスを獲得する-----------
                    intGroupNo = CCInt(mCmbGroupNo(i).SelectedValue)

                    For j As Integer = 0 To gCstChannelGroupMax - 1

                        If mGroupInfo(j).intGroupNo = intGroupNo Then ' ''' K.Tanigawa 2012/01/13 場所番号に変更

                            intIndex = j
                            Exit For

                        End If

                    Next
                    ''-------------------------------------------------------

                    mGroupInfo(intIndex).intDispIndex = i + 1

                End If

            Next

            Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : Cancelボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : フォームを閉じる
    '--------------------------------------------------------------------
    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click

        Try

            mintCancelFlag = 1
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

    '----------------------------------------------------------------------------
    ' 機能説明  ： グループ番号コンボ　変更時
    ' 引数      ： なし
    ' 戻値      ： なし
    ' 機能説明  ： 表示色も変更する
    '　　　　　　　選択したグループ番号が設定済みの場合はクリアする
    '----------------------------------------------------------------------------
    Private Sub cmbGroupNo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Try

            Dim strName As String, intNo As Integer
            Dim intGroupNo As Integer

            If mintEventCancelFlag = 1 Then
                mintEventCancelFlag = 0
                Exit Sub
            End If

            strName = CType(sender, System.Windows.Forms.ComboBox).Name
            intNo = Integer.Parse(strName.Substring(10, 2))

            If mCmbGroupNo(intNo - 1).Text <> "" Then

                intGroupNo = CCInt(mCmbGroupNo(intNo - 1).SelectedValue)

                ''グループ番号から表示色を獲得する
                For i As Integer = 0 To gCstChannelGroupMax - 1

                    ' '' ↓↓↓ K.Tanigawa 2012/01/12 グループ無し時の処理追加。グループ番号'0'はグループ無し（データは0xFF)
                    If mGroupInfo(i).intGroupNo = intGroupNo And mGroupInfo(i).intGroupNo <> &HFFFF Then

                        '' 背景色の変更不要   2014.11.18
                        'mFrame(intNo - 1).BackColor = mColorInfo(mGroupInfo(i).intColorNo)
                        Exit For

                    End If

                Next

                '選択したグループ番号が設定済みの場合はクリアする
                For i As Integer = 0 To gCstChannelGroupMax - 1

                    If i <> intNo - 1 Then

                        If mCmbGroupNo(i).SelectedValue = intGroupNo.ToString("00") Then

                            mintEventCancelFlag = 1
                            mCmbGroupNo(i).SelectedValue = ""
                            mFrame(i).BackColor = mColorInfo(0)
                            Exit For

                        End If

                    End If

                Next

            Else
                mFrame(intNo - 1).BackColor = mColorInfo(0)
            End If

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

            Dim cnt As Integer = 0

            ''グループ番号(表示位置)のダブりチェック
            For i As Integer = 1 To UBound(mCmbGroupNo) + 1

                cnt = 0
                For j As Integer = 0 To UBound(mCmbGroupNo)

                    If mCmbGroupNo(j).SelectedValue <> "" Then

                        If i = Integer.Parse(mCmbGroupNo(j).SelectedValue) Then
                            cnt += 1
                        End If

                    End If

                Next j

                If cnt > 1 Then
                    MsgBox("The group number is wrong.", MsgBoxStyle.Exclamation, "Group No. Input")
                    Return False
                End If

            Next i

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 設定値表示
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 表示位置番号と表示色を画面に表示する
    '--------------------------------------------------------------------
    Private Sub mSetDisplay()

        Try

            Dim intIndex As Integer = 0

            For i As Integer = 0 To gCstChannelGroupMax - 1

                mFrame(i).BackColor = mColorInfo(0)
                mCmbGroupNo(i).SelectedIndex = 0

            Next

            For i As Integer = 0 To gCstChannelGroupMax - 1

                If mGroupInfo(i).intDispIndex <> gCstCodeChGroupDisplayPositionNothing And _
                   mGroupInfo(i).intDispIndex <> gCstCodeChGroupDisplayPositionNothingConvert Then

                    intIndex = mGroupInfo(i).intDispIndex - 1

                    ''グループ番号
                    '' ↓↓↓ K.Tanigawa 2012/01/12 グループ無し処理を追加
                    If mGroupInfo(i).intGroupNo = &HFFFF Then
                        mCmbGroupNo(intIndex).SelectedValue = "00"             ' Group無し：0xFFFF → "00"
                    Else
                        mCmbGroupNo(intIndex).SelectedValue = mGroupInfo(i).intGroupNo.ToString("00")  ''グループ番号を表示する
                    End If

                    '' 背景色の変更不要    2014.11.18
                    'mFrame(intIndex).BackColor = mColorInfo(mGroupInfo(i).intColorNo)

                End If

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

    Private Sub cmbGroupNo01_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbGroupNo01.SelectedIndexChanged

    End Sub
End Class
