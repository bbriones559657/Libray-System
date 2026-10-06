Imports Microsoft.VisualBasic.ApplicationServices

Namespace My
    Partial Friend Class MyApplication
        Protected Overrides Sub OnCreateSplashScreen()
            Me.SplashScreen = New Global.Final_Project.SplashScreenForm()
        End Sub

        Private Sub MyApplication_ApplyApplicationDefaults(sender As Object, e As ApplyApplicationDefaultsEventArgs) Handles Me.ApplyApplicationDefaults
            e.MinimumSplashScreenDisplayTime = 1500
        End Sub
    End Class
End Namespace
