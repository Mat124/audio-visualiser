Public Class GraphSettingsForm
    Inherits UnclosableForm

    Friend WithEvents GraphColourDialog As ColorDialog
    Friend WithEvents LineWidthBox As NumericUpDown
    Friend WithEvents MaxHeightBox As NumericUpDown
    Friend WithEvents InvertSpectrumCheck As CheckBox
    Friend WithEvents ToggleScaleBox As CheckBox
    Friend WithEvents LineWidthText As TextBox
    Friend WithEvents MaxHeightText As TextBox
    Friend WithEvents PreviewChart As DataVisualization.Charting.Chart
    Friend WithEvents GraphTypeCombo As ComboBox
    Friend WithEvents FFTSizeCombo As ComboBox
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents FFTTextBox As TextBox
    Friend WithEvents ColourChooser As Button

    Public Function GetSettings() As SettingsList
        'Creates and returns a Settings data structure containing the current settings
        Dim CurrentSettings As SettingsList
        CurrentSettings.Colour = PreviewChart.Series(1).Color
        CurrentSettings.InvertSpectrum = InvertSpectrumCheck.Checked
        CurrentSettings.LineWidth = LineWidthBox.Value
        CurrentSettings.Type = GraphTypeCombo.SelectedValue
        CurrentSettings.XAxisScale = ToggleScaleBox.Checked
        CurrentSettings.PercentageMaxHeight = MaxHeightBox.Value
        CurrentSettings.FFTsize = FFTSizeCombo.SelectedItem
        Return CurrentSettings
    End Function
    Public Sub UpdateSettings(ByVal NewSettings As SettingsList)
        'sets the current settings to those parsed in the sub
        PreviewChart.Series(1).Color = NewSettings.Colour
        InvertSpectrumCheck.Checked = NewSettings.InvertSpectrum
        LineWidthBox.Value = NewSettings.LineWidth
        GraphTypeCombo.SelectedIndex = GraphTypeCombo.FindString(NewSettings.Type.ToString)
        ToggleScaleBox.Checked = NewSettings.XAxisScale
        MaxHeightBox.Value = NewSettings.PercentageMaxHeight
        FFTSizeCombo.SelectedIndex = FFTSizeCombo.FindString(NewSettings.FFTsize)
    End Sub
    Private Sub MaxHeightBox_ValueChanged(sender As Object, e As EventArgs) Handles MaxHeightBox.ValueChanged
        ' updates the preview graph with the new data for the maxheight of the graph
        PreviewChart.Series(1).Points.Clear()
        For Each point In PreviewChart.Series(0).Points
            PreviewChart.Series(1).Points.AddXY(point.XValue, (point.YValues(0) * MaxHeightBox.Value) / 100)
        Next
    End Sub
    Private Sub LineWidthBox_ValueChanged(sender As Object, e As EventArgs) Handles LineWidthBox.ValueChanged
        'updates the preview graph with new data entered into the linewidthbox
        PreviewChart.Series(1)("PixelPointWidth") = LineWidthBox.Value
    End Sub
    Private Sub ToggleScaleBox_CheckedChanged(sender As Object, e As EventArgs) Handles ToggleScaleBox.CheckedChanged
        ' updates preview graph with changes to the togglescale box
        If ToggleScaleBox.Checked Then
            Me.PreviewChart.ChartAreas(0).AxisX.LabelStyle.Enabled = True
            Me.PreviewChart.ChartAreas(0).AxisX.Minimum = 0
        Else
            Me.PreviewChart.ChartAreas(0).AxisX.LabelStyle.Enabled = False
        End If
    End Sub

    Private Sub StartUp() Handles MyBase.Load
        'two graphs are stored here so that the original values are preserved and 
        'the maxheight adjusted values can be shown over the top
        InitializeComponent()
        Me.PreviewChart.ChartAreas(0).AxisX.Enabled = False ' removes chart lines and labels
        Me.PreviewChart.ChartAreas(0).AxisY.Enabled = False
        Me.PreviewChart.ChartAreas(0).AxisX.LineWidth = 0
        Me.PreviewChart.ChartAreas(0).AxisY.LineWidth = 0
        Me.PreviewChart.ChartAreas(0).AxisX.MajorGrid.LineWidth = 0
        Me.PreviewChart.ChartAreas(0).AxisY.MajorGrid.LineWidth = 0
        Me.PreviewChart.ChartAreas(0).AxisX.MajorTickMark.LineWidth = 0
        Me.PreviewChart.ChartAreas(0).AxisY.MajorTickMark.LineWidth = 0
        Me.PreviewChart.ChartAreas(0).AxisX.LabelStyle.Enabled = False
        Me.PreviewChart.ChartAreas(0).AxisY.LabelStyle.Enabled = False
        Me.PreviewChart.Series(0).IsVisibleInLegend = False
        Me.PreviewChart.Series(1).IsVisibleInLegend = False
        PreviewChart.Series(0).Points.Clear()
        For i = 0 To 25
            PreviewChart.Series(0).Points.AddXY(i, Int(Rnd() * 9) + 1) 'adds 25 random values to the graph
        Next
        For Each point In PreviewChart.Series(0).Points
            PreviewChart.Series(1).Points.AddXY(point.XValue, point.YValues(0))
            ' series 1 is the display series, series 0 holds all the points so that they may be reused later
        Next
        Me.LineWidthBox.Minimum = 1 ' if set inside init then it crashes on start up
        Me.PreviewChart.Series(0).Color = Color.Transparent
        Me.MaxHeightBox.Value = 100
        Me.FFTSizeCombo.SelectedIndex = 4
    End Sub
    Private Sub ColourChooser_Click(sender As Object, e As EventArgs) Handles ColourChooser.Click
        ' shows the colour chooser dialog and sets the graph colour to it
        GraphColourDialog.ShowDialog()
        PreviewChart.Series(1).Color = GraphColourDialog.Color
    End Sub

    Private Sub ChangeGraphType() Handles GraphTypeCombo.SelectedValueChanged
        ' sets both graphs to the selected value
        PreviewChart.Series(1).ChartType = GraphTypeCombo.SelectedValue
        PreviewChart.Series(0).ChartType = GraphTypeCombo.SelectedValue
    End Sub
    Private Sub InvertSpectrumCheck_CheckedChanged(sender As Object, e As EventArgs) Handles InvertSpectrumCheck.CheckedChanged
        'reverses the graph or sets it the right way when checked
        If InvertSpectrumCheck.Checked Then
            PreviewChart.ChartAreas(0).AxisX.IsReversed = True
        Else
            PreviewChart.ChartAreas(0).AxisX.IsReversed = False
        End If
    End Sub

    Private Sub InitializeComponent()
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series2 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GraphSettingsForm))
        Me.GraphColourDialog = New System.Windows.Forms.ColorDialog()
        Me.ColourChooser = New System.Windows.Forms.Button()
        Me.LineWidthBox = New System.Windows.Forms.NumericUpDown()
        Me.MaxHeightBox = New System.Windows.Forms.NumericUpDown()
        Me.InvertSpectrumCheck = New System.Windows.Forms.CheckBox()
        Me.ToggleScaleBox = New System.Windows.Forms.CheckBox()
        Me.LineWidthText = New System.Windows.Forms.TextBox()
        Me.MaxHeightText = New System.Windows.Forms.TextBox()
        Me.PreviewChart = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.GraphTypeCombo = New System.Windows.Forms.ComboBox()
        Me.FFTSizeCombo = New System.Windows.Forms.ComboBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.FFTTextBox = New System.Windows.Forms.TextBox()
        CType(Me.LineWidthBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MaxHeightBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PreviewChart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ColourChooser
        '
        Me.ColourChooser.Location = New System.Drawing.Point(12, 12)
        Me.ColourChooser.Name = "ColourChooser"
        Me.ColourChooser.Size = New System.Drawing.Size(118, 32)
        Me.ColourChooser.TabIndex = 0
        Me.ColourChooser.Text = "Choose Colour"
        Me.ColourChooser.UseVisualStyleBackColor = True
        '
        'LineWidthBox
        '
        Me.LineWidthBox.Location = New System.Drawing.Point(12, 50)
        Me.LineWidthBox.Name = "LineWidthBox"
        Me.LineWidthBox.Size = New System.Drawing.Size(118, 20)
        Me.LineWidthBox.TabIndex = 1
        '
        'MaxHeightBox
        '
        Me.MaxHeightBox.Location = New System.Drawing.Point(12, 76)
        Me.MaxHeightBox.Name = "MaxHeightBox"
        Me.MaxHeightBox.Size = New System.Drawing.Size(118, 20)
        Me.MaxHeightBox.TabIndex = 2
        '
        'InvertSpectrumCheck
        '
        Me.InvertSpectrumCheck.AutoSize = True
        Me.InvertSpectrumCheck.Location = New System.Drawing.Point(12, 156)
        Me.InvertSpectrumCheck.Name = "InvertSpectrumCheck"
        Me.InvertSpectrumCheck.Size = New System.Drawing.Size(99, 17)
        Me.InvertSpectrumCheck.TabIndex = 5
        Me.InvertSpectrumCheck.Text = "Invert spectrum"
        Me.InvertSpectrumCheck.UseVisualStyleBackColor = True
        '
        'ToggleScaleBox
        '
        Me.ToggleScaleBox.AutoSize = True
        Me.ToggleScaleBox.Location = New System.Drawing.Point(12, 179)
        Me.ToggleScaleBox.Name = "ToggleScaleBox"
        Me.ToggleScaleBox.Size = New System.Drawing.Size(97, 17)
        Me.ToggleScaleBox.TabIndex = 6
        Me.ToggleScaleBox.Text = "Scale on x-axis"
        Me.ToggleScaleBox.UseVisualStyleBackColor = True
        '
        'LineWidthText
        '
        Me.LineWidthText.BackColor = System.Drawing.SystemColors.Window
        Me.LineWidthText.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.LineWidthText.Enabled = False
        Me.LineWidthText.Location = New System.Drawing.Point(137, 52)
        Me.LineWidthText.Name = "LineWidthText"
        Me.LineWidthText.Size = New System.Drawing.Size(100, 13)
        Me.LineWidthText.TabIndex = 7
        Me.LineWidthText.Text = "Bar width"
        '
        'MaxHeightText
        '
        Me.MaxHeightText.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.MaxHeightText.Enabled = False
        Me.MaxHeightText.Location = New System.Drawing.Point(137, 75)
        Me.MaxHeightText.Name = "MaxHeightText"
        Me.MaxHeightText.Size = New System.Drawing.Size(170, 13)
        Me.MaxHeightText.TabIndex = 8
        Me.MaxHeightText.Text = "Max height, % of window"
        '
        'PreviewChart
        '
        ChartArea1.Name = "ChartArea1"
        Me.PreviewChart.ChartAreas.Add(ChartArea1)
        Legend1.Name = "Legend1"
        Me.PreviewChart.Legends.Add(Legend1)
        Me.PreviewChart.Location = New System.Drawing.Point(346, 12)
        Me.PreviewChart.Name = "PreviewChart"
        Series1.ChartArea = "ChartArea1"
        Series1.Legend = "Legend1"
        Series1.Name = "Series1"
        Series2.ChartArea = "ChartArea1"
        Series2.Legend = "Legend1"
        Series2.Name = "Series2"
        Me.PreviewChart.Series.Add(Series1)
        Me.PreviewChart.Series.Add(Series2)
        Me.PreviewChart.Size = New System.Drawing.Size(418, 198)
        Me.PreviewChart.TabIndex = 11
        Me.PreviewChart.Text = "PreviewChart"
        '
        'GraphTypeCombo
        '
        Me.GraphTypeCombo.DataSource = New System.Windows.Forms.DataVisualization.Charting.SeriesChartType() {
            System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Area,
            System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar,
            System.Windows.Forms.DataVisualization.Charting.SeriesChartType.BoxPlot,
            System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bubble,
            System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick,
            System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column,
            System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut,
            System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine,
            System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastPoint,
            System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Funnel,
            System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line,
            System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie,
            System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point,
            System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Polar,
            System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pyramid,
            System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Radar,
            System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Range,
            System.Windows.Forms.DataVisualization.Charting.SeriesChartType.RangeBar,
            System.Windows.Forms.DataVisualization.Charting.SeriesChartType.RangeColumn,
            System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline,
            System.Windows.Forms.DataVisualization.Charting.SeriesChartType.SplineArea,
            System.Windows.Forms.DataVisualization.Charting.SeriesChartType.SplineRange,
            System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedArea,
            System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedBar,
            System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn,
            System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StepLine,
            System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Stock
        }
        Me.GraphTypeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.GraphTypeCombo.FormattingEnabled = True
        Me.GraphTypeCombo.Location = New System.Drawing.Point(12, 103)
        Me.GraphTypeCombo.Name = "GraphTypeCombo"
        Me.GraphTypeCombo.Size = New System.Drawing.Size(121, 21)
        Me.GraphTypeCombo.TabIndex = 12
        '
        'FFTSizeCombo
        '
        Me.FFTSizeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.FFTSizeCombo.FormattingEnabled = True
        Me.FFTSizeCombo.Items.AddRange(New Object() {"64", "128", "256", "512", "1024", "2048", "4096", "8192", "16384", "32768"})
        Me.FFTSizeCombo.Location = New System.Drawing.Point(13, 131)
        Me.FFTSizeCombo.Name = "FFTSizeCombo"
        Me.FFTSizeCombo.Size = New System.Drawing.Size(121, 21)
        Me.FFTSizeCombo.TabIndex = 14
        '
        'TextBox1
        '
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox1.Enabled = False
        Me.TextBox1.Location = New System.Drawing.Point(139, 106)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(116, 13)
        Me.TextBox1.TabIndex = 15
        Me.TextBox1.Text = "Graph Type"
        '
        'FFTTextBox
        '
        Me.FFTTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.FFTTextBox.Enabled = False
        Me.FFTTextBox.Location = New System.Drawing.Point(140, 134)
        Me.FFTTextBox.Multiline = True
        Me.FFTTextBox.Name = "FFTTextBox"
        Me.FFTTextBox.Size = New System.Drawing.Size(167, 62)
        Me.FFTTextBox.TabIndex = 16
        Me.FFTTextBox.Text = "FFT size, larger size means greater resolution but slower computing. 1024 should " &
    "run well on most systems"
        '
        'GraphSettingsForm
        '
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(767, 212)
        Me.Controls.Add(Me.FFTTextBox)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.FFTSizeCombo)
        Me.Controls.Add(Me.GraphTypeCombo)
        Me.Controls.Add(Me.PreviewChart)
        Me.Controls.Add(Me.MaxHeightText)
        Me.Controls.Add(Me.LineWidthText)
        Me.Controls.Add(Me.ToggleScaleBox)
        Me.Controls.Add(Me.InvertSpectrumCheck)
        Me.Controls.Add(Me.MaxHeightBox)
        Me.Controls.Add(Me.LineWidthBox)
        Me.Controls.Add(Me.ColourChooser)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "GraphSettingsForm"
        CType(Me.LineWidthBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MaxHeightBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PreviewChart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
End Class