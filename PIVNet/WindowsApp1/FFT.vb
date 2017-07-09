Module FFT
    Private TwiddleFactors() As ComplexDouble 'Stores the precomputation of the twiddle factors
    Private NMax As Integer

#Region "FFT Barebones"
    Public Function FFT_2D(InputArray(,) As ComplexDouble, Dir As Integer) As ComplexDouble(,)
        'Implements the 2D fast fourier transform.
        'Dir=1 -> Forward FFT /// Dir=-1 -> Inverse FFT

        'Makes sure the array in the input is correctly zero-padded

        Dim InputSizeX As Integer = UBound(InputArray, 1) + 1
        Dim InputSizeY As Integer = UBound(InputArray, 2) + 1
        Dim Pow2X As Integer = closestLargerPowerOf2(InputSizeX)
        Dim Pow2Y As Integer = closestLargerPowerOf2(InputSizeY)
        Dim RealSizeX As Integer = 2 ^ Pow2X
        Dim RealSizeY As Integer = 2 ^ Pow2Y

        Dim WorkingArray(,) As ComplexDouble
        ReDim WorkingArray(RealSizeX - 1, RealSizeY - 1) 'No need for zero-padding if the array realsize and inputsize coincides

        'This will perform the zero-padding if it's needed (probably). If not, it just copies the array.
        For X As Integer = 0 To RealSizeX - 1
            For Y As Integer = 0 To RealSizeX - 1
                If (X < InputSizeX) And (Y < InputSizeY) Then
                    WorkingArray(X, Y) = InputArray(X, Y)
                Else
                    'zero-pad
                    WorkingArray(X, Y) = New ComplexDouble(0, 0)
                End If
            Next
        Next

        'Row-wise 1D FFT
        For Y As Integer = 0 To RealSizeY - 1
            Dim CurrentRow() As ComplexDouble
            ReDim CurrentRow(RealSizeX - 1)

            For X As Integer = 0 To RealSizeX - 1
                CurrentRow(X) = WorkingArray(X, Y)
            Next
            Dim CurrentFFTRow() As ComplexDouble = FFT_1D(CurrentRow, Dir)

            For X As Integer = 0 To RealSizeX - 1
                WorkingArray(X, Y) = CurrentFFTRow(X)
            Next
        Next

        'Column-wise 1D FFT
        For X As Integer = 0 To RealSizeX - 1
            Dim CurrentColumn() As ComplexDouble
            ReDim CurrentColumn(RealSizeY - 1)

            For Y As Integer = 0 To RealSizeY - 1
                CurrentColumn(Y) = WorkingArray(X, Y)
            Next
            Dim CurrentFFTColumn() As ComplexDouble = FFT_1D(CurrentColumn, Dir)

            For Y As Integer = 0 To RealSizeY - 1
                WorkingArray(X, Y) = CurrentFFTColumn(Y)
            Next
        Next

        Return WorkingArray


    End Function


    Public Function FFT_1D(InputData() As ComplexDouble, Dir As Integer) As ComplexDouble()
        'Function wrapper for the FFT method. Also makes sure the size of the FFT is a power of 2.
        'Dir=1 -> Forward FFT /// Dir=-1 -> Inverse FFT
        Dim InputSize As Integer = UBound(InputData) + 1
        Dim RealSize As Integer
        Dim Result_FFT() As ComplexDouble

        Dim Pow2 As Integer = closestLargerPowerOf2(InputSize)
        If InputSize = 2 ^ Pow2 Then
            'No need to change
            ComputeTwiddleFactors(InputSize - 1, Dir)
            Result_FFT = PartialFFT_1D(InputData, InputSize, 1)
            RealSize = InputSize
        Else
            'Zero-pad until it reaches the size
            RealSize = 2 ^ Pow2
            Dim NewDataSet() As ComplexDouble = InputData
            ReDim Preserve NewDataSet(RealSize - 1)
            For I As Integer = InputSize To (RealSize - 1)
                NewDataSet(I) = New ComplexDouble(0, 0)
            Next

            ComputeTwiddleFactors(RealSize - 1, Dir)
            Result_FFT = PartialFFT_1D(NewDataSet, RealSize, 1) 'The resulting FFT will not remove the zero-padding. That's up to the user, as there is no way to make sure of the application.
        End If

        For I As Integer = 0 To RealSize - 1
            Result_FFT(I) = Result_FFT(I) / Math.Sqrt(RealSize)
        Next
        Return Result_FFT
    End Function

    Public Function PartialFFT_1D(InputData() As ComplexDouble, Npts As Integer, s As Integer) As ComplexDouble()
        'Implements the Cooley-Tukey algorithm for an FFT. 
        'Implementation from https://en.wikipedia.org/wiki/Cooley%E2%80%93Tukey_FFT_algorithm
        'Actually understood it after watching https://allsignalprocessing.com/fast-fourier-transform-fft-algorithm/
        'Read the original paper (it's actually readable) http://www.ams.org/journals/mcom/1965-19-090/S0025-5718-1965-0178586-1/S0025-5718-1965-0178586-1.pdf

        If Npts = 1 Then
            'Trivial case
            Dim TrivialResult(0) As ComplexDouble
            TrivialResult(0) = InputData(0)
            Return TrivialResult
        Else
            'Divide the strides

            'Even Stride
            Dim EvenStride() As ComplexDouble
            ReDim EvenStride((Npts / 2) - 1)
            For I As Integer = 0 To (Npts / 2) - 1
                EvenStride(I) = InputData(2 * I)
            Next
            Dim EvenFFT() As ComplexDouble
            EvenFFT = PartialFFT_1D(EvenStride, Npts / 2, 2 * s)

            'Odd Stride
            Dim OddStride() As ComplexDouble
            ReDim OddStride((Npts / 2) - 1)
            For I As Integer = 0 To (Npts / 2) - 1
                OddStride(I) = InputData(2 * I + 1)
            Next
            Dim OddFFT() As ComplexDouble
            OddFFT = PartialFFT_1D(OddStride, Npts / 2, 2 * s)


            Dim CombinedFFT() As ComplexDouble
            ReDim CombinedFFT(Npts - 1)

            Dim Twiddle As ComplexDouble
            For k As Integer = 0 To (Npts / 2) - 1
                'Twiddle = New ComplexDouble(0, 2 * Math.PI * k / Npts)
                'Twiddle = Twiddle.Exp

                Twiddle = TwiddleFactors(k * NMax / Npts) ' Uses the precomputed twiddle factors
                CombinedFFT(k) = EvenFFT(k) + Twiddle * OddFFT(k)
                CombinedFFT(k + Npts / 2) = EvenFFT(k) - Twiddle * OddFFT(k)
            Next

            Return CombinedFFT
        End If

    End Function


    Public Function closestLargerPowerOf2(Number As Integer) As Integer
        'Returns the closest power of 2 that is larger than this number
        Dim ShiftedNumber As Integer = Number
        Dim Power As Integer = 0
        Do While ShiftedNumber > 0
            ShiftedNumber = ShiftedNumber >> 1
            Power += 1
        Loop
        If 2 ^ (Power - 1) = Number Then Power = Power - 1
        Return Power
    End Function

    Public Sub ComputeTwiddleFactors(NPts As Integer, Dir As Integer)
        'Computes the following twiddle factors
        'Exp(−2πi*s*k/N) where I=2*k
        ReDim TwiddleFactors(NPts - 1)
        For I As Integer = 0 To NPts - 1
            Dim C As New ComplexDouble(0, -2 * Dir * Math.PI * I / NPts)
            TwiddleFactors(I) = C.Exp
        Next
        NMax = NPts
    End Sub

    Public Sub ToMagPhase(ByVal ComplexArray() As ComplexDouble, ByRef Mag() As Double, ByRef Phase() As Double)
        'Converts the complex array to a mag/phase double
        Dim M() As Double
        Dim P() As Double
        ReDim M(UBound(ComplexArray))
        ReDim P(UBound(ComplexArray))
        Dim Ma, Ph As Double

        For I = 0 To UBound(ComplexArray)
            ComplexArray(I).ToMagnitudePhase(Ma, Ph)
            M(I) = Ma
            P(I) = Ph
        Next
        Mag = M
        Phase = P
    End Sub



    Public Function DFT(InputData() As ComplexDouble) As ComplexDouble()
        'Implements a regular (slow) DFT.

        Dim Threshold As Double = 0.0000001 'Cutoff threshold
        Dim S As ComplexDouble
        Dim Npts As Integer = UBound(InputData) + 1
        Dim Term As ComplexDouble
        Dim DFTResult() As ComplexDouble
        ReDim DFTResult(Npts - 1)

        For k As Integer = 0 To Npts - 1
            S = New ComplexDouble(0, 0)

            Dim S_Accum() As Double
            ReDim S_Accum(Npts - 1)

            For n As Integer = 0 To Npts - 1
                Term = New ComplexDouble(0,
                                         -2 * Math.PI * n * k / Npts)
                S += InputData(n) * Term.Exp
                S_Accum(n) = S.Magnitude
            Next

            If S.Magnitude < Threshold Then
                DFTResult(k) = New ComplexDouble(0, 0)
            Else
                DFTResult(k) = S
            End If
            DFTResult(k) = DFTResult(k) / Math.Sqrt(Npts)
        Next
        Return DFTResult
    End Function
