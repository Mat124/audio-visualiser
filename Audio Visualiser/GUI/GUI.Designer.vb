<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class GUI
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GUI))
        Me.GraphSettingsBTN = New System.Windows.Forms.Button()
        Me.LEDSettingsBTN = New System.Windows.Forms.Button()
        Me.SaveProfileBTN = New System.Windows.Forms.Button()
        Me.LoadProfileBTN = New System.Windows.Forms.Button()
        Me.DisplayGraphBTN = New System.Windows.Forms.Button()
        Me.ProfileLoadingDialog = New System.Windows.Forms.OpenFileDialog()
        Me.UpdateGraphTimer = New System.Windows.Forms.Timer(Me.components)
        Me.FreezeGraph = New System.Windows.Forms.Button()
        Me.StartGraphButton = New System.Windows.Forms.Button()
        Me.SaveProfileDialog = New System.Windows.Forms.SaveFileDialog()
        Me.FourierDEscriptionTEXT = New System.Windows.Forms.TextBox()
        Me.GraphSensitivity = New System.Windows.Forms.TrackBar()
        Me.LEDsensitivity = New System.Windows.Forms.TrackBar()
        Me.GraphSensitivityTEXT = New System.Windows.Forms.TextBox()
        Me.LEDsensitivityTEXT = New System.Windows.Forms.TextBox()
        CType(Me.GraphSensitivity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LEDsensitivity, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GraphSettingsBTN
        '
        Me.GraphSettingsBTN.Location = New System.Drawing.Point(15, 13)
        Me.GraphSettingsBTN.Name = "GraphSettingsBTN"
        Me.GraphSettingsBTN.Size = New System.Drawing.Size(190, 51)
        Me.GraphSettingsBTN.TabIndex = 0
        Me.GraphSettingsBTN.Text = "Graph Settings"
        Me.GraphSettingsBTN.UseVisualStyleBackColor = True
        '
        'LEDSettingsBTN
        '
        Me.LEDSettingsBTN.Location = New System.Drawing.Point(15, 70)
        Me.LEDSettingsBTN.Name = "LEDSettingsBTN"
        Me.LEDSettingsBTN.Size = New System.Drawing.Size(190, 51)
        Me.LEDSettingsBTN.TabIndex = 1
        Me.LEDSettingsBTN.Text = "LED Settings"
        Me.LEDSettingsBTN.UseVisualStyleBackColor = True
        '
        'SaveProfileBTN
        '
        Me.SaveProfileBTN.Location = New System.Drawing.Point(15, 127)
        Me.SaveProfileBTN.Name = "SaveProfileBTN"
        Me.SaveProfileBTN.Size = New System.Drawing.Size(190, 51)
        Me.SaveProfileBTN.TabIndex = 2
        Me.SaveProfileBTN.Text = "Save Profile"
        Me.SaveProfileBTN.UseVisualStyleBackColor = True
        '
        'LoadProfileBTN
        '
        Me.LoadProfileBTN.Location = New System.Drawing.Point(15, 184)
        Me.LoadProfileBTN.Name = "LoadProfileBTN"
        Me.LoadProfileBTN.Size = New System.Drawing.Size(190, 51)
        Me.LoadProfileBTN.TabIndex = 3
        Me.LoadProfileBTN.Text = "Load Profile"
        Me.LoadProfileBTN.UseVisualStyleBackColor = True
        '
        'DisplayGraphBTN
        '
        Me.DisplayGraphBTN.Location = New System.Drawing.Point(211, 13)
        Me.DisplayGraphBTN.Name = "DisplayGraphBTN"
        Me.DisplayGraphBTN.Size = New System.Drawing.Size(190, 51)
        Me.DisplayGraphBTN.TabIndex = 5
        Me.DisplayGraphBTN.Text = "Display Graph"
        Me.DisplayGraphBTN.UseVisualStyleBackColor = True
        '
        'ProfileLoadingDialog
        '
        Me.ProfileLoadingDialog.FileName = "ProfileLoadingDialog"
        Me.ProfileLoadingDialog.Filter = "Dat Files|*.dat"
        '
        'UpdateGraphTimer
        '
        Me.UpdateGraphTimer.Interval = 15
        '
        'FreezeGraph
        '
        Me.FreezeGraph.Location = New System.Drawing.Point(211, 70)
        Me.FreezeGraph.Name = "FreezeGraph"
        Me.FreezeGraph.Size = New System.Drawing.Size(190, 51)
        Me.FreezeGraph.TabIndex = 10
        Me.FreezeGraph.Text = "Lock graph and remove control box"
        Me.FreezeGraph.UseVisualStyleBackColor = True
        '
        'StartGraphButton
        '
        Me.StartGraphButton.Location = New System.Drawing.Point(211, 127)
        Me.StartGraphButton.Name = "StartGraphButton"
        Me.StartGraphButton.Size = New System.Drawing.Size(190, 51)
        Me.StartGraphButton.TabIndex = 11
        Me.StartGraphButton.Text = "Start display"
        Me.StartGraphButton.UseVisualStyleBackColor = True
        '
        'SaveProfileDialog
        '
        Me.SaveProfileDialog.Filter = "Dat Files|*.dat"
        '
        'FourierDEscriptionTEXT
        '
        Me.FourierDEscriptionTEXT.Enabled = False
        Me.FourierDEscriptionTEXT.Location = New System.Drawing.Point(211, 184)
        Me.FourierDEscriptionTEXT.Multiline = True
        Me.FourierDEscriptionTEXT.Name = "FourierDEscriptionTEXT"
        Me.FourierDEscriptionTEXT.Size = New System.Drawing.Size(190, 71)
        Me.FourierDEscriptionTEXT.TabIndex = 12
        Me.FourierDEscriptionTEXT.Text = "Some combinations of settings may result in an unusable program, due to the deman" &
    "d on the CPU. This is especially common when using FFT sizes of 8192 or greater." &
    ""
        '
        'GraphSensitivity
        '
        Me.GraphSensitivity.Location = New System.Drawing.Point(15, 278)
        Me.GraphSensitivity.Maximum = 1000
        Me.GraphSensitivity.Name = "GraphSensitivity"
        Me.GraphSensitivity.Size = New System.Drawing.Size(386, 45)
        Me.GraphSensitivity.TabIndex = 13
        '
        'LEDsensitivity
        '
        Me.LEDsensitivity.Location = New System.Drawing.Point(15, 362)
        Me.LEDsensitivity.Maximum = 1000
        Me.LEDsensitivity.Name = "LEDsensitivity"
        Me.LEDsensitivity.Size = New System.Drawing.Size(386, 45)
        Me.LEDsensitivity.TabIndex = 14
        '
        'GraphSensitivityTEXT
        '
        Me.GraphSensitivityTEXT.Location = New System.Drawing.Point(15, 252)
        Me.GraphSensitivityTEXT.Name = "GraphSensitivityTEXT"
        Me.GraphSensitivityTEXT.ReadOnly = True
        Me.GraphSensitivityTEXT.Size = New System.Drawing.Size(100, 20)
        Me.GraphSensitivityTEXT.TabIndex = 15
        Me.GraphSensitivityTEXT.Text = "Graph sensitivity"
        '
        'LEDsensitivityTEXT
        '
        Me.LEDsensitivityTEXT.Location = New System.Drawing.Point(15, 336)
        Me.LEDsensitivityTEXT.Name = "LEDsensitivityTEXT"
        Me.LEDsensitivityTEXT.ReadOnly = True
        Me.LEDsensitivityTEXT.Size = New System.Drawing.Size(100, 20)
        Me.LEDsensitivityTEXT.TabIndex = 16
        Me.LEDsensitivityTEXT.Text = "LED sensitivity"
        '
        'GUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(414, 401)
        Me.Controls.Add(Me.LEDsensitivityTEXT)
        Me.Controls.Add(Me.GraphSensitivityTEXT)
        Me.Controls.Add(Me.LEDsensitivity)
        Me.Controls.Add(Me.GraphSensitivity)
        Me.Controls.Add(Me.FourierDEscriptionTEXT)
        Me.Controls.Add(Me.StartGraphButton)
        Me.Controls.Add(Me.FreezeGraph)
        Me.Controls.Add(Me.DisplayGraphBTN)
        Me.Controls.Add(Me.LoadProfileBTN)
        Me.Controls.Add(Me.SaveProfileBTN)
        Me.Controls.Add(Me.LEDSettingsBTN)
        Me.Controls.Add(Me.GraphSettingsBTN)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximumSize = New System.Drawing.Size(430, 440)
        Me.MinimumSize = New System.Drawing.Size(430, 440)
        Me.Name = "GUI"
        Me.Tag = ""
        Me.Text = "Audio Visualiser"
        CType(Me.GraphSensitivity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LEDsensitivity, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GraphSettingsBTN As Button
    Friend WithEvents LEDSettingsBTN As Button
    Friend WithEvents SaveProfileBTN As Button
    Friend WithEvents LoadProfileBTN As Button
    Friend WithEvents DisplayGraphBTN As Button
    Friend WithEvents ProfileLoadingDialog As OpenFileDialog
    Friend WithEvents UpdateGraphTimer As Timer
    Friend WithEvents FreezeGraph As Button
    Friend WithEvents StartGraphButton As Button
    Friend WithEvents SaveProfileDialog As SaveFileDialog
    Friend WithEvents FourierDEscriptionTEXT As TextBox
    Friend WithEvents GraphSensitivity As TrackBar
    Friend WithEvents LEDsensitivity As TrackBar
    Friend WithEvents GraphSensitivityTEXT As TextBox
    Friend WithEvents LEDsensitivityTEXT As TextBox
End Class
