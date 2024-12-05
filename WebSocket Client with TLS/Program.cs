
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace WebSocket_Client_with_TLS
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using (var client = new ClientWebSocket())
            {
                Uri serverUri = new Uri("wss://localhost:5001/ws");
                await client.ConnectAsync(serverUri, CancellationToken.None);
                Console.WriteLine("Connected to WebSocket server!");

                // Send a message
                string message = "Hello, WebSocket!";
                var buffer = Encoding.UTF8.GetBytes(message);
                await client.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
                Console.WriteLine($"Sent: {message}");

                // Receive a response
                buffer = new byte[1024];
                var result = await client.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                string response = Encoding.UTF8.GetString(buffer, 0, result.Count);
                Console.WriteLine($"Received: {response}");

                // Close the connection
                await client.CloseAsync(WebSocketCloseStatus.NormalClosure, "Client closed", CancellationToken.None);
                Console.WriteLine("WebSocket connection closed.");
            }
        }
    }
}
