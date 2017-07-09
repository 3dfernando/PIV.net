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
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim ChartArea2 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend2 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series2 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim ChartArea3 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend3 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series3 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Me.TChart = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.DFTChart = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.FFTChart = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.OriginalPicture = New System.Windows.Forms.PictureBox()
        Me.FFTPicture = New System.Windows.Forms.PictureBox()
        Me.OpenFile = New System.Windows.Forms.OpenFileDialog()
        CType(Me.TChart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DFTChart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FFTChart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OriginalPicture, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FFTPicture, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TChart
        '
        ChartArea1.Name = "ChartArea1"
        Me.TChart.ChartAreas.Add(ChartArea1)
        Legend1.Name = "Legend1"
        Me.TChart.Legends.Add(Legend1)
        Me.TChart.Location = New System.Drawing.Point(33, 12)
        Me.TChart.Name = "TChart"
        Series1.ChartArea = "ChartArea1"
        Series1.Legend = "Legend1"
        Series1.Name = "Series1"
        Me.TChart.Series.Add(Series1)
        Me.TChart.Size = New System.Drawing.Size(411, 105)
        Me.TChart.TabIndex = 1
        '
        'DFTChart
        '
        ChartArea2.Name = "ChartArea1"
        Me.DFTChart.ChartAreas.Add(ChartArea2)
        Legend2.Name = "Legend1"
        Me.DFTChart.Legends.Add(Legend2)
        Me.DFTChart.Location = New System.Drawing.Point(33, 123)
        Me.DFTChart.Name = "DFTChart"
        Series2.ChartArea = "ChartArea1"
        Series2.Legend = "Legend1"
        Series2.Name = "Series1"
        Me.DFTChart.Series.Add(Series2)
        Me.DFTChart.Size = New System.Drawing.Size(411, 105)
        Me.DFTChart.TabIndex = 2
        '
        'FFTChart
        '
        ChartArea3.Name = "ChartArea1"
        Me.FFTChart.ChartAreas.Add(ChartArea3)
        Legend3.Name = "Legend1"
        Me.FFTChart.Legends.Add(Legend3)
        Me.FFTChart.Location = New System.Drawing.Point(33, 234)
        Me.FFTChart.Name = "FFTChart"
        Series3.ChartArea = "ChartArea1"
        Series3.Legend = "Legend1"
        Series3.Name = "Series1"
        Me.FFTChart.Series.Add(Series3)
        Me.FFTChart.Size = New System.Drawing.Size(411, 105)
        Me.FFTChart.TabIndex = 3
        '
        'OriginalPicture
        '
        Me.OriginalPicture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.OriginalPicture.Location = New System.Drawing.Point(33, 345)
        Me.OriginalPicture.Name = "OriginalPicture"
        Me.OriginalPicture.Size = New System.Drawing.Size(512, 512)
        Me.OriginalPicture.TabIndex = 4
        Me.OriginalPicture.TabStop = False
        '
        'FFTPicture
        '
        Me.FFTPicture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.FFTPicture.Location = New System.Drawing.Point(551, 345)
        Me.FFTPicture.Name = "FFTPicture"
        Me.FFTPicture.Size = New System.Drawing.Size(512, 512)
        Me.FFTPicture.TabIndex = 5
        Me.FFTPicture.TabStop = False
        '
        'OpenFile
        '
        Me.OpenFile.FileName = "OpenFileDialog1"
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1189, 914)
        Me.Controls.Add(Me.FFTPicture)
        Me.Controls.Add(Me.OriginalPicture)
        Me.Controls.Add(Me.FFTChart)
        Me.Controls.Add(Me.DFTChart)
        Me.Controls.Add(Me.TChart)
        Me.Name = "Main"
        Me.Text = "Form1"
        CType(Me.TChart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DFTChart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FFTChart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OriginalPicture, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FFTPicture, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TChart As DataVisualization.Charting.Chart
    Friend WithEvents DFTChart As DataVisualization.Charting.Chart
    Friend WithEvents FFTChart As DataVisualization.Charting.Chart
    Friend WithEvents OriginalPicture As PictureBox
    Friend WithEvents FFTPicture As PictureBox
    Friend WithEvents OpenFile As OpenFileDialog
End Class
