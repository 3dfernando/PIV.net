Module NormXCorr
    'Implements functions needed for performing normalized cross correlations

#Region "Image Pair Comparison"

    Public Function DisplacementVectorField(ImgA As Bitmap, ImgB As Bitmap, MaxVectorDisplacement As Point, WindowSize As Size, GridSize As Size, ByRef Spacing As Point) As VectorDisplacement(,)
        'Generates the displacement vector field, given two images.
        Dim ResultingVectors(,) As VectorDisplacement
        ReDim ResultingVectors(GridSize.Width - 1, GridSize.Height - 1)

        If Not ImgA.Size = ImgB.Size Then Return Nothing

        'Makes complexdouble correspondents for each image
        Dim ImgArrayA(,) As ComplexDouble = BitmapToComplexArray(ImgA)
        Dim ImgArrayB(,) As ComplexDouble = BitmapToComplexArray(ImgB)


        'Rounds up the window size to the highest even number to avoid half-pixels
        If WindowSize.Height Mod 2 <> 0 Then WindowSize.Height = Math.Ceiling(WindowSize.Height / 2) * 2
        If WindowSize.Width Mod 2 <> 0 Then WindowSize.Width = Math.Ceiling(WindowSize.Width / 2) * 2

        Dim InterrogationWindowSize As New Size(WindowSize.Height + MaxVectorDisplacement.X * 2, WindowSize.Width + MaxVectorDisplacement.Y * 2)

        Spacing = New Point((ImgA.Width - InterrogationWindowSize.Width) / (GridSize.Width - 1), (ImgA.Height - InterrogationWindowSize.Height) / (GridSize.Height - 1)) 'Outputs the spacing of the grid points


        For GridX As Integer = 0 To GridSize.Width - 1
            For GridY As Integer = 0 To GridSize.Height - 1
                'Calculates the pixel centre for this grid point and rounds it (the pixel center won't be / shouldn't be a fractional value)
                Dim GridPixelPos As New Point()

                GridPixelPos.X = Int((InterrogationWindowSize.Width / 2) + GridX * Spacing.X)
                If GridPixelPos.X < 0 Then GridPixelPos.X = 0
                If GridPixelPos.X > (ImgA.Width - 1 - InterrogationWindowSize.Width / 2) Then GridPixelPos.X = (ImgA.Width - 1 - InterrogationWindowSize.Width / 2)

                GridPixelPos.Y = Int((InterrogationWindowSize.Height / 2) + GridY * Spacing.Y)
                If GridPixelPos.Y < 0 Then GridPixelPos.Y = 0
                If GridPixelPos.Y > (ImgA.Height - 1 - InterrogationWindowSize.Height / 2) Then GridPixelPos.Y = (ImgA.Height - 1 - InterrogationWindowSize.Height / 2)

                Dim SearchWindow(,) As ComplexDouble = CropArray(ImgArrayB, GridPixelPos, InterrogationWindowSize)
                Dim Pattern(,) As ComplexDouble = CropArray(ImgArrayA, GridPixelPos, WindowSize)

                Dim NormXCorrResult(,) As ComplexDouble = NormalizedCrossCorrelation(SearchWindow, Pattern)
                NormXCorrResult = CropArray(NormXCorrResult, New Point(InterrogationWindowSize.Width / 2, InterrogationWindowSize.Height / 2), New Size(MaxVectorDisplacement.X * 2, MaxVectorDisplacement.Y * 2))

                'Finds the maximum value position
                Dim MaxAt As New Point
                Dim M As Double = MaximumValue(NormXCorrResult, MaxAt)

                MaxAt.X -= MaxVectorDisplacement.X - 1
                MaxAt.Y -= MaxVectorDisplacement.Y - 1

                'Adds this displacement to the map
                ResultingVectors(GridX, GridY) = New VectorDisplacement(GridPixelPos.X, GridPixelPos.Y, MaxAt.X, MaxAt.Y)

                'PictureDebug.ShowComplexDoubleArray(NormXCorrResult)
            Next
        Next

        Return ResultingVectors

    End Function

    Public Function DrawVectorFieldOntoBMP(ByVal Image As Bitmap, VectorField(,) As VectorDisplacement, Scaling As Single, C As Color, Grid As Boolean, Optional GridColor As Color = Nothing) As Bitmap
        'Draws lines onto the image with the vector sizes
        Dim B As New Bitmap(Image)
        Dim Canvas As Graphics = Graphics.FromImage(B)
        Dim P As New Pen(C)
        Dim PG As New Pen(GridColor)

        For GridX As Integer = 0 To UBound(VectorField, 1)
            Canvas.DrawLine(PG, VectorField(GridX, 0).PixelX, 0, VectorField(GridX, 0).PixelX, Image.Height)
        Next

        For Gridy As Integer = 0 To UBound(VectorField, 2)
            Canvas.DrawLine(PG, 0, VectorField(0, Gridy).PixelY, Image.Height, VectorField(0, Gridy).PixelY)
        Next

        For GridX As Integer = 0 To UBound(VectorField, 1)
            For GridY As Integer = 0 To UBound(VectorField, 2)
                'Draws the vector
                Canvas.DrawLine(P, VectorField(GridX, GridY).PixelX, VectorField(GridX, GridY).PixelY,
                                VectorField(GridX, GridY).PixelX + Scaling * VectorField(GridX, GridY).DisplacementX, VectorField(GridX, GridY).PixelY + Scaling * VectorField(GridX, GridY).DisplacementY)

            Next
        Next

        'Dim B As New Bitmap(Image.Width, Image.Height, Canvas)

        Return B
    End Function

    Public Class VectorDisplacement
        'Stores X,Y pixel coordinates of the displacement and their vectorial values.
        Public PixelX As Integer
        Public PixelY As Integer
        Public DisplacementX As Single
        Public DisplacementY As Single

        Public Sub New(Px As Integer, Py As Integer, Dx As Single, Dy As Single)
            Me.PixelX = Px
            Me.PixelY = Py
            Me.DisplacementX = Dx
            Me.DisplacementY = Dy
        End Sub
    End Class