#End Region

#Region "Filtering"
    Public Function GaussianBlur(Sigma As Double, OriginalImage As ComplexDouble(,)) As ComplexDouble(,)
        'Generates an array of ComplexDouble for the convolution of a gaussian filter
        'OriginalImage is an array (the image) that will be the recipient of the convolution operation.

        Dim ImageFFT(,) As ComplexDouble = FFT_2D(OriginalImage, 1)

        'Makes the gaussian mask
        Dim GaussianMask(,) As ComplexDouble
        ReDim GaussianMask(UBound(ImageFFT, 1), UBound(ImageFFT, 2))
        Dim Cutoff As Double = 0.00001

        For X As Integer = 0 To UBound(ImageFFT, 1)
            For Y As Integer = 0 To UBound(ImageFFT, 2)
                GaussianMask(X, Y) = New ComplexDouble(Math.Exp((-(X * X) - (Y * Y)) / (2 * Sigma * Sigma)) / (2 * Math.PI * Sigma * Sigma),
                                                       0)
            Next
        Next

        'Multiplies the FFTs (Convolves) and then takes the inverse FFT
        Dim ResultImage(,) As ComplexDouble
        ResultImage = FFT_2D(GaussianMask, 1)
        ResultImage = MultiplyMatrices(ResultImage, ImageFFT)
        ResultImage = FFT_2D(ResultImage, -1)

        Return ResultImage
    End Function

    Public Function MultiplyMatrices(M1(,) As ComplexDouble, M2(,) As ComplexDouble) As ComplexDouble(,)
        'Multiplication term by term.
        If Not (UBound(M1, 1) = UBound(M2, 1) AndAlso UBound(M1, 2) = UBound(M2, 2)) Then Return Nothing 'Makes sure the sizes are equal

        Dim Result(,) As ComplexDouble
        ReDim Result(UBound(M1, 1), UBound(M1, 2))
        For X As Integer = 0 To UBound(M1, 1)
            For Y As Integer = 0 To UBound(M1, 2)
                Result(X, Y) = M1(X, Y) * M2(X, Y)
            Next
        Next

        Return Result
    End Function

#End Region
End Module
