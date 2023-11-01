using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using TMPro;

public class GetIPAddress : MonoBehaviour
{
    public TextMeshProUGUI hintText;

    private void Start()
    {
        GetIP();
    }

    public void GetIP(){

        // hintText.text = GetLocalIPAddress();
        hintText.text = new WebClient().DownloadString("http://icanhazip.com");

        // GetLocalIPv4();
     }

    // public string GetLocalIPAddress()
    // {
    //     var host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
    //     foreach (var ip in host.AddressList)
    //     {
    //         if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
    //         {
    //             return ip.ToString();
    //         }
    //     }

    //     throw new System.Exception("No network adapters with an IPv4 address in the system!");

    // }

    // public string GetLocalIPv4()
    // {
    //     return Dns.GetHostEntry(Dns.GetHostName())
    //     .AddressList.First(
    //     f => f.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
    //     .ToString();
    // }

}
