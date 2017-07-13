Public Class PictureDebug
    Public BMPtoShow As Bitmap

    Public Sub ShowComplexDoubleArray(A(,) As ComplexDouble)
        Dim F As New PictureDebug

        F.BMPtoShow = ComplexArrayToBitmap(A)
        F.PicturePlace.Image = New Bitmap(F.BMPtoShow, F.PicturePlace.Size)
        F.Show()
        F.PicturePlace.Update()
    End Sub


    Private Sub PictureDebug_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        'If Not IsNothing(BMPtoShow) Then
        '    Me.PicturePlace.Image = New Bitmap(BMPtoShow, Me.PicturePlace.Size)
        'End If
    End Sub
End Class