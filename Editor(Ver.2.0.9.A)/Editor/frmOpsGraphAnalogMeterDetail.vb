Public Class frmOpsGraphAnalogMeterDetail

#Region "定数定義"

    Private mudtSetOpsGraphNew As gTypSetOpsGraphAnalogMeterSetting = Nothing
    Private mintRtn As Integer

    ''画面サイズ
    'Private Const mCstCodeScreenHight As Integer = 230

    'T.Ueki 色関係表示無しのため画面サイズ変更
    Private Const mCstCodeScreenHight As Integer = 190

#End Region

#Region "画面表示関数"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : 0:OK  <> 0:キャンセル
    ' 引き数    : ARG1 - (IO) OPS設定：アナログメーター設定構造体
    ' 機能説明  : 本画面を表示する
    ' 備考      : 
    '--------------------------------------------------------------------
    Friend Function gShow(ByRef udtSetting As gTypSetOpsGraphAnalogMeterSetting, _
                          ByRef frmOwner As Form) As Integer

        Try

            ''ボタン選択フラグ初期化
            mintRtn = 1

            ''引数保存
            mudtSetOpsGraphNew = udtSetting

            ''本画面表示
            Call gShowFormModelessForCloseWait2(Me, frmOwner)

            ''OKで閉じる場合は戻り値設定
            If mintRtn = 0 Then udtSetting = mudtSetOpsGraphNew

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
    Private Sub frmOpsGraphAnalogMeterDetail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''画面サイズ設定
            Me.Height = mCstCodeScreenHight

            ''コンボボックス初期設定
            Call gSetComboBox(cmbDisplayPoint, gEnmComboType.ctOpsGraphAnalogDetailChNameDispPoint)
            Call gSetComboBox(cmbMark, gEnmComboType.ctOpsGraphAnalogDetailMarkNumValue)
            Call gSetComboBox(cmbPointerFrame, gEnmComboType.ctOpsGraphAnalogDetailPointerFrame)
            Call gSetComboBox(cmbPointerColorChange, gEnmComboType.ctOpsGraphAnalogDetailPointerColor)

            ''2011.06.09 仕様変更
            'Call gSetComboBox(cmbNameSideColorSymbol, gEnmComboType.ctOpsGraphAnalogDetailSideColor)

            ''画面設定
            Call mSetDisplay(mudtSetOpsGraphNew)

            '■外販
            '外販の場合、CH Name Display PointはCenter固定。Pointer Color Changeは非表示
            If gintNaiGai = 1 Then
                cmbDisplayPoint.SelectedValue = 2
                cmbDisplayPoint.Enabled = False
                Label13.Visible = False
                cmbPointerColorChange.Visible = False
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： OKボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click

        Try

            ''設定値の保存
            Call mSetStructure(mudtSetOpsGraphNew)

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

            mintRtn = 1
            Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： フォームクローズ
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub frmOpsGraphAnalogMeterDetail_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try

            Me.Dispose()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "内部関数"

    '--------------------------------------------------------------------
    ' 機能      : 設定値格納
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) アナログメーター設定構造体
    ' 機能説明  : 構造体に設定を格納する
    '--------------------------------------------------------------------
    Private Sub mSetStructure(ByRef udtSet As gTypSetOpsGraphAnalogMeterSetting)

        Try

            With udtSet

                .bytChNameDisplayPoint = CCbyte(cmbDisplayPoint.SelectedValue)          ''CH名称表示位置
                .bytMarkNumericalValue = CCbyte(cmbMark.SelectedValue)                  ''目盛数値表示方法

                'T.Ueki 使用しないため強制的に無しに変更
                cmbPointerFrame.SelectedValue = "0"

                .bytPointerFrame = CCbyte(cmbPointerFrame.SelectedValue)                ''指針の縁取り
                .bytPointerColorChange = CCbyte(cmbPointerColorChange.SelectedValue)    ''指針の色変更

                ''2011.06.09 仕様変更
                '.bytSideColorSymbol = CCbyte(cmbNameSideColorSymbol.SelectedValue)      ''シンボル表示有無

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 設定値表示
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) アナログメーター設定構造体
    ' 機能説明  : 構造体の設定を画面に表示する
    '--------------------------------------------------------------------
    Private Sub mSetDisplay(ByRef udtSet As gTypSetOpsGraphAnalogMeterSetting)

        Try

            With udtSet

                cmbDisplayPoint.SelectedValue = .bytChNameDisplayPoint.ToString         ''CH名称表示位置
                cmbMark.SelectedValue = .bytMarkNumericalValue.ToString                 ''目盛数値表示方法
                cmbPointerFrame.SelectedValue = .bytPointerFrame.ToString               ''指針の縁取り
                cmbPointerColorChange.SelectedValue = .bytPointerColorChange.ToString   ''指針の色変更

                ''2011.06.09 仕様変更
                'cmbNameSideColorSymbol.SelectedValue = .bytSideColorSymbol.ToString     ''シンボル表示有無

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 構造体複製
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 複製元
    ' 　　　    : ARG1 - ( O) 複製先
    ' 機能説明  : 構造体を複製する
    ' 備考　　  : 構造体メンバの中に構造体配列がいると単純に = では複製できないため関数を用意
    ' 　　　　  : ↑ = でやると配列部分が参照渡しになり（？）値更新時に両方更新されてしまう
    ' 　　　　  : 構造体メンバの中に構造体配列がいない場合は、この関数を使わずに = で処理しても良い
    '--------------------------------------------------------------------
    Private Sub mCopyStructure(ByVal udtSource As gTypSetOpsGraphAnalogMeterSetting, _
                               ByRef udtTarget As gTypSetOpsGraphAnalogMeterSetting)

        Try

            udtTarget.bytChNameDisplayPoint = udtSource.bytChNameDisplayPoint   ''CH名称表示位置
            udtTarget.bytMarkNumericalValue = udtSource.bytMarkNumericalValue   ''目盛数値表示方法
            udtTarget.bytPointerFrame = udtSource.bytPointerFrame               ''指針の縁取り
            udtTarget.bytPointerColorChange = udtSource.bytPointerColorChange   ''指針の色変更

            ''2011.06.09 仕様変更
            'udtTarget.bytSideColorSymbol = udtSource.bytSideColorSymbol         ''シンボル表示有無

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

    Private Sub cmbPointerColorChange_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPointerColorChange.SelectedIndexChanged

    End Sub
End Class


