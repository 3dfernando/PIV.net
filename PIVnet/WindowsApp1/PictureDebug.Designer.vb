<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PictureDebug
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.PicturePlace = New System.Windows.Forms.PictureBox()
        CType(Me.PicturePlace, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PicturePlace
        '
        Me.PicturePlace.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PicturePlace.Location = New System.Drawing.Point(0, 0)
        Me.PicturePlace.Name = "PicturePlace"
        Me.PicturePlace.Size = New System.Drawing.Size(435, 437)
        Me.PicturePlace.TabIndex = 1
        Me.PicturePlace.TabStop = False
        '
        'PictureDebug
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(435, 437)
        Me.Controls.Add(Me.PicturePlace)
        Me.Name = "PictureDebug"
        Me.Text = "Picture Debug Window"
        CType(Me.PicturePlace, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PicturePlace As PictureBox
End Class
