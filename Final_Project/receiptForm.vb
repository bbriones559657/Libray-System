Imports System.Drawing.Printing

Public Class receiptForm
    Inherits Form

    Private ReadOnly receiptBody As String
    Private ReadOnly receiptHeading As String
    Private ReadOnly txtReceipt As New TextBox()
    Private ReadOnly btnPrint As New Button()
    Private ReadOnly btnClose As New Button()
    Private WithEvents printDocument As New PrintDocument()

    Public Sub New(title As String, body As String)
        receiptHeading = title
        receiptBody = body
        initializeReceiptForm()
    End Sub

    Private Sub initializeReceiptForm()
        Me.Text = receiptHeading
        Me.StartPosition = FormStartPosition.CenterParent
        Me.BackColor = Color.WhiteSmoke
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.ClientSize = New Size(470, 560)

        Dim lblTitle As New Label()
        lblTitle.AutoSize = True
        lblTitle.Font = New Font("Segoe UI", 16.0F, FontStyle.Bold)
        lblTitle.ForeColor = Color.SteelBlue
        lblTitle.Location = New Point(22, 18)
        lblTitle.Text = receiptHeading

        txtReceipt.Multiline = True
        txtReceipt.ReadOnly = True
        txtReceipt.ScrollBars = ScrollBars.Vertical
        txtReceipt.Font = New Font(FontFamily.GenericMonospace, 10.0F)
        txtReceipt.BackColor = Color.White
        txtReceipt.ForeColor = Color.Black
        txtReceipt.Location = New Point(22, 60)
        txtReceipt.Size = New Size(426, 425)
        txtReceipt.Text = receiptBody

        btnPrint.BackColor = Color.SteelBlue
        btnPrint.FlatStyle = FlatStyle.Flat
        btnPrint.FlatAppearance.BorderSize = 0
        btnPrint.ForeColor = Color.White
        btnPrint.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        btnPrint.Location = New Point(22, 505)
        btnPrint.Size = New Size(200, 38)
        btnPrint.Text = "PRINT RECEIPT"

        btnClose.BackColor = Color.White
        btnClose.FlatStyle = FlatStyle.Flat
        btnClose.FlatAppearance.BorderColor = Color.SteelBlue
        btnClose.ForeColor = Color.SteelBlue
        btnClose.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        btnClose.Location = New Point(248, 505)
        btnClose.Size = New Size(200, 38)
        btnClose.Text = "CLOSE"

        AddHandler btnPrint.Click, AddressOf btnPrint_Click
        AddHandler btnClose.Click, AddressOf btnClose_Click

        Controls.Add(lblTitle)
        Controls.Add(txtReceipt)
        Controls.Add(btnPrint)
        Controls.Add(btnClose)
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs)
        Try
            Using dialog As New PrintDialog()
                dialog.Document = printDocument
                dialog.UseEXDialog = True

                If dialog.ShowDialog(Me) = DialogResult.OK Then
                    printDocument.Print()
                End If
            End Using
        Catch ex As Exception
            MsgBox("Unable to print the receipt." & Environment.NewLine & ex.Message,
                   vbOKOnly Or vbCritical, "Print Error")
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub printDocument_PrintPage(sender As Object, e As PrintPageEventArgs) Handles printDocument.PrintPage
        Using titleFont As New Font("Segoe UI", 14.0F, FontStyle.Bold)
            Using bodyFont As New Font(FontFamily.GenericMonospace, 10.0F)
                Dim leftMargin As Single = e.MarginBounds.Left
                Dim topMargin As Single = e.MarginBounds.Top
                Dim printableWidth As Single = e.MarginBounds.Width

                e.Graphics.DrawString(receiptHeading, titleFont, Brushes.Black, leftMargin, topMargin)

                Dim bodyRectangle As New RectangleF(
                    leftMargin,
                    topMargin + titleFont.GetHeight(e.Graphics) + 18,
                    printableWidth,
                    e.MarginBounds.Height - titleFont.GetHeight(e.Graphics) - 18)

                e.Graphics.DrawString(receiptBody, bodyFont, Brushes.Black, bodyRectangle)
                e.HasMorePages = False
            End Using
        End Using
    End Sub
End Class
