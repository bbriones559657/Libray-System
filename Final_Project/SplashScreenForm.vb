Public Class SplashScreenForm
    Inherits Form

    Public Sub New()
        Me.FormBorderStyle = FormBorderStyle.None
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.BackColor = Color.WhiteSmoke
        Me.ClientSize = New Size(520, 260)
        Me.ShowInTaskbar = False

        Dim border As New Panel() With {
            .Dock = DockStyle.Fill,
            .BackColor = Color.White,
            .Padding = New Padding(2)
        }

        Dim content As New Panel() With {
            .Dock = DockStyle.Fill,
            .BackColor = Color.WhiteSmoke
        }

        Dim title As New Label() With {
            .AutoSize = False,
            .Text = "LIBRARY MANAGEMENT SYSTEM",
            .Font = New Font("Segoe UI", 22.0F, FontStyle.Bold),
            .ForeColor = Color.SteelBlue,
            .TextAlign = ContentAlignment.MiddleCenter,
            .Location = New Point(20, 65),
            .Size = New Size(480, 50)
        }

        Dim subtitle As New Label() With {
            .AutoSize = False,
            .Text = "Books • Members • Borrowing • Reports",
            .Font = New Font("Segoe UI", 10.5F),
            .ForeColor = Color.DarkSlateGray,
            .TextAlign = ContentAlignment.MiddleCenter,
            .Location = New Point(20, 120),
            .Size = New Size(480, 30)
        }

        Dim loading As New Label() With {
            .AutoSize = False,
            .Text = "Loading system...",
            .Font = New Font("Segoe UI", 9.0F),
            .ForeColor = Color.Gray,
            .TextAlign = ContentAlignment.MiddleCenter,
            .Location = New Point(20, 180),
            .Size = New Size(480, 25)
        }

        Dim progress As New ProgressBar() With {
            .Style = ProgressBarStyle.Marquee,
            .MarqueeAnimationSpeed = 25,
            .Location = New Point(90, 215),
            .Size = New Size(340, 8)
        }

        content.Controls.Add(title)
        content.Controls.Add(subtitle)
        content.Controls.Add(loading)
        content.Controls.Add(progress)
        border.Controls.Add(content)
        Me.Controls.Add(border)
    End Sub
End Class
