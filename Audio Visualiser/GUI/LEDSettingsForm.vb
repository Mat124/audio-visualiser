Public Class LEDSettingsForm
    Inherits UnclosableForm
    Friend WithEvents VolumeMeasurementType As ComboBox
    Friend WithEvents VolumeMeasurementTypeText As TextBox
    Friend WithEvents LEDcolourTEXT As TextBox
    Friend WithEvents ColourButton As Button
    Friend WithEvents LEDcolourdialog As ColorDialog
    Friend WithEvents LEDbrightness As NumericUpDown
    Friend WithEvents LEDbrightnessTEXT As TextBox
    Private Sub init() Handles Me.Load
        'inits the settings form
        InitializeComponent()
        'defaults to these settings
        VolumeMeasurementType.SelectedIndex = 0
        LEDbrightness.Value = 10
        LEDcolourdialog.Color = Color.Red
    End Sub
    Public Function GetSettings() As LEDSettingsList
        'fetch settings function, returns ledsettingslist data structure
        Dim NewSettings As LEDSettingsList
        NewSettings.LEDColour = LEDcolourdialog.Color
        NewSettings.VolumeMeasurementType = VolumeMeasurementType.Text
        NewSettings.LEDbrightness = (LEDbrightness.Value * 255) / 100
        Return NewSettings
    End Function
    Public Sub UpdateSettings(ByVal NewSettings As LEDSettingsList)
        'updates settings to those parsed by the settingslist structure
        LEDcolourdialog.Color = NewSettings.LEDColour
        VolumeMeasurementType.Text = NewSettings.VolumeMeasurementType
        LEDbrightness.Value = (NewSettings.LEDbrightness * 100) / 255
    End Sub
    Private Sub InitializeComponent()
        Me.VolumeMeasurementType = New System.Windows.Forms.ComboBox()
        Me.VolumeMeasurementTypeText = New System.Windows.Forms.TextBox()
        Me.LEDcolourTEXT = New System.Windows.Forms.TextBox()
        Me.ColourButton = New System.Windows.Forms.Button()
        Me.LEDcolourdialog = New System.Windows.Forms.ColorDialog()
        Me.LEDbrightness = New System.Windows.Forms.NumericUpDown()
        Me.LEDbrightnessTEXT = New System.Windows.Forms.TextBox()
        CType(Me.LEDbrightness, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'VolumeMeasurementType
        '
        Me.VolumeMeasurementType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.VolumeMeasurementType.FormattingEnabled = True
        Me.VolumeMeasurementType.Items.AddRange(New Object() {"Max all volume", "Max single frequency volume"})
        Me.VolumeMeasurementType.Location = New System.Drawing.Point(14, 17)
        Me.VolumeMeasurementType.Name = "VolumeMeasurementType"
        Me.VolumeMeasurementType.Size = New System.Drawing.Size(121, 21)
        Me.VolumeMeasurementType.TabIndex = 2
        '
        'VolumeMeasurementTypeText
        '
        Me.VolumeMeasurementTypeText.Location = New System.Drawing.Point(141, 18)
        Me.VolumeMeasurementTypeText.Name = "VolumeMeasurementTypeText"
        Me.VolumeMeasurementTypeText.ReadOnly = True
        Me.VolumeMeasurementTypeText.Size = New System.Drawing.Size(148, 20)
        Me.VolumeMeasurementTypeText.TabIndex = 3
        Me.VolumeMeasurementTypeText.Text = "volume measurement type"
        '
        'LEDcolourTEXT
        '
        Me.LEDcolourTEXT.Location = New System.Drawing.Point(141, 47)
        Me.LEDcolourTEXT.Name = "LEDcolourTEXT"
        Me.LEDcolourTEXT.ReadOnly = True
        Me.LEDcolourTEXT.Size = New System.Drawing.Size(148, 20)
        Me.LEDcolourTEXT.TabIndex = 6
        Me.LEDcolourTEXT.Text = "LED colour"
        '
        'ColourButton
        '
        Me.ColourButton.Location = New System.Drawing.Point(15, 47)
        Me.ColourButton.Name = "ColourButton"
        Me.ColourButton.Size = New System.Drawing.Size(120, 20)
        Me.ColourButton.TabIndex = 10
        Me.ColourButton.Text = "Choose colour"
        Me.ColourButton.UseVisualStyleBackColor = True
        '
        'LEDbrightness
        '
        Me.LEDbrightness.Location = New System.Drawing.Point(15, 73)
        Me.LEDbrightness.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.LEDbrightness.Name = "LEDbrightness"
        Me.LEDbrightness.Size = New System.Drawing.Size(120, 20)
        Me.LEDbrightness.TabIndex = 11
        Me.LEDbrightness.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'LEDbrightnessTEXT
        '
        Me.LEDbrightnessTEXT.Location = New System.Drawing.Point(141, 72)
        Me.LEDbrightnessTEXT.Name = "LEDbrightnessTEXT"
        Me.LEDbrightnessTEXT.ReadOnly = True
        Me.LEDbrightnessTEXT.Size = New System.Drawing.Size(148, 20)
        Me.LEDbrightnessTEXT.TabIndex = 12
        Me.LEDbrightnessTEXT.Text = "LED brightness %"
        '
        'LEDSettingsForm
        '
        Me.ClientSize = New System.Drawing.Size(301, 108)
        Me.Controls.Add(Me.LEDbrightnessTEXT)
        Me.Controls.Add(Me.LEDbrightness)
        Me.Controls.Add(Me.ColourButton)
        Me.Controls.Add(Me.LEDcolourTEXT)
        Me.Controls.Add(Me.VolumeMeasurementTypeText)
        Me.Controls.Add(Me.VolumeMeasurementType)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximumSize = New System.Drawing.Size(500, 500)
        Me.Name = "LEDSettingsForm"
        CType(Me.LEDbrightness, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private Sub ColourButton_Click(sender As Object, e As EventArgs) Handles ColourButton.Click
        'changes colour using the colour dialog
        LEDcolourdialog.ShowDialog()
    End Sub
End Class