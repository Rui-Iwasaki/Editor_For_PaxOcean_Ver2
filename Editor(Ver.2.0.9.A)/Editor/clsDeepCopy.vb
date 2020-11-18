Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Runtime.CompilerServices

Public Module DeepCopyUtils

    <Extension()> _
    Public Function DeepCopy(ByVal target As Object) As Object

        Dim result As Object
        Dim b As New BinaryFormatter

        Dim mem As New System.IO.MemoryStream()

        Try

            b.Serialize(mem, target)
            mem.Position = 0
            result = b.Deserialize(mem)

        Finally

            mem.Close()

        End Try

        Return result

    End Function

End Module

Public Class DeepCopyHelper

    Public Shared Function DeepCopy(Of T)(ByVal target As T) As T

        Dim result As T
        Dim b As New BinaryFormatter

        Dim mem As New System.IO.MemoryStream()

        Try

            b.Serialize(mem, target)
            mem.Position = 0
            result = CType(b.Deserialize(mem), T)

        Finally

            mem.Close()

        End Try

        Return result

    End Function

End Class