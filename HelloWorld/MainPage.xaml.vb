' The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

''' <summary>
''' An empty page that can be used on its own or navigated to within a Frame.
''' </summary>
Imports System
Imports System.Diagnostics
Imports System.ComponentModel
Imports Google.GData.Client
Imports Google.YouTube
Imports Google.GData.YouTube
Imports Google.GData.Extensions.MediaRss
Public NotInheritable Class MainPage

    Inherits Page
    Dim timer As New DispatcherTimer
    Dim Alarm As New DispatcherTimer

    Private Sub MainPage_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        CancelA.Visibility = Windows.UI.Xaml.Visibility.Collapsed
        State.Visibility = Windows.UI.Xaml.Visibility.Collapsed
        timer.Interval = TimeSpan.FromSeconds(1)
        AddHandler timer.Tick, AddressOf Timer_Tick
        timer.Start()
        Alarm.Interval = TimeSpan.FromSeconds(1)
        AddHandler Alarm.Tick, AddressOf Alarm_Tick

    End Sub

    Private Sub SetAlarm_Click(sender As Object, e As RoutedEventArgs) Handles SetAlarm.Click
        If UrlBox.Text = "" Then
            UrlBox.PlaceholderText = "Please Enter a URL"
        Else
            UrlBox.IsEnabled = False
            State.Visibility = Windows.UI.Xaml.Visibility.Visible
            TimePick.IsEnabled = False
            SetAlarm.Content = "Thy Shall be Done !"
            SetAlarm.IsEnabled = False
            CancelA.Visibility = Windows.UI.Xaml.Visibility.Visible
        End If
        Alarm.Start()
    End Sub
    Public Sub Timer_Tick(sender As Object, e As EventArgs)
        Time.Text = System.DateTime.Now.ToString("hh:mm:ss tt")
        secondHand.Angle = DateTime.Now.Second * 6
        minuteHand.Angle = DateTime.Now.Minute * 6
        hourHand.Angle = (DateTime.Now.Hour * 30) + (DateTime.Now.Minute * 0.5)
        Dim per As Integer
        per = DateTime.Now.Hour + 50
        Pic.Opacity = per / 100
    End Sub
    Public Async Sub Alarm_Tick(sender As Object, e As EventArgs)
        Dim alarm As String
        Dim RtNow As String
        RtNow = DateTime.Now.ToString("HH:mm:00")
        alarm = TimePick.Time.ToString()
        If String.Equals(alarm, RtNow) = True Then
            Await Loops()
        End If
    End Sub
    Public Async Function Loops() As Task
        Alarm.Stop()
        State.Visibility = Windows.UI.Xaml.Visibility.Collapsed
        CancelA.Visibility = Windows.UI.Xaml.Visibility.Collapsed
        Dim addr As String
        addr = UrlBox.Text
        Dim uria As New Uri(addr)
        'Await Windows.System.Launcher.LaunchUriAsync(uria)
        wv.Source = New Uri(uria, UriKind.Absolute)
        SetAlarm.IsEnabled = True
        SetAlarm.Content = "Wake Me!"
        TimePick.IsEnabled = True
        UrlBox.IsEnabled = True
    End Function

    Private Sub CancelA_Click(sender As Object, e As RoutedEventArgs) Handles CancelA.Click
        UrlBox.IsEnabled = True
        UrlBox.Text = ""
        State.Visibility = Windows.UI.Xaml.Visibility.Collapsed
        TimePick.IsEnabled = True
        SetAlarm.Content = "Wake Me!"
        SetAlarm.IsEnabled = True
        CancelA.Visibility = Windows.UI.Xaml.Visibility.Collapsed
    End Sub
End Class
