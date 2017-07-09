Public Class Main

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Dataset() As ComplexDouble
        Dim N As Integer = 1100
        ReDim Dataset(N - 1)

        For I As Integer = 0 To N - 1
            Dataset(I) = New ComplexDouble(Math.Sin(2 * Math.PI * I / 250) * 2 + Rnd() * 0.4 + Math.Sin(2 * Math.PI * I / 100) * 0.5 + 1.2, 0)
            'Dataset(I) = New ComplexDouble(Rnd(), 0)
        Next

        'Zeropad dataset to correspond to power of 2 (for comparison)
        Dim Pow2 As Integer = closestLargerPowerOf2(N)
        If Not N = 2 ^ Pow2 Then
            ReDim Preserve Dataset(2 ^ Pow2 - 1)
            For I As Integer = N To UBound(Dataset)
                Dataset(I) = New ComplexDouble(0, 0)
            Next
        End If

        Dim ForwardFFT() As ComplexDouble = FFT_1D(Dataset, 1)
        Dim InverseFFT() As ComplexDouble = FFT_1D(ForwardFFT, -1)
        Dim DirectFT() As ComplexDouble = DFT(Dataset)

        Dim Diff() As ComplexDouble
        ReDim Diff(N - 1)
        For I = 0 To N - 1
            Diff(I) = ForwardFFT(I) - DirectFT(I)
        Next



        MakeChart(TChart, Dataset, "Real", "Time Series")
        MakeChart(DFTChart, InverseFFT, "Real", "") ', "DFT - Time Taken: " & DFTWatch.ElapsedMilliseconds & " ms")
        MakeChart(FFTChart, ForwardFFT, "Magnitude", "") ', "FFT - Time Taken: " & FFTWatch.ElapsedMilliseconds & " ms")

        'Me.Text = "Time Multiplier Between DFT and FFT: " & (DFTWatch.ElapsedMilliseconds / FFTWatch.ElapsedMilliseconds).ToString & "x"




    End Sub


    Private Sub OriginalPicture_Click(sender As Object, e As EventArgs) Handles OriginalPicture.Click
        'Opens a picture
        OpenFile.ShowDialog()
        If System.IO.File.Exists(OpenFile.FileName) Then
            Try
                'Tries to open the file
                Dim Timekeeper As New Stopwatch
                Timekeeper.Start()

                Dim BMPSize As New Size(400, 400)
                Dim OriginalBMP As New Bitmap(New Bitmap(OpenFile.FileName), BMPSize)

                Dim OriginalComplexArray(,) As ComplexDouble = BitmapToComplexArray(OriginalBMP)

                Dim FFTArray(,) As ComplexDouble = FFT_2D(OriginalComplexArray, 1)
                Dim IFFTArray(,) As ComplexDouble = FFT_2D(FFTArray, -1)
                Dim GaussianBlurredImage(,) As ComplexDouble = GaussianBlur(20, OriginalComplexArray)

                Dim NewBMP As Bitmap = ComplexArrayToBitmap(GaussianBlurredImage)
                Dim NewIBMP As Bitmap = ComplexArrayToBitmap(IFFTArray)




                OriginalPicture.Image = New Bitmap(OriginalBMP, OriginalPicture.Size)

                FFTPicture.Image = NewBMP

                Timekeeper.Stop()
                MsgBox("It took " & Timekeeper.ElapsedMilliseconds & "ms")

            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        End If
    End Sub

#Region "Picture Display"
    Public Function BitmapToComplexArray(Bmp As Bitmap) As ComplexDouble(,)
        'Converts a bitmap image into a complex number array
        Dim ResultArray(,) As ComplexDouble
        Dim Intensity As Double
        ReDim ResultArray(Bmp.Width - 1, Bmp.Height - 1)

        For X As Integer = 0 To Bmp.Width - 1
            For Y As Integer = 0 To Bmp.Height - 1
                Intensity = Bmp.GetPixel(X, Y).GetBrightness
                ResultArray(X, Y) = New ComplexDouble(Intensity, 0)
            Next
        Next

        Return ResultArray
    End Function

    Public Function ComplexArrayToBitmap(CArray(,) As ComplexDouble) As Bitmap
        'Converts a bitmap image into a complex number array
        Dim SizeX As Integer = UBound(CArray, 1) + 1
        Dim SizeY As Integer = UBound(CArray, 2) + 1
        Dim ResultBmp As New Bitmap(SizeX, SizeY)
        Dim NormalizedIntensity As Double
        Dim GrayscaleLevel As Integer
        Dim M As Double
        Dim MaximumMagnitude As Double = Double.MinValue

        For X As Integer = 0 To SizeX - 1
            For Y As Integer = 0 To SizeY - 1
                M = CArray(X, Y).Magnitude
                If MaximumMagnitude < M Then MaximumMagnitude = M
            Next
        Next

        For X As Integer = 0 To SizeX - 1
            For Y As Integer = 0 To SizeY - 1
                NormalizedIntensity = CArray(X, Y).Magnitude / MaximumMagnitude
                GrayscaleLevel = Int(NormalizedIntensity * 255)
                ResultBmp.SetPixel(X, Y, Color.FromArgb(GrayscaleLevel, GrayscaleLevel, GrayscaleLevel))
            Next
        Next

        Return ResultBmp
    End Function