#End Region


#Region "Cross-Correlation"

    Public Function NormalizedCrossCorrelation(SearchWindow(,) As ComplexDouble, Template(,) As ComplexDouble) As ComplexDouble(,)
        'Tries to correlate the Template with the SearchWindow.
        'Returns an array of the size of the SearchWindow (which must be bigger than the Template)
        'This array contains numbers between -1 (completely uncorrelated) and +1 (maximally correlated)

        'Ensures the SearchWindow is larger than the template
        If UBound(SearchWindow, 1) < UBound(Template, 1) Or UBound(SearchWindow, 2) < UBound(Template, 2) Then Return Nothing

        'Calculates the elements for the norm X corr:
        'C=Num/(DenA*DenB) // Review presentation I made for Mechops
        Dim Num(,) As ComplexDouble
        Dim DenA As Double
        Dim DenB(,) As ComplexDouble

        Dim SW_NoAverage(,) As ComplexDouble = AddConstant(SearchWindow, (-1) * Avg(SearchWindow))
        Dim Tmp_NoAverage(,) As ComplexDouble = AddConstant(Template, (-1) * Avg(Template))

        Num = Convolve2D(SW_NoAverage, Tmp_NoAverage)
        DenA = StdDev(Template)


        'DenB is harder:
        'DenB=(N-1)*sqrt(Conv_s_w-(Conv_s_w²/N))
        Dim W(,) As ComplexDouble 'W is the moving window. It is a full array of ones, of the size of the template.
        ReDim W(UBound(Template, 1), UBound(Template, 2))
        Dim Conv_s2_w(,) As ComplexDouble
        Dim Conv_s_w_2(,) As ComplexDouble
        Dim N As Integer = (UBound(Template, 1) + 1) * (UBound(Template, 2) + 1) 'Number of items on the template

        'Makes W
        For X As Integer = 0 To UBound(Template, 1)
            For Y As Integer = 0 To UBound(Template, 2)
                W(X, Y) = New ComplexDouble(1, 0)
            Next
        Next
        'Convolves W into SearchWindow
        Conv_s2_w = Convolve2D(MultiplyMatrices(SearchWindow, SearchWindow), W) 'Square before convolving
        Conv_s_w_2 = Convolve2D(SearchWindow, W)
        Conv_s_w_2 = MultiplyMatrices(Conv_s_w_2, Conv_s_w_2) 'Squared version after convolving

        'Makes DenB
        DenB = MultiplyByConstant(Conv_s_w_2, 1 / N)
        DenB = SubtractMatrices(Conv_s2_w, DenB)
        DenB = MultiplyByConstant(DenB, N - 1)
        DenB = SquareRootMatrix(DenB)

        'Now makes the whole thing
        Dim C(,) As ComplexDouble
        C = MultiplyByConstant(DenB, DenA)
        C = DivideMatrices(Num, C)

        Return C

    End Function

    Public Function BorderRemoval(M(,) As ComplexDouble, BorderWidth As Integer) As ComplexDouble(,)
        'Removes a border from the array M (border=0), to make sure no peaks are outside the relevant search area (out of bounds)

        Dim ResultArray(,) As ComplexDouble
        ReDim ResultArray(UBound(M, 1), UBound(M, 2))

        For X = 0 To UBound(M, 1)
            For Y = 0 To UBound(M, 2)
                If (X < (BorderWidth / 2 - 1)) Or (Y < (BorderWidth / 2 - 1)) Or (X > (UBound(M, 1) - BorderWidth / 2)) Or (Y > (UBound(M, 2) - BorderWidth / 2)) Then
                    ResultArray(X, Y) = New ComplexDouble(0, 0)
                Else
                    ResultArray(X, Y) = M(X, Y)
                End If
            Next
        Next

        Return ResultArray
    End Function

    Public Function CropArray(A(,) As ComplexDouble, C As Point, S As Size) As ComplexDouble(,)
        'Returns a cropped (smaller) version of A(,)
        'Centered in C and of window size of S
        'Center in C means the center point is BEFORE C (between C-1 and C)
        'Size MUST be even or rounding will occur
        Dim XBegin As Integer = C.X - (S.Width / 2)
        Dim XEnd As Integer = C.X + (S.Width / 2)

        Dim YBegin As Integer = C.Y - (S.Height / 2)
        Dim YEnd As Integer = C.Y + (S.Height / 2)

        Try
            Dim ResultArray(,) As ComplexDouble
            ReDim ResultArray(XEnd - XBegin, YEnd - YBegin)

            For X As Integer = XBegin To XEnd
                For Y As Integer = YBegin To YEnd
                    ResultArray(X - XBegin, Y - YBegin) = A(X, Y)
                Next
            Next

            Return ResultArray
        Catch ex As Exception
            Throw New Exception("Out of bounds of the cropped array")
        End Try

    End Function
