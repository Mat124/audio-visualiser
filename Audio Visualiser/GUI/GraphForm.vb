Public Class GraphForm
    Inherits UnclosableForm

    Private debug As Boolean = True
    Private invertspectrum As Boolean = False
    Private heightpercentage As Integer = 100
    Public Frozen As Boolean

    Friend WithEvents Chart1 As DataVisualization.Charting.Chart
    Public Sub FormLoad() Handles MyBase.Load
        TransparencyKey = Color.Magenta ' anything coloured magenta in the form will be transparent
        InitializeComponent()
        GraphInitData()
        SetUpVisuals()
    End Sub
    Private Sub SetUpVisuals() ' sets up the visuals, removing unnecessary bits like the key for the graph
        Me.BackColor = Color.Magenta
        Me.Chart1.ChartAreas(0).AxisX.Enabled = False
        Me.Chart1.ChartAreas(0).AxisY.Enabled = False
        Me.Chart1.ChartAreas(0).BackColor = Color.Magenta
        Me.Chart1.ChartAreas(0).AxisX.LineWidth = 0
        Me.Chart1.ChartAreas(0).AxisY.LineWidth = 0
        Me.Chart1.ChartAreas(0).AxisX.MajorGrid.LineWidth = 0
        Me.Chart1.ChartAreas(0).AxisY.MajorGrid.LineWidth = 0
        Me.Chart1.ChartAreas(0).AxisX.MajorTickMark.LineWidth = 0
        Me.Chart1.ChartAreas(0).AxisY.MajorTickMark.LineWidth = 0
        Me.Chart1.ChartAreas(0).AxisX.LabelStyle.Enabled = False
        Me.Chart1.ChartAreas(0).AxisY.LabelStyle.Enabled = False
        Me.Chart1.ChartAreas(0).AxisY.Maximum = 50000
        Me.Chart1.ChartAreas(0).AxisX.Minimum = 0
        Me.Chart1.ChartAreas(0).AxisY.Minimum = 0
        Me.Chart1.Series(0).IsVisibleInLegend = False
    End Sub
    Public Sub UpdateVisuals(ByVal NewSettings As SettingsList) ' takes in new settings as a settingslist parameter
        Chart1.Series(0)("PixelPointWidth") = NewSettings.LineWidth
        Chart1.Series(0).ChartType = NewSettings.Type
        Chart1.Series(0).Color = NewSettings.Colour
        Chart1.ChartAreas(0).AxisX.LabelStyle.Enabled = NewSettings.XAxisScale
        heightpercentage = NewSettings.PercentageMaxHeight
        Chart1.Height = Me.ClientSize.Height * heightpercentage / 100
        Chart1.ChartAreas(0).AxisX.IsReversed = NewSettings.InvertSpectrum
    End Sub
    Private Sub GraphInitData() ' shows some inital data so that the graph doesnt disappear
        For i = 1 To 10
            Me.Chart1.Series(0).Points.AddXY(i, i)
        Next
    End Sub
    Public Sub FormSizeChanged() Handles Me.ClientSizeChanged ' keeps the graph the same size as the form
        Chart1.Height = Me.ClientSize.Height * heightpercentage / 100
        Chart1.Width = Me.ClientSize.Width
    End Sub
    Public Sub Freeze() 'removes or returns the control box to the graph, as well as the border style
        If Me.ControlBox = False Then
            Me.FormBorderStyle = FormBorderStyle.Sizable
            Me.ControlBox = True
            Me.Chart1.BackColor = Color.White ' allows window to be resized again
            Frozen = False
        Else
            Me.FormBorderStyle = FormBorderStyle.None
            Me.ControlBox = False
            Me.Chart1.BackColor = Color.Magenta
            Frozen = True
        End If
    End Sub
    Public Sub NewData(ByVal data() As Double, ByVal samplesize As Integer) 'takes new data and adds it to the graph
        Chart1.Series(0).Points.Clear()
        For i = 0 To (data.Length - 1) / 2
            Chart1.Series(0).Points.AddXY((i * 44100) / samplesize, data(i))
        Next
    End Sub

    Private Sub InitializeComponent()
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Me.Chart1 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Chart1
        '
        ChartArea1.Name = "ChartArea1"
        Me.Chart1.ChartAreas.Add(ChartArea1)
        Legend1.Name = "Legend1"
        Me.Chart1.Legends.Add(Legend1)
        Me.Chart1.Location = New System.Drawing.Point(0, 0)
        Me.Chart1.Name = "Chart1"
        Series1.ChartArea = "ChartArea1"
        Series1.Legend = "Legend1"
        Series1.Name = "Series1"
        Me.Chart1.Series.Add(Series1)
        Me.Chart1.Size = New System.Drawing.Size(300, 300)
        Me.Chart1.TabIndex = 0
        Me.Chart1.Text = "Chart1"
        '
        'GraphForm
        '
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(809, 336)
        Me.Controls.Add(Me.Chart1)
        Me.Name = "GraphForm"
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
End Class