#End Region

#Region "Chart Display"

    Public Sub MakeChart(WhicheverGraph As DataVisualization.Charting.Chart, Data() As ComplexDouble, DataToShow As String, Title As String)

        Dim DataSeries(0) As DataVisualization.Charting.Series
        DataSeries(0) = New DataVisualization.Charting.Series

        For I As Integer = 0 To UBound(Data)
            Select Case DataToShow.ToLower
                Case "magnitude"
                    DataSeries(0).Points.AddXY(I, Data(I).Magnitude)
                Case "real"
                    DataSeries(0).Points.AddXY(I, Data(I).Real)
                Case "imaginary"
                    DataSeries(0).Points.AddXY(I, Data(I).Imaginary)
            End Select

        Next


        ClearGraph(WhicheverGraph)
        InitializeGraph(WhicheverGraph)

        UpdateGraph(WhicheverGraph, Title, DataSeries, "X", "Y")

    End Sub

    Private Sub ClearGraph(WhicheverGraph As DataVisualization.Charting.Chart)
        'Clears the data in the chart
        WhicheverGraph.Titles.Clear()
        WhicheverGraph.Series.Clear()
    End Sub

    Private Sub InitializeGraph(WhicheverGraph As DataVisualization.Charting.Chart)
        'Initializes the graph formatting (because the default formatting is awful!!)
        Dim LightGrayColor, DarkGrayColor As Color
        LightGrayColor = Color.FromArgb(230, 230, 230)
        DarkGrayColor = Color.FromArgb(100, 100, 100)

        With WhicheverGraph
            .ChartAreas(0).AxisX.LineColor = DarkGrayColor
            .ChartAreas(0).AxisX.MajorGrid.LineColor = LightGrayColor
            .ChartAreas(0).AxisX.MinorGrid.LineColor = LightGrayColor
            .ChartAreas(0).AxisX.MajorTickMark.LineColor = DarkGrayColor
            .ChartAreas(0).AxisX.MinorTickMark.LineColor = DarkGrayColor
            .ChartAreas(0).AxisY.LineColor = DarkGrayColor
            .ChartAreas(0).AxisY.MajorGrid.LineColor = LightGrayColor
            .ChartAreas(0).AxisY.MinorGrid.LineColor = LightGrayColor
            .ChartAreas(0).AxisY.MajorTickMark.LineColor = DarkGrayColor
            .ChartAreas(0).AxisY.MinorTickMark.LineColor = DarkGrayColor

            .ChartAreas(0).AxisX.LabelStyle.Font = New Font(.ChartAreas(0).AxisX.LabelStyle.Font.FontFamily, 8)
            .ChartAreas(0).AxisY.LabelStyle.Font = New Font(.ChartAreas(0).AxisY.LabelStyle.Font.FontFamily, 8)
            .ChartAreas(0).AxisX.LabelStyle.ForeColor = DarkGrayColor
            .ChartAreas(0).AxisY.LabelStyle.ForeColor = DarkGrayColor

        End With
    End Sub

    Private Sub UpdateGraph(WhicheverGraph As DataVisualization.Charting.Chart, Title As String,
                            Data() As DataVisualization.Charting.Series, XLabel As String, YLabel As String)
        'Updates the graph with the three results from the simulation
        'Axis title
        WhicheverGraph.ChartAreas(0).AxisX.Title = XLabel
        WhicheverGraph.ChartAreas(0).AxisY.Title = YLabel


        'Titles
        WhicheverGraph.Titles.Clear()
        WhicheverGraph.Titles.Add(Title)

        'Series
        Dim XMin, XMax, YMin, YMax, padding As Double
        padding = 0.1

        XMin = Double.MaxValue
        YMin = Double.MaxValue
        XMax = Double.MinValue
        YMax = Double.MinValue


        For Each S As DataVisualization.Charting.Series In Data
            WhicheverGraph.Series.Add(S)

            'Finds the minima and maxima
            For Each P As DataVisualization.Charting.DataPoint In S.Points
                If P.XValue < XMin Then XMin = P.XValue
                If P.XValue > XMax Then XMax = P.XValue

                If P.YValues(0) < YMin Then YMin = P.YValues(0)
                If P.YValues(0) > YMax Then YMax = P.YValues(0)
            Next
        Next


        'Chart resizing
        Dim XInterval As Double = XMax - XMin
        Dim YInterval As Double = YMax - YMin

        If Not XMin = 0 Then
            WhicheverGraph.ChartAreas(0).AxisX.Minimum = XMin - padding * XInterval
        Else
            WhicheverGraph.ChartAreas(0).AxisX.Minimum = 0
        End If
        WhicheverGraph.ChartAreas(0).AxisX.Maximum = XMax + padding * XInterval

        If Not YMin = 0 Then
            WhicheverGraph.ChartAreas(0).AxisY.Minimum = YMin - padding * YInterval
        Else
            WhicheverGraph.ChartAreas(0).AxisY.Minimum = 0
        End If
        WhicheverGraph.ChartAreas(0).AxisY.Maximum = YMax + padding * YInterval


        'Implements smart axis tick marks
        With WhicheverGraph.ChartAreas(0)
            .AxisX.Interval = GetTickInterval(.AxisX.Minimum, .AxisX.Maximum, 5)
            .AxisX.MajorTickMark.Interval = .AxisX.Interval
            .AxisX.Minimum = Int(.AxisX.Minimum / .AxisX.Interval) * .AxisX.Interval
            .AxisX.Maximum = Int(.AxisX.Maximum / .AxisX.Interval) * .AxisX.Interval

            .AxisY.Interval = GetTickInterval(.AxisY.Minimum, .AxisY.Maximum, 5)
            .AxisY.MajorTickMark.Interval = .AxisY.Interval
            .AxisY.Minimum = Int(.AxisY.Minimum / .AxisY.Interval) * .AxisY.Interval
            .AxisY.Maximum = Int(.AxisY.Maximum / .AxisY.Interval) * .AxisY.Interval
        End With

        'Binds axes tooltips and legends
        WhicheverGraph.Legends.Clear()
        For Each S As DataVisualization.Charting.Series In WhicheverGraph.Series
            S.ToolTip = "x=#VALX" & vbCrLf & "y=#VALY"
        Next

        'Legends
        'WhicheverGraph.Legends(0).Position = New DataVisualization.Charting.ElementPosition(80, 10, 15, 20)

        'Zooming and panning capability
        WhicheverGraph.ChartAreas(0).CursorX.IsUserSelectionEnabled = True
        WhicheverGraph.ChartAreas(0).CursorY.IsUserSelectionEnabled = True
        WhicheverGraph.ChartAreas(0).AxisX.ScrollBar.Enabled = True
        WhicheverGraph.ChartAreas(0).AxisX.ScaleView.Zoomable = True
        WhicheverGraph.ChartAreas(0).AxisY.ScrollBar.Enabled = True
        WhicheverGraph.ChartAreas(0).AxisY.ScaleView.Zoomable = True

    End Sub

    Public Function GetTickInterval(MinValue As Double, MaxValue As Double, MinDivisions As Double) As Double
        'This function returns a smart tick interval for the chart
        Dim Interval As Double = Math.Floor((MaxValue - MinValue) / MinDivisions)
        If Interval < 0 Then Interval = -Interval
        Dim Magnitude As Double = Math.Pow(10, Int(Math.Log10(Interval)))

        If Magnitude > Interval Then
            GetTickInterval = Magnitude * 0.5
        ElseIf Magnitude * 2 > Interval Then
            GetTickInterval = Magnitude * 1
        ElseIf Magnitude * 5 > Interval Then
            GetTickInterval = Magnitude * 2
        Else
            GetTickInterval = Magnitude * 5
        End If

    End Function

#End Region

End Class
