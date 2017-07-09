<DebuggerDisplay("{Real}+{Imaginary}i")>
Public Class ComplexDouble
    'Implements a complex number, double precision
    Public Real As Double
    Public Imaginary As Double

    Public Sub New(R As Double, I As Double)
        Me.Real = R
        Me.Imaginary = I
    End Sub

    Public Shared Operator +(n1 As ComplexDouble, n2 As ComplexDouble) As ComplexDouble
        Return New ComplexDouble(n1.Real + n2.Real, n1.Imaginary + n2.Imaginary)
    End Operator

    Public Shared Operator -(n1 As ComplexDouble, n2 As ComplexDouble) As ComplexDouble
        Return New ComplexDouble(n1.Real - n2.Real, n1.Imaginary - n2.Imaginary)
    End Operator

    Public Shared Operator *(n1 As Double, n2 As ComplexDouble) As ComplexDouble
        Return New ComplexDouble(n1 * n2.Real, n1 * n2.Imaginary)
    End Operator

    Public Shared Operator *(n2 As ComplexDouble, n1 As Double) As ComplexDouble
        Return New ComplexDouble(n1 * n2.Real, n1 * n2.Imaginary)
    End Operator

    Public Shared Operator *(n1 As ComplexDouble, n2 As ComplexDouble) As ComplexDouble
        Return New ComplexDouble(n1.Real * n2.Real - n1.Imaginary * n2.Imaginary,
                                    n1.Real * n2.Imaginary + n1.Imaginary * n2.Real)
    End Operator

    Public Shared Operator /(n1 As ComplexDouble, n2 As Double) As ComplexDouble
        Return New ComplexDouble(n1.Real / n2, n1.Imaginary / n2)
    End Operator

    Public Function Exp() As ComplexDouble
        'Returns e^(me)
        Return New ComplexDouble(Math.Exp(Me.Real) * Math.Cos(Me.Imaginary),
                                 Math.Exp(Me.Real) * Math.Sin(Me.Imaginary))
    End Function

    Public Function Magnitude() As Double
        Return Math.Sqrt(Real * Real + Imaginary * Imaginary)
    End Function

    Public Sub ToMagnitudePhase(ByRef Mag As Double, ByRef Phase_Rad As Double)
        'Converts the double complex pair into a polar pair
        Mag = Math.Sqrt(Real * Real + Imaginary * Imaginary)
        Phase_Rad = Math.Atan2(Imaginary, Real)
    End Sub
End Class
