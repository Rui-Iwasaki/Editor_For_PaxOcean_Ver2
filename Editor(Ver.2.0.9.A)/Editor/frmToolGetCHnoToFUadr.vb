Public Class frmToolGetCHnoToFUadr

#Region "変数"

#End Region

#Region "Disp"
	Private Sub btnGO_Click(sender As Object, e As EventArgs) Handles btnGO.Click
		Dim strFileLine() As String = Nothing
		Dim i As Integer
		Dim j As Integer
		Dim strPath As String = ""

		'ダイアログを表示する
		If dlgGetCSV.ShowDialog() = DialogResult.OK Then
			strPath = System.IO.Path.GetDirectoryName(dlgGetCSV.FileName)
			'OKボタンがクリックされたとき、選択されたファイルを読み取り専用で開く
			Dim stream As System.IO.Stream
			stream = dlgGetCSV.OpenFile()
			If Not (stream Is Nothing) Then
				'内容を読み込み、表示する
				Dim sr As New System.IO.StreamReader(stream)
				Dim strFileData As String = sr.ReadToEnd()
				strFileLine = Split(strFileData, vbCrLf)
				'閉じる
				sr.Close()
				stream.Close()
			End If
		End If

		If strFileLine Is Nothing Then
			Exit Sub
		End If

		Dim strOutRec As String = ""
		Dim strFU As String = ""
		Dim strFUslot As String = ""
		Dim strFUpin As String = ""

		Call subOpen(strPath)
		Call subWrite("CHNo,FU1,FU2,FU3")
		For i = 0 To UBound(strFileLine) Step 1
			If strFileLine(i).Trim <> "" Then
				strOutRec = ""
				'CHNoを探す
				With gudt.SetChInfo
					For j = 0 To UBound(.udtChannel) Step 1
						If CInt(strFileLine(i)).ToString = .udtChannel(j).udtChCommon.shtChno.ToString Then
							strFU = .udtChannel(j).udtChCommon.shtFuno.ToString
							strFUslot = .udtChannel(j).udtChCommon.shtPortno.ToString
							strFUpin = .udtChannel(j).udtChCommon.shtPin.ToString
							strOutRec = strFileLine(i) & "," & strFU & "," & strFUslot & "," & strFUpin
							Exit For
						End If
					Next j
				End With

				'出力
				If strOutRec <> "" Then
					Call subWrite(strOutRec)
				End If
			End If
		Next i
		Call subClose()

		Me.Close()
	End Sub

	Private Sub btnEND_Click(sender As Object, e As EventArgs) Handles btnEND.Click
		Me.Close()
	End Sub

#End Region

#Region "関数"
#Region "出力ファイル関連"
	Private sw As IO.StreamWriter
	'ファイルオープン
	Private Sub subOpen(pstrPath As String)
		Dim dt As DateTime = Now
		Dim strPathBase As String = ""
		strPathBase = System.IO.Path.Combine(pstrPath, "CHout.csv")

		sw = Nothing
		Try
			sw = New IO.StreamWriter(strPathBase, True, System.Text.Encoding.GetEncoding("Shift-JIS"))
		Catch ex As Exception
		End Try
	End Sub
	'データ書き込み
	Private Sub subWrite(pstrMsg As String)
		sw.WriteLine(pstrMsg)
	End Sub
	'ファイルクローズ
	Private Sub subClose()
		If sw Is Nothing = False Then sw.Close()
	End Sub

#End Region
#End Region

End Class