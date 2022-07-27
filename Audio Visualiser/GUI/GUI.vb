Imports System.IO
Imports NAudio
Imports System.Runtime.Serialization.Formatters.Binary
Public Class GUI
    Private Const SerialBaudRate As Integer = 9600 'constant BaudRate for serial
    Private Samples As Integer 'num of samples for the FFT
    Private GraphSettingsGUI As New GraphSettingsForm
    Private LEDSettingsGUI As New LEDSettingsForm
    Private graph As New GraphForm
    Private graphcreated As Boolean = False
    Private SoundWaveCapture As NAudio.Wave.WasapiLoopbackCapture = New Wave.WasapiLoopbackCapture()
    Private SoundData As New List(Of Int16)
    Private ComPort As String = "COM3" 'its always com3
    Private FinalAmp(0) As Double
    Private ArduinoConnected As Boolean = False
    Private GraphSettings As SettingsList
    Private LEDSettings As LEDSettingsList
    Private LEDnum As Integer
    Private LEDsens As Integer
    Private Event UpdateLEDs()

    Private Shadows Sub Closing() Handles Me.FormClosing
        'stops recording as the application is closed while still recording, otherwise continues
        UpdateGraphTimer.Stop()
        SoundWaveCapture.StopRecording()
        Using sw As New StreamWriter(File.Open("previous.dat", FileMode.OpenOrCreate))
            sw.WriteLine(GraphSettings.Colour.R)
            sw.WriteLine(GraphSettings.Colour.G)
            sw.WriteLine(GraphSettings.Colour.B)
            sw.WriteLine(GraphSettings.InvertSpectrum)
            sw.WriteLine(GraphSettings.LineWidth)
            sw.WriteLine(GraphSettings.PercentageMaxHeight)
            sw.WriteLine(GraphSettings.Type)
            sw.WriteLine(GraphSettings.XAxisScale)
            sw.WriteLine(GraphSettings.FFTsize)
            sw.WriteLine(LEDSettings.LEDColour.R)
            sw.WriteLine(LEDSettings.LEDColour.G)
            sw.WriteLine(LEDSettings.LEDColour.B)
            sw.WriteLine(LEDSettings.VolumeMeasurementType)
            sw.WriteLine(LEDSettings.LEDbrightness)
        End Using
    End Sub
    Private Sub GraphClosed() 'when the graph is closed, stop recording
        If Not graph.Visible Then
            StopDisplay(LEDSettings.VolumeMeasurementType)
        End If
    End Sub
    Private Sub GUI_Load(sender As Object, e As EventArgs) Handles Me.Load 'loading sub
        Do
            Try
                LEDnum = InputBox("Restart the arduino, wait 10 seconds and then enter number of " &
                                  " LEDs connected. Max 250, min 1. Restart the arduino each time  " &
                                  "the program is restarted, or errors may occur.")
            Catch
                MsgBox("Invalid input!")
            End Try
        Loop Until LEDnum > 0 And LEDnum < 251 'loop for valid input for LEDnum
        Try
            Using com3 As IO.Ports.SerialPort = My.Computer.Ports.OpenSerialPort(ComPort)
                com3.BaudRate = SerialBaudRate
                com3.Write(LEDnum & "~")
                '"~" is the character the arduino stops at when reading a string or number
            End Using
            ArduinoConnected = True
        Catch ex As Exception
            MsgBox("Couldn't find arduino! Please connect it! e: " + ex.Message)
            ArduinoConnected = False
        End Try
        GraphSettingsGUI.Show()
        'inits the graphsettingsGUI so that if display graph is used before settings are changed, it still exists
        GraphSettingsGUI.ShowInTaskbar = False
        GraphSettingsGUI.Text = "Graph Settings"
        GraphSettingsGUI.Hide()
        'inits the LEDsettingsgui so that if display graph is used before settings are changed, it still exists
        LEDSettingsGUI.Show()
        LEDSettingsGUI.ShowInTaskbar = False
        LEDSettingsGUI.Text = "LED Settings"
        LEDSettingsGUI.Hide()
        graph.Show() 'inits the graph so that if settings are changed before use it still exists
        graph.Hide()
        If Not ReturnSettingsFromFileUpdateGraph("previous.dat") Then 'default settings file loads
            ReturnSettingsFromFileUpdateGraph("default.dat")
        End If
        'some handlers didnt want to be added in the sub name, so are added here instead
        AddHandler GraphSettingsGUI.VisibleChanged, AddressOf UpdateRealGraph
        AddHandler LEDSettingsGUI.VisibleChanged, AddressOf UpdateRealGraph
        AddHandler graph.VisibleChanged, AddressOf GraphClosed
    End Sub
    Private Sub LEDsenschanged() Handles LEDsensitivity.ValueChanged
        LEDsens = LEDsensitivity.Value
    End Sub
    Private Sub ChangeSampleSize()    'Samples is used many places, hence the use of a global
        SoundData.Clear()
        For i = 0 To Samples - 1
            SoundData.Add(1)
        Next
        ReDim FinalAmp(Samples)
    End Sub
    Private Sub UpdateRealGraph() ' doesnt allow handler here
        If SoundWaveCapture.CaptureState <> CoreAudioApi.CaptureState.Capturing Then
            'if the audio recorder is capturing this can cause crash
            GraphSettings = GraphSettingsGUI.GetSettings()
            'fetching new settings from the settings forms
            LEDSettings = LEDSettingsGUI.GetSettings()
            Samples = GraphSettings.FFTsize
            graph.UpdateVisuals(GraphSettings)
            ChangeSampleSize()
            Try
                Using com3 As IO.Ports.SerialPort = My.Computer.Ports.OpenSerialPort(ComPort)
                    com3.BaudRate = SerialBaudRate
                    com3.Write("colour~")
                    com3.Write(LEDSettings.LEDColour.R & "~")
                    com3.Write(LEDSettings.LEDColour.G & "~")
                    com3.Write(LEDSettings.LEDColour.B & "~")
                    com3.Write("brightness~")
                    com3.Write(LEDSettings.LEDbrightness & "~")
                End Using
                ArduinoConnected = True
            Catch ex As Exception
                MsgBox("Couldn't find arduino! Please connect it!")
                ArduinoConnected = False
            End Try
        Else
                MsgBox("Currently displaying visualisation, try changing settings again when not playing music.")
        End If
    End Sub

    Private Sub GraphSettingsBTN_Click(sender As Object, e As EventArgs) Handles GraphSettingsBTN.Click
        'shows the graph settings gui box as a seperate window, and removes it from the task bar to avoid clutter
        If SoundWaveCapture.CaptureState <> CoreAudioApi.CaptureState.Capturing Then
            GraphSettingsGUI.Show()
        Else
            MsgBox("Currently displaying visualisation, try changing settings again when not playing music.")
        End If
    End Sub

    Private Sub LEDSettingsBTN_Click(sender As Object, e As EventArgs) Handles LEDSettingsBTN.Click
        'shows the led settings gui box as a seperate window, and removes it from the task bar to avoid clutter
        If SoundWaveCapture.CaptureState <> CoreAudioApi.CaptureState.Capturing Then
            LEDSettingsGUI.Show()
        Else
            MsgBox("Currently displaying visualisation, try changing settings again when not playing music.")
        End If
    End Sub

    Private Sub SaveProfileBTN_Click(sender As Object, e As EventArgs) Handles SaveProfileBTN.Click
        'write current settings to a file
        MsgBox("Files must end in .dat, and the default file loaded on bootup is previous.dat, or default.dat if not found.")
        If SaveProfileDialog.ShowDialog() = DialogResult.OK Then
            Using sw As New StreamWriter(File.Open(SaveProfileDialog.FileName, FileMode.OpenOrCreate))
                sw.WriteLine(GraphSettings.Colour.R)
                sw.WriteLine(GraphSettings.Colour.G)
                sw.WriteLine(GraphSettings.Colour.B)
                sw.WriteLine(GraphSettings.InvertSpectrum)
                sw.WriteLine(GraphSettings.LineWidth)
                sw.WriteLine(GraphSettings.PercentageMaxHeight)
                sw.WriteLine(GraphSettings.Type)
                sw.WriteLine(GraphSettings.XAxisScale)
                sw.WriteLine(GraphSettings.FFTsize)
                sw.WriteLine(LEDSettings.LEDColour.R)
                sw.WriteLine(LEDSettings.LEDColour.G)
                sw.WriteLine(LEDSettings.LEDColour.B)
                sw.WriteLine(LEDSettings.VolumeMeasurementType)
                sw.WriteLine(LEDSettings.LEDbrightness)
            End Using
        End If
    End Sub

    Private Sub LoadProfileBTN_Click(sender As Object, e As EventArgs) Handles LoadProfileBTN.Click
        'handler for loading settings during use
        If ProfileLoadingDialog.ShowDialog() = DialogResult.OK Then
            ReturnSettingsFromFileUpdateGraph(ProfileLoadingDialog.FileName)
        End If
    End Sub
    Private Function ReturnSettingsFromFileUpdateGraph(ByVal filename As String)
        'loads settings from a file
        If File.Exists(filename) Then
            'checks file exists first and gives error if not
            Try 'ensures no crashes from bad files
                Using sr As New StreamReader(File.Open(filename, FileMode.Open))
                    GraphSettings.Colour = Color.FromArgb(sr.ReadLine, sr.ReadLine, sr.ReadLine)
                    GraphSettings.InvertSpectrum = sr.ReadLine
                    GraphSettings.LineWidth = sr.ReadLine
                    GraphSettings.PercentageMaxHeight = sr.ReadLine
                    GraphSettings.Type = sr.ReadLine
                    GraphSettings.XAxisScale = sr.ReadLine
                    GraphSettings.FFTsize = CInt(sr.ReadLine)
                    LEDSettings.LEDColour = Color.FromArgb(sr.ReadLine, sr.ReadLine, sr.ReadLine)
                    LEDSettings.VolumeMeasurementType = sr.ReadLine
                    LEDSettings.LEDbrightness = sr.ReadLine
                End Using
                Samples = GraphSettings.FFTsize
                GraphSettingsGUI.UpdateSettings(GraphSettings)
                LEDSettingsGUI.UpdateSettings(LEDSettings)
                UpdateRealGraph()
            Catch e As Exception
                MsgBox("Invalid file contents! Select a valid file when loading settings.")
            End Try
        Else
            MsgBox("Cannot find settings file!")
            Return False
        End If
        Return True
    End Function
    Private Sub DisplayGraphBTN_Click(sender As Object, e As EventArgs) Handles DisplayGraphBTN.Click
        graph.Show()
        graph.Size = New Drawing.Size(800, 400) ' redraws the graph to a base size each time
    End Sub

    Private Sub UpdateGraphTimer_Tick(sender As Object, e As EventArgs) Handles UpdateGraphTimer.Tick
        'updates the graph shown to user
        Dim soundarray(Samples - 1) As Double
        For i = 0 To Samples - 1
            'copies across sounddata to a local variable multiplied by the sensitivity
            soundarray(i) = SoundData(i) * GraphSensitivity.Value / 200
        Next
        'calling the FFT
        Dim Data(,) As Double = ditfft2(soundarray, Samples)
        For i = 0 To Data.Length / 4 - 2
            'the rest of the array is the reverse of the first section
            'vector to scalar
            FinalAmp(i) = Math.Sqrt(Data(i, 0) ^ 2 + Data(i, 1) ^ 2)
        Next
        graph.NewData(FinalAmp, Samples)
    End Sub
    Private Sub SendSerialDataBaseAudioData() 'sends data to arduino using raw captured data
        Dim LEDsOn As String
        Using com3 As IO.Ports.SerialPort = My.Computer.Ports.OpenSerialPort(ComPort)
            com3.BaudRate = SerialBaudRate
            LEDsOn = Str(Int((SoundData.Max / 32767 * LEDsens / 100) * LEDnum)) 'between 0 and 10
            com3.Write(LEDsOn & "~")
        End Using
    End Sub
    Private Sub SendSerialDataFrequencyData() 'sends data to arduino using fft data
        Dim LEDsOn As String
        Using com3 As IO.Ports.SerialPort = My.Computer.Ports.OpenSerialPort(ComPort)
            com3.BaudRate = SerialBaudRate
            LEDsOn = Str(Int((FinalAmp.Max / 20000 * LEDsens / 100) * LEDnum)) 'between 0 and 10
            com3.Write(LEDsOn & "~")
        End Using
    End Sub

    Private Sub NewSoundsStereo(sender As Object, e As Wave.WaveInEventArgs)
        ' handler created in runtime for stereo speakers
        Dim RawSampleL As Single
        Dim RawSampleR As Single
        Dim RawSample As Single
        Dim bytes As Integer = e.BytesRecorded
        For i = 0 To e.BytesRecorded - 8 Step 8
            RawSampleL = BitConverter.ToSingle(e.Buffer, i)
            ' conversion from bytes to 32bit IEEE, left channel
            RawSampleR = BitConverter.ToSingle(e.Buffer, i + 4)
            ' conversion from bytes to 32bit IEEE, right channel
            RawSample = (RawSampleL + RawSampleR) / 2
            ' merging the channels
            SoundData.Add(RawSample * 32767) ' manual conversion from 32bit IEEE to 16 bit int for the fft
        Next
        SoundData.RemoveRange(0, e.BytesRecorded / 8)
        ' /8 as 2 32 bit floats, 8 bytes, are captured per 16 bit int i care about
        RaiseEvent UpdateLEDs()
    End Sub
    Private Sub NewSoundsMono(sender As Object, e As Wave.WaveInEventArgs)
        ' handler created in runtime for mono speakers
        Dim RawSample As Single
        For i = 0 To e.BytesRecorded Step 4
            RawSample = BitConverter.ToSingle(e.Buffer, i) ' conversion from bytes to 32bit IEEE, left channel
            SoundData.Add(RawSample * 32767) ' manual conversion from 32bit IEEE to 16 bit int for the fft
        Next
        SoundData.RemoveRange(0, e.BytesRecorded / 4)
        ' /4 as a 32 bit float, 4 bytes, is captured per 16 bit int i care about
        RaiseEvent UpdateLEDs()
    End Sub
    Private Sub FreezeGraph_Click(sender As Object, e As EventArgs) Handles FreezeGraph.Click
        'locks graph in place, cant be lcoked if graph not shown but can be unlocked
        If graph.Visible And Not graph.Frozen Then
            FreezeGraph.Text = "Unlock graph and return control box"
            graph.Freeze()
        ElseIf graph.Frozen Then
            FreezeGraph.Text = "Lock graph and remove control box"
            graph.Freeze()
        Else
            MsgBox("Graph not visible!")
        End If
    End Sub

    Private Sub StartGraphButton_Click(sender As Object, e As EventArgs) Handles StartGraphButton.Click
        'starts or stops display, other subs are used as starting and stopping is done by program sometimes
        Dim LEDhandler As String = LEDSettings.VolumeMeasurementType
        If UpdateGraphTimer.Enabled Then
            StopDisplay(LEDhandler)
        ElseIf graph.Visible Then
            StartDisplay(LEDhandler)
        Else
        End If
    End Sub
    Private Sub StopDisplay(ByVal LEDhandler As String)
        'stops display, removes handlers, stops recording, stops timers, changes button text
        UpdateGraphTimer.Stop()
        SoundWaveCapture.StopRecording()
        If SoundWaveCapture.WaveFormat.Channels = 2 Then
            RemoveHandler SoundWaveCapture.DataAvailable, AddressOf NewSoundsStereo
        ElseIf SoundWaveCapture.WaveFormat.Channels = 1 Then
            RemoveHandler SoundWaveCapture.DataAvailable, AddressOf NewSoundsMono
        End If
        If LEDhandler = "Max all volume" Then
            RemoveHandler UpdateLEDs, AddressOf SendSerialDataBaseAudioData
        ElseIf LEDhandler = "Max single frequency volume" Then
            RemoveHandler UpdateLEDs, AddressOf SendSerialDataFrequencyData
        End If
        StartGraphButton.Text = "Start display"
        Threading.Thread.Sleep(100)
        If ArduinoConnected Then
            Using com3 As IO.Ports.SerialPort = My.Computer.Ports.OpenSerialPort(ComPort)
                com3.BaudRate = SerialBaudRate
                com3.Write(-1 & "~")
            End Using
        End If

    End Sub
    Private Sub StartDisplay(ByVal LEDhandler As String)
        'starts display, adds handlers, starts recording, starts timers, changes button text
        If ArduinoConnected Then
            Using com3 As IO.Ports.SerialPort = My.Computer.Ports.OpenSerialPort(ComPort)
                com3.BaudRate = SerialBaudRate
                com3.Write("music~")
                'tells arduino to expect music data
            End Using
        End If
        'handler for mono or stereo speakers
        If SoundWaveCapture.WaveFormat.Channels = 2 Then
            AddHandler SoundWaveCapture.DataAvailable, AddressOf NewSoundsStereo
        ElseIf SoundWaveCapture.WaveFormat.Channels = 1 Then
            AddHandler SoundWaveCapture.DataAvailable, AddressOf NewSoundsMono
        End If
        If ArduinoConnected Then
            'checks if arudino connected first
            If LEDhandler = "Max all volume" Then
                AddHandler UpdateLEDs, AddressOf SendSerialDataBaseAudioData
            ElseIf LEDhandler = "Max single frequency volume" Then
                AddHandler UpdateLEDs, AddressOf SendSerialDataFrequencyData
            End If
        Else
            MsgBox("Not outputting to arduino, not connected. Change settings and try again.")
        End If
        SoundWaveCapture.StartRecording()
        UpdateGraphTimer.Start()
        StartGraphButton.Text = "Stop display"
    End Sub
End Class