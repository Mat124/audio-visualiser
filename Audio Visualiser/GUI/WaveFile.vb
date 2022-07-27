Imports System.IO
Class WaveFile
    ''' <summary>
    ''' contains variables for everything from a wav file
    ''' </summary>
    Public sample(1) As Integer
    Public filename As String
    Public riff As String
    Public filesize As Int64
    Public filetypeheader As String
    Public formatchunkheader As String
    Public formatdatalength As Integer
    Public PCM As Integer
    Public channels As Integer
    Public samplerate As Int64
    Public bytespersecond As Int64
    Public idk As Integer
    Public bitspersample As Integer
    Public datachunkheader As String
    Public datalength As Int64
    Public timelength As Integer
    Public samples As Integer
    Public left As Int16
    Public right As Int16
    Public Sub open(filename)
        Using read As BinaryReader = New BinaryReader(File.OpenRead(filename))
            riff = read.ReadChars(4)
            filesize = read.ReadInt32
            filetypeheader = read.ReadChars(4)
            formatchunkheader = read.ReadChars(4)
            formatdatalength = read.ReadInt32
            PCM = read.ReadInt16
            channels = read.ReadInt16
            samplerate = read.ReadInt32
            bytespersecond = read.ReadInt32
            idk = read.ReadInt16
            bitspersample = read.ReadInt16
            datachunkheader = read.ReadChars(4)
            datalength = read.ReadInt32
            samples = datalength / (bitspersample / 8)
            ReDim Preserve sample(samples)
            timelength = samples / samplerate
            For i = 0 To samples - 2
                left = read.ReadByte
                right = read.ReadByte
                sample(i) = right
            Next
        End Using
    End Sub
End Class
