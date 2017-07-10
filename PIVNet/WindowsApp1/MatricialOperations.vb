Module MatricialOperations
    'Implements various matrix operations

    Public Function AddConstant(M1(,) As ComplexDouble, C As Double) As ComplexDouble(,)
        'Multiplication term by term by a constant.

        Dim Result(,) As ComplexDouble
        ReDim Result(UBound(M1, 1), UBound(M1, 2))
        For X As Integer = 0 To UBound(M1, 1)
            For Y As Integer = 0 To UBound(M1, 2)
                Result(X, Y) = M1(X, Y) + New ComplexDouble(C, 0)
            Next
        Next

        Return Result
    End Function

    Public Function MultiplyByConstant(M1(,) As ComplexDouble, C As Double) As ComplexDouble(,)
        'Multiplication term by term by a constant.

        Dim Result(,) As ComplexDouble
        ReDim Result(UBound(M1, 1), UBound(M1, 2))
        For X As Integer = 0 To UBound(M1, 1)
            For Y As Integer = 0 To UBound(M1, 2)
                Result(X, Y) = M1(X, Y) * C
            Next
        Next

        Return Result
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

    Public Function DivideMatrices(M1(,) As ComplexDouble, M2(,) As ComplexDouble) As ComplexDouble(,)
        'Division term by term. Only real part
        If Not (UBound(M1, 1) = UBound(M2, 1) AndAlso UBound(M1, 2) = UBound(M2, 2)) Then Return Nothing 'Makes sure the sizes are equal

        Dim Result(,) As ComplexDouble
        ReDim Result(UBound(M1, 1), UBound(M1, 2))
        For X As Integer = 0 To UBound(M1, 1)
            For Y As Integer = 0 To UBound(M1, 2)
                Result(X, Y) = New ComplexDouble(M1(X, Y).Magnitude / M2(X, Y).Magnitude, 0)
            Next
        Next

        Return Result
    End Function

    Public Function SubtractMatrices(M1(,) As ComplexDouble, M2(,) As ComplexDouble) As ComplexDouble(,)
        'Subtraction term by term.
        If Not (UBound(M1, 1) = UBound(M2, 1) AndAlso UBound(M1, 2) = UBound(M2, 2)) Then Return Nothing 'Makes sure the sizes are equal

        Dim Result(,) As ComplexDouble
        ReDim Result(UBound(M1, 1), UBound(M1, 2))
        For X As Integer = 0 To UBound(M1, 1)
            For Y As Integer = 0 To UBound(M1, 2)
                Result(X, Y) = M1(X, Y) - M2(X, Y)
            Next
        Next

        Return Result
    End Function

    Public Function SquareRootMatrix(M1(,) As ComplexDouble) As ComplexDouble(,)
        'Takes the square root of the real part of the terms in the matrix.

        Dim Result(,) As ComplexDouble
        ReDim Result(UBound(M1, 1), UBound(M1, 2))
        For X As Integer = 0 To UBound(M1, 1)
            For Y As Integer = 0 To UBound(M1, 2)
                Result(X, Y) = New ComplexDouble(Math.Sqrt(M1(X, Y).Magnitude), 0)
            Next
        Next

        Return Result
    End Function
End Module
