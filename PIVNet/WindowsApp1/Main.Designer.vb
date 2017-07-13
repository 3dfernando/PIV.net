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
        Me.DisplacedPicture = New System.Windows.Forms.PictureBox()
        Me.OpenFile = New System.Windows.Forms.OpenFileDialog()
        Me.CorrelationImage = New System.Windows.Forms.PictureBox()
        Me.ColorBox = New System.Windows.Forms.PictureBox()
        Me.ValueLabel = New System.Windows.Forms.Label()
        Me.cmdImg = New System.Windows.Forms.Button()
        Me.txtWindowSize = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.MaxDisp = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.NGrid = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        CType(Me.OriginalPicture, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DisplacedPicture, System.ComponentModel.ISupportInitialize).BeginInit()
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
        'DisplacedPicture
        '
        Me.DisplacedPicture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.DisplacedPicture.Location = New System.Drawing.Point(533, 12)
        Me.DisplacedPicture.Name = "DisplacedPicture"
        Me.DisplacedPicture.Size = New System.Drawing.Size(512, 512)
        Me.DisplacedPicture.TabIndex = 5
        Me.DisplacedPicture.TabStop = False
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
        'cmdImg
        '
        Me.cmdImg.Location = New System.Drawing.Point(77, 540)
        Me.cmdImg.Name = "cmdImg"
        Me.cmdImg.Size = New System.Drawing.Size(126, 33)
        Me.cmdImg.TabIndex = 9
        Me.cmdImg.Text = "Load Image Pair"
        Me.cmdImg.UseVisualStyleBackColor = True
        '
        'txtWindowSize
        '
        Me.txtWindowSize.Location = New System.Drawing.Point(238, 598)
        Me.txtWindowSize.Name = "txtWindowSize"
        Me.txtWindowSize.Size = New System.Drawing.Size(71, 20)
        Me.txtWindowSize.TabIndex = 10
        Me.txtWindowSize.Text = "50"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(58, 601)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 13)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Window Size:"
        '
        'MaxDisp
        '
        Me.MaxDisp.Location = New System.Drawing.Point(238, 624)
        Me.MaxDisp.Name = "MaxDisp"
        Me.MaxDisp.Size = New System.Drawing.Size(71, 20)
        Me.MaxDisp.TabIndex = 10
        Me.MaxDisp.Text = "10"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(58, 627)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(169, 13)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Maximum Expected Displacement:"
        '
        'NGrid
        '
        Me.NGrid.Location = New System.Drawing.Point(238, 650)
        Me.NGrid.Name = "NGrid"
        Me.NGrid.Size = New System.Drawing.Size(71, 20)
        Me.NGrid.TabIndex = 10
        Me.NGrid.Text = "5"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(58, 653)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(88, 13)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Nº of Grid Points:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(315, 601)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(18, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "px"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(317, 627)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(0, 13)
        Me.Label6.TabIndex = 11
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(315, 627)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(18, 13)
        Me.Label7.TabIndex = 11
        Me.Label7.Text = "px"
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1187, 914)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.NGrid)
        Me.Controls.Add(Me.MaxDisp)
        Me.Controls.Add(Me.txtWindowSize)
        Me.Controls.Add(Me.cmdImg)
        Me.Controls.Add(Me.ValueLabel)
        Me.Controls.Add(Me.ColorBox)
        Me.Controls.Add(Me.CorrelationImage)
        Me.Controls.Add(Me.DisplacedPicture)
        Me.Controls.Add(Me.OriginalPicture)
        Me.Name = "Main"
        Me.Text = "Form1"
        CType(Me.OriginalPicture, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DisplacedPicture, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CorrelationImage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ColorBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OriginalPicture As PictureBox
    Friend WithEvents DisplacedPicture As PictureBox
    Friend WithEvents OpenFile As OpenFileDialog
    Friend WithEvents CorrelationImage As PictureBox
    Friend WithEvents ColorBox As PictureBox
    Friend WithEvents ValueLabel As Label
    Friend WithEvents cmdImg As Button
    Friend WithEvents txtWindowSize As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents MaxDisp As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents NGrid As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
End Class
