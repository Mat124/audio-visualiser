Public Class UnclosableForm 'wasn't allowed to be MustInherit as would stop children being designable - don't instantiate this
    Inherits Form
    Protected Overridable Sub Form_Closing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        'hides form instead of closing so that its functions and variables can still be accessed
        e.Cancel = True
        Me.Hide()
    End Sub
End Class
