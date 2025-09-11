' ****************************************
' Student: Landon Alexander III
' Project: Measurement Converter 2.0
' Class: Graph User Interface Dev
' Description: This form focuses on calculating the meters of certain values (either inches
' to meters, or meters to inches), and then saving the results into a file
' ****************************************************

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set up initial state when form loads
        ClearResults()
        RadioButton1.Checked = True ' Set "Inches to Meters" as default
        ListBox1.Items.Clear() ' Clear the list box
        Label4.Text = "" ' Clear the status label
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Convert button click handler
        Dim inputValue As String = TextBox1.Text.Trim()
        Dim numericValue As Double
        Dim result As Double
        Dim conversionText As String = ""

        ' Check if input is numeric
        If Not Double.TryParse(inputValue, numericValue) Then
            MessageBox.Show("Please enter a numeric value.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Check if input is positive
        If numericValue < 0 Then
            MessageBox.Show("Please enter a positive value.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Perform conversion based on selected radio button
        If RadioButton1.Checked Then
            ' Inches to Meters
            result = numericValue * 0.0254
            conversionText = $"{numericValue.ToString("0.###")} Inches = {result.ToString("0.###")} Meters"
        Else
            ' Meters to Inches
            result = numericValue / 0.0254
            conversionText = $"{numericValue.ToString("0.###")} Meters = {result.ToString("0.###")} Inches"
        End If

        ' Display the result
        Label3.Text = conversionText
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ' Clear Results button click handler
        ClearResults()
    End Sub

    Private Sub ClearResults()
        ' Clear input and results, reset to default state
        TextBox1.Text = ""
        Label3.Text = ""
        RadioButton1.Checked = True
        TextBox1.Focus()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' Save Results button click handler
        If Not String.IsNullOrEmpty(Label3.Text) Then
            ListBox1.Items.Add(Label3.Text)
        Else
            MessageBox.Show("No conversion result to save.", "No Result", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ' Clear List button click handler
        ListBox1.Items.Clear()
        Label4.Text = ""
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        ' Save To File button click handler
        If ListBox1.Items.Count = 0 Then
            MessageBox.Show("No items in the list to save.", "Empty List", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Try
            ' Write all items to the file
            System.IO.File.WriteAllLines("measures.txt", ListBox1.Items.Cast(Of String).ToArray())
            Label4.Text = $"{ListBox1.Items.Count} records saved to file."
        Catch ex As Exception
            MessageBox.Show($"Error saving file: {ex.Message}", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