#End Region


#Region "Picture to Complex Array correlation"
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
        Dim MinimumMagnitude As Double = Double.MaxValue
        Dim MaximumMagnitude As Double = Double.MinValue

        For X As Integer = 0 To SizeX - 1
            For Y As Integer = 0 To SizeY - 1
                M = CArray(X, Y).Real
                If MaximumMagnitude < M Then MaximumMagnitude = M
                If MinimumMagnitude > M Then MinimumMagnitude = M
            Next
        Next

        For X As Integer = 0 To SizeX - 1
            For Y As Integer = 0 To SizeY - 1
                If CArray(X, Y).Real = MaximumMagnitude Then
                    ResultBmp.SetPixel(X, Y, Color.Red)
                Else
                    NormalizedIntensity = (CArray(X, Y).Real - MinimumMagnitude) / (MaximumMagnitude - MinimumMagnitude)
                    GrayscaleLevel = Int(NormalizedIntensity * 255)

                    ResultBmp.SetPixel(X, Y, Color.FromArgb(GrayscaleLevel, GrayscaleLevel, GrayscaleLevel))
                End If
            Next
        Next

        Return ResultBmp
    End Function

#End Region


#Region "Filtering"

    Public Function GaussianBlur(Sigma As Double, OriginalImage As ComplexDouble(,)) As ComplexDouble(,)
        'Generates an array of ComplexDouble for the convolution of a gaussian filter
        'OriginalImage is an array (the image) that will be the recipient of the convolution operation.

        'Dim ImageFFT(,) As ComplexDouble = FFT_2D(OriginalImage, 1)

        'Makes the gaussian mask
        Dim GaussianMask(,) As ComplexDouble
        ReDim GaussianMask(Int(Sigma * 6), Int(Sigma * 6))
        Dim TempDouble As ComplexDouble
        Dim X1, Y1 As Double

        For X As Integer = 0 To UBound(GaussianMask, 1)
            For Y As Integer = 0 To UBound(GaussianMask, 2)
                X1 = X - (UBound(GaussianMask, 1) + 1) / 2
                Y1 = Y - (UBound(GaussianMask, 2) + 1) / 2

                TempDouble = New ComplexDouble(Math.Exp((-(X1 * X1) - (Y1 * Y1)) / (2 * Sigma * Sigma)) / (2 * Math.PI * Sigma * Sigma),
                                                       0)
                GaussianMask(X, Y) = TempDouble
            Next
        Next

        Return Convolve2D(OriginalImage, GaussianMask)
    End Function

    Public Function Convolve2D(M1(,) As ComplexDouble, M2(,) As ComplexDouble) As ComplexDouble(,)
        'Defines the best convolution algorithm to use
        'Time that takes for the convolutions:

        Dim W1 As Integer = UBound(M1, 1) + 1
        Dim H1 As Integer = UBound(M1, 2) + 1
        Dim W2 As Integer = UBound(M2, 1) + 1
        Dim H2 As Integer = UBound(M2, 2) + 1

        'Estimates Convolution time
        Dim FFT_SizeW As Integer = 2 ^ closestLargerPowerOf2(W1 + W2)
        Dim FFT_SizeH As Integer = 2 ^ closestLargerPowerOf2(H1 + H2)

        Dim N_Op_FFT As Double = 3 * 2 * FFT_SizeH * FFT_SizeW * Math.Ceiling((Math.Log(FFT_SizeW) + Math.Log(FFT_SizeH)) / Math.Log(2)) 'Operations to make the 2D FFT. *3=3 FFTs (2 direct, 1 inverse) and *2=1add+1mult per loop in the FFT
        Dim N_Op_Direct As Double = 2.0 * W1 * W2 * H1 * H2

        If N_Op_FFT <= N_Op_Direct Then
            Return Convolve2D_FFT(M1, M2)
        Else
            'Not implemented
            Return Nothing
        End If

    End Function


    Public Function Convolve2D_FFT(M1(,) As ComplexDouble, M2(,) As ComplexDouble) As ComplexDouble(,)
        'Performs the convolution operation of two matrices based on the convolution theorem
        'A (X) B = ifft(fft(A)*fft(B))

        'Stores the array sizes to find the largest
        Dim W1 As Integer = UBound(M1, 1) + 1
        Dim H1 As Integer = UBound(M1, 2) + 1
        Dim W2 As Integer = UBound(M2, 1) + 1
        Dim H2 As Integer = UBound(M2, 2) + 1

        'The working arrays sizes must be adjusted to S1+S2
        Dim M1Used(,) As ComplexDouble
        Dim M2Used(,) As ComplexDouble
        ReDim M1Used(W1 + W2 - 1, H1 + H2 - 1)
        ReDim M2Used(W1 + W2 - 1, H1 + H2 - 1)

        Dim X_Original, Y_Original As Integer
        Dim M2_180(,) As ComplexDouble = Rot180(M2)


        'Zero-pads the arrays
        For X = 0 To W1 + W2 - 1
            For Y = 0 To H1 + H2 - 1
                'Works on image 1
                X_Original = X - Int(W2 / 2)
                Y_Original = Y - Int(H2 / 2)

                If (X_Original >= 0 And Y_Original >= 0 And X_Original < W1 And Y_Original < H1) Then
                    'It's inside bounds
                    M1Used(X, Y) = M1(X_Original, Y_Original)
                Else
                    'Zero pad
                    M1Used(X, Y) = New ComplexDouble(0, 0)
                End If


                'Works on image 2 (Won't be centered)
                'X_Original = X - Int(W1 / 2)
                'Y_Original = Y - Int(H1 / 2)
                X_Original = X
                Y_Original = Y

                If (X_Original >= 0 And Y_Original >= 0 And X_Original < W2 And Y_Original < H2) Then
                    'It's inside bounds
                    M2Used(X, Y) = M2_180(X_Original, Y_Original)
                Else
                    'Zero pad
                    M2Used(X, Y) = New ComplexDouble(0, 0)
                End If
            Next
        Next

        'FFTs both matrices and convolves them
        Dim FFTResult(,) As ComplexDouble = FFT_2D(MultiplyMatrices(FFT_2D(M1Used, 1), FFT_2D(M2Used, 1)), -1)
        Erase M1Used
        Erase M2Used

        'Crops the result back to size of W1, H1
        Dim ResultingImage(,) As ComplexDouble
        ReDim ResultingImage(W1 - 1, H1 - 1)
        For X = 0 To W1 - 1
            For Y = 0 To H1 - 1
                ResultingImage(X, Y) = FFTResult(X + W2, Y + H2) 'This is not W2/2 or H2/2 so it crops centered to the original image (corresponds to the image M2 having half of it in negative coordinates)
            Next
        Next

        Return ResultingImage

    End Function

    Public Function StdDev(M(,) As ComplexDouble) As Double
        'Returns NOT the Standard Deviation, BUT the StdDev*(N-1)=sqrt(summation(terms²)).
        'Does only for the real part to save time. Returns in the complex domain just because it's needed.
        Dim Sum As Double = 0
        For X As Integer = 0 To UBound(M, 1)
            For Y As Integer = 0 To UBound(M, 2)
                Sum += M(X, Y).Real * M(X, Y).Real
            Next
        Next

        Return Math.Sqrt(Sum)
    End Function

    Public Function Avg(M(,) As ComplexDouble) As Double
        'Returns the average of the terms real part in this array
        Dim Sum As Double = 0
        For X As Integer = 0 To UBound(M, 1)
            For Y As Integer = 0 To UBound(M, 2)
                Sum += M(X, Y).Real
            Next
        Next

        Return Sum / ((UBound(M, 1) + 1) * (UBound(M, 2) + 1))
    End Function

    Public Function MaximumValue(M(,) As ComplexDouble, ByRef Optional MaxAt As Point = Nothing) As Double
        'Returns the maximum value in the array
        Dim Max As Double = Double.MinValue
        MaxAt = New Point
        For X As Integer = 0 To UBound(M, 1)
            For Y As Integer = 0 To UBound(M, 2)
                If Max < M(X, Y).Real Then
                    Max = M(X, Y).Real
                    MaxAt.X = X
                    MaxAt.Y = Y
                End If
            Next
        Next


        MaximumValue = Max
    End Function

    Public Function Rot180(M(,) As ComplexDouble) As ComplexDouble(,)
        'Returns the array M rotated 180 degrees
        Dim ReturnArray(,) As ComplexDouble
        ReDim ReturnArray(UBound(M, 1), UBound(M, 2))

        For X As Integer = 0 To UBound(M, 1)
            For Y As Integer = 0 To UBound(M, 1)
                ReturnArray(UBound(M, 1) - X, UBound(M, 2) - Y) = M(X, Y)
            Next
        Next
        Return ReturnArray
    End Function

#End Region

End Module
