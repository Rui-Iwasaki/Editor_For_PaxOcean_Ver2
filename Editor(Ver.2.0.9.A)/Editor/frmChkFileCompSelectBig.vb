Public Class frmChkFileCompSelectBig

#Region "変数"

#End Region

#Region "画面"
    Private Sub frmChkFileCompSelectBig_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        '項目を画面へ反映
        chkAll.Checked = gCompareChkBIG(0)
        chkCH.Checked = gCompareChkBIG(1)
        chkTer.Checked = gCompareChkBIG(2)
        chkAdr.Checked = gCompareChkBIG(3)
        chkTbl.Checked = gCompareChkBIG(4)
    End Sub

    Private Sub chkAll_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkAll.CheckedChanged
        '全点チェックがONの場合、計測点と端子表は強制OFF
        If chkAll.Checked = True Then
            chkCH.Checked = False
            chkTer.Checked = False
        End If
    End Sub
    Private Sub chkCH_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkCH.CheckedChanged
        '計測点チェックがONの場合、全点チェックはOFF
        If chkCH.Checked = True Then
            chkAll.Checked = False
        End If
    End Sub
    Private Sub chkTer_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkTer.CheckedChanged
        '端子表チェックがONの場合、全点チェックはOFF
        If chkTer.Checked = True Then
            chkAll.Checked = False
        End If
    End Sub

    Private Sub btnDetail_Click(sender As System.Object, e As System.EventArgs) Handles btnDetail.Click
        'ChList詳細項目選択画面起動
        Call frmChkFileCompSelectDetail.ShowDialog()
    End Sub

    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click
        'Save
        'チェックを格納
        gCompareChkBIG(0) = chkAll.Checked
        gCompareChkBIG(1) = chkCH.Checked
        gCompareChkBIG(2) = chkTer.Checked
        gCompareChkBIG(3) = chkAdr.Checked
        gCompareChkBIG(4) = chkTbl.Checked

        Call subSetCompare()

        Me.Close()
    End Sub

    Private Sub btnEXIT_Click(sender As System.Object, e As System.EventArgs) Handles btnEXIT.Click
        'Exit
        Me.Close()
    End Sub

#End Region

#Region "関数"
    Private Sub subSetCompare()
        '大比較項目をベースに小項目を編集する
        Dim i As Integer = 0

        '>>>全点チェックの場合、全部ON
        If gCompareChkBIG(0) = True Then
            For i = 0 To UBound(gCompareChk) Step 1
                gCompareChk(i) = True
            Next i
        Else
            'OFFの場合、全点ここでOFFにする
            For i = 0 To UBound(gCompareChk) Step 1
                gCompareChk(i) = False
            Next i
        End If


        '>>>計測点の場合、0と1をON
        If gCompareChkBIG(1) = True Then
            gCompareChk(0) = True
            gCompareChk(1) = True
        End If

        '>>>端子表の場合、3をON
        If gCompareChkBIG(2) = True Then
            gCompareChk(3) = True
        End If

    End Sub
#End Region


End Class