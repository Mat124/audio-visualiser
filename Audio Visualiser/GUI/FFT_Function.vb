Module FFT_Function
    'import to use functions
    Public Function fft(ByVal SampleArray() As Double, ByVal N As Integer) As Double(,)
        'SampleArray() - short array containing the samples recorded
        'N - Integer containing the length of the final array outputted by the cooley-tukey algorithm
        Dim FFTofSampleArray(N / 2 - 1, 1) As Double ' length needs to be 
        For frequency = 0 To (SampleArray.Length - 1)
            For J = 0 To SampleArray.Length - 1
                FFTofSampleArray(frequency, 0) += Math.Cos(-2 * frequency * J / SampleArray.Length * Math.PI) * SampleArray(J) 'real part of array
                FFTofSampleArray(frequency, 1) += Math.Sin(-2 * frequency * J / SampleArray.Length * Math.PI) * SampleArray(J) 'imaginary part of array
            Next
        Next
        Return FFTofSampleArray
    End Function
    Public Function arraysplitter(ByVal OriginalArray() As Double, ByVal Start As Integer) As Double() ' array input length must be powers of 2
        'splits an array in two, selecting every other item, starting from the given point
        Dim ArraySliceLength As Integer = OriginalArray.Length / 2
        Dim ArraySlice(ArraySliceLength - 1) As Double
        For i = 0 To ArraySliceLength - 1
            ArraySlice(i) = OriginalArray(2 * i + Start)
        Next
        Return ArraySlice
    End Function
    Public Function ditfft2(ByVal SampleArray() As Double, ByVal N As Integer) As Double(,)
        'recursive cooley-tukey radix-2
        Dim Output(SampleArray.Length, 1) As Double
        If SampleArray.Length <= 32 Then 'base case when the array is small enough
            Return fft(SampleArray, N) 'returns the basic fft of the array
        Else 'recursive case
            Dim SampleArray_odd(,) As Double = ditfft2(arraysplitter(SampleArray, 1), N) 'starts another iteration using the odd indexed half of the given array
            Dim SampleArray_even(,) As Double = ditfft2(arraysplitter(SampleArray, 0), N) 'starts another interation using the even indexed half of the given array
            For k = 0 To (SampleArray.Length / 2) - 1 ' combining the two arrays
                Output(k, 0) = SampleArray_even(k, 0) + Math.Cos(-2 * Math.PI * k / SampleArray.Length) * SampleArray_odd(k, 0) + Math.Sin(-2 * Math.PI * k / SampleArray.Length) * SampleArray_odd(k, 1) 'real part of array
                Output(k, 1) = SampleArray_even(k, 1) + Math.Cos(-2 * Math.PI * k / SampleArray.Length) * SampleArray_odd(k, 1) + Math.Sin(-2 * Math.PI * k / SampleArray.Length) * SampleArray_odd(k, 0) 'imaginary part of array
                Output(k + SampleArray.Length / 2, 0) = SampleArray_even(k, 0) - (Math.Cos(-2 * Math.PI * k / SampleArray.Length) * SampleArray_odd(k, 0) + Math.Sin(-2 * Math.PI * k / SampleArray.Length) * SampleArray_odd(k, 1)) 'real part of array
                Output(k + SampleArray.Length / 2, 1) = SampleArray_even(k, 1) - (Math.Cos(-2 * Math.PI * k / SampleArray.Length) * SampleArray_odd(k, 1) + Math.Sin(-2 * Math.PI * k / SampleArray.Length) * SampleArray_odd(k, 0)) 'imaginary part of array
            Next
            Return Output 'returns the final output
        End If
    End Function
End Module
