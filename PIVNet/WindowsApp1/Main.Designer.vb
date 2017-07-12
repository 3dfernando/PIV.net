<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
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
        Me.OriginalPicture = New System.Windows.Forms.PictureBox()
        Me.FFTPicture = New System.Windows.Forms.PictureBox()
        Me.OpenFile = New System.Windows.Forms.OpenFileDialog()
        Me.CorrelationImage = New System.Windows.Forms.PictureBox()
        Me.ColorBox = New System.Windows.Forms.PictureBox()
        Me.ValueLabel = New System.Windows.Forms.Label()
        CType(Me.OriginalPicture, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FFTPicture, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CorrelationImage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ColorBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'OriginalPicture
        '
        Me.OriginalPicture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.OriginalPicture.Location = New System.Drawing.Point(15, 12)
        Me.OriginalPicture.Name = "OriginalPicture"
        Me.OriginalPicture.Size = New System.Drawing.Size(512, 512)
        Me.OriginalPicture.TabIndex = 4
        Me.OriginalPicture.TabStop = False
        '
        'FFTPicture
        '
        Me.FFTPicture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.FFTPicture.Location = New System.Drawing.Point(533, 12)
        Me.FFTPicture.Name = "FFTPicture"
        Me.FFTPicture.Size = New System.Drawing.Size(512, 512)
        Me.FFTPicture.TabIndex = 5
        Me.FFTPicture.TabStop = False
        '
        'OpenFile
        '
        Me.OpenFile.FileName = "OpenFileDialog1"
        '
        'CorrelationImage
        '
        Me.CorrelationImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CorrelationImage.Location = New System.Drawing.Point(533, 530)
        Me.CorrelationImage.Name = "CorrelationImage"
        Me.CorrelationImage.Size = New System.Drawing.Size(117, 104)
        Me.CorrelationImage.TabIndex = 6
        Me.CorrelationImage.TabStop = False
        '
        'ColorBox
        '
        Me.ColorBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ColorBox.Location = New System.Drawing.Point(1051, 12)
        Me.ColorBox.Name = "ColorBox"
        Me.ColorBox.Size = New System.Drawing.Size(46, 41)
        Me.ColorBox.TabIndex = 7
        Me.ColorBox.TabStop = False
        '
        'ValueLabel
        '
        Me.ValueLabel.AutoSize = True
        Me.ValueLabel.Location = New System.Drawing.Point(1051, 56)
        Me.ValueLabel.Name = "ValueLabel"
        Me.ValueLabel.Size = New System.Drawing.Size(16, 13)
        Me.ValueLabel.TabIndex = 8
        Me.ValueLabel.Text = "   "
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1189, 914)
        Me.Controls.Add(Me.ValueLabel)
        Me.Controls.Add(Me.ColorBox)
        Me.Controls.Add(Me.CorrelationImage)
        Me.Controls.Add(Me.FFTPicture)
        Me.Controls.Add(Me.OriginalPicture)
        Me.Name = "Main"
        Me.Text = "Form1"
        CType(Me.OriginalPicture, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FFTPicture, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CorrelationImage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ColorBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OriginalPicture As PictureBox
    Friend WithEvents FFTPicture As PictureBox
    Friend WithEvents OpenFile As OpenFileDialog
    Friend WithEvents CorrelationImage As PictureBox
    Friend WithEvents ColorBox As PictureBox
    Friend WithEvents ValueLabel As Label
End Class
