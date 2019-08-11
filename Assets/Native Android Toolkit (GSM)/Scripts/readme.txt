****README version 1.0****
Current Date: 5/31/2013 
Written by: Mathew Mozer, Stephen King - Typon Pacific Studios
Contact: support@typac.zendesk.com
Software and versions: Android Native Toolkit 2 (Google Cloude Messaging) v1.0
Relevant documentation: See the comments inside of GCM.cs

************************************************
General purpose:

This plugin allows easy access Google Cloud Messaging, which can be used to
send messages directly to the user's device. You could, for example, alert
players in an MMO of server maintainence, friend requests, or other important
information. For those who are developming non-game apps, the plugin 
saves time and money by allowing the use of GCM from Unity without writing 
a single line of code inside Eclipse.

************************************************
Installation:

Import the following files into your project:
-GCM.cs
-AndroidManifest.xml
-unitygcmplugin.jar
-gcm.jar
-readme.txt

************************************************
Basic operation:

Configuring a Google Cloud Messaging Project (if you have already done this,
then move on to Configuring Your Unity Project with GCM):

Step 1: Go to http://code.google.com/apis/console and create a new project.

Step 2: Inside the Google Cloud Messaging API console, select API Access from 
the left menu.

Step 3: Click Create New Server Key and insert the IP address of your server(s), 
then click create. This also gives you your API key.

Step 4: Click Overview in the left menu to get your Project ID.

===============================================
Configuring your Unity Project to work with GCM:

Step 1) Make sure all permissions and intent filters/receivers are properly located 
in your Manifest. There are comments in the Manifest to show you what you need. 

step 2) Register device with GCM:
All devices must be registered on the GCM server using your projectID (See step 1 of 
Configuring a Google Cloud Messaging Project). Once your device is registered you will 
receive a GCM message on your device when your Unity project utilizes GCMReceiver 
(from the GCM.cs script). It will have the string "GCMRegistered" along with the Device 
Registration ID.

obj_Activity.CallStatic ("registerDeviceWithGCM", new object[]{"YourProjectIDHere"});


step 3) prepare to start receiving messages:
By default the script is on the Main Camera with a function of GCMReceiver. This must 
be exact unless you call:

		obj_Activity.CallStatic ("setListener", new object[]{"Object","Method"});

This will allow you to tell the plugin where you want the data to be sent to.

Step 4) Receieve message:
Messages are returned as follows:

	Key:Text;Key2:Text2
	
==============================================

Configuring a basic ASPX Server

Private Function SendNotification(ByVal messagetxt As String, ByVal deviceid As String, ByVal type As String) As String
        'ServicePointManager.ServerCertificateValidationCallback = Function(sender As Object, certificate As X509Certificate, chain As X509Chain, sslPolicyErrors As SslPolicyErrors) True
        Dim request As WebRequest = WebRequest.Create("https://android.googleapis.com/gcm/send")

        Dim authstring As String = "" ' Your Authorization String here
        request.Method = "POST"
        request.ContentType = "application/x-www-form-urlencoded"
        request.Headers.Add(String.Format("Authorization: key={0}", authstring))
        Dim collaspeKey As String = Guid.NewGuid().ToString("n")
        Dim postData As String = String.Format("registration_id={0}&data.message={1}&data.type={2}&data&collapse_key={3}", deviceid, messagetxt, type, collaspeKey)
        Dim byteArray As Byte() = Encoding.UTF8.GetBytes(postData)
        request.ContentLength = byteArray.Length
        Dim dataStream As Stream = request.GetRequestStream()
        dataStream.Write(byteArray, 0, byteArray.Length)
        dataStream.Close()
        Dim response As WebResponse = request.GetResponse()
        dataStream = response.GetResponseStream()
        Dim reader As New StreamReader(dataStream)
        Dim responseFromServer As String = reader.ReadToEnd()
        reader.Close()
        dataStream.Close()
        response.Close()

        Return responseFromServer
    End Function

************************************************
Resources:
Additional help with Unity development: http://unity3d.com/learn.
Additional Unity assets: https://www.assetstore.unity3d.com/
MSDN C# Programming Reference: http://msdn.microsoft.com/en-us/library/618ayhy6.aspx
Information on Google Cloud Messaging: http://developer.android.com/google/gcm/gcm.html

************************************************
